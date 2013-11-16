using Common.Logging;

namespace ScriptCs.AzureManagement.ScriptPack
{
  public class AzureManagementContext
  {
    private readonly ILog _logger;
    private readonly string[] _scriptArgs;

    public AzureManagementContext(ILog logger, string[] scriptArgs)
    {
      _logger     = logger;
      _scriptArgs = scriptArgs;
    }

    public ILog Logger { get { return _logger; } }
    public string[] ScriptArgs { get { return _scriptArgs;  }
    }
  }
}
