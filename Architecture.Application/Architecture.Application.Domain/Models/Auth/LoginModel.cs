using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Architecture.Application.Domain.Models.Auth;

public class LoginModel
{
    [DefaultValue("lcseverton")]
    public string Username { get; set; }
    [DefaultValue("123456")]
    public string Password { get; set; }
}

public class TokenModel
{
    public string TokenJWT { get; set; }
    public DateTime DataExpiracao { get; set; }
}