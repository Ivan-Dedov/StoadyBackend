using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Stoady.DataAccess.DataContexts;
using Stoady.Models.Handlers.Team.SelectTeam;

namespace Stoady.Handlers.Team.SelectTeam
{
    public sealed class SelectTeamCommandHandler
    : IRequestHandler<SelectTeamCommand, SelectTeamResponse>
    {
        private readonly StoadyDataContext _context;

        public SelectTeamCommandHandler(
            StoadyDataContext context)
        {
            _context = context;
        }

        public async Task<SelectTeamResponse> Handle(
            SelectTeamCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record SelectTeamCommand(
            long TeamId)
        : IRequest<SelectTeamResponse>;
}
