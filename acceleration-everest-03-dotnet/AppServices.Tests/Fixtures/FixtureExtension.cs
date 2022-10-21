using Newtonsoft.Json;

namespace AppServices.Tests.Fixtures;

public static class FixtureExtension
{
    public static string DumpString(this object obj)
    {
        return JsonConvert.SerializeObject(obj, Formatting.Indented);
    }
}