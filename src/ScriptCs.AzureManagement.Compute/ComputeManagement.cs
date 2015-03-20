using Common.Logging;
using Microsoft.WindowsAzure.Management.Compute;
using ScriptCs.AzureManagement.Common;
using ScriptCs.AzureManagement.Common.Credentials;

namespace ScriptCs.AzureManagement.Compute
{
  public class ComputeManagement
  {
    private readonly ILog _logger;
    private readonly ICredentialManager _credentialManager;

    public ComputeManagement(ManagementContext managementContext)
    {
      _logger = managementContext.Logger;
      _credentialManager = managementContext.CredentialManager;
    }

    public ComputeManagementClient CreateClient() 
    {
      var credentials = _credentialManager.GetManagementCredentials();
      return new ComputeManagementClient(credentials);
    }
  }
}
