using ProjectLibrary.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectLibrary.Models
{
    public class User : BaseModel
    {
        [Required]
        public AuthInfo Auth { get; set; }

        public List<Book> Books { get; set; }
    }
}
