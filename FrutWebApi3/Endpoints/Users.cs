using am.BL;
using FrutWebApi3.Database;
using System.Data;

namespace FrutWebApi3.Endpoints;

public class Users
{
    public static void map(WebApplication app)
    {
        app.MapGet("/", () => 220709);
        app.MapGet("/GetDeviceInfo/{uuid}", (string uuid) => FrutDb.GetDeviceInfo(uuid));
        app.MapGet("/SetDeviceInfo/{uuid};{lgn}", (string uuid, string lgn) => FrutDb.SetDeviceInfo(uuid, lgn));
    }
}
