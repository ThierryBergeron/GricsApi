using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Models
{
    [FirestoreData]
    public class FirestoreBaseModel
    {
        [FirestoreDocumentId]
        public string Id { get; set; }
        [FirestoreDocumentCreateTimestamp]
        public Timestamp CreateTime { get; set; }
        [FirestoreDocumentUpdateTimestamp]
        public Timestamp UpdateTime { get; set; }
        [FirestoreDocumentReadTimestamp]
        public Timestamp ReadTime { get; set; }
    }
}
