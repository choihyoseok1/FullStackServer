using System;
using System.IO;
using System.Text.Json;

public class Program
{
    public static void Main(string[] args)
    {
        string jsonString = @"{
        ""squadName"": ""Super hero squad"",
        ""homeTown"": ""Metro City"",
        ""formed"": 2016,
        ""secretBase"": ""Super tower"",
        ""active"": true,
        ""members"": [
        {
        ""name"": ""Molecule Man"",
        ""age"": 29,
        ""secretIdentity"": ""Dan Jukes"",
        ""powers"": [
        ""Radiation resistance"",
        ""Turning tiny"",
        ""Radiation blast""
        ]
        },
        {
        ""name"": ""Madame Uppercut"",
        ""age"": 39,
        ""secretIdentity"": ""Jane Wilson"",
        ""powers"": [
        ""Million tonne punch"",
        ""Damage resistance"",
        ""Superhuman reflexes""
        ]
        },
        {
        ""name"": ""Eternal Flame"",
        ""age"": 1000000,
        ""secretIdentity"": ""Unknown"",
        ""powers"": [
        ""Immortality"",
        ""Heat Immunity"",
        ""Inferno"",
        ""Teleportation"",
        ""Interdimensional travel""
        ]
        }
        ]
        }";


        JsonDocument JsonDoc = JsonDocument.Parse(jsonString);
        
        Console.WriteLine(JsonDoc.RootElement.GetProperty("homeTown"));
        Console.WriteLine(JsonDoc.RootElement.GetProperty("active"));
        Console.WriteLine(JsonDoc.RootElement.GetProperty("members")[1].GetProperty("powers")[2]);

        string fileName = "lec-06-prg-04-json-example.json";
        var writerOptions = new JsonWriterOptions{Indented = true};
        FileStream fileStream = File.Create(fileName);
        Utf8JsonWriter writer = new Utf8JsonWriter(fileStream, writerOptions);
        JsonDoc.WriteTo(writer);
        writer.Flush();
        fileStream.Close();
    }
}

