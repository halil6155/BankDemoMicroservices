namespace BanksDemo.User.Settings;

public class DatabaseConfigurationSetting:IDatabaseConfigurationSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }
}