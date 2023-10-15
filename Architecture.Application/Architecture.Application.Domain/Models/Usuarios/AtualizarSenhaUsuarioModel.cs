using System.ComponentModel;

namespace Architecture.Application.Domain.Models.Usuarios;

public class AtualizarSenhaUsuarioModel
{
    [DefaultValue("123456")]
    public string Password { get; set; }
}
