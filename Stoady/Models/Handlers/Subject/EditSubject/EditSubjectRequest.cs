namespace Stoady.Models.Handlers.Subject.EditSubject
{
    public sealed record EditSubjectRequest(
        long SubjectId,
        string SubjectName,
        string SubjectPicture,
        string SubjectDescription);
}
