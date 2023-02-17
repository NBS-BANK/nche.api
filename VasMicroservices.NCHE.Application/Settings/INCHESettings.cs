namespace VasMicroservices.NCHE.Application.Settings
{
    public interface INCHESettings
    {
        string ClientId { get; }
        string ClientKey { get; }
        string Url { get; }
    }
}