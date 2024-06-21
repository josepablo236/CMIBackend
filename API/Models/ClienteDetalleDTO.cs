namespace API.Models
{
    public class ClienteDetalleDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
