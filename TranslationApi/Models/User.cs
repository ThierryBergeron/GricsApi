using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Models
{
    [FirestoreData]
    public class User : FirestoreBaseModel
    {
        [FirestoreProperty]
        virtual public string Name { get; set; }
        [FirestoreProperty]
        virtual public string Email { get; set; }
        [FirestoreProperty]
        virtual public AuthorizationLevelEnum AuthorizationLevel { get; set; }
    }
}
