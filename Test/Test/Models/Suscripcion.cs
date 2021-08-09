using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    [Table("Cuestionario")]
    public class Suscripcion
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo nombre requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo apellido requerido")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El campo celular requerido")]
        [MinLength(10,ErrorMessage ="Minimo 10 digitos"), System.ComponentModel.DataAnnotations.MaxLength(10,ErrorMessage ="Maximo 10 digitos")]
        public string Celular { get; set; }
        [Required(ErrorMessage = "El campo email es requerido")]
        [EmailAddress(ErrorMessage ="Correo no valido")]
        public string Correo { get; set; }
        public string TipoSubscripcion { get; set; }
        public bool RecibirEmail { get; set; }
        public DateTime FechaInsert { get; set; }
        public string UsuarioInsert { get; set; }
    }
}
