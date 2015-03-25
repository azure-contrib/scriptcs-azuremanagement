namespace ScriptCs.AzureManagement.ExpressRoute
{
    using global::Common.Logging;
    using Microsoft.WindowsAzure.Management.ExpressRoute;
    using ScriptCs.AzureManagement.Common;
    using ScriptCs.AzureManagement.Common.Credentials;

    public class ExpressRouteManagement
    {
        private readonly ILog _logger;
        private readonly ICredentialManager _credentialManager;

        public ExpressRouteManagement(ManagementContext managementContext)
        {
            _logger = managementContext.Logger;
            _credentialManager = managementContext.CredentialManager;
        }

        public ExpressRouteManagementClient CreateClient()
        {
            var credentials = _credentialManager.GetManagementCredentials();
            return new ExpressRouteManagementClient(credentials);
        }
    }
}
