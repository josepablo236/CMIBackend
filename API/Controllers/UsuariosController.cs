using API.Context;
using API.Models;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Response = API.Models.Response;

namespace API.Controllers
{
    [Route("api")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        CmiContext _context = new CmiContext();

        [HttpPost("autenticacion")]
        public async Task<Response> login([FromBody] Login user)
        {
            var response = new Response();
            try
            {
                var usuario = await _context.Usuarios.Where(u => u.Usuario1 == user.Usuario && u.Contraseña == user.Contraseña).FirstOrDefaultAsync();
                if (usuario != null)
                {
                    response.Codigo = 0;
                    response.Mensaje = "Login exitoso";
                    response.Detalle = usuario;
                }
                else
                {
                    response.Codigo = 1;
                    response.Mensaje = "Usuario no encontrado";
                    response.Detalle = null;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Codigo = 2;
                response.Mensaje = "Error al realizar login";
                response.Detalle = ex.Message;
                return response;
            }
        }
    }
}
