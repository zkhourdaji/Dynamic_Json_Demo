using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JsonController : Controller
    {
        [HttpGet("[action]")]
        public IActionResult Static()
        {
            JObject zaferJObject = new JObject();

            zaferJObject.Add("FirstName", "Zafer");
            zaferJObject.Add("LastName", "Khourdaji");
            JArray hobbiesJArray = new JArray();

            string[] hobbiesStringArray = { "Programming", "Piano", "Guitar", "Russian" };

            hobbiesJArray.Add(hobbiesStringArray);
            //foreach (string hobbie in hobbiesStringArray)
            //{
            //    hobbiesJArray.Add(hobbie);
            //}
            zaferJObject.Add("Hobbies", hobbiesJArray);

            JArray objectsArray = new JArray();

            JObject object1 = new JObject();
            object1.Add("Key1", "Value1");
            object1.Add("Key2", "Value2");
            objectsArray.Add(object1);

            JObject object2 = new JObject();
            object2.Add("Key3", "Value3");
            objectsArray.Add(object2);

            JObject nestedObject = new JObject();
            nestedObject.Add("nestedKey1", "nestedValue1");
            nestedObject.Add("nestedKey2", "nestedValue2");
            object1.Add("nestedObject", nestedObject);

            JObject object3 = new JObject();
            object3.Add("nestedObject", nestedObject);

            zaferJObject.Add("ObjectsArray", objectsArray);

            return Json(zaferJObject);
        }

        [HttpGet("[action]")]
        public IActionResult Dynamic()
        {
            dynamic dynamicObject = new JObject() as dynamic;

            dynamicObject.FirstName = "Zafer";
            dynamicObject.LastName = "Khourdaji";
            dynamicObject.Hobbies = new JArray { "Programming", "Piano", "Guitar", "Russian" };

            dynamicObject.objectsArray = new JArray() {
                new JObject {
                    { "Key1", "Value1" },
                    { "Key2", "Value2" },
                    {
                        "nestedObject", new JObject{
                            { "nestedKey1","nestedKey1"},
                            { "nestedKey2","nestedKey2"}
                        }
                    }
                },
                new JObject {
                    { "Key3", "Value3" }
                }
            };

            return Json(dynamicObject);
        }
    }
}