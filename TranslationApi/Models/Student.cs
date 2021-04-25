using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Models
{
    [FirestoreData]
    public class Student : FirestoreBaseModel
    {
        [FirestoreProperty]
        public string FirstName { get; set; }

        [FirestoreProperty]
        public string LastName { get; set; }

        [FirestoreProperty]
        public DateTime Dob { get; set; }

        [FirestoreProperty]
        public string Sex { get; set; }

        [FirestoreProperty]
        public string SchoolId { get; set; }

        [FirestoreProperty]
        public string TeacherId { get; set; }
    }
}
