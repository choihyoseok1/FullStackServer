using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<MembershipHandler>();
builder.WebHost.UseUrls("http://localhost:5060");
var app = builder.Build();



app.MapPost("/membership_api/{member_id}", (string member_id, [FromBody] string value, MembershipHandler handler) =>
{
    return handler.Create(member_id, value);
});

app.MapGet("/membership_api/{member_id}", (string member_id, MembershipHandler handler) =>
{
    return handler.Read(member_id);
});

app.MapPut("/membership_api/{member_id}", (string member_id, [FromBody] string value, MembershipHandler handler) =>
{
    return handler.Update(member_id, value);
});

app.MapDelete("/membership_api/{member_id}", (string member_id, MembershipHandler handler) =>
{
    return handler.Delete(member_id);
});

app.Run();

class MembershipHandler
{
    private Dictionary<string, string> database = new();

    public object Create(string id, string value)
    {
        if (database.ContainsKey(id))
        {
            return new Dictionary<string, string> { { id, "None" } };
        }
        else
        {
            database[id] = value;
            return new Dictionary<string, string> { { id, database[id] } };
        }
    }

    public object Read(string id)
    {
        if (database.ContainsKey(id))
        {
            return new Dictionary<string, string> { { id, database[id] } };
        }
        else
        {
            return new Dictionary<string, string> { { id, "None" } };
        }
    }

    public object Update(string id, string value)
    {
        if (database.ContainsKey(id))
        {
            database[id] = value;
            return new Dictionary<string, string> { { id, database[id] } };
        }
        else
        {
            return new Dictionary<string, string> { { id, "None" } };
        }
    }

    public object Delete(string id)
    {
        if (database.ContainsKey(id))
        {
            database.Remove(id);
            return new Dictionary<string, string> { { id, "Removed" } };
        }
        else
        {
            return new Dictionary<string, string> { { id, "None" } };
        }
    }
}