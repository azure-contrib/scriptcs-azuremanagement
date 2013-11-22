using Common.Logging;
using Microsoft.WindowsAzure.Management.Monitoring.Autoscale;
using ScriptCs.AzureManagement.Common;
using ScriptCs.AzureManagement.Common.Credentials;

namespace ScriptCs.AzureManagement.Monitoring
{
  public class MonitoringManagementAutoscale
  {
    private readonly ILog _logger;
    private readonly ICredentialManager _credentialManager;

    public MonitoringManagementAutoscale(ManagementContext managementContext)
    {
      _logger = managementContext.Logger;
      _credentialManager = managementContext.CredentialManager;
    }

    public AutoscaleClient CreateClient()
    {
      var credentials = _credentialManager.GetManagementCredentials();
      return new AutoscaleClient(credentials);
    }
  }
}