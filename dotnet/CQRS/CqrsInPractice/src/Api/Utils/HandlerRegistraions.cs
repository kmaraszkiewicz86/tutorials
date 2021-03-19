using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Logic.Decorators;
using Logic.Students.CommandHandlers;
using Logic.Students.Commands;
using Logic.Students.QueryHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace Logic.Utils
{
    public static class HandlerRegistraions
    {
        public static void AddHandlers(this IServiceCollection services)
        {
            List<Type> handlerHelpers = typeof(ICommand).Assembly.GetTypes()
                .Where(x => x.GetInterfaces().Any(y => IsHandlerInterface(y)))
                .Where(x => x.Name.EndsWith("Handler"))
                .ToList();

            foreach (Type type in handlerHelpers)
            {
                AddHandler(services, type);
            }
        }

        private static void AddHandler(IServiceCollection services, Type type)
        {
            object[] attributes = type.GetCustomAttributes(false);

            List<Type> piplines = attributes
                .Select(x => ToDecorator(x))
                .Concat(new[] { type })
                .Reverse()
                .ToList();

            Type interfaceType = type.GetInterfaces().Single(y => IsHandlerInterface(y));
            Func<IServiceProvider, object> factory = BuildPipline(piplines, interfaceType);

            services.AddTransient(interfaceType, factory);
        }

        private static Func<IServiceProvider, object> BuildPipline(List<Type> piplines, Type interfaceType)
        {
            List<ConstructorInfo> ctors = piplines
                .Select(x =>
                {
                    Type type = x.IsGenericType ? x.MakeGenericType(interfaceType.GenericTypeArguments) : x;
                    return type.GetConstructors().Single();
                })
                .ToList();

            Func<IServiceProvider, object> func = provider =>
            {
                object current = null;

                foreach (ConstructorInfo constructorInfo in ctors)
                {
                    List<ParameterInfo> parameterInfos = constructorInfo.GetParameters().ToList();

                    object[] parameters = GetParameters(parameterInfos, current, provider);

                    current = constructorInfo.Invoke(parameters);
                }

                return current;
            };

            return func;
        }

        private static object[] GetParameters(List<ParameterInfo> parameterInfos, object current, IServiceProvider provider)
        {
            var result = new object[parameterInfos.Count];

            for (int i = 0; i < parameterInfos.Count; i++)
            {
                result[i] = GetParameter(parameterInfos[i], current, provider);
            }

            return result;
        }

        private static object GetParameter(ParameterInfo parameterInfo, object current, IServiceProvider provider)
        {
            Type parameterType = parameterInfo.ParameterType;

            if (IsHandlerInterface(parameterType))
                return current;

            object service = provider.GetService(parameterType);
            if (service != null)
                return service;

            throw new ArgumentException($"Type {parameterType} not found");
        }

        private static Type ToDecorator(object attribute)
        {
            Type type = attribute.GetType();

            if (type == typeof(DatabaseRetryAttribute))
                return typeof(DatabaseRetryDecorator<>);

            if (type == typeof(AuditLoggingAttribute))
                return typeof(AuditLoggingDecorator<>);

            throw new ArgumentException(attribute.ToString());
        }

        private static bool IsHandlerInterface(Type type)
        {
            if (!type.IsGenericType)
                return false;

            Type typeDefinition = type.GetGenericTypeDefinition();

            return typeDefinition == typeof(ICommandHandler<>) || typeDefinition == typeof(IQueryHandler<,>);
        }
    }
}