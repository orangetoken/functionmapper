using System.Net;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info($"C# HTTP trigger function processed a request. RequestUri={req.RequestUri}");

    // parse query parameter
    string statusCode = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "statusCode", true) == 0)
        .Value;

    // Get request body
    dynamic data = await req.Content.ReadAsAsync<object>();

    // Set name to query string or body data
    statusCode = statusCode ?? data?.statusCode;
    
    bool value = false;
    
    if (statusCode == "1")
        value = true;
    
    return req.CreateResponse(HttpStatusCode.OK, value);
}