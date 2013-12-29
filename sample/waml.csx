var waml = Require<AzureManagement>();
var storageAccountName = "mystorageaccount";

var storageManagement = waml.StorageManagement;
using (var storageClient = storageManagement.CreateClient())
{
  var checkNameResponse = storageClient.StorageAccounts.CheckNameAvailability(storageAccountName);
  if (checkNameResponse.IsAvailable)
  {
    var createResult = storageClient.StorageAccounts.Create(new StorageAccountCreateParameters
    {
      ServiceName   = storageAccountName,
      Location      = LocationNames.SoutheastAsia
    });
    Console.WriteLine("{0}: {1}", createResult.RequestId, createResult.StatusCode);

    var listResponse = storageClient.StorageAccounts.List();
    Console.WriteLine(String.Join("\n", listResponse.StorageServices.Select(x => x.ServiceName)));
  }
}