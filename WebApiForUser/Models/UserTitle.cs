using System.ComponentModel.DataAnnotations;
using UserWebApi.Data;

namespace WebApi.Models
{
    public class UserTitle
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }
    }
}
