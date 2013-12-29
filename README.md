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

    var waml = Require<AzureManagement>();

Obtain a reference to the various management classes.

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

Create a client. This will wrap the Windows Azure Service Management REST APIs.

	var computeManagementClient = computeManagement.CreateClient();
	var infrastructureManagementClient = infrastructureManagement.CreateClient();
	var mediaManagementClient = mediaManagement.CreateClient();
	var schedulerManagementClient = schedulerManagement.CreateClient();
	var serviceBusManagementClient = serviceBusManagement.CreateClient();
	var sqlManagementClient = sqlManagement.CreateClient();
	var storageManagementClient = storageManagement.CreateClient();
	var virtualNetworkManagementClient = virtualNetworkManagement.CreateClient();
	var websiteManagementClient = websiteManagement.CreateClient();

The creation of a Monitoring Management client is slightly different.

	var alertsManagementClient = monitoringManagement.Alerts.CreateClient();
	var autoscaleManagementClient = monitoringManagement.Autoscale.CreateClient();
	var metricsManagementClient = monitoringManagement.Metrics.CreateClient();

The following is an example for interacting with Storage Accounts via the Storage Management class. If the name **mystorageaccount** is available, the storage account is created. A list of all storage accounts in the subscription is then produced.

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

          var listResponse = storageClient.StorageAccounts.List();
          Console.WriteLine(String.Join("\n", listResponse.StorageServices.Select(x => x.ServiceName)));
        }
    }

Both synchronous and asynchronous versions of the methods are available. Since async/await is not yet available via Roslyn (and scriptcs) you will have to use the Task based methods to work with the asynchronous methods.

    using (var client = storageManagement.CreateClient())
    {
    	var response1 = client.StorageAccounts.CheckNameAvailabilityAsync("mystorageaccount").Result;
    	Console.WriteLine(String.Format("{0}: {1}", response1.IsAvailable, response1.Reason));

    	var response2 = client.StorageAccounts.CheckNameAvailability("mystorageaccount");
    	Console.WriteLine(String.Format("{0}: {1}", response2.IsAvailable, response2.Reason));

    	var task = client.StorageAccounts.CheckNameAvailabilityAsync("mystorageaccount");
    	task.ContinueWith(t => { Console.WriteLine(String.Format("{0}: {1}", t.Result.IsAvailable, t.Result.Reason)); }).Wait();
    }

# Configuration #

To configure the credentials and subscription information, place a WAML.config.json in the same folder as your script that contains the following:

	{
		Subscriptions : [
			{
				Name : "Azure Subscription - MSDN",
				SubscriptionId : "4dbddf3c-82ab-44b1-851a-75cab776d13d",
				ManagementCertificate:
				{
					Thumbprint : "A4F7C2B685534A9EB7A4E24615E22E395F5F839A"
				}
			}
		]
	}

At the moment a single subscription and the thumprint to the management certificate in your local certificate store are required. But more options are on the way ...

# Tracing #

If you would like to see the REST API HTTP traffic for debugging or interest, then add a `-HttpTraceEnabled` switch when calling your script.

    scriptcs waml.csx -- -HttpTraceEnabled
