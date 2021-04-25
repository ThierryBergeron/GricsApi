using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Models
{
    [FirestoreData]
    public class Teacher : FirestoreBaseModel
    {
        [FirestoreProperty]
        public string FirstName { get; set; }

        [FirestoreProperty]
        public string LastName { get; set; }

        [FirestoreProperty]
        public string TelephoneNumber { get; set; }

        [FirestoreProperty]
        [EmailAddress]
        public string Email { get; set; }

        [FirestoreProperty]
        public string Sex { get; set; }

        [FirestoreProperty]
        public string SchoolId { get; set; }
    }
}
