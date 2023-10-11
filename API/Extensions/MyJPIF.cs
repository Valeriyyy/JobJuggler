using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;

namespace JobJuggler.API.Extensions;

// What is the point of this extenstion? It is for enabling patch requests using newtonsoft.
// This is required to do patch requests because test.json apparently does not support json patching.
// This class enables newtonsoft to be used for patch requests while making all other requests and
// responses use the already existing system.text.json.
// Read up more from the docs: https://learn.microsoft.com/en-us/aspnet/core/web-api/jsonpatch?view=aspnetcore-7.0
public static class MyJPIF {
    public static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() {
        var builder = new ServiceCollection()
            .AddLogging()
            .AddMvc()
            .AddNewtonsoftJson()
            .Services.BuildServiceProvider();

        return builder
            .GetRequiredService<IOptions<MvcOptions>>()
            .Value
            .InputFormatters
            .OfType<NewtonsoftJsonPatchInputFormatter>()
            .First();
    }
}
