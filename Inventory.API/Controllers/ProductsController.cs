using Inventory.BusinessLogic;
using Inventory.DTOs.Model;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> Get()
        {
            var Products = await clsProducts.GetAllProducts();
            return Ok(Products);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetById(int id)
        {
            var Product = await clsProducts.Find(id);
            if (Product == null)
            {
                return NotFound($"Product with {id} not found.");
            }
            return Ok(Product);
        }


        [HttpPost]
        public async Task<ActionResult<ProductModel>> Add(ProductModel product)
        {
            clsProducts newProduct = new clsProducts();
            newProduct.ProductName = product.ProductName;
            newProduct.Quantity = product.Quantity;
            newProduct.Price = product.Price;
            newProduct.MinStockLevel = product.MinStockLevel;
            newProduct.CategoryID = product.CategoryID;

            if (!await newProduct.Save())
            {
                return BadRequest("Failed to add new product.");
            }
            else
            {
                product.ProductID = newProduct.ProductID;
                return CreatedAtAction(nameof(GetById), new { id = product.ProductID }, product);
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ProductModel product)
        {
            if (id != product.ProductID)
            {
                return BadRequest("Product ID mismatch.");
            }

            clsProducts clsProducts = new clsProducts();
            clsProducts.ProductID = product.ProductID;
            clsProducts.ProductName = product.ProductName;
            clsProducts.Quantity = product.Quantity;
            clsProducts.Price = product.Price;
            clsProducts.MinStockLevel = product.MinStockLevel;
            clsProducts.CategoryID = product.CategoryID;
            clsProducts.Mode=clsProducts.enMode.update;

            if (!await clsProducts.Save())
            {
                return BadRequest("Failed to update product.");
            }
            else
            {
                return NoContent();
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await clsProducts.Delete(id))
            {
                return NoContent();
            }
            else
            {
                return BadRequest($"Failed to delete product with id {id}.");
            }
        }
    }
}