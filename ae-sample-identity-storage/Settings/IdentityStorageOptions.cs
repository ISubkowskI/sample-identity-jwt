namespace Ae.Sample.Identity.Settings
{
    public sealed class IdentityStorageOptions
    {
        public const string IdentityStorage = "IdentityStorage";

        public string ConnectionString { get; set; } = String.Empty;
    }
}
