using Common.Logging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Management.Sql;
using ScriptCs.AzureManagement.Common;
using ScriptCs.AzureManagement.Common.Credentials;

namespace ScriptCs.AzureManagement.Sql
{
  public class SqlManagement
  {
    private readonly ILog _logger;
    private readonly ICredentialManager _credentialManager;

    public SqlManagement(ManagementContext managementContext)
    {
      _logger            = managementContext.Logger;
      _credentialManager = managementContext.CredentialManager;
    }

    public SqlManagementClient CreateClient() 
    {
      var credentials = _credentialManager.GetManagementCredentials();
      return new SqlManagementClient(credentials);
    }
  }
}
