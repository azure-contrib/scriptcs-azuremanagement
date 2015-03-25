using Common.Logging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Management.ServiceBus;
using ScriptCs.AzureManagement.Common;
using ScriptCs.AzureManagement.Common.Credentials;

namespace ScriptCs.AzureManagement.ServiceBus
{
  public class ServiceBusManagement
  {
    private readonly ILog _logger;
    private readonly ICredentialManager _credentialManager;

    public ServiceBusManagement(ManagementContext managementContext)
    {
      _logger            = managementContext.Logger;
      _credentialManager = managementContext.CredentialManager;
    }

    public ServiceBusManagementClient CreateClient() 
    {
      var credentials = _credentialManager.GetManagementCredentials();
      return new ServiceBusManagementClient(credentials);
    }
  }
}
