using Microsoft.AspNetCore.Mvc;
using ArtwiseAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace ArtwiseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<Usuario> usuarios = new List<Usuario>();
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public UsersController()
        {
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        [HttpPost]
        public IActionResult CriarUsuario([FromBody] Usuario usuario)
        {
            // Hash da senha antes de armazenar
            usuario.SenhaHash = _passwordHasher.HashPassword(usuario, usuario.SenhaHash);

            // Simula adição em um banco de dados
            usuarios.Add(usuario);

            return Ok(new { Id = usuario.Id, Email = usuario.Email, Tipo = usuario.Tipo });
        }
    }
}
