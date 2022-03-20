namespace Stoady.Models.Handlers.Subject.AddSubject
{
    public sealed record AddSubjectRequest(
        long TeamId,
        string SubjectName,
        string SubjectDescription);
}
