using Core.Models;
using System.Collections.Generic;

namespace Core.Services.PersonServiceImpl
{
    /// <summary>
    /// Manages person object functionality in database
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Inserts the specified person model.
        /// </summary>
        /// <param name="personModel">The person model.</param>
        void Insert(PersonModel personModel);

        /// <summary>
        /// Updates the specified person model.
        /// </summary>
        /// <param name="personModel">The person model.</param>
        void Update(PersonModel personModel);

        /// <summary>
        /// Gets all persons
        /// </summary>
        /// <returns>The list of <see cref="PersonModel"/> from database</returns>
        IEnumerable<PersonModel> GetAll();
    }
}