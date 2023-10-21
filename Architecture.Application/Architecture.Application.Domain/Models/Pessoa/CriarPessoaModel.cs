﻿using Architecture.Application.Domain.Models.Base;
using Architecture.Application.Domain.Models.Endereco;

namespace Architecture.Application.Domain.Models.Pessoa;

public class CriarPessoaModel : IValidationAsync
{
    public string PrimeiroNome { get; set; }
    public string Sobrenome { get; set; }
    public string Email { get; set; }
    public DateTime? DataNascimento { get; set; }
    public EnderecoModel Endereco { get; set; }
}
