using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Architecture.Application.Domain.Models.Usuarios;

public class AtualizarSenhaUsuarioDto
{
    [JsonIgnore]
    [FromRoute(Name = "id")]
    public virtual string Id { get; set; }

    [FromBody]
    public AtualizarSenhaUsuarioModel Body { get; set; }
}

public class AtualizarSenhaUsuarioModel
{
    [DefaultValue("123456")]
    public string Password { get; set; }
}

public class SenhaUsuarioAtualizadaModel
{
    public bool Sucess { get; set; }
}
