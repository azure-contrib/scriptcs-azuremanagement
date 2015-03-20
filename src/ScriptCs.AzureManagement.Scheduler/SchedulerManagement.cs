using Common.Logging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Management.Scheduler;
using ScriptCs.AzureManagement.Common;
using ScriptCs.AzureManagement.Common.Credentials;

namespace ScriptCs.AzureManagement.Scheduler
{
  public class SchedulerManagement
  {
    private readonly ILog _logger;
    private readonly ICredentialManager _credentialManager;

    public SchedulerManagement(ManagementContext managementContext)
    {
      _logger = managementContext.Logger;
      _credentialManager = managementContext.CredentialManager;
    }

    public SchedulerManagementClient CreateClient() 
    {
      var credentials = _credentialManager.GetManagementCredentials();
      return new SchedulerManagementClient(credentials);
    }
  }
}
