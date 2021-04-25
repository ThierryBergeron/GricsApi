using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using SimpleApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Models.Firestore
{
    public class FirestoreData
    {
        public FirestoreDb _firestoreDb { get; set; }

        public FirestoreData(FirestoreCredentials firestoreCredentials)
        {
            var builder = new FirestoreClientBuilder { JsonCredentials = firestoreCredentials.Credentials };
            _firestoreDb = FirestoreDb.Create("emerald-road-305215", builder.Build());
        }
    }
}
