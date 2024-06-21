using API.Context;
using API.Models;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Response = API.Models.Response;

namespace API.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        CmiContext _context = new CmiContext();


        [HttpPost("cliente")]
        public async Task<Response> InsertarCliente([FromBody] ClienteDetalleDTO cliente)
        {
            var response = new Response();
            try
            {
                var parameters = new[]
                {
                new SqlParameter("@Nombre", cliente.Nombre),
                new SqlParameter("@Apellido", cliente.Apellido),
                new SqlParameter("@Correo", cliente.Correo),
                new SqlParameter("@Telefono", cliente.Telefono),
                new SqlParameter("@Direccion", cliente.Direccion),
                new SqlParameter("@FechaNacimiento", cliente.FechaNacimiento)
                };

                await _context.Database.ExecuteSqlRawAsync("EXEC InsertarCliente @Nombre, @Apellido, @Correo, @Telefono, @Direccion, @FechaNacimiento", parameters);

                response.Codigo = 0;
                response.Mensaje = "Insert exitoso";
                response.Detalle = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Codigo = 2;
                response.Mensaje = "Error al insertar";
                response.Detalle = ex.Message;
                return response;
            }
        }

        [HttpGet("clientes")]
        public async Task<Response> ObtenerClientes()
        {
            var response = new Response();
            try
            {
                var clientesDetalle = await (from cliente in _context.Clientes
                                              join detalle in _context.ClientesDetalles
                                              on cliente.ClienteId equals detalle.ClienteId
                                              select new ClienteDetalleDTO
                                              {
                                                  Nombre = cliente.Nombre,
                                                  Apellido = cliente.Apellido,
                                                  Correo = cliente.Correo,
                                                  Telefono = cliente.Telefono,
                                                  FechaRegistro = cliente.FechaRegistro,
                                                  Direccion = detalle.Direccion,
                                                  FechaNacimiento = detalle.FechaNacimiento
                                              }).ToListAsync();

                response.Codigo = 0;
                response.Mensaje = "Consulta exitosa";
                response.Detalle = clientesDetalle;
                return response;
            }
            catch(Exception ex)
            {
                response.Codigo = 2;
                response.Mensaje = "Error al insertar";
                response.Detalle = ex.Message;
                return response;
            }
        }

        [HttpGet("clientes/eliminados")]
        public async Task<Response> ObtenerClientesEliminados()
        {
            var response = new Response();
            try
            {
                var clientes = await _context.ClientesEliminados.ToListAsync();
                response.Codigo = 0;
                response.Mensaje = "Consulta exitosa";
                response.Detalle = clientes;
                return response;
            }
            catch(Exception ex) {
                response.Codigo = 2;
                response.Mensaje = "Error en la consulta";
                response.Detalle = ex.Message;
                return response;
            }
        }

        [HttpPut("cliente/{correo}")]
        public async Task<Response> EditarCliente(string correo, [FromBody] ClienteDetalleDTO cliente)
        {
            var response = new Response();
            try
            {
                // Buscar el Cliente por Correo
                var clienteDB = await _context.Clientes.FirstOrDefaultAsync(c => c.Correo == correo);
                if (clienteDB == null)
                {
                    response.Codigo = 1;
                    response.Mensaje = "Cliente no encontrado";
                    response.Detalle = null;
                    return response;
                }

                // Ejecutar el procedimiento almacenado para editar el cliente
                var parameters = new[]
                {
            new SqlParameter("@ClienteID", clienteDB.ClienteId),
            new SqlParameter("@Nombre", cliente.Nombre),
            new SqlParameter("@Apellido", cliente.Apellido),
            new SqlParameter("@Correo", cliente.Correo),
            new SqlParameter("@Telefono", cliente.Telefono),
            new SqlParameter("@Direccion", cliente.Direccion),
            new SqlParameter("@FechaNacimiento", cliente.FechaNacimiento)
        };

                await _context.Database.ExecuteSqlRawAsync("EXEC EditarCliente @ClienteID, @Nombre, @Apellido, @Correo, @Telefono, @Direccion, @FechaNacimiento", parameters);

                response.Codigo = 0;
                response.Mensaje = "Edit exitoso";
                response.Detalle = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Codigo = 2;
                response.Mensaje = "Error en el edit";
                response.Detalle = ex.Message;
                return response;
            }
        }

        [HttpDelete("cliente/{correo}")]
        public async Task<Response> EliminarCliente(string correo)
        {
            var response = new Response();
            try
            {
                // Buscar el ClienteID basado en el correo
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Correo == correo);
                if (cliente == null)
                {
                    response.Codigo = 1;
                    response.Mensaje = "Cliente no encontrado";
                    response.Detalle = null;
                    return response;
                }

                // Ejecutar el procedimiento almacenado para eliminar el cliente
                var parameter = new SqlParameter("@ClienteID", cliente.ClienteId);
                await _context.Database.ExecuteSqlRawAsync("EXEC EliminarCliente @ClienteID", parameter);

                response.Codigo = 0;
                response.Mensaje = "Delete exitoso";
                response.Detalle = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Codigo = 2;
                response.Mensaje = "Error en el delete";
                response.Detalle = ex.Message;
                return response;
            }
        }
    }
}
