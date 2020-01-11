using System.Threading.Tasks;
using System.Web.Http;
using WebApp.Command;
using WebApp.Command.Person;
using WebApp.Domain;

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
        [Route("v1/createPerson")]
        //[SwaggerOperation("CreateOPUAAPs")]
        //[SwaggerResponse(200, type: typeof(OPUAAPViewModel))]
        public async Task<IHttpActionResult> Create([FromBody] CreatePersonCommand body)
        {
            if (body == null)
            {
                return BadRequest("Unable to parse body, please ensure the format is correct.");
            }

            var data = _personService.CreateAsync(body);

            return Ok(data);
        }

        [HttpPost]
        [Route("v1/updatePerson")]
        //[SwaggerOperation("CreateOPUAAPs")]
        //[SwaggerResponse(200, type: typeof(OPUAAPViewModel))]
        public async Task<IHttpActionResult> Update([FromBody] UpdatePersonCommand body)
        {
            if (body == null)
            {
                return BadRequest("Unable to parse body, please ensure the format is correct.");
            }

            var data = _personService.UpdateAsync(body);

            return Ok(data);
        }
    }
}
