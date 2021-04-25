using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Models
{
    [FirestoreData]
    public class School : FirestoreBaseModel
    {
        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public string TelephoneNumber { get; set; }
    }
}
