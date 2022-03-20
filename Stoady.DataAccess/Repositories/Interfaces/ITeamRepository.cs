using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Stoady.DataAccess.Models.Dao;
using Stoady.DataAccess.Models.Parameters;

namespace Stoady.DataAccess.Repositories.Interfaces
{
    public interface ITeamRepository
    {
        Task<TeamDao> GetTeamById(
            long id,
            CancellationToken ct);

        Task<TeamDao> GetTeamByName(
            string name,
            CancellationToken ct);

        Task<IEnumerable<UserDao>> GetTeamMembersByTeamId(
            long teamId,
            CancellationToken ct);

        Task<int> CreateTeam(
            CreateTeamParameters parameters,
            CancellationToken ct);

        Task<int> AddMember(
            AddMemberParameters parameters,
            CancellationToken ct);

        Task<int> ChangeMemberStatus(
            ChangeMemberStatusParameters parameters,
            CancellationToken ct);

        Task<int> ChangeTeamAvatar(
            ChangeTeamAvatarParameters parameters,
            CancellationToken ct);

        Task<int> RemoveMember(
            long userId,
            long teamId,
            CancellationToken ct);
    }
}
