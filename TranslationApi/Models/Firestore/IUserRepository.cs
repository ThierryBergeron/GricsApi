using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Models.Firestore
{
    public interface IUserRepository : IFirestoreRepository<FirestoreUser>
    {
        FirestoreUser GetUserWithNormalizedEmail(string email);
        Task<FirestoreUser> GetUserWithNormalizedEmailAsync(string email);
    }
}
