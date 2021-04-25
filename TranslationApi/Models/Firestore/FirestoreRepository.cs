using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Newtonsoft.Json;
using SimpleApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SimpleApi.Models.Firestore
{
    public class FirestoreRepository
    {
        private string _collectionName;
        public FirestoreDb firestoreDb;

        public FirestoreRepository(string collectionName, FirestoreCredentials credentials)
        {
            //var _credentials = GoogleCredential.GetApplicationDefault();
            
            var builder = new FirestoreClientBuilder { JsonCredentials = credentials.Credentials};
            firestoreDb = FirestoreDb.Create("emerald-road-305215", builder.Build());
            _collectionName = collectionName;
        }

        public T Add<T>(T record) where T : FirestoreBaseModel
        {
            CollectionReference colRef = firestoreDb.Collection(_collectionName);
            DocumentReference doc = colRef.AddAsync(record).GetAwaiter().GetResult();
            record.Id = doc.Id;
            return record;
        }
        async public Task<T> AddAsync<T>(T record) where T : FirestoreBaseModel
        {
            CollectionReference colRef = firestoreDb.Collection(_collectionName);
            DocumentReference doc = await colRef.AddAsync(record);
            record.Id = doc.Id;
            return record;
        }

        public bool Delete<T>(T record) where T : FirestoreBaseModel
        {
            DocumentReference recordRef = firestoreDb.Collection(_collectionName).Document(record.Id);
            recordRef.DeleteAsync().GetAwaiter().GetResult();
            return true;
        }
        async public Task<bool> DeleteAsync<T>(T record) where T : FirestoreBaseModel
        {
            DocumentReference recordRef = firestoreDb.Collection(_collectionName).Document(record.Id);
            await recordRef.DeleteAsync();
            return true;
        }

        public T Get<T>(T record) where T : FirestoreBaseModel
        {
            DocumentReference docRef = firestoreDb.Collection(_collectionName).Document(record.Id);
            DocumentSnapshot snapshot = docRef.GetSnapshotAsync().GetAwaiter().GetResult();
            if (snapshot.Exists)
            {
                T usr = snapshot.ConvertTo<T>();
                usr.Id = snapshot.Id;
                return usr;
            }
            else
            {
                return null;
            }
        }
        async public Task<T> GetAsync<T>(T record) where T : FirestoreBaseModel
        {
            DocumentReference docRef = firestoreDb.Collection(_collectionName).Document(record.Id);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                T usr = snapshot.ConvertTo<T>();
                usr.Id = snapshot.Id;
                return usr;
            }
            else
            {
                return null;
            }
        }


        public List<T> GetAll<T>() where T : FirestoreBaseModel
        {
            var query = firestoreDb.Collection(_collectionName);
            QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();
            List<T> list = new List<T>();
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Dictionary<string, object> dict = documentSnapshot.ToDictionary();
                    string json = JsonConvert.SerializeObject(dict);
                    T newItem = JsonConvert.DeserializeObject<T>(json);
                    newItem.Id = documentSnapshot.Id;
                    list.Add(newItem);
                }
            }
            return list;
        }
        async public Task<List<T>> GetAllAsync<T>() where T : FirestoreBaseModel
        {
            var query = firestoreDb.Collection(_collectionName);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
            List<T> list = new List<T>();
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Dictionary<string, object> dict = documentSnapshot.ToDictionary();
                    string json = JsonConvert.SerializeObject(dict);
                    T newItem = JsonConvert.DeserializeObject<T>(json);
                    newItem.Id = documentSnapshot.Id;
                    list.Add(newItem);
                }
            }
            return list;
        }

        async public Task<T> GetByIdAsync<T>(string id) where T : FirestoreBaseModel
        {
            DocumentReference recordRef = firestoreDb.Collection(_collectionName).Document(id);
            DocumentSnapshot snapshot = await recordRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                return snapshot.ConvertTo<T>();
            }
            return null;
        }

        public bool Update<T>(T record) where T : FirestoreBaseModel
        {
            DocumentReference recordRef = firestoreDb.Collection(_collectionName).Document(record.Id);
            recordRef.SetAsync(record, SetOptions.MergeAll).GetAwaiter().GetResult();
            return true;
        }
        async public Task<bool> UpdateAsync<T>(T record) where T : FirestoreBaseModel
        {
            DocumentReference recordRef = firestoreDb.Collection(_collectionName).Document(record.Id);
            await recordRef.SetAsync(record, SetOptions.MergeAll);
            return true;
        }

        public List<T> QueryRecords<T>(Query query) where T : FirestoreBaseModel
        {
            QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();
            List<T> list = new List<T>();
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Dictionary<string, object> city = documentSnapshot.ToDictionary();
                    string json = JsonConvert.SerializeObject(city);
                    T newItem = JsonConvert.DeserializeObject<T>(json);
                    newItem.Id = documentSnapshot.Id;
                    list.Add(newItem);
                }
            }
            return list;
        }

        public Query GetQuery()
        {
            return firestoreDb.Collection(_collectionName);
        }
    }
}
