using System.Net.Http.Json;
using System.Text.Json.Nodes;
class Program
{
    static string BaseUrl = "http://localhost:5060/membership_api";
    static HttpClient client = new HttpClient();
    static async Task Main(string[] args)
    {

        // #1 Reads a non registered member : error-case
        var response = await client.GetAsync($"{BaseUrl}/{"0001"}");
        await PrintResult("#1", response, "0001");

        // #2 Creates a new registered member : non-error case
        response = await client.PostAsJsonAsync($"{BaseUrl}/{"0001"}", "apple");
        await PrintResult("#2", response, "0001");

        // #3 Reads a registered member : non-error case
        response = await client.GetAsync($"{BaseUrl}/{"0001"}");
        await PrintResult("#3", response, "0001");

        // #4 Creates an already registered member : error case
        response = await client.PostAsJsonAsync($"{BaseUrl}/{"0001"}", "xpple");
        await PrintResult("#4", response, "0001");

        // #5 Updates a non registered member : error case
        response = await client.PutAsJsonAsync($"{BaseUrl}/{"0002"}", "xrange");
        await PrintResult("#5", response, "0002");

        // #6 Updates a registered member : non-error case
        response = await client.PostAsJsonAsync($"{BaseUrl}/{"0002"}", "xrange");
        response = await client.PutAsJsonAsync($"{BaseUrl}/{"0002"}", "orange");
        await PrintResult("#6", response, "0002");

        // #7 Delete a registered member : non-error case
        response = await client.DeleteAsync($"{BaseUrl}/{"0001"}");
        await PrintResult("#7", response, "0001");

        // #8 Delete a non registered member : non-error case
        response = await client.DeleteAsync($"{BaseUrl}/{"0001"}");
        await PrintResult("#8", response, "0001");
    }

    
    static async Task PrintResult(string stepName, HttpResponseMessage response, string key)
    {
        int statusCode = (int)response.StatusCode;
        
        string content = await response.Content.ReadAsStringAsync();

        string jsonResult = "None";
        var jsonNode = JsonNode.Parse(content);
        var resultNode = jsonNode?[key];
        if (resultNode != null)
        {
            jsonResult = resultNode.GetValue<string>() ?? "None";
        }
        Console.WriteLine($"{stepName} Code: {statusCode} >> JSON: {content} >> JSON Result: {jsonResult}");
    }
}