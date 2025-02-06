﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Models
{
    public class ApplicationUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName {  get; set; }  
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Address {  get; set; }
        [Required]
        public int PhoneNumber {  get; set; }
        [Required]
        public DateTime DateCreated {  get; set; }

        public ICollection<Review> Reviews = new List<Review>();
    }
}
