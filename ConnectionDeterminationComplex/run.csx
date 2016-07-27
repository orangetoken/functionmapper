using System.Net;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info($"C# HTTP trigger function processed a request. RequestUri={req.RequestUri}");

    // parse query parameter
    string connectionDeterminationComplex = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "connectionDeterminationComplex", true) == 0)
        .Value;

    // Get request body
    dynamic data = await req.Content.ReadAsAsync<object>();

    // Set name to query string or body data
    connectionDeterminationComplex = connectionDeterminationComplex ?? data?.connectionDeterminationComplex;
    
    int value = 0;
    
    switch(connectionDeterminationComplex)
    {
        case "J":
            value = 1;
            break;
        case "N":
            value = 2;
            break;
        case "nvt":
            value = 3;
            break;            
        default:
            value = 3;
            break;
    }
    
    return req.CreateResponse(HttpStatusCode.OK, value);
}