namespace Stoady.Models
{
    public sealed record TokenModel(
        string AccessToken,
        string RefreshToken);
}
