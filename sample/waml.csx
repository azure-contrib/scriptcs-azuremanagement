var waml = Require<AzureManagement>();

var storageManagement = waml.StorageManagement;
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