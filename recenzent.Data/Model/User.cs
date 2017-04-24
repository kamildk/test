using Microsoft.AspNet.Identity;
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
        public string Nick { get; set; }

        public int AffiliationId { get; set; }
        [ForeignKey("AffiliationId")]
        public Affiliation Affiliation { get; set; }

        public int EditorId { get; set; }
        [ForeignKey("EditorId")]
        public Editor Editor { get; set; }

        public int AdminId { get; set; }
        [ForeignKey("AdminId")]
        public Admin Admin { get; set; }

        public ICollection<View> Views { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
