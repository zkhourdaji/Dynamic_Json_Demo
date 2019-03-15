using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DynamicJsonDemo.Controllers
{
    public class JsonController : Controller
    {

        private readonly HttpClient _httpClient;
        
        public JsonController(IHttpClientFactory iHttpClientFactory)
        {
            _httpClient = iHttpClientFactory.CreateClient();
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dynamic()
        {
            HttpResponseMessage response = _httpClient.GetAsync("https://localhost:44396/api/json/dynamic").Result;
            HttpContent content = response.Content;
            string jsonResultString = content.ReadAsStringAsync().Result;

            dynamic dynamicData = JObject.Parse(jsonResultString) as dynamic;

            string firstName = dynamicData.FirstName;
            string lastName = dynamicData.LastName;
            string[] Hobbies = dynamicData.Hobbies.ToObject<string[]>();
            var programming = Hobbies[0];

            var objectsArray = dynamicData.ObjectsArray;
            var value1 = objectsArray[0].Key1.Value;
            var value2 = objectsArray[0].Key2.Value;
            var value3 = objectsArray[1].Key3.Value;

            var nestedObject = objectsArray[0].nestedObject;
            var nestedValue1 = nestedObject.nestedKey1.Value;
            var nestedValue2 = nestedObject.nestedKey2.Value;

            return Ok();

        }

        public IActionResult Static()
        {
            HttpResponseMessage response = _httpClient.GetAsync("https://localhost:44396/api/json/static").Result;
            HttpContent content = response.Content;
            string jsonResultString = content.ReadAsStringAsync().Result;

            JObject staticData = JObject.Parse(jsonResultString);

            string firstName = (string) staticData.GetValue("FirstName");
            string lastName = staticData.GetValue("LastName").ToString();
            string[] hobbies = staticData.GetValue("Hobbies").ToObject<string[]>();
            string programming = hobbies[0];
            string piano = hobbies[1];
            string guitar = hobbies[2];
            string russian = hobbies[3];

            //JArray objectsArray = (JArray)staticData.GetValue("ObjectsArray");
            JToken objectsArray = staticData.GetValue("ObjectsArray");
            string value1 = (string)objectsArray.SelectToken("[0].Key1");
            string value2 = (string)objectsArray.SelectToken("[0].Key2");
            string value3 = (string)objectsArray.SelectToken("[1].Key3");

            JObject nestedObject = (JObject) objectsArray.SelectToken("[0].nestedObject");
            string nestedValue1 = nestedObject.GetValue("nestedKey1").ToString();
            string nestedValue2 = nestedObject.GetValue("nestedKey2").ToString();

            return Ok();
        }
    }
}