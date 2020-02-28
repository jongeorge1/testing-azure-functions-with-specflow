// <copyright file="SampleFunction.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace DemoFunction
{
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    /// <summary>
    /// Sample functions for the demo to invoke.
    /// </summary>
    public class SampleFunction
    {
        /// <summary>
        /// Sample endpoint that returns a basic Hello World string with a value drawn from either the
        /// querystring or the request body.
        /// </summary>
        /// <param name="req">The incoming request.</param>
        /// <param name="log">The logger.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [FunctionName("SampleFunction-Get")]
        public async Task<IActionResult> SampleGetHandler(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "{*path}")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync().ConfigureAwait(false);
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name ??= data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
