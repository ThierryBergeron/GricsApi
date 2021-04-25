using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleApi.Models.Firestore;

namespace SimpleApi.Models.Repositories
{
    public interface ISchoolRepository : IFirestoreRepository<School>
    {
        Task<School> GetSchoolWithSchoolIdAsync(string id);
        Task<List<School>> GetSchoolWithSchoolNameAsync(string name);
    }
}
