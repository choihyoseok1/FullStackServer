using System;
using System.IO;
using System.Text.Json;

public class Program
{
    public static void Main(string[] args)
    {
        string jsonString_Source = @"{
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

        JsonDocument JsonDoc = JsonDocument.Parse(jsonString_Source);
        var JsonOptions = new JsonSerializerOptions{WriteIndented = true};
        string jsonString = JsonSerializer.Serialize(JsonDoc.RootElement, JsonOptions);
        Console.WriteLine(jsonString);
    }
}