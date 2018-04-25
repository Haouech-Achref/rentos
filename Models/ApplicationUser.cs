using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Rentos.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() => Rents = new List<Rent>();

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Rent> Rents { get; set; }
    }
}
