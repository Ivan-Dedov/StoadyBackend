using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Models.Handlers.User.GetUserAvatar;

namespace Stoady.Handlers.User.GetUserAvatar
{
    public sealed record GetUserAvatarCommand(
            long UserId)
        : IRequest<GetUserAvatarResponse>;

    public sealed class GetUserAvatarCommandHandler
        : IRequestHandler<GetUserAvatarCommand, GetUserAvatarResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetUserAvatarCommandHandler> _logger;

        public GetUserAvatarCommandHandler(
            ILogger<GetUserAvatarCommandHandler> logger,
            IUserRepository userRepository,
            IRoleRepository roleRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<GetUserAvatarResponse> Handle(
            GetUserAvatarCommand request,
            CancellationToken ct)
        {
            var userId = request.UserId;

            var user = await _userRepository.GetUserById(userId, ct);

            return new GetUserAvatarResponse
            {
                AvatarId = user.AvatarId
            };
        }
    }
}
