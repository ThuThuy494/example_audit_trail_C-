using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApp.Command.PersonDetail;
using WebApp.Domain;

namespace WebApp.Controllers
{
    public class PersonDetailController : ApiController
    {
        private readonly IPersonDetailService _personDetailService;
        public PersonDetailController(IPersonDetailService personDetailService)
        {
            _personDetailService = personDetailService;
        }

        // GET api/values
        /// <summary>
        /// this is example
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [Route("v1/createPersonDetail")]
        //[SwaggerOperation("CreateOPUAAPs")]
        //[SwaggerResponse(200, type: typeof(OPUAAPViewModel))]
        public async Task<IHttpActionResult> Create([FromBody] CreatePersonDetailCommand body)
        {
            if (body == null)
            {
                return BadRequest("Unable to parse body, please ensure the format is correct.");
            }

            var data = await _personDetailService.CreateAsync(body);

            return Ok(data);
        }

        [HttpPost]
        [Route("v1/updatePersonDetail")]
        //[SwaggerOperation("CreateOPUAAPs")]
        //[SwaggerResponse(200, type: typeof(OPUAAPViewModel))]
        public async Task<IHttpActionResult> Update([FromBody] UpdatePersonDetailCommand body)
        {
            if (body == null)
            {
                return BadRequest("Unable to parse body, please ensure the format is correct.");
            }

            var data = await _personDetailService.UpdateAsync(body);

            return Ok(data);
        }
    }
}
