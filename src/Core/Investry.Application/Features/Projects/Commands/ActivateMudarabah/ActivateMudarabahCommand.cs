using Investry.Application.Common;
using MediatR;

namespace Investry.Application.Features.Projects.Commands.ActivateMudarabah
{
    public record ActivateMudarabahCommand(Guid ProjectId) : IRequest<Result<bool>>;
}
