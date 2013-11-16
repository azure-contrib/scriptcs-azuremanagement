using Common.Logging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Management.VirtualNetworks;
using ScriptCs.AzureManagement.Common;
using ScriptCs.AzureManagement.Common.Credentials;

namespace ScriptCs.AzureManagement.VirtualNetwork
{
  public class VirtualNetworkManagement
  {
    private readonly ILog _logger;
    private readonly ICredentialManager _credentialManager;

    public VirtualNetworkManagement(ManagementContext managementContext)
    {
      _logger = managementContext.Logger;
      _credentialManager = managementContext.CredentialManager;
    }

    public VirtualNetworkManagementClient CreateClient() 
    {
      var credentials = _credentialManager.GetManagementCredentials();
      return CloudContext.Clients.CreateVirtualNetworkManagementClient(credentials);
    }
  }
}
