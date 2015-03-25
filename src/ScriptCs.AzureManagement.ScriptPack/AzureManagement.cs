using Common.Logging;
using ScriptCs.AzureManagement.Automation;
using ScriptCs.AzureManagement.Common;
using ScriptCs.AzureManagement.Common.Configuration;
using ScriptCs.AzureManagement.Common.Credentials;
using ScriptCs.AzureManagement.Common.TracingInterceptors;
using ScriptCs.AzureManagement.Compute;
using ScriptCs.AzureManagement.ExpressRoute;
using ScriptCs.AzureManagement.Infrastructure;
using ScriptCs.AzureManagement.Media;
using ScriptCs.AzureManagement.Monitoring;
using ScriptCs.AzureManagement.Scheduler;
using ScriptCs.AzureManagement.ServiceBus;
using ScriptCs.AzureManagement.Sql;
using ScriptCs.AzureManagement.Storage;
using ScriptCs.AzureManagement.TrafficManager;
using ScriptCs.AzureManagement.VirtualNetwork;
using ScriptCs.AzureManagement.WebSite;
using ScriptCs.Contracts;
using System;

namespace ScriptCs.AzureManagement.ScriptPack
{
  public partial class AzureManagement : IScriptPackContext
  {
    private readonly ILog _logger;
    private readonly ICredentialManager _credentialManager;
    private readonly IConfigurationManager _configurationManager;

    private readonly string[] _scriptArgs;

    private HttpTracingInterceptor _httpTracingInterceptor;

    private Lazy<AutomationManagement> _automationManagement;
    private Lazy<ComputeManagement> _computeManagement;
    private Lazy<InfrastructureManagement> _infrastructureManagement;
    private Lazy<MediaManagement> _mediaManagement;
    private Lazy<MonitoringManagement> _monitoringManagement;
    private Lazy<SchedulerManagement> _schedulerManagement;
    private Lazy<ServiceBusManagement> _serviceBusManagement;
    private Lazy<SqlManagement> _sqlManagement;
    private Lazy<StorageManagement> _storageManagement;
    private Lazy<VirtualNetworkManagement> _virtualNetworkManagement;
    private Lazy<TrafficManagerManagement> _trafficManagerManagement;
    private Lazy<ExpressRouteManagement> _expressRouteManagement;
    private Lazy<WebSiteManagement> _webSiteManagement;    

    public AzureManagement(AzureManagementContext context)
    {
      _logger = context.Logger;
      _scriptArgs = context.ScriptArgs;

      _configurationManager = new ConfigurationManager(_logger);
      _credentialManager = new CredentialManager(_logger);
    }

    private AzureManagement InitialiseScriptPack()
    {      
      _configurationManager.AddProvider(new ScriptArgsConfigurationProvider(_scriptArgs));
      _configurationManager.Initialise();

      _credentialManager.Initialise();
      
      _httpTracingInterceptor = new HttpTracingInterceptor(_logger, isEnabled: ConfigurationManager.Config.HttpTraceEnabled);
      // CloudContext.Configuration.Tracing.AddTracingInterceptor(_httpTracingInterceptor);


      var managementContext = new ManagementContext
      {
        Logger            = _logger,
        CredentialManager = _credentialManager
      };
      _automationManagement     = new Lazy<AutomationManagement>(() => new AutomationManagement(managementContext));
      _computeManagement        = new Lazy<ComputeManagement>(() => new ComputeManagement(managementContext));
      _infrastructureManagement = new Lazy<InfrastructureManagement>(() => new InfrastructureManagement(managementContext));
      _monitoringManagement     = new Lazy<MonitoringManagement>(() => new MonitoringManagement(managementContext));
      _mediaManagement          = new Lazy<MediaManagement>(() => new MediaManagement(managementContext));
      _schedulerManagement      = new Lazy<SchedulerManagement>(() => new SchedulerManagement(managementContext));
      _serviceBusManagement     = new Lazy<ServiceBusManagement>(() => new ServiceBusManagement(managementContext));
      _sqlManagement            = new Lazy<SqlManagement>(() => new SqlManagement(managementContext));
      _storageManagement        = new Lazy<StorageManagement>(() => new StorageManagement(managementContext));
      _virtualNetworkManagement = new Lazy<VirtualNetworkManagement>(() => new VirtualNetworkManagement(managementContext));
      _trafficManagerManagement = new Lazy<TrafficManagerManagement>(() => new TrafficManagerManagement(managementContext));
      _webSiteManagement        = new Lazy<WebSiteManagement>(() => new WebSiteManagement(managementContext));
      _expressRouteManagement   = new Lazy<ExpressRouteManagement>(() => new ExpressRouteManagement(managementContext));

      return this;
    }

    public AutomationManagement AutomationManagement { get { return _automationManagement.Value; } }
    public ComputeManagement ComputeManagement { get { return _computeManagement.Value; } }
    public InfrastructureManagement InfrastructureManagement { get { return _infrastructureManagement.Value; } }
    public MediaManagement MediaManagement { get { return _mediaManagement.Value; } }
    public MonitoringManagement MonitoringManagement { get { return _monitoringManagement.Value; } }
    public SchedulerManagement SchedulerManagement { get { return _schedulerManagement.Value; } }
    public ServiceBusManagement ServiceBusManagement { get { return _serviceBusManagement.Value; } }
    public SqlManagement SqlManagement { get { return _sqlManagement.Value; } }
    public StorageManagement StorageManagement { get { return _storageManagement.Value; } }
    public VirtualNetworkManagement VirtualNetworkManagement { get { return _virtualNetworkManagement.Value; } }
    public TrafficManagerManagement TrafficManagerManagement { get { return _trafficManagerManagement.Value; } }
    public WebSiteManagement WebSiteManagement { get { return _webSiteManagement.Value; } }
    public ExpressRouteManagement ExpressRouteManagement { get { return _expressRouteManagement.Value; } }

    public string ActiveSubscription
    {
      get { return _credentialManager.ActiveSubscription.Name ?? "Unknown Subscription"; }
      set { _credentialManager.SetActiveSubscription(value); }
    }

    public bool HttpTraceEnabled
    {
      get { return _httpTracingInterceptor.IsEnabled; }
      set { _httpTracingInterceptor.IsEnabled = value; }
    }
  }
}
