namespace Stoady.DataAccess.Models.Parameters
{
    public sealed class EditSubjectParameters
    {
        public long SubjectId { get; init; }

        public string SubjectName { get; init; }

        public string SubjectDescription { get; init; }
    }
}
