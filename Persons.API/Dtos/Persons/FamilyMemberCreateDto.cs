using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Persons.API.Dtos.Persons
{
    public class FamilyMemberCreateDto
    {
        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "Los {0} son requeridos")]
        [StringLength(30, ErrorMessage = "Los {0} Deben Tener Entre {2} y {1} Caracteres",MinimumLength = 3)]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "Los {0} son requeridos")]
        [StringLength(30, ErrorMessage = "Los {0} Deben Tener Entre {2} y {1} Caracteres", MinimumLength = 3)]
        public string LastName { get; set; }

        [Display(Name = "Parentesco")]
        [Required(ErrorMessage = "El {0} es Requerido")]
        [StringLength(20, ErrorMessage = "El {0} Debe Tener Entre {2} y {1} Caracteres", MinimumLength = 3)]
        public string Relationship { get; set; }
    }
}
