using System.Diagnostics;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CalculatorApiTest.Common;
using CalculatorApiTest.Model;
using RestSharp;

namespace CalculatorApiTest.Actions
{
    public class CalculateAPI : HttpRequestWrapper
    {
        private string resource = "/calculate";
        private string url;
        private string key;
        private const string SchemaPathOfPostCalculateResponse = @"JsonSchema\PostCalculateResponseJsonSchema.json";
        private IRestResponse restResponse;
        JsonSchemaComparer schemaComparer = new JsonSchemaComparer();

        public CalculateAPI(string baseUrl, string functionsKey)
        {
            url = baseUrl;
            key = functionsKey;
            System.Console.WriteLine($"The base url is '{url}'");
            Debug.WriteLine($"The base url is '{url}'");
        }

        public void SendAPostRequest(PostCalculateRequest request)
        {
            ClearRequest();
            SetMethod(Method.POST);
            SetResourse(resource);
            AddAHeader("x-functions-key", key);
            AddJsonContent(request);
            restResponse = Execute(url);
        }

        public void SendAPostRequestWithoutAuthentication(PostCalculateRequest request)
        {
            ClearRequest();
            SetMethod(Method.POST);
            SetResourse(resource);
            AddJsonContent(request);
            restResponse = Execute(url);
        }

        public void VerifyStatusCode(HttpStatusCode statusCode)
        {
            Assert.AreEqual(statusCode, restResponse.StatusCode,
                $"{restResponse.Request.Method} : {restResponse.ResponseUri} => {restResponse.Content}");
        }

        public void VerifyPostCalculateResponse(int value)
        {
            PostCalculateResponse result = JsonConvert.DeserializeObject<PostCalculateResponse>(restResponse.Content);

            //Check if it returns the expected value
            Assert.IsNotNull(result, "Empty data is returned in the response.");
            Assert.AreEqual(value, result.Value);
        }

        public void VerifyPostCalculateResponseSchema()
        {
            //Check if the schema matches
            schemaComparer.AssertObjectSchema(JObject.Parse(restResponse.Content), SchemaPathOfPostCalculateResponse);
        }
    }
}
