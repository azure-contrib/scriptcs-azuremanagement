using System;
using Common.Logging;
using Microsoft.WindowsAzure;
using ScriptCs.AzureManagement.Common;
using ScriptCs.AzureManagement.Common.Configuration;
using ScriptCs.AzureManagement.Common.Credentials;
using ScriptCs.AzureManagement.Common.TracingInterceptors;
using ScriptCs.AzureManagement.Compute;
using ScriptCs.AzureManagement.Infrastructure;
using ScriptCs.AzureManagement.Monitoring;
using ScriptCs.AzureManagement.Scheduler;
using ScriptCs.AzureManagement.Storage;
using ScriptCs.AzureManagement.VirtualNetwork;
using ScriptCs.AzureManagement.WebSite;
using ScriptCs.Contracts;

namespace ScriptCs.AzureManagement.ScriptPack
{
  public class AzureManagement : IScriptPackContext
  {
    private ILog _logger;
    private ICredentialManager _credentialManager;
    private HttpTracingInterceptor _httpTracingInterceptor;

    private Lazy<ComputeManagement> _computeManagement;
    private Lazy<InfrastructureManagement> _infrastructureManagement;
    private Lazy<MonitoringManagement> _monitoringManagement;
    private Lazy<SchedulerManagement> _schedulerManagement;
    private Lazy<StorageManagement> _storageManagement;
    private Lazy<VirtualNetworkManagement> _virtualNetworkManagement;
    private Lazy<WebSiteManagement> _webSiteManagement;    

    public AzureManagement(AzureManagementContext context)
    {
      Initialise(context);
    }

    private void Initialise(AzureManagementContext context)
    {
      var config = new ConfigurationManager();
      config.AddProvider(new JsonFileConfigurationProvider("WAML.config.json"));
      config.AddProvider(new ScriptArgsConfigurationProvider(context.ScriptArgs));      
      config.Initialise();

      _logger = context.Logger;
      _credentialManager = new CredentialManager();

      _httpTracingInterceptor = new HttpTracingInterceptor(_logger, isEnabled: ConfigurationManager.Config.HttpTraceEnabled);
      CloudContext.Configuration.Tracing.AddTracingInterceptor(_httpTracingInterceptor);

      var managementContext = new ManagementContext
      {
        Logger = _logger,
        CredentialManager = _credentialManager
      };

      _computeManagement = new Lazy<ComputeManagement>(() => new ComputeManagement(managementContext));
      _infrastructureManagement = new Lazy<InfrastructureManagement>(() => new InfrastructureManagement(managementContext));
      _monitoringManagement = new Lazy<MonitoringManagement>(() => new MonitoringManagement(managementContext));
      _schedulerManagement = new Lazy<SchedulerManagement>(() => new SchedulerManagement(managementContext));
      _storageManagement = new Lazy<StorageManagement>(() => new StorageManagement(managementContext));
      _virtualNetworkManagement = new Lazy<VirtualNetworkManagement>(() => new VirtualNetworkManagement(managementContext));
      _webSiteManagement = new Lazy<WebSiteManagement>(() => new WebSiteManagement(managementContext));      
    }

    public ComputeManagement ComputeManagement { get { return _computeManagement.Value; } }
    public InfrastructureManagement InfrastructureManagement { get { return _infrastructureManagement.Value; } }
    public MonitoringManagement MonitoringManagement { get { return _monitoringManagement.Value; } }
    public SchedulerManagement SchedulerManagement { get { return _schedulerManagement.Value; } }
    public StorageManagement StorageManagement { get { return _storageManagement.Value; } }
    public VirtualNetworkManagement VirtualNetworkManagement { get { return _virtualNetworkManagement.Value; } }
    public WebSiteManagement WebSiteManagement { get { return _webSiteManagement.Value; } }

    public bool HttpTraceEnabled
    {
      get { return _httpTracingInterceptor.IsEnabled; }
      set { _httpTracingInterceptor.IsEnabled = value; }
    }
  }
}
