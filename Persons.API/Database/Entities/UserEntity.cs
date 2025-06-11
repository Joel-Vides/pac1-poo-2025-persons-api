using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Persons.API.Database.Entities
{
    public class UserEntity : IdentityUser
    {
        [Column("first_name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Column("last_name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Column("avatar_url")]
        [StringLength(256)]
        public string AvatarUrl { get; set; }

        [Column("birth_date")]
        public DateTime BirthDate { get; set; }
    }
}