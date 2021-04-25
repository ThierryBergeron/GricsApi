using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Models
{
    [FirestoreData]
    public class ReportCard :FirestoreBaseModel
    {
        [FirestoreProperty]
        public int Francais { get; set; }
        
        [FirestoreProperty]
        public int Mathematique { get; set; }
        
        [FirestoreProperty]
        public int Sciences { get; set; }
        
        [FirestoreProperty]
        public int Arts { get; set; }
        
        [FirestoreProperty]
        public string StudentId { get; set; }
        
        [FirestoreProperty]
        public string TeacherId { get; set; }

        [FirestoreProperty]
        public string SchoolId { get; set; }
        
        [FirestoreProperty]
        public int Grade { get; set; }
    }
}
