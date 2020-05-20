using Core.Models;
using System.Collections.Generic;
using Core.Models.Enums;

namespace Core.Services.PersonServiceImpl
{
    /// <summary>
    /// Manages person object functionality in database
    /// </summary>
    /// <seealso cref="Core.Services.PersonServiceImpl.IPersonService" />
    public class PersonService: IPersonService
    {
        /// <summary>
        /// Inserts the specified person model.
        /// </summary>
        /// <param name="personModel">The person model.</param>
        public void Insert(PersonModel personModel)
        {
            
        }

        /// <summary>
        /// Updates the specified person model.
        /// </summary>
        /// <param name="personModel">The person model.</param>
        public void Update(PersonModel personModel)
        {
            
        }

        /// <summary>
        /// Gets all persons
        /// </summary>
        /// <returns>
        /// The list of <see cref="PersonModel" /> from database
        /// </returns>
        public IEnumerable<PersonModel> GetAll()
        {
            return new List<PersonModel>
            {
                new PersonModel
                {
                    Id = 1,
                    Name = "Jan",
                    Surname = "Kowalski",
                    GenderType = GenderType.Male

                }
            };
        }
    }
}