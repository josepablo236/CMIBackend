using System;
using System.Collections.Generic;

namespace API.Models;

public partial class ClientesDetalle
{
    public int DetalleId { get; set; }

    public int ClienteId { get; set; }

    public string? Direccion { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;
}
