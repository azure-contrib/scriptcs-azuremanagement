using Common.Logging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Management;
using ScriptCs.AzureManagement.Common;
using ScriptCs.AzureManagement.Common.Credentials;

namespace ScriptCs.AzureManagement.Infrastructure
{
  public class InfrastructureManagement
  {
    private readonly ILog _logger;
    private readonly ICredentialManager _credentialManager;

    public InfrastructureManagement(ManagementContext managementContext)
    {
      _logger = managementContext.Logger;
      _credentialManager = managementContext.CredentialManager;
    }

    public ManagementClient CreateClient()
    {
      var credentials = _credentialManager.GetManagementCredentials();
      return new ManagementClient(credentials);
    }
  }
}
