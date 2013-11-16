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
        "Microsoft.WindowsAzure.Management.Compute.Models",
        "Microsoft.WindowsAzure.Management.Compute",
        "Microsoft.WindowsAzure.Management.Models",
        "Microsoft.WindowsAzure.Management.Storage.Models",
        "Microsoft.WindowsAzure.Management.Storage",
        "Microsoft.WindowsAzure.Management.VirtualNetworks.Models",
        "Microsoft.WindowsAzure.Management.VirtualNetworks",
        "Microsoft.WindowsAzure.Management.WebSites.Models",
        "Microsoft.WindowsAzure.Management.WebSites",
        "Microsoft.WindowsAzure.Management",
        "Microsoft.WindowsAzure",
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

    public IScriptPackContext GetContext()
    {
      var context = new AzureManagementContext(_logger, _scriptArgs);
      return new AzureManagement(context);
    }
  }
}
