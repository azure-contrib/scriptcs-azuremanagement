using System.IO;
using Newtonsoft.Json;

namespace ScriptCs.AzureManagement.Common.Configuration
{
  public class JsonFileConfigurationProvider : IConfigurationProvider
  {
    private readonly string _fileName;

    public JsonFileConfigurationProvider(string fileName)
    {
      _fileName = Path.GetFullPath(fileName);
    }

    public Config PopulateConfiguration(Config config)
    {
      string json;
      
      if (!File.Exists(_fileName))
      {
        throw new FileNotFoundException(string.Format("The configuration file could not be found at '{0}'.", _fileName));
      }

      using (var r = new StreamReader(_fileName))
      {
        json = r.ReadToEnd();
      }
      return JsonConvert.DeserializeObject<Config>(json);
    }
  }
}
