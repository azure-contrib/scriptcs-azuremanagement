namespace ScriptCs.AzureManagement.TrafficManager
{
    using global::Common.Logging;
    using Microsoft.WindowsAzure.Management.TrafficManager;
    using ScriptCs.AzureManagement.Common;
    using ScriptCs.AzureManagement.Common.Credentials;

    public class TrafficManagerManagement
    {
        private readonly ILog _logger;
        private readonly ICredentialManager _credentialManager;

        public TrafficManagerManagement(ManagementContext managementContext)
        {
            _logger = managementContext.Logger;
            _credentialManager = managementContext.CredentialManager;
        }

        public TrafficManagerManagementClient CreateClient()
        {
            var credentials = _credentialManager.GetManagementCredentials();
            return new TrafficManagerManagementClient(credentials);
        }
    }
}