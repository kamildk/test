﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model
{
    public class User : IdentityUser
    {
        //TODO: tutaj możecie dodać właściwości aby dołożyć coś do tabeli użytkowników
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }

        public int? AffiliationId { get; set; }
        [ForeignKey("AffiliationId")]
        public virtual Affiliation Affiliation { get; set; }
        //public Employee Employee { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Change> Changes { get; set; }
        //public ICollection<Publication_Autors> PublicationAutors { get; set; }
        public virtual ICollection<Publication> Publications { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public bool wantToBeReviewer { get; set; }
        public bool wantToBeAuthor { get; set; }
        public User()
        {
            Comments = new List<Comment>();
            Ratings = new List<Rating>();
            Changes = new List<Change>();
            Publications = new List<Publication>();
            Reviews = new List<Review>();

        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
            
        }
    }
}
