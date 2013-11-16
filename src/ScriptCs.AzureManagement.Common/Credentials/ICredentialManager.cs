using Microsoft.WindowsAzure;

namespace ScriptCs.AzureManagement.Common.Credentials
{
  public interface ICredentialManager
  {
    SubscriptionCloudCredentials GetManagementCredentials();
  }
}