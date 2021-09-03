using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProjectLibrary.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLibrary.Models
{
    public class User : BaseModel
    {
        [Required]
        public AuthInfo Auth { get; set; }

        public List<Book> Books { get; set; }
    }
}
