using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleApi.Models.Firestore;
using SimpleApi.Helpers;

namespace SimpleApi.Models.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _collectionName;
        private readonly FirestoreRepository _firestore;
        public StudentRepository(FirestoreCredentials firestoreCredentials)
        {
            _collectionName = "Students";
            _firestore = new FirestoreRepository(_collectionName, firestoreCredentials);
        }

        public Student Add(Student Record)
        {
            throw new NotImplementedException();
        }
        async public Task<Student> AddAsync(Student Record) => await _firestore.AddAsync(Record);

        public bool Delete(Student Record)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Student Record)
        {
            throw new NotImplementedException();
        }

        public Student Get(Student Record)
        {
            throw new NotImplementedException();
        }

        public List<Student> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Student>> GetAllAsync() => _firestore.GetAllAsync<Student>();

        public Task<Student> GetAsync(Student Record)
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Query GetQuery() => _firestore.firestoreDb.Collection(_collectionName);

        public bool Update(Student Record)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Student Record)
        {
            throw new NotImplementedException();
        }
    }
}
