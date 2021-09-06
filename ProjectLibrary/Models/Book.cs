using ProjectLibrary.Models.Base;
using System.ComponentModel.DataAnnotations;

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
