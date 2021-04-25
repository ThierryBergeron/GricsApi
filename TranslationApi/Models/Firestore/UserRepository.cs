using SimpleApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Models.Firestore
{
    public class UserRepository : IUserRepository
    {
        private readonly FirestoreRepository _firestoreRepository;
        private readonly string _collectionName = "Users";
        public UserRepository(FirestoreCredentials firestoreCredentials)
        {
            _firestoreRepository = new FirestoreRepository(_collectionName, firestoreCredentials);
        }

        public FirestoreUser Add(FirestoreUser record) => _firestoreRepository.Add(record);

        async public Task<FirestoreUser> AddAsync(FirestoreUser record) => await _firestoreRepository.AddAsync(record);

        public bool Delete(FirestoreUser record) => _firestoreRepository.Delete(record);

        async public Task<bool> DeleteAsync(FirestoreUser record) => await _firestoreRepository.DeleteAsync(record);

        public FirestoreUser Get(FirestoreUser record) => _firestoreRepository.Get(record);

        public List<FirestoreUser> GetAll() => _firestoreRepository.GetAll<FirestoreUser>();

        async public Task<List<FirestoreUser>> GetAllAsync() => await _firestoreRepository.GetAllAsync<FirestoreUser>();

        async public Task<FirestoreUser> GetAsync(FirestoreUser record) => await _firestoreRepository.GetAsync(record);

        async public Task<FirestoreUser> GetByIdAsync(string id) => await _firestoreRepository.GetByIdAsync<FirestoreUser>(id);

        public Google.Cloud.Firestore.Query GetQuery()
        {
            throw new NotImplementedException();
        }

        public FirestoreUser GetUserWithNormalizedEmail(string email)
        {
            throw new NotImplementedException();
        }

        async public Task<FirestoreUser> GetUserWithNormalizedEmailAsync(string email)
        {
            var documentReference = _firestoreRepository.firestoreDb.Collection(_collectionName).Document(email);
            var snapshot = await documentReference.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                return snapshot.ConvertTo<FirestoreUser>();
            }

            return null;
        }

        public bool Update(FirestoreUser record) => _firestoreRepository.Update(record);

        async public Task<bool> UpdateAsync(FirestoreUser record) => await _firestoreRepository.UpdateAsync<FirestoreUser>(record);
    }
}
