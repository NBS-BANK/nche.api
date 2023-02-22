namespace VasMicroservices.NCHE.Application.Settings
{
    public interface INCHESettings
    {
        string ClientId { get; }
        string AuthToken { get; }
        string Url { get; }
    }
}