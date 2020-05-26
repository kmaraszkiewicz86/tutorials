using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Transactions;
using Core.Models;
using Core.Models.Enums;
using WcfServiceWebApplication.ServiceContracts.PersonServiceContract;

namespace WcfClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string address = "http://localhost:50230/ServiceContracts/PersonServiceContract/PersonServiceContract.svc";
            Binding binding = new WSHttpBinding(SecurityMode.None, true);
            
            
            ChannelFactory<IPersonServiceContract> channelFactory =
                new ChannelFactory<IPersonServiceContract>(binding, address);
            IPersonServiceContract client = channelFactory.CreateChannel();
            ((IChannel)client).Open();

            using (var ts = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    client.Insert(new PersonModel
                    {
                        Name = "test123445353",
                        Surname = "rwerwe",
                        GenderType = GenderType.Male
                    });

                    throw new FaultException();

                    ts.Complete();
                }
                catch (Exception e)
                {
                    ts.Dispose();
                }
            }

            foreach (var personModel in client.GetAll())
            {
                Console.WriteLine($"Id => {personModel.Id}; Name => {personModel.Name}; " +
                                  $"Surname => {personModel.Surname}; Gender => {personModel.GenderType}");
            }

            ((IChannel)client).Close();

            Console.ReadKey();
        }
    }
}
