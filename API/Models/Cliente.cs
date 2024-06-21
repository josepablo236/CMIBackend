using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string? Telefono { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<ClientesDetalle> ClientesDetalles { get; set; } = new List<ClientesDetalle>();
}
