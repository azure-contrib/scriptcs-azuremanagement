using Common.Logging;
using Microsoft.WindowsAzure.Management.Monitoring.Metrics;
using ScriptCs.AzureManagement.Common;
using ScriptCs.AzureManagement.Common.Credentials;

namespace ScriptCs.AzureManagement.Monitoring
{
  public class MonitoringManagementMetrics
  {
    private readonly ILog _logger;
    private readonly ICredentialManager _credentialManager;

    public MonitoringManagementMetrics(ManagementContext managementContext)
    {
      _logger = managementContext.Logger;
      _credentialManager = managementContext.CredentialManager;
    }

    public MetricsClient CreateClient()
    {
      var credentials = _credentialManager.GetManagementCredentials();
      return new MetricsClient(credentials);
    }
  }
}