﻿namespace Architecture.Application.Domain.Models.Auth;

public class TokenFlowModel
{
    public string token_type { get; set; }
    public string access_token { get; set; }
}