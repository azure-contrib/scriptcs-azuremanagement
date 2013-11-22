using System;
using Common.Logging;
using ScriptCs.AzureManagement.Common;
using ScriptCs.AzureManagement.Common.Credentials;

namespace ScriptCs.AzureManagement.Monitoring
{
  public class MonitoringManagement
  {
    private readonly ILog _logger;
    private readonly ICredentialManager _credentialManager;

    private readonly Lazy<MonitoringManagementAlerts> _alerts;
    private readonly Lazy<MonitoringManagementAutoscale> _autoscale;
    private readonly Lazy<MonitoringManagementMetrics> _metrics;

    public MonitoringManagement(ManagementContext managementContext)
    {
      _logger = managementContext.Logger;
      _credentialManager = managementContext.CredentialManager;

      _alerts = new Lazy<MonitoringManagementAlerts>(() => new MonitoringManagementAlerts(managementContext));
      _autoscale = new Lazy<MonitoringManagementAutoscale>(() => new MonitoringManagementAutoscale(managementContext));
      _metrics = new Lazy<MonitoringManagementMetrics>(() => new MonitoringManagementMetrics(managementContext));
    }

    public MonitoringManagementAlerts Alerts { get { return _alerts.Value; } }
    public MonitoringManagementAutoscale Autoscale { get { return _autoscale.Value; } }
    public MonitoringManagementMetrics Metrics { get { return _metrics.Value; } }
  }
}
