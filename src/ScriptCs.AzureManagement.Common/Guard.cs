using System;
using System.Globalization;

namespace ScriptCs.AzureManagement.Common
{
  public static class Guard
  {
    public static void AgainstNull<T>(string parameterName, T argument) where T : class
    {
      if (argument == null)
      {
        throw new ArgumentNullException(parameterName, string.Format(CultureInfo.InvariantCulture, "'{0}' is null.", parameterName));
      }
    }

    public static void AgainstNullAndEmpty(string parameterName, string argument)
    {
      AgainstNull(parameterName, argument);

      if (argument.Equals(String.Empty))
      {
        throw new ArgumentException(parameterName, string.Format(CultureInfo.InvariantCulture, "'{0}' is empty.", parameterName));
      }
    }
  }
}
