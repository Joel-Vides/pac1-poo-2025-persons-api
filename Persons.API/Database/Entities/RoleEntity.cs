using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Persons.API.Database.Entities
{
    public class RoleEntity : IdentityRole
    {
        [Column("description")]
        [StringLength(256)]
        public string Description { get; set; }
    }
}