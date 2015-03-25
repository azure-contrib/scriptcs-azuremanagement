var waml = Require<AzureManagement>()
  .LoadConfigFromJsonFile("WAML.config.json")
  .SetActiveSubscription("SUBSCRIPTION1_NAME")
  .Initialise();

var storageAccountName = "mystorageaccount";

var storageManagement = waml.StorageManagement;
using (var storageClient = storageManagement.CreateClient())
{
  var checkNameResponse = storageClient.StorageAccounts.CheckNameAvailability(storageAccountName);
  if (checkNameResponse.IsAvailable)
  {
    var createResult = storageClient.StorageAccounts.Create(new StorageAccountCreateParameters
    {
      Name = storageAccountName,
      Location = GeoRegionNames.SoutheastAsia,
      AccountType = StorageAccountTypes.StandardLRS
    });
    Console.WriteLine("{0}: {1}", createResult.RequestId, createResult.StatusCode);

    var listResponse = storageClient.StorageAccounts.List();
    Console.WriteLine(String.Join("\n", listResponse.StorageAccounts.Select(x => x.Name)));
  }
}
