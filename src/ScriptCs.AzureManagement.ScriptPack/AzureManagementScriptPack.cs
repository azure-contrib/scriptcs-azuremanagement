using System.ComponentModel.Composition;
using System.Linq;
using Common.Logging;
using ScriptCs.Contracts;

namespace ScriptCs.AzureManagement.ScriptPack
{
  public class AzureManagementScriptPack : IScriptPack
  {
    private readonly ILog _logger;
    private string[] _scriptArgs;

    public void Initialize(IScriptPackSession session)
    {
      var namespaces = new[]
      {
        "System.Net",
        "Microsoft.WindowsAzure.Management.Automation",
        "Microsoft.WindowsAzure.Management.Automation.Models",
        "Microsoft.WindowsAzure.Management.Compute",
        "Microsoft.WindowsAzure.Management.Compute.Models",
        "Microsoft.WindowsAzure.Management.ExpressRoute",
        "Microsoft.WindowsAzure.Management.ExpressRoute.Models",
        "Microsoft.WindowsAzure.Management.MediaServices",
        "Microsoft.WindowsAzure.Management.MediaServices.Models",
        "Microsoft.WindowsAzure.Management.Models",
        "Microsoft.WindowsAzure.Management.Monitoring",
        "Microsoft.WindowsAzure.Management.Monitoring.Alerts",
        "Microsoft.WindowsAzure.Management.Monitoring.Alerts.Models",
        "Microsoft.WindowsAzure.Management.Monitoring.Autoscale",
        "Microsoft.WindowsAzure.Management.Monitoring.Autoscale.Models",
        "Microsoft.WindowsAzure.Management.Monitoring.Metrics",
        "Microsoft.WindowsAzure.Management.Monitoring.Metrics.Models",
        "Microsoft.WindowsAzure.Management.Scheduler",
        "Microsoft.WindowsAzure.Management.Scheduler.Models",
        "Microsoft.WindowsAzure.Management.ServiceBus",
        "Microsoft.WindowsAzure.Management.ServiceBus.Models",
        "Microsoft.WindowsAzure.Management.Sql.Models",
        "Microsoft.WindowsAzure.Management.Sql",
        "Microsoft.WindowsAzure.Management.Storage.Models",
        "Microsoft.WindowsAzure.Management.Storage",
        "Microsoft.WindowsAzure.Management.Network.Models",
        "Microsoft.WindowsAzure.Management.Network",
        "Microsoft.WindowsAzure.Management.TrafficManager",
        "Microsoft.WindowsAzure.Management.TrafficManager.Models",
        "Microsoft.Azure.Management.WebSites.Models",
        "Microsoft.Azure.Management.WebSites",
        "Microsoft.WindowsAzure.Management",
        "Microsoft.WindowsAzure",
       // "Microsoft.WindowsAzure.Common",
      }.ToList();

      var references = new[]
      {
        "System.Net",
      }.ToList();

      namespaces.ForEach(session.ImportNamespace);
      references.ForEach(session.AddReference);

      _scriptArgs = session.ScriptArgs;
    }

    public void Terminate() { }

    [ImportingConstructor]
    public AzureManagementScriptPack(ILog logger)
    {
        _logger = logger;
    }

    // public AzureManagementScriptPack() { _logger = new global::Common.Logging.Simple.NoOpLogger(); }

    public IScriptPackContext GetContext()
    {
      var context = new AzureManagementContext(_logger, _scriptArgs);
      return new AzureManagement(context);
    }
  }
}
