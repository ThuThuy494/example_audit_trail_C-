using System.Threading.Tasks;
using System.Web.Http;
using WebApp.Command;
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
        [Route("v1/Create")]
        //[SwaggerOperation("CreateOPUAAPs")]
        //[SwaggerResponse(200, type: typeof(OPUAAPViewModel))]
        public async Task<IHttpActionResult> CreateOPUAAPs([FromBody] PersonCommand body)
        {
            if (body == null)
            {
                return BadRequest("Unable to parse body, please ensure the format is correct.");
            }

            var oPUAAP = _personService.CreateAsync(body);

            return Ok(oPUAAP);
        }
    }
}
