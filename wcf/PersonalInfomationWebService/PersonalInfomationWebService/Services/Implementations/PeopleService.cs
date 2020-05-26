using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PersonalInfomationWebService.Core;
using PersonalInfomationWebService.Expetions;
using PersonalInfomationWebService.Models;
using PersonalInfomationWebService.Models.Requests;
using PersonalInfomationWebService.Models.Responses;
using PersonalInfomationWebService.Services.Interfaces;

namespace PersonalInfomationWebService.Services.Implementations
{
    public class PeopleService: IPeopleService
    {
        private PeopleDbEntities _db;

        public PeopleService(PeopleDbEntities peopleDbEntities)
        {
            _db = peopleDbEntities;
        }

        public PeopleCollectionModelResponse GetAll()
        {
            var people = _db.PEOPLE.Join(_db.GENDERS,
                p => p.GENDERID,
                g => g.ID,
                (p, g) => new PeopleWithGender
                {
                    Id = (int)p.ID,
                    Name = p.NAME,
                    Surname = p.SURNAME,
                    GenderType = g.NAME
                }).ToList();

            return new PeopleCollectionModelResponse(people.Select(p =>
                    new PersonModelResponse(p.Id, p.Name, p.Surname, p.GenderType))
                .ToList());
        }

        public async Task<PersonModelResponse> Get(PersonModelRequest model)
        {
            var person = await GetJoin().FirstOrDefaultAsync(p => p.Id == model.PersonId);

            if (person == null)
            {
                throw new NotFoundExceptions("No people found");
            }

            return new PersonModelResponse(person.Id, person.Name, person.Surname, person.GenderType);
        }

        private IQueryable<PeopleWithGender> GetJoin()
        {
            return _db.PEOPLE.Join(_db.GENDERS,
                p => p.GENDERID,
                g => g.ID,
                (p, g) => new PeopleWithGender
                {
                    Id = (int) p.ID,
                    Name = p.NAME,
                    Surname = p.SURNAME,
                    GenderType = g.NAME
                });
        }
    }
}