using System.ComponentModel.DataAnnotations;

namespace ArtwiseAPI.Models
{
    public enum TipoUsuario
    {
        Normal,
        Administrador
    }

    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string SenhaHash { get; set; }

        [Required]
        public TipoUsuario Tipo { get; set; }
    }
}
