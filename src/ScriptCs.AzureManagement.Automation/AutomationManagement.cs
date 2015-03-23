namespace ScriptCs.AzureManagement.Automation
{
    using global::Common.Logging;
    using Microsoft.WindowsAzure.Management.Automation;
    using ScriptCs.AzureManagement.Common;
    using ScriptCs.AzureManagement.Common.Credentials;

    public class AutomationManagement
    {
        private readonly ILog _logger;
        private readonly ICredentialManager _credentialManager;

        public AutomationManagement(ManagementContext managementContext)
        {
            _logger = managementContext.Logger;
            _credentialManager = managementContext.CredentialManager;
        }

        public AutomationManagementClient CreateClient()
        {
            var credentials = _credentialManager.GetManagementCredentials();
            return new AutomationManagementClient(credentials);
        }
    }
}
