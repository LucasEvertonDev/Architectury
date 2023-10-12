using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Architecture.Application.Domain.Models.Usuarios;

public class ExcluirUsuarioDto
{
    [JsonIgnore]
    [FromRoute(Name = "id")]
    public string Id { get; set; }
}

public class UsuarioExcluidoModel
{
    [DefaultValue("true")]
    public bool Sucess { get; set; }
}
