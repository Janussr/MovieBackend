using System.ComponentModel.DataAnnotations;

namespace CodeFirstProject.Models
{
    public class User
    {

        [Key]
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? ContactNo { get; set; }
    }
}
