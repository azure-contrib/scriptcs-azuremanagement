Azure Management Script Pack
============================================

# About #

This [Script Pack](https://github.com/scriptcs/scriptcs/wiki) for [scriptcs](http://scriptcs.net/) provides management of Windows Azure resources via the [Windows Azure Management Libraries](http://www.nuget.org/packages/Microsoft.WindowsAzure.Management.Libraries).

Provides the following:

- Compute Management (Deployments, Hosted/Cloud Services, Operating Systems, Service Certificates, Virtual Machines, Virtual Machine Images & Disks)
- Infrastructure Management (Affinity Groups, Locations, Management Certificates, Subscriptions)
- Media Management (Accounts)
- Monitoring Management (Alerts - Incidents, Rules)
- Monitoring Management (Autoscale - Settings)
- Monitoring Management (Metrics - Definitions, Settings, Values)
- Scheduler Management (Jobs, Job Collections)
- Service Bus Management (Namespaces, Notification Hubs, Queues, Relays, Topics)
- Sql Management (Dac, Database Operations, Databases, Firewall Rule, Servers)
- Storage Management (Storage Accounts)
- Virtual Network Management (Client Root Certificates, Gateways, Networks, Reserved IPs)
- WebSite Management (Server Farms, Web Sites, Web Spaces)

# Installation #

Install the nuget package by running

	scriptcs -install ScriptCs.AzureManagement -pre

# Usage #

Obtain a reference to the Script Pack.

```csharp
    var waml = Require<AzureManagement>()
		.LoadConfigFromJsonFile("WAML.config.json")
		.Initialise();
```

The `WAML.config.json` file contains Subscription and Credential configuration information. See the [Configuration](#configuration) section for more detailed information.

When the file is referenced by just the file name it is expected to be found in the same folder as your script. You may also specify a fully qualified path to the file. You may name the configuration file anything - it does not have to be called `WAML.config.json`.

Obtain a reference to the various management classes.

```csharp
	var computeManagement = waml.ComputeManagement;
	var infrastructureManagement = waml.InfrastructureManagement;
	var mediaManagement = waml.MediaManagement;
	var monitoringManagement = waml.MonitoringManagement;
	var schedulerManagement = waml.SchedulerManagement;
	var serviceBusManagement = waml.ServiceBusManagement;
	var sqlManagement = waml.SqlManagement;
    var storageManagement = waml.StorageManagement;
	var virtualNetworkManagement = waml.VirtualNetworkManagement;
    var websiteManagement = waml.WebSiteManagement;
```

Create a client. This will wrap the Windows Azure Service Management REST APIs.

```csharp
	var computeManagementClient = computeManagement.CreateClient();
	var infrastructureManagementClient = infrastructureManagement.CreateClient();
	var mediaManagementClient = mediaManagement.CreateClient();
	var schedulerManagementClient = schedulerManagement.CreateClient();
	var serviceBusManagementClient = serviceBusManagement.CreateClient();
	var sqlManagementClient = sqlManagement.CreateClient();
	var storageManagementClient = storageManagement.CreateClient();
	var virtualNetworkManagementClient = virtualNetworkManagement.CreateClient();
	var websiteManagementClient = websiteManagement.CreateClient();
```

The creation of a Monitoring Management client is slightly different.

```csharp
	var alertsManagementClient = monitoringManagement.Alerts.CreateClient();
	var autoscaleManagementClient = monitoringManagement.Autoscale.CreateClient();
	var metricsManagementClient = monitoringManagement.Metrics.CreateClient();
```

The following is an example for interacting with Storage Accounts via the Storage Management class. If the name **mystorageaccount** is available, the storage account is created. A list of all storage accounts in the subscription is then produced.

```csharp
    using (var client = storageManagement.CreateClient())
    {
		var checkNameResponse = client.StorageAccounts.CheckNameAvailability("mystorageaccount");
        if (checkNameResponse.IsAvailable)
        {
          var createResult = client.StorageAccounts.Create(new StorageAccountCreateParameters
          {
            ServiceName = "mystorageaccount",
            Location = LocationNames.SoutheastAsia
          });
          Console.WriteLine("{0}: {1}", createResult.RequestId, createResult.StatusCode);

          var listResponse = client.StorageAccounts.List();
          Console.WriteLine(String.Join("\n", listResponse.StorageServices.Select(x => x.ServiceName)));
        }
    }
```

Both synchronous and asynchronous versions of the methods are available. Since async/await is not yet available via scriptcs Roslyn CTP engine, you can use the Task direcly.

```csharp
using (var client = storageManagement.CreateClient())
{
    var task = client.StorageAccounts.CheckNameAvailabilityAsync("mystorageaccount");
    task.ContinueWith(t => { Console.WriteLine(String.Format("{0}: {1}", t.Result.IsAvailable, t.Result.Reason)); }).Wait();
}
```

You can also use the mono engine which supports async/await! Running the following will work if you run with `--modules "mono"`. Notice in order to work with async, you must create an async method which returns a Task and then invoke Wait() on the return value.

```csharp
async Task CreateAsync() {
  var waml = Require<AzureManagement>()
      .LoadConfigFromJsonFile("WAML.config.json")
      .Initialise();
 
  var storageManagement = waml.StorageManagement;
 
  using (var client = storageManagement.CreateClient())
  {
      var checkNameResponse = await client.StorageAccounts.CheckNameAvailabilityAsync("storageaccount1");
      if (checkNameResponse.IsAvailable)
      {
        var createResult = await client.StorageAccounts.CreateAsync(new StorageAccountCreateParameters
        {
          ServiceName = "storageaccount1",
          Location = LocationNames.WestUS
        });
        Console.WriteLine("{0}: {1}", createResult.RequestId, createResult.StatusCode);
 
        var listResponse = await client.StorageAccounts.ListAsync();
        Console.WriteLine(String.Join("\n", listResponse.StorageServices.Select(x => x.ServiceName)));
      }
  }
}
 
CreateAsync().Wait();
```

# Configuration #

To configure the Subscription and Credential information, create a config file with the following json:
```javascript

	{
		Subscriptions : [
			{
				Name : "MSDN Premium",
				SubscriptionId : "4dbddf3c-82ab-44b1-851a-75cab776d13d",
				ManagementCertificate:
				{
					Thumbprint : "A4F7C2B685534A9EB7A4E24615E22E395F5F839A",
					Base64Data : "MIIKBAIBAzCCCcQGCSqGSIb3DQEHA..."
				}
			},
			{
				Name : "Personal",
				SubscriptionId : "4dbddf3c-82ab-44b1-851a-75cab776d13e",
				ManagementCertificate:
				{
					Thumbprint : "A4F7C2B685534A9EB7A4E24615E22E395F5F839B",
					Base64Data : "MIIKBAIBAzCCCcQGCSqGSIb3DQEHB..."
				}
			},
		]
	}
```

One or more Subscriptons can be configured via the config file. You can also specify either a Thumbprint for the Management Certificate associated with the Subscription or the Base64 encoded certificate data. If you specify both, the Thumbprint has a higher priority. 

# Subscription and Credential Selection

If no Subscription is specified when the Script Pack is required, then the first Subscription in the configuration will be used. So in the following scenario, the `MSDN Premium` Subscription from the config in the [Configuration](#configuration) section will be used.

```csharp
    var waml = Require<AzureManagement>()
		.LoadConfigFromJsonFile("WAML.config.json")
		.Initialise();
```

If a Subscription is specified when the Script Pack is required, then that Subscription in the configuration will be used. So in the following scenario, the `Personal` Subscription from the config in the [Configuration](#configuration) section will be used.

```csharp
    var waml = Require<AzureManagement>()
		.LoadConfigFromJsonFile("WAML.config.json")
		.SetActiveSubscription("Personal")
		.Initialise();
```

At any time in your script (`.csx`) you can change the active Subscription by using the `ActiveSubscription` property on the Script Pack reference. The following will change the active Subscription to `Personal`:

	waml.ActiveSubscription("Personal");

# Tracing #

If you would like to see the REST API HTTP traffic for debugging or interest, then add a `-HttpTraceEnabled` switch when calling your script.

```bash
    scriptcs waml.csx -- -HttpTraceEnabled
```

Ensure that you add the `--` before the `-HttpTraceEnabled` switch. This is a marker to separate scriptcs (`scriptcs.exe`) arguments from script (`.csx`) arguments.

# Sample #

A example of how to use the Script Pack is available in the ``sample`` folder. Remember to configure the json config file with your Azure Subscription details.
