using Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Core.Models.Enums;

namespace Core.Services.PersonServiceImpl
{
    /// <summary>
    /// Manages person object functionality in database
    /// </summary>
    /// <seealso cref="Core.Services.PersonServiceImpl.IPersonService" />
    public class PersonService: IPersonService
    {
        private static List<PersonModel> _personModels = new List<PersonModel>
        {
            new PersonModel
            {
                Id = 1,
                Name = "Jan",
                Surname = "Kowalski",
                GenderType = GenderType.Male

            }
        };

        private int NextId =>
            _personModels?.Max(p => p.Id) + 1 ?? 1;

        /// <summary>
        /// Inserts the specified person model.
        /// </summary>
        /// <param name="personModel">The person model.</param>
        public void Insert(PersonModel personModel)
        {
            CheckPersonModelIsValid(personModel);

            _personModels.Add(new PersonModel
            {
                Id = personModel.Id == 0 ? NextId : personModel.Id,
                Name = personModel.Name,
                Surname = personModel.Surname,
                GenderType = personModel.GenderType
            });
        }

        /// <summary>
        /// Updates the specified person model.
        /// </summary>
        /// <param name="personModel">The person model.</param>
        public void Update(PersonModel personModel)
        {
            if (CheckIfPersonIdIsEmpty(personModel))
            {
                throw new FaultException($"The {personModel.Id} is required");
            }

            var personModelToUpdate = _personModels.FirstOrDefault(p => p.Id == personModel.Id);

            if (personModelToUpdate == null)
            {
                throw new FaultException($"The person not found");
            }

            personModelToUpdate.Name = personModel.Name;
            personModelToUpdate.Surname = personModel.Surname;
            personModelToUpdate.GenderType = personModel.GenderType;
        }

        /// <summary>
        /// Gets all persons
        /// </summary>
        /// <returns>
        /// The list of <see cref="PersonModel" /> from database
        /// </returns>
        public IEnumerable<PersonModel> GetAll()
        {
            return _personModels;
        }

        private void CheckPersonModelIsValid(PersonModel personModel)
        {
            CheckIfRequiredFieldIsFilled(nameof(personModel.Name), personModel.Name);
            CheckIfRequiredFieldIsFilled(nameof(personModel.Surname), personModel.Surname);

            if (!CheckIfGenderTypeIsNotEmpty(personModel))
            {
                throw new FaultException(GetIsRequiredErrorMessage(nameof(personModel.GenderType)));
            }
        }

        private bool CheckIfGenderTypeIsNotEmpty(PersonModel personModel)
        {
            return personModel.GenderType != GenderType.None;
        }

        private bool CheckIfPersonIdIsEmpty(PersonModel personModel) =>
            personModel.Id == 0;

        private string GetIsRequiredErrorMessage(string fieldName) =>
            $"The {fieldName} of model is required";

        private void CheckIfRequiredFieldIsFilled(string fieldName, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new FaultException(GetIsRequiredErrorMessage(fieldName));
            }
        }
    }
}