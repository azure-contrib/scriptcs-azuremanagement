namespace ScriptCs.AzureManagement.ScriptPack
{
  public interface IFluentConfiguration
  {
    IFluentConfiguration LoadConfigFromJsonFile(string filePath);
    IFluentConfiguration SetActiveSubscription(string subscriptionName);
    AzureManagement Initialise();
  }
}
