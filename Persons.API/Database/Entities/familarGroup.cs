using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Persons.API.Database.Entities.Common;

namespace Persons.API.Database.Entities
{
    [Table("FamiliarGroup")]
    public class familarGroup : BaseEntity
    {
        [Column("first_familiar_name")]
        [Required]
        public string FirstFamiliarName { get; set; }

        [Column("last_familiar_name")]
        [Required]
        public string LastFamiliarName { get; set; }

        [Column("relationship")]
        [Required]
        public string Relationship { get; set; }
    }
}
