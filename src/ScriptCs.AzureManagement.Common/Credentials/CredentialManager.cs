using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using ScriptCs.AzureManagement.Common.Configuration;

namespace ScriptCs.AzureManagement.Common.Credentials
{
  public class CredentialManager : ICredentialManager
  {
    public SubscriptionCloudCredentials GetManagementCredentials()
    {
      var config = ConfigurationManager.Config;
      var settings = new Dictionary<string, object>
      {
        { "SubscriptionId", config.Subscriptions.First().SubscriptionId }, 
        { "ManagementCertificate", config.Subscriptions.First().ManagementCertificate.Thumbprint }
      };

      return CertificateCloudCredentials.Create(settings);
    }
  }
}
