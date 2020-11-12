using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTaskCompletionSource2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async  Task<ActionResult<IEnumerable<string>>> Get()
        {
            var res = new WebClient();
           var t = await  res.DownloadStringTask2Async(new Uri("http://professorweb.ru"));
            return new string[] { "value1", "value2" };
        }
      

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
    public static class extention
    {
        public static Task<string> DownloadStringTask2Async(this WebClient client, Uri address)
        {
            var tcs = new TaskCompletionSource<string>();
            DownloadStringCompletedEventHandler handler = null;
            handler = (a, e) =>
            {
                client.DownloadStringCompleted -= handler;
                tcs.SetResult(e.Result);
            };
            client.DownloadStringCompleted += handler;
            client.DownloadStringAsync(address);
            return tcs.Task;
        }
    }
}
