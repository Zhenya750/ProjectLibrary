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
    public class Book : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string[] Genres { get; set; }
    }
}
