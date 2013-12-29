using ScriptCs.AzureManagement.Common;
using ScriptCs.AzureManagement.Common.Configuration;

namespace ScriptCs.AzureManagement.ScriptPack
{
    public partial class AzureManagement : IFluentConfiguration
    {
      public IFluentConfiguration LoadConfigFromJsonFile(string filePath)
      {
        Guard.AgainstNullAndEmpty("Json FilePath", filePath);
        _configurationManager.AddProvider(new JsonFileConfigurationProvider(filePath));

        return this;
      }

      public IFluentConfiguration SetActiveSubscription(string subscriptionName)
      {
        Guard.AgainstNullAndEmpty("Subscription Name", subscriptionName);
        _credentialManager.SetActiveSubscriptionForInitialisation(subscriptionName);

        return this;
      }

      public AzureManagement Initialise()
      {
        return InitialiseScriptPack();
      }
    }
}
