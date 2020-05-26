using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using Core.Models;
using Core.Services.PersonServiceImpl;

namespace WcfServiceWebApplication.ServiceContracts.PersonServiceContract
{
    public class PersonServiceContract : IPersonServiceContract
    {
        /// <summary>
        /// The person service
        /// </summary>
        private readonly IPersonService _personService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonServiceContract"/> class.
        /// </summary>
        /// <param name="personService">The person service.</param>
        public PersonServiceContract(IPersonService personService)
        {
            _personService = personService;
        }

        /// <summary>
        /// Inserts the specified person model.
        /// </summary>
        /// <param name="personModel">The person model.</param>
        [OperationBehavior(TransactionScopeRequired = true)]
        public void Insert(PersonModel personModel)
        {
            OnAction(() => _personService.Insert(personModel));
        }

        /// <summary>
        /// Updates the specified person model.
        /// </summary>
        /// <param name="personModel">The person model.</param>
        [OperationBehavior(TransactionScopeRequired = true)]
        public void Update(PersonModel personModel)
        {
            OnAction(() => _personService.Update(personModel));
        }

        /// <summary>
        /// Gets all people items
        /// </summary>
        /// <returns><see cref="IEnumerable{PersonModel}"/></returns>
        public IEnumerable<PersonModel> GetAll()
        {
            return OnAction(() => _personService.GetAll());
        }

        /// <summary>
        /// Called when request executed.
        /// </summary>
        /// <param name="onAction">The on request action.</param>
        /// <exception cref="FaultException"></exception>
        /// <exception cref="FaultReason"></exception>
        private void OnAction(Action onAction)
        {
            try
            {
                onAction();
            }
            catch (Exception e)
            {
                throw new FaultException(new FaultReason(e.Message), FaultCode.CreateSenderFaultCode("err", "test"));
            }
        }

        /// <summary>
        /// Called when request executed
        /// </summary>
        /// <typeparam name="TReturnType">The type of the return type.</typeparam>
        /// <param name="onFunc">The on function.</param>
        /// <returns><see cref="TReturnType"/></returns>
        private TReturnType OnAction<TReturnType>(Func<TReturnType> onFunc)
        {
            TReturnType results = default(TReturnType);

            OnAction(() =>
            {
                results = onFunc();
            });

            return results;
        }
    }
}