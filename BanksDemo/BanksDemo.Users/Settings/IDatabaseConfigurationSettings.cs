namespace BanksDemo.User.Settings;

public interface IDatabaseConfigurationSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }
}