namespace Data.Client;

internal sealed class Settings
{
    public Dictionary<string, DataContext> Connections { get; set; } = new Dictionary<string, DataContext>();
}