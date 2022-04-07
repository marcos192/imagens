using imagem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace imagem.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly imagem.Data.AppContext _appContext;

        public ProdutoController(imagem.Data.AppContext appContext)
        {
            _appContext = appContext;
        }

        //GET: Produtocontroller

        public async Task<ActionResult> Index()
        {
            var allProducts = await _appContext.Produtos.ToListAsync();
            return View(allProducts);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Produto produto, IList<IFormFile> Img)
        {
            //Verificar tamanho da imagem
            IFormFile uploadedImage = Img.FirstOrDefault();
            MemoryStream ms = new MemoryStream();

            if (Img.Count > 0)
            {
                uploadedImage.OpenReadStream().CopyTo(ms);
                produto.Foto = ms.ToArray();               
            }

            _appContext.Produtos.Add(produto);
            await _appContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _appContext.Produtos.FindAsync(id);

            if (produto == null)
            {
                return BadRequest();
            }
            return View (produto);

        }
    }
}
