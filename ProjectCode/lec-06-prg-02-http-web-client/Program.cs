using System;
using System.Collections.Generic;
using System.Net.Http;

class MainProgram{
    public static void Main(string[] args)
    {
        HttpClient httpClient = new HttpClient();
        Console.WriteLine("## HTTP client started.");
        Console.WriteLine("## GET request for http://localhost:8080/temp/");
        var http_request1 = httpClient.GetAsync("http://localhost:8080/temp/").Result;
        Console.WriteLine("## GET response [start]");
        Console.WriteLine(http_request1.ToString());
        Console.WriteLine("## GET response [end]");

        Console.WriteLine("## GET request for http://localhost:8080/?var1=9&var2=9");
        var http_request2 = httpClient.GetAsync("http://localhost:8080/?var1=9&var2=9").Result;
        Console.WriteLine("## GET response [start]");
        Console.WriteLine(http_request2.ToString());
        Console.WriteLine("## GET response [end]");

        Console.WriteLine("## POST request for http://localhost:8080/ with var1 is 9 and var2 is 9");
        var values = new Dictionary<string, string>{ { "var1", "9" }, { "var2", "9" }};
        using var Httpcontent = new FormUrlEncodedContent(values);
        var http_request3 = httpClient.PostAsync("http://localhost:8080", Httpcontent).Result;
        Console.WriteLine("## POST response [start]");
        Console.WriteLine(http_request3.ToString());
        Console.WriteLine("## POST response [end]");

        Console.WriteLine("## HTTP client completed.");
    }
}
