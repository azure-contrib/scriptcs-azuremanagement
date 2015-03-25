using Common.Logging;
using Microsoft.WindowsAzure.Management.Network;
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

    public NetworkManagementClient CreateClient() 
    {
      var credentials = _credentialManager.GetManagementCredentials();
      return new NetworkManagementClient(credentials);
    }
  }
}
