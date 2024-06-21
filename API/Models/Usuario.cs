using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Usuario1 { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Contraseña { get; set; } = null!;
}
