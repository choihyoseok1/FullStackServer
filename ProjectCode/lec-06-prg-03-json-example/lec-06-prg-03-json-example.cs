using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        string fileName = "lec-06-prg-03-json-example.json";

        string jsonString = File.ReadAllText(fileName);

        JsonDocument JsonDoc = JsonDocument.Parse(jsonString);

        Console.WriteLine(JsonDoc.RootElement.GetProperty("homeTown"));
        Console.WriteLine(JsonDoc.RootElement.GetProperty("active"));
        Console.WriteLine(JsonDoc.RootElement.GetProperty("members")[1].GetProperty("powers")[2]);
    }
}