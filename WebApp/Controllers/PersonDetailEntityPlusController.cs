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
    public class PersonDetailEntityPlusController : ApiController
    {
        private readonly IPersonDetailPlusService _personDetailService;

        public PersonDetailEntityPlusController(IPersonDetailPlusService personDetailService)
        {
            _personDetailService = personDetailService;
        }

        [HttpPost]
        [Route("v1/createPersonDetailPlus")]
        //[SwaggerOperation("CreateOPUAAPs")]
        //[SwaggerResponse(200, type: typeof(OPUAAPViewModel))]
        public async Task<IHttpActionResult> Create([FromBody] CreatePersonPlusDetailCommand body)
        {
            if (body == null)
            {
                return BadRequest("Unable to parse body, please ensure the format is correct.");
            }

            var data = await _personDetailService.CreateAsync(body);

            return Ok(data);
        }

        [HttpPost]
        [Route("v1/updatePersonDetailPLus")]
        //[SwaggerOperation("CreateOPUAAPs")]
        //[SwaggerResponse(200, type: typeof(OPUAAPViewModel))]
        public async Task<IHttpActionResult> Update([FromBody] UpdatePersonPlusDetailCommand body)
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
