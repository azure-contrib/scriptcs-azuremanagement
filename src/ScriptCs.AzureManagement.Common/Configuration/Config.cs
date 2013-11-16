using System.Collections.Generic;

namespace ScriptCs.AzureManagement.Common.Configuration
{
  public class Config
  {
    public bool HttpTraceEnabled { get; set; }
    public IList<Subscription> Subscriptions { get; set; }

    public class Subscription
    {
      public string Name { get; set; }
      public string SubscriptionId { get; set; }
      public ManagementCertificate ManagementCertificate { get; set; }
    }

    public class ManagementCertificate
    {
      public string Thumbprint { get; set; }
      public string Base64Data { get; set; }
    }
  }
}
