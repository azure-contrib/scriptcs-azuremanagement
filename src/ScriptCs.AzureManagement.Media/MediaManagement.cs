using Common.Logging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Management.MediaServices;
using ScriptCs.AzureManagement.Common;
using ScriptCs.AzureManagement.Common.Credentials;

namespace ScriptCs.AzureManagement.Media
{
  public class MediaManagement
  {
    private readonly ILog _logger;
    private readonly ICredentialManager _credentialManager;

    public MediaManagement(ManagementContext managementContext)
    {
      _logger = managementContext.Logger;
      _credentialManager = managementContext.CredentialManager;
    }

    public MediaServicesManagementClient CreateClient()
    {
      var credentials = _credentialManager.GetManagementCredentials();
      return new MediaServicesManagementClient(credentials);
    }
  }
}
