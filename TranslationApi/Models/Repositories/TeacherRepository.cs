using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleApi.Models.Firestore;
using SimpleApi.Helpers;

namespace SimpleApi.Models.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly FirestoreRepository _firestore;
        private readonly string _collectionName;
        public TeacherRepository(FirestoreCredentials firestoreCredentials)
        {
            _collectionName = "Teachers";
            _firestore = new FirestoreRepository(_collectionName, firestoreCredentials);
        }
        public Teacher Add(Teacher Record)
        {
            throw new NotImplementedException();
        }

        async public Task<Teacher> AddAsync(Teacher Record) => await _firestore.AddAsync(Record);

        public bool Delete(Teacher Record)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Teacher Record)
        {
            throw new NotImplementedException();
        }

        public Teacher Get(Teacher Record)
        {
            throw new NotImplementedException();
        }

        public List<Teacher> GetAll()
        {
            throw new NotImplementedException();
        }

        async public Task<List<Teacher>> GetAllAsync() => await _firestore.GetAllAsync<Teacher>();


        public Task<Teacher> GetAsync(Teacher Record)
        {
            throw new NotImplementedException();
        }

        public Task<Teacher> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Query GetQuery() => _firestore.firestoreDb.Collection(_collectionName);

        public bool Update(Teacher Record)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Teacher Record)
        {
            throw new NotImplementedException();
        }
    }
}
