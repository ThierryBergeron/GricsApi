using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Models
{
    [FirestoreData(ConverterType = typeof(FirestoreEnumNameConverter<AuthorizationLevelEnum>))]
    public enum AuthorizationLevelEnum
    {
        BaseUser,
        PowerUser
    }

    [FirestoreData]
    public class FirestoreUser : User
    {
        [FirestoreProperty]
        override public string Name { get; set; }
        [Required]
        [FirestoreProperty]
        override public string Email { get; set; }
        [FirestoreProperty]
        public string NormalizedEmail { get; set; }
        [Required]
        [FirestoreProperty]
        public string Password { get; set; }
        [Required]
        [FirestoreProperty]
        override public AuthorizationLevelEnum AuthorizationLevel { get; set; }
    }
}
