using Common.Logging;
using Microsoft.WindowsAzure.Management.Monitoring.Alerts;
using ScriptCs.AzureManagement.Common;
using ScriptCs.AzureManagement.Common.Credentials;

namespace ScriptCs.AzureManagement.Monitoring
{
  public class MonitoringManagementAlerts
  {
    private readonly ILog _logger;
    private readonly ICredentialManager _credentialManager;

    public MonitoringManagementAlerts(ManagementContext managementContext)
    {
      _logger = managementContext.Logger;
      _credentialManager = managementContext.CredentialManager;
    }

    public AlertsClient CreateClient()
    {
      var credentials = _credentialManager.GetManagementCredentials();
      return new AlertsClient(credentials);
    }
  }
}