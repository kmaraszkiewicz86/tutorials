using Core.Models;
using System.Collections.Generic;
using System.ServiceModel;

namespace WcfServiceWebApplication.ServiceContracts.PersonServiceContract
{
    /// <summary>
    /// The person service contract
    /// </summary>
    [ServiceContract]
    public interface IPersonServiceContract
    {
        /// <summary>
        /// Inserts the specified person model.
        /// </summary>
        /// <param name="personModel">The person model.</param>
        [OperationContract]
        void Insert(PersonModel personModel);

        /// <summary>
        /// Updates the specified person model.
        /// </summary>
        /// <param name="personModel">The person model.</param>
        [OperationContract]
        void Update(PersonModel personModel);

        [OperationContract]
        IEnumerable<PersonModel> GetAll();
    }
}
