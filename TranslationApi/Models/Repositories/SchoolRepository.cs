using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleApi.Models.Firestore;
using SimpleApi.Helpers;

namespace SimpleApi.Models.Repositories
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly FirestoreRepository _firestore;
        private readonly string _collectionName;
        
        public SchoolRepository(FirestoreCredentials firestoreCredentials)
        {
            _collectionName = "Schools";
            _firestore = new FirestoreRepository(_collectionName, firestoreCredentials);
        }
        public School Add(School Record)
        {
            throw new NotImplementedException();
        }

        public Task<School> AddAsync(School Record) => _firestore.AddAsync(Record);

        public bool Delete(School Record)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(School Record)
        {
            throw new NotImplementedException();
        }

        public School Get(School Record)
        {
            throw new NotImplementedException();
        }

        public List<School> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<School>> GetAllAsync() => _firestore.GetAllAsync<School>();

        public Task<School> GetAsync(School Record)
        {
            throw new NotImplementedException();
        }

        async public Task<School> GetByIdAsync(string id) => await _firestore.GetByIdAsync<School>(id);

        public Query GetQuery() => _firestore.firestoreDb.Collection(_collectionName);

        async public Task<School> GetSchoolWithSchoolIdAsync(string id)
        {
            var documentReference = _firestore.firestoreDb.Collection(_collectionName).Document(id);
            var snapshot = await documentReference.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                return snapshot.ConvertTo<School>();
            }
            return null;
        }

        async public Task<List<School>> GetSchoolWithSchoolNameAsync(string name)
        {
            var query = _firestore.firestoreDb.Collection(_collectionName);
            var colRefs = query.WhereEqualTo("Name", name.ToLower());
            var snapshot = await colRefs.GetSnapshotAsync();
            //if(snapshot)
            return snapshot.Select(s => s.ConvertTo<School>()).ToList();
        }

        public bool Update(School Record)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(School Record) => _firestore.UpdateAsync(Record);
    }
}
