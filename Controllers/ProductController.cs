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
        [HttpGet]
        [Route(template:"")]
        public async Task<IActionResult> GetAsync(
            [FromServices]AppDbContext context){
            var Products = await context
            .Products
            .AsNoTracking()
            .ToListAsync();
            return Ok(Products);
        }

        [HttpGet]
        [Route(template:"{id}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices]AppDbContext context,
            [FromRoute] int id){
            var Product = await context
            .Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
            return Product == null ? NotFound() : Ok(Product);
        }

        [HttpGet]
        [Route(template:"name/{name}")]
        public async Task<IActionResult> GetByNameAsync(
            [FromServices]AppDbContext context,
            [FromRoute] string name){
            var Product = await context
            .Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name.Equals(name));
            return Product == null ? NotFound() : Ok(Product);
        }

        [HttpPost]
        [Route(template:"")]
        public async Task<IActionResult> PostAsync(
            [FromServices]AppDbContext context,
            [FromBody] Product Model){
            
            if(!ModelState.IsValid)
                return BadRequest();  

            try{
                await context.Products.AddAsync(Model);
                await context.SaveChangesAsync();
                return Created(uri:$"v1/products/{Model.Id}", Model);
            }
            catch (Exception){
                return BadRequest();
            }                
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id){
            var product = await context
            .Products
            .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
                return NotFound();
                
            try{
                context.Products.Remove(product);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception){
                return BadRequest();
            }
        }
    }
}