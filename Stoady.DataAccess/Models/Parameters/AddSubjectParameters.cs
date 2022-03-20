namespace Stoady.DataAccess.Models.Parameters
{
    public sealed class AddSubjectParameters
    {
        public long TeamId { get; init; }

        public string SubjectName { get; init; }

        public string SubjectPicture { get; init; }

        public string SubjectDescription { get; init; }
    }
}
