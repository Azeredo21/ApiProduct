using System;
using System.Threading.Tasks;
using Api_Teste0002.Data;
using Api_Teste0002.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Teste0002.Controllers
{
    [ApiController]
    [Route(template:"v1/categories")]
    public class CategoryController : ControllerBase{

        [HttpGet(template:"")]
        public async Task<IActionResult> GetAsync([FromServices]AppDbContext context){
             var Categories = await context
            .Categories
            .AsNoTracking()
            .ToListAsync();
            return Ok(Categories);
        }
     
        [HttpGet(template:"{id}")]
        public async Task<IActionResult> GetByNameAsync([FromServices]AppDbContext context, [FromRoute] int id){
            var Category = await context
            .Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

            return Category == null ? NotFound("Categoria não encontrada") : Ok(Category);
        }

        [HttpGet(template:"{name}")]
        public async Task<IActionResult> GetByNameAsync([FromServices]AppDbContext context, [FromRoute] string name){
            var Category = await context
            .Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name.Equals(name));

            return Category == null ? NotFound("Categoria não encontrada") : Ok(Category);
        }

        [HttpPost(template:"")]
        public async Task<IActionResult> PostAsync([FromServices]AppDbContext context, [FromBody] Category Model){
  
            var resp = await context
                .Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name.Equals(Model.Name));

            if(resp != null)
                return BadRequest("Categoria já cadastrada");

            try{
                await context.Categories.AddAsync(Model);
                await context.SaveChangesAsync();
                return Ok("Categoria cadastrada com sucesso");
            }catch (Exception){
                return BadRequest("Exceção não tratata");
            }                
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromServices] AppDbContext context, [FromRoute] int id){
            var category = await context
            .Categories
            .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound("Categoria não encontrada");
                
            try{
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return Ok("Categoria removida com sucesso");
            }catch (Exception){
                return BadRequest("Exceção não tratata");
            }
        }

    }
}