using Microsoft.Azure;
using ScriptCs.AzureManagement.Common.Configuration;

namespace ScriptCs.AzureManagement.Common.Credentials
{
  public interface ICredentialManager
  {
    void SetActiveSubscriptionForInitialisation(string subscriptionName);
    void Initialise();

    Config.Subscription ActiveSubscription { get; }
    void SetActiveSubscription(string subscriptionName);
    SubscriptionCloudCredentials GetManagementCredentials();
  }
}