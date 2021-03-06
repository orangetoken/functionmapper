using System.Net;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info($"C# HTTP trigger function processed a request. RequestUri={req.RequestUri}");

    // parse query parameter
    string connectionType = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "connectionType", true) == 0)
        .Value;

    // Get request body
    dynamic data = await req.Content.ReadAsAsync<object>();

    // Set name to query string or body data
    connectionType = connectionType ?? data?.connectionType;
    
    string value = "";
    
    switch(connectionType)
    {
        case "1":
            value = "ELECTRICITY";
            break;
        case "2":
            value = "GAS";
            break;
        default:
            break;
    }
    
    return req.CreateResponse(HttpStatusCode.OK, value);
}
