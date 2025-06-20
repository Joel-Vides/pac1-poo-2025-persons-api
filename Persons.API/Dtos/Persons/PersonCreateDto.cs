﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Persons.API.Dtos.Persons;

namespace Persons.API.Dtos.Countries
{
    public class PersonCreateDto
    {
        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El Campo {0} debe Tener un Minimo de {2} y un Máximo de {1} Caracteres")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El Campo {0} debe Tener un Minimo de {2} y un Máximo de {1} Caracteres")]
        public string LastName { get; set; }

        [Display(Name = "Documento Nacional de Identidad")]
        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "El Campo {0} debe Tener un Minimo de {2} y un Máximo de {1} Caracteres")]
        public string DNI { get; set; }

        [Display(Name = "Genero")]
        [Required(ErrorMessage = "El Campo es Requerido")]
        [StringLength(1, ErrorMessage = "El {0} solo Acepta {1} Caracter")]
        public string Gender { get; set; }

        [Display(Name = "Pais")]
        [Required(ErrorMessage = "El {0} es Requerido.")]
        public string CountryId { get; set; } = null;

        // Arreglo de Family Member

        public List<FamilyMemberCreateDto> Family { get; set; }
    }
}