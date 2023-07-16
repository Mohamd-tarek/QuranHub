
namespace QuranHub.Web.Extensions;

    public static class SessionExtensions {

    public static void SetSession(this ISession session, string key, object value)
    {
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        var jsonData = JsonSerializer.Serialize(value, options);

        session.SetString(key, jsonData);
    }
}

