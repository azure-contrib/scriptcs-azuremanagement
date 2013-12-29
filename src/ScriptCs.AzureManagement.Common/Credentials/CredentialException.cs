using System;

namespace ScriptCs.AzureManagement.Common.Credentials
{
  public class CredentialException : Exception
  {
    public CredentialException() { }
    public CredentialException(string message) : base(message) { }
    public CredentialException(string message, Exception innerException) : base(message, innerException) { }
  }
}
