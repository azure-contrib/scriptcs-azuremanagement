Azure Management Script Pack
============================================

# About #

This [Script Pack](https://github.com/scriptcs/scriptcs/wiki) for [scriptcs](http://scriptcs.net/) provides management of Windows Azure resources via the [Windows Azure Management Libraries](http://www.nuget.org/packages/Microsoft.WindowsAzure.Management.Libraries).

Provides the following:

- Infrastructure Management (Locations, Credentials, Subscriptions, Certificates)
- Compute Management (Hosted Services, Deployments, Virtual Machines, Virtual Machine Images & Disks)
- Monitoring Management (Alerts, Autoscale, Metrics)
- Scheduler Management (Jobs)
- Storage Management (Storage Accounts)
- WebSite Management (Web Sites, Web Site Publish Profiles, Usage Metrics, Repositories)
- Virtual Network Management (Networks, Gateways)

# Installation #

Install the nuget package by running

	scriptcs -install ScriptCs.AzureManagement -pre

# Usage #

Obtain a reference to the Script Pack.

    var waml = Require<AzureManagement>();

Obtain a reference to the various management classes.

	var infrastructureManagement = waml.InfrastructureManagement;
	var computeManagement = waml.ComputeManagement;
	var monitoringManagement = waml.MonitoringManagement;
	var schedulerManagement = waml.SchedulerManagement;
    var storageManagement = waml.StorageManagement;
    var websiteManagement = waml.WebSiteManagement;
	var virtualNetworkManagement = waml.VirtualNetworkManagement;

Create a client. This will wrap the Windows Azure Service Management REST APIs.

	var infrastructureManagementClient = infrastructureManagement.CreateClient();
	var computeManagementClient = computeManagement.CreateClient();
	var schedulerManagementClient = schedulerManagement.CreateClient();
	var storageManagementClient = storageManagement.CreateClient();
	var websiteManagementClient = websiteManagement.CreateClient();
	var virtualNetworkManagementClient = virtualNetworkManagement.CreateClient();

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
