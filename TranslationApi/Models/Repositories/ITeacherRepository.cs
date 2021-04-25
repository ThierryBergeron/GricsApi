using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleApi.Models.Firestore;

namespace SimpleApi.Models.Repositories
{
    public interface ITeacherRepository : IFirestoreRepository<Teacher>
    {

    }
}
