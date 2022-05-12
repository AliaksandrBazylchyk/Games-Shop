using GamesShop.Catalog.API.Models;
using GamesShop.Catalog.BLL.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;

namespace GamesShop.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CatalogController(
            ICategoryService categoryService
        )
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] string categoryId)
        {
            var response = await _categoryService.GetByIdAsync(categoryId);

            return Ok(response);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _categoryService.GetAllAsync();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateCategoryModel model)
        {
            var response = await _categoryService.CreateAsync(model.Name);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync()
        {
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> PatchAsync()
        {
            return Ok();
        }
    }
}