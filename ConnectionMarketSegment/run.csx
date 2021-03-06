using System.Net;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info($"C# HTTP trigger function processed a request. RequestUri={req.RequestUri}");

    // parse query parameter
    string connectionMarketSegment = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "connectionMarketSegment", true) == 0)
        .Value;

    // Get request body
    dynamic data = await req.Content.ReadAsAsync<object>();

    // Set name to query string or body data
    connectionMarketSegment = connectionMarketSegment ?? data?.connectionMarketSegment;
    
    int value = 0;
    
    switch(connectionMarketSegment)
    {
        case "KVB":
            value = 1;
            break;
        case "ART":
            value = 2;
            break;
        case "GVB":
            value = 3;
            break;            
        default:
            break;
    }
    
    return req.CreateResponse(HttpStatusCode.OK, value);
}