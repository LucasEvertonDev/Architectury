﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Architecture.Application.Domain.Models.Auth;

public class RefreshTokenDto
{
    [FromHeader(Name = "client_id")]
    [DefaultValue("7064bbbf-5d11-4782-9009-95e5a6fd6822")]
    public virtual string ClientId { get; set; }

    [FromHeader(Name = "client_secret")]
    [DefaultValue("dff0bcb8dad7ea803e8d28bf566bcd354b5ec4e96ff4576a1b71ec4a21d56910")]
    public virtual string ClientSecret { get; set; }
}
