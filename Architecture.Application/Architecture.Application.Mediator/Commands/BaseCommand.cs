using Architecture.Application.Domain.Models.Base;
using MediatR;

namespace Architecture.Application.Mediator.Commands;

public class BaseCommand : IRequest<ResponseDto>
{
}
