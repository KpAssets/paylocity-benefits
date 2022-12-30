namespace Data.Client;

internal sealed class DataContext
{
    public DatabaseType DatabaseType { get; set; }
    public string DatabaseName { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
}