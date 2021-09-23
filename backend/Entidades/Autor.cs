using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Entidades
{
    public class Autor
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo {0} es requerido para la creación de un autor.")]
        [StringLength(maximumLength:5, ErrorMessage ="El campo {0} no debe tener una longitud mayor a {1} carácteres.")]
        public string Nombre { get; set; }
        [Range (10,100)]
        [NotMapped]
        public int Edad { get; set; }
        [CreditCard]
        [NotMapped]
        public string TarjetaDeCredito{ get; set; }
        [Url]
        [NotMapped]
        public string ip { get; set; }
        public List<Libro> Libros { get; set; }
    }
}
