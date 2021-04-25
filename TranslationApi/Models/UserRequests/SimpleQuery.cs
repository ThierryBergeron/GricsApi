using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Models.UserRequest
{
    public class SimpleQuery
    {
        [Required]
        [MinLength(length: 4)]
        [MaxLength(length:30)]
        public string Target { get; set; }

        [MaxLength(length:50)]
        public Dictionary<string, string> Where { get; set; }

        [MaxLength(length:50)]
        public Dictionary<string, string> Contains { get; set; }

        [MaxLength(length:50)]
        public Dictionary<string, Dictionary<string, string>> Compound { get; set; }
    }
}
