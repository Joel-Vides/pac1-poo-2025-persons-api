using System.ComponentModel.DataAnnotations;

namespace Persons.API.Dtos.Security.Roles
{
    public class RoleCreateDto
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio")]
        [StringLength(50, ErrorMessage = "El Campo {0} no Puede Tener Más de 50 Letras")]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(256, ErrorMessage = "El Campo {0} no Puede Tener Más de 256 Letras")]
        public string Description { get; set; }
    }
}
