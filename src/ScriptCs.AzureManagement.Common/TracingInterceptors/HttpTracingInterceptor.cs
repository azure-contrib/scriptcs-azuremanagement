using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Common.Logging;
using Hyak.Common;

namespace ScriptCs.AzureManagement.Common.TracingInterceptors
{

    public class HttpTracingInterceptor : ICloudTracingInterceptor
  {
    private readonly ILog _logger;

    public HttpTracingInterceptor(ILog logger, bool isEnabled)
    {
      _logger = logger;
      IsEnabled = isEnabled;
    }

    public bool IsEnabled { get; set; }

    #region ICloudTracingInterceptor Methods

    public void Information(string message) { }
    public void Configuration(string source, string name, string value) {}
    public void Enter(string invocationId, object instance, string method, IDictionary<string, object> parameters) {}
    public void Error(string invocationId, Exception ex) {}
    public void Exit(string invocationId, object returnValue) {}

    public void SendRequest(string invocationId, HttpRequestMessage request)
    {
      if (!IsEnabled) return;
      _logger.Info(BuildRequestLogMessage(invocationId, request));
    }

    public void ReceiveResponse(string invocationId, HttpResponseMessage response)
    {
      if (!IsEnabled) return;
      _logger.Info(BuildResponseLogMessage(invocationId, response));

    }

    #endregion ICloudTracingInterceptor Methods

    private string BuildRequestLogMessage(string invocationId, HttpRequestMessage request)
    {
      var stringBuilder = new StringBuilder();

      stringBuilder.AppendFormat("[{0}] - REST API Request ", invocationId).AppendLine();
      stringBuilder.AppendLine(BuildSeparator()).AppendLine();
      stringBuilder.AppendFormat("  {0} {1}", request.Method, request.RequestUri).AppendLine().AppendLine();
      stringBuilder.AppendLine(BuildTitle("Headers")).AppendLine();
      stringBuilder.Append("  ").Append(request.Headers.ToString().Replace("\n", "\n  ").TrimEnd()).AppendLine().AppendLine();
      if (request.Content != null)
      {
        stringBuilder.AppendLine(BuildTitle("Body")).AppendLine();
        stringBuilder.Append("  ").AppendLine(request.Content.ReadAsStringAsync().Result).AppendLine();
      }
      stringBuilder.AppendLine(BuildSeparator());

      return stringBuilder.ToString();
    }

    private string BuildResponseLogMessage(string invocationId, HttpResponseMessage response)
    {
      var stringBuilder = new StringBuilder();

      stringBuilder.AppendFormat("[{0}] - REST API Response ", invocationId).AppendLine();
      stringBuilder.AppendLine(BuildSeparator()).AppendLine();
      stringBuilder.AppendLine(BuildTitle("Headers")).AppendLine();
      stringBuilder.Append("  ").Append(response.Headers.ToString().Replace("\n", "\n  ").TrimEnd()).AppendLine().AppendLine();
      if (response.Content != null)
      {
        stringBuilder.AppendLine(BuildTitle("Body")).AppendLine();
        stringBuilder.Append("  ").AppendLine(response.Content.ReadAsStringAsync().Result).AppendLine();
      }
      stringBuilder.AppendLine(BuildTitle("Status Code")).AppendLine();
      stringBuilder.Append("  ").AppendLine(response.StatusCode.ToString()).AppendLine();
      stringBuilder.AppendLine(BuildSeparator());

      return stringBuilder.ToString();
    }

    private string BuildSeparator()
    {
      return @"============================================================================";
    }

    private string BuildTitle(string title)
    {
      return String.Format("  __{0}", title.PadRight(72, '_'));
    }

  }
}
