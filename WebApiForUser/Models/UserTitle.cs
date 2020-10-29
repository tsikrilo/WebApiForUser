using System.ComponentModel.DataAnnotations;

namespace LRSIntroductoryWebApi.Models
{
    public class UserTitle
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }
    }
}
