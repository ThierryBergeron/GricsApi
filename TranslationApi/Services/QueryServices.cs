using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SimpleApi.Models;
using SimpleApi.Models.Firestore;
using SimpleApi.Models.UserRequest;
using SimpleApi.Models.Repositories;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System.Collections;
using SimpleApi.Helpers;

namespace SimpleApi.Services
{
    public class QueryServices : IQueryServices
    {
        private readonly ISchoolRepository _schoolRepo;
        private readonly ITeacherRepository _teacherRepo;
        private readonly IStudentRepository _studentRepo;
        private readonly IReportCardRepository _reportCardRepo;
        private readonly FirestoreCredentials _firestoreCredentials;

        public QueryServices(ISchoolRepository schoolRepository, ITeacherRepository teacherRepository, IStudentRepository studentRepository, IReportCardRepository reportCardRepository, FirestoreCredentials firestoreCredentials)
        {
            _schoolRepo = schoolRepository;
            _teacherRepo = teacherRepository;
            _studentRepo = studentRepository;
            _reportCardRepo = reportCardRepository;
            _firestoreCredentials = firestoreCredentials;
        }

        public string GenerateCollectionName(string targetName)
        {
            return $"{targetName.Substring(0, 1).ToUpper()}{targetName[1..]}s";
        }

        async public Task<IEnumerable<Student>> QueryStudentAsync(SimpleQuery simpleQuery)
        {
            var query = _studentRepo.GetQuery();
            var querySnapshot = await GetQuerySnapshot(query, simpleQuery);
            return querySnapshot.Select(s => s.ConvertTo<Student>()).Take(50);
        }

        async public Task<IEnumerable<Teacher>> QueryTeacherAsync(SimpleQuery simpleQuery)
        {
            var query = _teacherRepo.GetQuery();
            var querySnapshot = await GetQuerySnapshot(query, simpleQuery);
            return querySnapshot.Select(s => s.ConvertTo<Teacher>()).Take(50);
        }

        async public Task<IEnumerable<School>> QuerySchoolAsync(SimpleQuery simpleQuery)
        {
            var query = _schoolRepo.GetQuery();
            var querySnapshot = await GetQuerySnapshot(query, simpleQuery);
            return querySnapshot.Select(s => s.ConvertTo<School>()).Take(50);
        }

        async public Task<IEnumerable<ReportCard>> QueryReportCardAsync(SimpleQuery simpleQuery)
        {
            var query = _reportCardRepo.GetQuery();
            var querySnapshot = await GetQuerySnapshot(query, simpleQuery);
            return querySnapshot.Select(s => s.ConvertTo<ReportCard>()).Take(50);
        }

        async private Task<QuerySnapshot> GetQuerySnapshot(Query query, SimpleQuery simpleQuery)
        {
            // sub query
            if(simpleQuery.Compound != null)
            {
                // 1. get the collection name
                // 2. get the field
                // 3. get the id
                // 4. get the nameof subfield
                foreach(var kvp in simpleQuery.Compound)
                {
                    var collectionName = GenerateCollectionName(kvp.Key);
                    var firestore = new FirestoreRepository(collectionName, _firestoreCredentials);
                    var subFieldName = $"{kvp.Key}Id";
                    var subquery = firestore.GetQuery();

                    foreach(var subKvp in kvp.Value)
                    {
                        subquery = subquery.WhereEqualTo(subKvp.Key, subKvp.Value);
                    }

                    var subSnapshot = await subquery.GetSnapshotAsync();
                    var records = subSnapshot.Select(r => r.ConvertTo<FirestoreBaseModel>()).Take(10);

                    foreach(var r in records)
                    {
                        simpleQuery.Where.Add(subFieldName, r.Id);
                    }
                }

            }


            // single field
            if (simpleQuery.Where != null)
            {
                foreach (var kvp in simpleQuery.Where)
                {
                    query = query.WhereEqualTo(kvp.Key, kvp.Value);
                }
            }

            // array field
            return await query.GetSnapshotAsync();
        }
    }
}
