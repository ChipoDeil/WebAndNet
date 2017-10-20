using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

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
            //get data
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
            return listOfPeople.Find(people => (people.Id).Equals(id));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class People {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; } 
        public string Sex { get; set; }
        public bool Costil = false;
    }

    
}

