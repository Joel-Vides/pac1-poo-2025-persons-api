using System.ComponentModel.DataAnnotations;

namespace Persons.API.Dtos.Coutries
{
    public class CountriesCreateDto
    {

        [Display(Name = "Pais")]
        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El Campo {0} debe Tener un Minimo de {2} y un Máximo de {1} Caracteres")]
        public string Name { get; set; }

        [Display(Name = "Alpha Code 3")]
        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "El Campo {0} debe Tener un Minimo de {2} y un Máximo de {1} Caracteres")]
        public string AlphaCode3 { get; set; }

    }
}
