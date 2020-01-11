using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApp.Command.Category;
using WebApp.Domain;

namespace WebApp.Controllers
{
    public class CategoryEntityPlusController : ApiController
    {
        private readonly ICategoryService _categoryService;
        public CategoryEntityPlusController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [Route("v1/createCategory")]
        //[SwaggerOperation("CreateOPUAAPs")]
        //[SwaggerResponse(200, type: typeof(OPUAAPViewModel))]
        public async Task<IHttpActionResult> Create([FromBody] CreateCategoryCommand body)
        {
            if (body == null)
            {
                return BadRequest("Unable to parse body, please ensure the format is correct.");
            }

            var data = _categoryService.CreateAsync(body);

            return Ok(data);
        }

        [HttpPost]
        [Route("v1/updateCategory")]
        //[SwaggerOperation("CreateOPUAAPs")]
        //[SwaggerResponse(200, type: typeof(OPUAAPViewModel))]
        public async Task<IHttpActionResult> Update([FromBody] UpdateCategoryCommand body)
        {
            if (body == null)
            {
                return BadRequest("Unable to parse body, please ensure the format is correct.");
            }

            var data = _categoryService.UpdateAsync(body);

            return Ok(data);
        }
    }
}
