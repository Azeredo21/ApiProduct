using System;
using System.Threading.Tasks;
using Api_Teste0002.Data;
using Api_Teste0002.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Teste0002.Controllers
{
    [ApiController]
    [Route(template:"v1/products")]
    public class ProductController : ControllerBase{

        [HttpGet(template:"")]
        public async Task<IActionResult> GetAsync([FromServices]AppDbContext context){
            var Products = await context
            .Products
            .AsNoTracking()
            .Include(x => x.Category)
            .ToListAsync();

            return Ok(Products);
        }

        [HttpGet(template:"{id}")]
        public async Task<IActionResult> GetByIdAsync([FromServices]AppDbContext context, [FromRoute] int id){
            var Product = await context
            .Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

            return Product == null ? NotFound("Produto não encontrado") : Ok(Product);
        }

        [HttpGet(template:"name/{name}")]
        public async Task<IActionResult> GetByNameAsync([FromServices]AppDbContext context, [FromRoute] string name){
            var Product = await context
            .Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name.Equals(name));

            return Product == null ? NotFound("Produto não encontrado") : Ok(Product);
        }

        [HttpPost(template:"")]
        public async Task<IActionResult> PostAsync([FromServices]AppDbContext context, [FromBody] Product Model){
            
            try{
                await context.Products.AddAsync(Model);
                await context.SaveChangesAsync();
                return Ok("Produto cadastrado com sucesso");
            }catch (Exception){
                return BadRequest("Exceção não tratata");
            }                
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromServices] AppDbContext context, [FromRoute] int id){
            var product = await context
            .Products
            .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
                return NotFound("Produto não encontrado");
                
            try{
                context.Products.Remove(product);
                await context.SaveChangesAsync();

                return Ok("Produto removido com sucesso");
            }catch (Exception){
                return BadRequest("Exceção não tratata");
            }
        }
    }
}