using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Architecture.Application.Domain.Models.Auth;

public class LoginDto
{
    [JsonIgnore]
    [FromHeader(Name = "client_id")]
    [DefaultValue("7064bbbf-5d11-4782-9009-95e5a6fd6822")]
    public virtual string ClientId { get; set; }

    [JsonIgnore]
    [FromHeader(Name = "client_secret")]
    [DefaultValue("dff0bcb8dad7ea803e8d28bf566bcd354b5ec4e96ff4576a1b71ec4a21d56910")]
    public virtual string ClientSecret { get; set; }

    [FromBody]
    public LoginModel Body { get; set; }
}

public class LoginInfo
{
    [FromHeader(Name = "Authorization")]
    public string Authorization { get; set; }
    public string username { get; set; }
    public string password { get; set; }
}

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