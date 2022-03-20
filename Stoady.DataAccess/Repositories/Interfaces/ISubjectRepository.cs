using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Stoady.DataAccess.Models.Dao;
using Stoady.DataAccess.Models.Parameters;

namespace Stoady.DataAccess.Repositories.Interfaces
{
    public interface ISubjectRepository
    {
        Task<SubjectDao> GetSubjectById(
            long id,
            CancellationToken ct);

        Task<IEnumerable<SubjectDao>> GetSubjectsByTeamId(
            long teamId,
            CancellationToken ct);

        Task<int> AddSubject(
            AddSubjectParameters parameters,
            CancellationToken ct);

        Task<int> EditSubject(
            EditSubjectParameters parameters,
            CancellationToken ct);

        Task<int> RemoveSubject(
            long id,
            CancellationToken ct);
    }
}
