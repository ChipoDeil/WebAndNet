using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Newtonsoft.Json;

namespace FirstHttpServer.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public List<People> listOfPeople;
        public ValuesController() {
            listOfPeople = new List<People>();
            StreamReader io = new StreamReader("wwwroot/data.json");
            string json = io.ReadToEnd();
            listOfPeople = JsonConvert.DeserializeObject<List<People>>(json);
        }

        // GET api/values
        [HttpGet]
        public string Get()
        {
            return JsonConvert.SerializeObject(listOfPeople);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public People Get(string id)
        {
            People currentMan = listOfPeople.Find(people => (people.Id).Equals(id));
            currentMan.ShowAge = true;
            return currentMan;
        }
    }

    public class People {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; } 
        public string Sex { get; set; }
        [JsonIgnore]
        public bool ShowAge = false;
        public bool ShouldSerializeAge() {
            return ShowAge;
        }
    }
}

