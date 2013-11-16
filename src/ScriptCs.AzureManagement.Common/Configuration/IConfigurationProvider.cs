namespace ScriptCs.AzureManagement.Common.Configuration
{
  public interface IConfigurationProvider
  {
    Config PopulateConfiguration(Config config);
  }
}
