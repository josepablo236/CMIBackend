using System;
using System.Collections.Generic;

namespace API.Models;

public partial class ClientesEliminado
{
    public int? ClienteId { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Correo { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public DateTime? FechaEliminacion { get; set; }
}
