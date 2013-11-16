using System;
using System.IO;
using Newtonsoft.Json;

namespace ScriptCs.AzureManagement.Common.Configuration
{
  public class JsonFileConfigurationProvider : IConfigurationProvider
  {
    private readonly string _fileName;

    public JsonFileConfigurationProvider(string fileName)
    {
      _fileName = fileName;
    }

    public Config PopulateConfiguration(Config config)
    {
      string json;
      var fileName = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + _fileName;
      if (!File.Exists(fileName))
      {
        throw new FileNotFoundException(string.Format("The configuration file could not be found at '{0}'.", fileName));
      }

      using (var r = new StreamReader(fileName))
      {
        json = r.ReadToEnd();
      }
      return JsonConvert.DeserializeObject<Config>(json);
    }
  }
}
