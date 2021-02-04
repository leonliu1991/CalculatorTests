using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorApiTest.Actions;
using CalculatorApiTest.Model;
using System.Net;

namespace CalculatorApiTest.Tests
{
    [TestClass]
    [TestCategory("API")]
    public class CalculatorApiTests
    {
        private CalculateAPI calculateAPI;
        public static string baseUrl;
        public static string functionsKey;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            baseUrl = context.Properties["CalculateAPIBaseUrl"].ToString();
            functionsKey = context.Properties["FuntionsKey"].ToString();
        }

        [TestInitialize]
        public void Setup()
        {
            calculateAPI = new CalculateAPI(baseUrl, functionsKey);
        }

        [TestMethod]
        [DataRow("Verify the addition of 2 positive numbers", 123, "+", 456, 579)]
        [DataRow("Verify the addition of 2 negative numbers", -123, "+", -456, -579)]
        [DataRow("Verify the addition of 1 positive number and 1 negative number", 123, "+", -456, -333)]
        [DataRow("Verify the substraction of 2 positive numbers", 123, "-", 456, -333)]
        [DataRow("Verify the substraction of 2 negative numbers", -123, "-", -456, 333)]
        [DataRow("Verify the substraction of 1 positive number and 1 negative number", 123, "-", -456, 579)]
        [DataRow("Verify the multiplication of 2 positive numbers", 123, "*", 456, 56088)]
        [DataRow("Verify the multiplication of 2 negative numbers", -123, "*", -456, 56088)]
        [DataRow("Verify the multiplication of 1 positive number and 1 negative number", 123, "*", -456, -56088)]
        [DataRow("Verify the division of 2 positive numbers", 999, "/", 999, 1)]
        [DataRow("Verify the division of 2 negative numbers", -999, "/", -9, 111)]
        [DataRow("Verify the division of 1 positive number and 1 negative number", 999, "/", -1, -999)]
        [DataRow("Verify the division of zero by any number", 0, "/", 999, 0)]
        public void ApiTest_PositiveTests_FunctionalTests(string scenario, int leftNumber, string operatorUsed, int rightNumber, int expectedResult)
        {
            var postCalculateRequest = new PostCalculateRequest()
            {
                LeftNumber = leftNumber,
                RightNumber = rightNumber,
                Operator = operatorUsed
            };
            calculateAPI.SendAPostRequest(postCalculateRequest);

            //Verify if the schema matches
            calculateAPI.VerifyPostCalculateResponseSchema();

            //Verify if the status code is correct
            calculateAPI.VerifyStatusCode(System.Net.HttpStatusCode.OK);

            //Verify if returns correct value
            calculateAPI.VerifyPostCalculateResponse(expectedResult);
        }

        [TestMethod]
        [DataRow(int.MaxValue, 1, "+", int.MinValue)]
        [DataRow(int.MaxValue, int.MaxValue, "-", 0)]
        [DataRow(int.MaxValue, 3, "/", int.MaxValue / 3)]
        [DataRow(int.MaxValue / 3, 3, "*", int.MaxValue / 3 * 3)]
        [DataRow(int.MinValue, int.MaxValue, "+", int.MinValue + int.MaxValue)]
        public void ApiTest_PositiveTests_BoundaryTests(int leftNumber, int rightNumber, string operatorUsed, int expectedResult)
        {
            var postCalculateRequest = new PostCalculateRequest()
            {
                LeftNumber = leftNumber,
                RightNumber = rightNumber,
                Operator = operatorUsed
            };
            calculateAPI.SendAPostRequest(postCalculateRequest);

            //Verify if the schema matches
            calculateAPI.VerifyPostCalculateResponseSchema();

            //Verify if the status code is correct
            calculateAPI.VerifyStatusCode(System.Net.HttpStatusCode.OK);

            //Verify if returns correct value
            calculateAPI.VerifyPostCalculateResponse(expectedResult);
        }

        [TestMethod]
        [DataRow("Verify the division of a number by zero", 123, "/", 0, 400)]
        public void ApiTest_NegativeTests_BadRequest(string scenario, int leftNumber, string operatorUsed, int rightNumber, HttpStatusCode statusCode)
        {
            var postCalculateRequest = new PostCalculateRequest()
            {
                LeftNumber = leftNumber,
                RightNumber = rightNumber,
                Operator = operatorUsed
            };
            calculateAPI.SendAPostRequest(postCalculateRequest);

            //Verify if the status code is correct
            calculateAPI.VerifyStatusCode(statusCode);
        }

        [TestMethod]
        [DataRow("Verify the response when no authentication", 123, "/", 123, 401)]
        public void ApiTest_NegativeTests_Unauthorized(string scenario, int leftNumber, string operatorUsed, int rightNumber, HttpStatusCode statusCode)
        {
            var postCalculateRequest = new PostCalculateRequest()
            {
                LeftNumber = leftNumber,
                RightNumber = rightNumber,
                Operator = operatorUsed
            };
            calculateAPI.SendAPostRequestWithoutAuthentication(postCalculateRequest);

            //Verify if the status code is correct
            calculateAPI.VerifyStatusCode(statusCode);
        }

        [TestMethod]
        [DataRow( 123, null, 123, 404)]
        [DataRow(123, "123", 123, 404)]
        [DataRow(123, "", 123, 404)]
        public void ApiTest_NegativeTests_InvalidOperator(int leftNumber, string operatorUsed, int rightNumber, HttpStatusCode statusCode)
        {
            var postCalculateRequest = new PostCalculateRequest()
            {
                LeftNumber = leftNumber,
                RightNumber = rightNumber,
                Operator = operatorUsed
            };
            calculateAPI.SendAPostRequest(postCalculateRequest);

            //Verify if the status code is correct
            calculateAPI.VerifyStatusCode(statusCode);
        }
    }
}
