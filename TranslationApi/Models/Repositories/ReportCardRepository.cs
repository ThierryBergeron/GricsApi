using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleApi.Models.Firestore;
using SimpleApi.Helpers;

namespace SimpleApi.Models.Repositories
{
    public class ReportCardRepository : IReportCardRepository
    {
        private readonly string _collectionName;
        private readonly FirestoreRepository _firestore;
        public ReportCardRepository(FirestoreCredentials firestoreCredentials)
        {
            _collectionName = "ReportCards";
            _firestore = new FirestoreRepository(_collectionName, firestoreCredentials);
        }

        public ReportCard Add(ReportCard Record)
        {
            throw new NotImplementedException();
        }

        async public Task<ReportCard> AddAsync(ReportCard Record) => await _firestore.AddAsync(Record);

        public bool Delete(ReportCard Record)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(ReportCard Record)
        {
            throw new NotImplementedException();
        }

        public ReportCard Get(ReportCard Record)
        {
            throw new NotImplementedException();
        }

        public List<ReportCard> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<ReportCard>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ReportCard> GetAsync(ReportCard Record)
        {
            throw new NotImplementedException();
        }

        public Task<ReportCard> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ReportCard> GetByStudentId(string id)
        {
            //var query = _firestore.firestoreDb.Collection(_collectionName);
            //var colRefs = query.WhereEqualTo("Name", name.ToLower());
            //var snapshot = await colRefs.GetSnapshotAsync();
            ////if(snapshot)
            //return snapshot.Select(s => s.ConvertTo<School>()).ToList();

            throw new NotImplementedException();
        }

        public Query GetQuery() => _firestore.GetQuery();

        public bool Update(ReportCard Record)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ReportCard Record)
        {
            throw new NotImplementedException();
        }
    }
}
