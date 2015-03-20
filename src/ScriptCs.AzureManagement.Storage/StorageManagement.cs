using Common.Logging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Management.Storage;
using ScriptCs.AzureManagement.Common;
using ScriptCs.AzureManagement.Common.Credentials;

namespace ScriptCs.AzureManagement.Storage
{
  public class StorageManagement
  {
    private readonly ILog _logger;
    private readonly ICredentialManager _credentialManager;

    public StorageManagement(ManagementContext managementContext)
    {
      _logger            = managementContext.Logger;
      _credentialManager = managementContext.CredentialManager;
    }

    public StorageManagementClient CreateClient()
    {
      var credentials = _credentialManager.GetManagementCredentials();
      return new StorageManagementClient(credentials);
    }
  }
}
