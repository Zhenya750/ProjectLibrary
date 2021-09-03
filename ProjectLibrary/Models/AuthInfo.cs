using MongoDB.Bson.Serialization.Attributes;
using ProjectLibrary.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLibrary.Models
{
    public class AuthInfo
    {
        [BsonId]
        [Required]
        [MinLength(3, ErrorMessage = "Login must contain at least 3 symbols")]
        public string Login { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Password must contain at least 3 symbols")]
        public string Password { get; set; }
    }
}
