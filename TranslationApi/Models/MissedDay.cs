using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Models
{
    [FirestoreData]
    public class MissedDay : FirestoreBaseModel
    {
        [FirestoreProperty]
        public string TeacherId { get; set; }
        [FirestoreProperty]
        public string StudentId { get; set; }
        [FirestoreProperty]
        public DateTime Date { get; set; }
    }
}
