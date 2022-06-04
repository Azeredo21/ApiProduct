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
        [HttpGet]
        [Route(template:"")]
        public async Task<IActionResult> GetAsync(
            [FromServices]AppDbContext context){
            var Categories = await context
            .Categories
            .AsNoTracking()
            .ToListAsync();
            return Ok(Categories);
        }
     
        [HttpGet]
        [Route(template:"{name}")]
        public async Task<IActionResult> GetByNameAsync(
            [FromServices]AppDbContext context,
            [FromRoute] string name){
            var Category = await context
            .Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name.Equals(name));
            return Category == null ? NotFound() : Ok(Category);
        }

        [HttpPost(template:"")]
        public async Task<IActionResult> PostAsync(
            [FromServices]AppDbContext context,
            [FromBody] Category Model){
            
            if(!ModelState.IsValid)
                return BadRequest();  

            var resp = await context
                .Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name.Equals(Model.Name));

            if(resp != null)
                return BadRequest();

            try{
                await context.Categories.AddAsync(Model);
                await context.SaveChangesAsync();
                return Created(uri:$"v1/categories/{Model.Id}", Model);
            }catch (Exception){
                return BadRequest();
            }                
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id){
            var category = await context
            .Categories
            .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound();
                
            try{
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return Ok();
            }catch (Exception){
                return BadRequest();
            }
        }

    }
}