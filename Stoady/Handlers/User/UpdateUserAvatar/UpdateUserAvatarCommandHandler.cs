using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.DataContexts;

namespace Stoady.Handlers.User.UpdateUserAvatar
{
    public sealed class UpdateUserAvatarCommandHandler
        : IRequestHandler<UpdateUserAvatarCommand, Unit>
    {
        private readonly StoadyDataContext _context;
        private readonly ILogger<UpdateUserAvatarCommandHandler> _logger;

        public UpdateUserAvatarCommandHandler(
            StoadyDataContext context,
            ILogger<UpdateUserAvatarCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(
            UpdateUserAvatarCommand request,
            CancellationToken cancellationToken)
        {
            var (userId, avatarId) = request;

            if (_context.Users.Count(x => x.Id == userId) != 1)
            {
                var message = $"Could not find user with ID = {userId}";
                throw new ApplicationException(message);
            }

            var user = _context.Users.First(x => x.Id == userId);
            user.AvatarId = avatarId;

            if (await _context.SaveChangesAsync() != 1)
            {
                var message = $"Could not update avatar for user with ID = {userId}";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }
    }

    public sealed record UpdateUserAvatarCommand(
            long UserId,
            int AvatarId)
        : IRequest<Unit>;
}
