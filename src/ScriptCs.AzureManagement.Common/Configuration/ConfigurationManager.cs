using System.Collections.Generic;
using Common.Logging;

namespace ScriptCs.AzureManagement.Common.Configuration
{
  public class ConfigurationManager : IConfigurationManager
  {
    private readonly ILog _logger;
    private static Config _config;
    private static readonly object _lock = new object();

    private readonly IList<IConfigurationProvider> _configurationProviders = new List<IConfigurationProvider>();

    public ConfigurationManager(ILog logger)
    {
      _logger = logger;
    }

    public void AddProvider(IConfigurationProvider configurationProvider)
    {
      _configurationProviders.Add(configurationProvider);
    }

    public void Initialise()
    {
      foreach (var provider in _configurationProviders)
      {
        lock (_lock)
        {
          _config = provider.PopulateConfiguration(_config);
        }
      }
    }

    public static Config Config { get { return _config; } }
  }
}
