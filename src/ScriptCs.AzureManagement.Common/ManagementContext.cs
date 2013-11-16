using Common.Logging;
using ScriptCs.AzureManagement.Common.Credentials;

namespace ScriptCs.AzureManagement.Common
{
  public class ManagementContext
  {
    public ILog Logger { get; set; }
    public ICredentialManager CredentialManager { get; set; }
  }
}
