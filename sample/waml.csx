var waml = Require<AzureManagement>();

var storageManagement = waml.StorageManagement;
using (var storageClient = storageManagement.CreateClient())
{
    var checkNameResponse = storageClient.StorageAccounts.CheckNameAvailability("mystorageaccount");
    if (checkNameResponse.IsAvailable)
    {
      var createResult = storageClient.StorageAccounts.Create(new StorageAccountCreateParameters
      {
        ServiceName = "mystorageaccount",
        Location = LocationNames.SoutheastAsia
      });
      Console.WriteLine("{0}: {1}", createResult.RequestId, createResult.StatusCode);

      var listResponse = storageClient.StorageAccounts.List();
      Console.WriteLine(String.Join("\n", listResponse.StorageServices.Select(x => x.ServiceName)));
    }
}