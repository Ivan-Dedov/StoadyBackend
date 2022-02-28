namespace Stoady.Models.Handlers.Testing.SaveTestResults
{
    public sealed record SaveTestResultsRequest(
        long UserId,
        long TopicId,
        int Result);
}
