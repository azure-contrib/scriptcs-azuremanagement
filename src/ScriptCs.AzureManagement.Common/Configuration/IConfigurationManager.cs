namespace ScriptCs.AzureManagement.Common.Configuration
{
  public interface IConfigurationManager
  {
    void AddProvider(IConfigurationProvider configurationProvider);
    void Initialise();
  }
}
