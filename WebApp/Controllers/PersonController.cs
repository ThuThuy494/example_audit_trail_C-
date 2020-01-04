using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApp.Domain;
using WebApp.ViewModel;

namespace WebApp.Controllers
{
    public class PersonController : ApiController
    {
        private readonly IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost]
        [Route("v1/Create")]
        //[SwaggerOperation("CreateOPUAAPs")]
        //[SwaggerResponse(200, type: typeof(OPUAAPViewModel))]
        public async Task<IHttpActionResult> CreateOPUAAPs([FromBody] PersonModel body)
        {
            if (body == null)
            {
                return BadRequest("Unable to parse body, please ensure the format is correct.");
            }

            var oPUAAP = _personService.Create(body);

            return Ok(oPUAAP);
        }
    }
}
