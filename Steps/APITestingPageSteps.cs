using MicroappPlatformQaAutomation.Core.Commons;
using MicroappPlatformQaAutomation.Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Diagnostics;
using TechTalk.SpecFlow;
using Assert = NUnit.Framework.Assert;

namespace MicroappPlatformQaAutomation
{
    [Binding]
    public class APITestingPageSteps
    {
        private readonly static string yamlAPITestDataFilePath = "Resources/TestData/APITestingTestData.yaml";
        private RestClient client;
        private RestRequest request;
        private RestResponse response;
        private readonly BeautifyJson _beautifyJson;
        public Stopwatch Stopwatch = new Stopwatch();

        public APITestingPageSteps(BeautifyJson beautifyJson)
        {
            _beautifyJson = beautifyJson;
        }
        [Given(@"the user has search keyword")]
        public void GivenTheUserHasSearchKeyword()
        {
            throw new PendingStepException();
        }

        [When(@"the user sends GET request to ""([^""]*)""")]
        public void WhenTheUserSendsGETRequestTo(string endpoint)
        {
            Stopwatch.Start();
            endpoint = YamlReader.FetchYamlData<APITestingContentDTO>(yamlAPITestDataFilePath).GetEndpoint;
            client = new RestClient(YamlReader.FetchYamlData<APITestingContentDTO>(yamlAPITestDataFilePath).PostmanSampleClientURL);
            request = new RestRequest(endpoint, Method.Get);

            response = client.Execute(request);
            Console.Write(response);
            //IDENTIFYING RESPONSE TIME
            Stopwatch.Stop();
            long respTime = Stopwatch.ElapsedMilliseconds;
            Console.WriteLine("RESPONSE TIME" + respTime);
        }

        [When(@"the user post request ""([^""]*)""")]
        public void WhenTheUserPostRequest(string endpoint)
        {
            endpoint = YamlReader.FetchYamlData<APITestingContentDTO>(yamlAPITestDataFilePath).PostEndpoint;
            client = new RestClient(YamlReader.FetchYamlData<APITestingContentDTO>(yamlAPITestDataFilePath).PostmanSampleClientURL);
            request = new RestRequest(endpoint, Method.Post);
            request.AddBody(new { name = "Sona", age = "24", company = "Hexaware" });
            response = client.Execute(request);
        }

        [Then(@"capture the error if it fails")]
        public void ThenCaptureTheErrorIfItFails()
        {
            if (((int)response.StatusCode) != 200)
            {
                //Error message 
                Console.WriteLine(response.ErrorMessage);
                //Stacktrace
                Console.WriteLine(response.ErrorException);
            }
        }

        [When(@"the user send request for authorize ""([^""]*)""")]
        public void WhenTheUserSendRequestForAuthorize(string endpoint)
        {
            string username = YamlReader.FetchYamlData<APITestingContentDTO>(yamlAPITestDataFilePath).UserName;
            string password = YamlReader.FetchYamlData<APITestingContentDTO>(yamlAPITestDataFilePath).Password;
            client = new RestClient(YamlReader.FetchYamlData<APITestingContentDTO>(yamlAPITestDataFilePath).ReqresSampleClientURL);
            request = new RestRequest(YamlReader.FetchYamlData<APITestingContentDTO>(yamlAPITestDataFilePath).LoginEndpoint, Method.Post);
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            response = client.Execute(request);
            var resourceRequest = new RestRequest("protected/resource", Method.Get);
            var token = response.Content;
            request.AddHeader("Authorization", "Bearer" + token);
            RestResponse resourceResponse = client.Execute(resourceRequest);
            if (resourceResponse.IsSuccessful)
            {
                Console.WriteLine(resourceResponse.Content);
            }
            else
            {
                Console.WriteLine("Failed to access protected resource" + resourceResponse.ErrorMessage);
            }
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int statusCode)
        {
            Console.WriteLine(response.Content + "Response Content");
            Console.WriteLine(response + "Response");
            Assert.AreEqual(statusCode, (int)response.StatusCode);
        }

        [Then(@"verify the response for user authorization")]
        public void ThenVerifyTheResponseForUserAuthorization()
        {
            Assert.IsNotNull(response);
        }


        [Then(@"verify the response for user delete")]
        public void ThenVerifyTheResponseForUserDelete()
        {
            Console.WriteLine(response.Content + "Response Content");
            Assert.IsEmpty(response.Content);

        }


        [When(@"the user send request for update ""([^""]*)""")]
        public void WhenTheUserSendRequestForUpdate(string endpoint)
        {
            endpoint = YamlReader.FetchYamlData<APITestingContentDTO>(yamlAPITestDataFilePath).UpdateEndpoint;
            client = new RestClient(YamlReader.FetchYamlData<APITestingContentDTO>(yamlAPITestDataFilePath).ReqresSampleClientURL);
            request = new RestRequest(endpoint, Method.Put);
            request.AddBody(new { name = "Jegan", job = "Dev Lead" });
            Console.WriteLine("Before Beautification");
            response = client.Execute(request);
            Console.WriteLine(response.Content);
            Console.WriteLine("After Beautification");
            string beautifiedResponse = _beautifyJson.BeautifyResponse(response);
            Console.WriteLine(beautifiedResponse);


        }

        [When(@"the user send request for delete ""([^""]*)""")]
        public void WhenTheUserSendRequestForDelete(string endpoint)
        {
            endpoint = YamlReader.FetchYamlData<APITestingContentDTO>(yamlAPITestDataFilePath).DeleteEndpoint;
            client = new RestClient(YamlReader.FetchYamlData<APITestingContentDTO>(yamlAPITestDataFilePath).ReqresSampleClientURL);
            request = new RestRequest(endpoint, Method.Delete);
            response = client.Execute(request);
            Console.WriteLine(response.Content);
        }

        [Then(@"verify whether response contains expected text for user update")]
        public void ThenVerifyWhetherResponseContainsExpectedTextForUserUpdate()
        {
            Assert.IsTrue(response.Content.Contains("Jegan"));
            Assert.IsTrue(response.Content.Contains("Dev Lead"));
            Console.WriteLine((int)response.StatusCode);
        }


        [When(@"the user post request for creation ""([^""]*)""")]
        public void WhenTheUserPostRequestForCreation(string endpoint)
        {
            endpoint = YamlReader.FetchYamlData<APITestingContentDTO>(yamlAPITestDataFilePath).CreateEndpoint;
            client = new RestClient(YamlReader.FetchYamlData<APITestingContentDTO>(yamlAPITestDataFilePath).ReqresSampleClientURL);
            request = new RestRequest(endpoint, Method.Post);
            request.AddBody(new { name = "Agus", job = "UX Lead" });
            response = client.Execute(request);
            string beautifiedResponse = _beautifyJson.BeautifyResponse(response);
            Console.WriteLine(beautifiedResponse + "After Beautification");
        }


        [Then(@"verify whether response contains expected text")]
        public void ThenVerifyWhetherResponseContainsExpectedText()
        {
            //Content Not Null Assertion
            Assert.IsNotNull(response.Content);
            //Content Assertion
            Assert.IsTrue(response.Content.Contains("Sona"));
            //Content Type Assertion
            Assert.AreEqual("application/json", response.ContentType);
        }

        [Then(@"verify whether response contains expected text for user creation")]
        public void ThenVerifyWhetherResponseContainsExpectedTextForUserCreation()
        {
            Assert.IsTrue(response.Content.Contains("Agus"));
            Assert.IsTrue(response.Content.Contains("UX Lead"));
            Console.WriteLine((int)response.StatusCode);
        }

        [Given(@"I have the API endpoint")]
        public void GivenIHaveTheAPIEndpoint()
        {
            client = new RestClient(YamlReader.FetchYamlData<APITestingContentDTO>(yamlAPITestDataFilePath).ReqresSampleClientURL);
            request = new RestRequest(YamlReader.FetchYamlData<APITestingContentDTO>(yamlAPITestDataFilePath).SerDeserEndpoint, Method.Get);
            response = client.Execute(request);
        }

        [Then(@"serilaizing the JSON response")]
        public void ThenSerilaizingTheJSONResponse()
        {
            var apidatas = new APIDTO
            {
                data = new APIDTO.Data
                {

                    id = 2,
                    name = "fuchsia rose",
                    year = 2001,
                    color = "#C74375",
                    pantone_value = "17-2031",
                },
                support = new APIDTO.Support
                {

                    url = "https://reqres.in/#support-heading",
                    text = "To keep ReqRes free, contributions towards server costs are appreciated!"
                }
            };
            var dataser = JsonConvert.SerializeObject(apidatas, Formatting.Indented);
            Console.WriteLine("serialized data" + dataser);
        }

        [Then(@"Deserializing the JSON response and asserting it")]
        public void ThenDeserializingTheJSONResponseAndAssertingIt()
        {
            string json = YamlReader.FetchYamlData<APITestingContentDTO>(yamlAPITestDataFilePath).JSONResponse;
            var desData = JsonConvert.DeserializeObject<APIDTO>(json);
            Assert.AreEqual(YamlReader.FetchYamlData<APITestingContentDTO>(yamlAPITestDataFilePath).NameValidation, desData.data.name);
            Assert.AreEqual(YamlReader.FetchYamlData<APITestingContentDTO>(yamlAPITestDataFilePath).URLValidation, desData.support.url);
            Console.WriteLine(desData.data.name);
            Console.WriteLine(desData.support.text);
        }
    }
}
