using System.Linq;

namespace ScriptCs.AzureManagement.Common.Configuration
{
  public class ScriptArgsConfigurationProvider : IConfigurationProvider
  {
    private readonly string[] _scriptArgs;

    public ScriptArgsConfigurationProvider(string[] scriptArgs)
    {
      _scriptArgs = scriptArgs;
    }

    public Config PopulateConfiguration(Config config)
    {
      if (_scriptArgs != null && _scriptArgs.Contains("-HttpTraceEnabled"))
      {
        config.HttpTraceEnabled = true;
      }

      return config;
    }
  }
}