using _18_webapi.DataContext;
using _18_webapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _18_webapi.Controllers
{
    [Route("api/[controller]/[action]")] //API endpoint routing : api/Product/ActionName
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _context;

        public ProductController(ProductContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (_context.Products.Count() >0)
            {
                var totalCount = await _context.Products.CountAsync();
                var products = await _context.Products
                    .Skip((page - 1) * pageSize) //sayfa numarasına göre atlanması gereken ürün sayısı
                    .Take(pageSize) //belirtilen sayıda ürünü al
                    .ToListAsync(); //sonuçları liste olarak al

                Response.Headers.Add("X-Total-Count", totalCount.ToString()); //toplam ürün sayısını başlıklara ekle
                Response.Headers.Add("X-Page-Number", page.ToString()); //mevcut sayfa numarasını başlıklara ekle
                Response.Headers.Add("X-Page-Size", pageSize.ToString()); //sayfa boyutunu başlıklara ekle
                //Header a eklenen bu bilgiler, istemcilerin sayfalama bilgilerini anlamalarına yardımcı olur

                return products;
            }
            return NotFound("Ürün Bulunamadı");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Ürün Bulunamadı");
            }
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product); //201 Created yanıtı döner ve yeni oluşturulan kaynağın URI'sini içerir

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ürün eklenirken hata oluştur {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("Ürün ID'si uyuşmuyor");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(product).State = EntityState.Modified; //ürünün durumunu 'Modified' olarak işaretle
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(e => e.Id == id))
                {
                    return NotFound("Ürün Bulunamadı");
                }
                else
                {
                    return StatusCode(500, "Veritabanı güncellenirken eşzamanlılık hatası oluştu");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ürün güncellenirken hata oluştu: {ex.Message}");
            }
            return NoContent(); //Başarılı ama içerik yok
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Ürün Bulunamadı");
            }
            try
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return NoContent(); //Başarılı ama içerik yok
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ürün silinirken hata oluştu: {ex.Message}");
            }
        }  
    }

}
