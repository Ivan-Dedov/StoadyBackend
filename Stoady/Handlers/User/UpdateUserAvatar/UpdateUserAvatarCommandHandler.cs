using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Models.Parameters;
using Stoady.DataAccess.Repositories.Interfaces;

namespace Stoady.Handlers.User.UpdateUserAvatar
{
    public sealed record UpdateUserAvatarCommand(
            long UserId,
            int AvatarId)
        : IRequest<Unit>;

    public sealed class UpdateUserAvatarCommandHandler
        : IRequestHandler<UpdateUserAvatarCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UpdateUserAvatarCommandHandler> _logger;

        public UpdateUserAvatarCommandHandler(
            ILogger<UpdateUserAvatarCommandHandler> logger,
            IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(
            UpdateUserAvatarCommand request,
            CancellationToken ct)
        {
            var (userId, avatarId) = request;

            var result = await _userRepository.ChangeUserAvatarById(
                new ChangeUserAvatarParameters
                {
                    UserId = userId,
                    AvatarId = avatarId
                },
                ct);

            if (result != 1)
            {
                const string message = "Something went wrong when changing the avatar. Please, try again.";
                _logger.LogWarning(message + Environment.NewLine + $" (UserId = {userId})");
                throw new ApplicationException(message + Environment.NewLine + $" (UserId = {userId})");
            }

            return Unit.Value;
        }
    }
}
