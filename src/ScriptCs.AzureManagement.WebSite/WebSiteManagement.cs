using Common.Logging;
using Microsoft.Azure.Management.WebSites;
using ScriptCs.AzureManagement.Common;
using ScriptCs.AzureManagement.Common.Credentials;

namespace ScriptCs.AzureManagement.WebSite
{
  public class WebSiteManagement
  {
    private readonly ILog _logger;
    private readonly ICredentialManager _credentialManager;

    public WebSiteManagement(ManagementContext managementContext)
    {
      _logger = managementContext.Logger;
      _credentialManager = managementContext.CredentialManager;
    }

    public WebSiteManagementClient CreateClient() 
    {
      var credentials = _credentialManager.GetManagementCredentials();
      return new WebSiteManagementClient(credentials);
    }
  }
}
