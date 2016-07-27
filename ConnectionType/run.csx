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
    
    int value = 0;
    
    switch(connectionType)
    {
        case "E":
            value = 1;
            break;
        case "G":
            value = 2;
            break;
        default:
            break;
    }
    
    return req.CreateResponse(HttpStatusCode.OK, value);
}