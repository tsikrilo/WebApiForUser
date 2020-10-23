using System.ComponentModel.DataAnnotations;
using UserWebApi.Data;

namespace WebApi.Models
{
    public class UserType
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}
