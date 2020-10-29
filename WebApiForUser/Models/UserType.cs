﻿using System.ComponentModel.DataAnnotations;

namespace LRSIntroductoryWebApi.Models
{
    public class UserType
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}
