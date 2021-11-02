using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ControleCelulasWebMvc.Models.ViewModels;
using ControleCelulasWebMvc.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleCelulasWebMvc.Controllers
{
    public class HomeController : Controller
    {        
        private readonly WebDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, WebDbContext context)
        {
            _logger = logger;  
            _context = context;          
        }

        public IActionResult Index()
        {  
            ViewData["CelulaId"] = new SelectList(_context.Celula, "Id", "Nome");
            ViewData["PessoaId"] = new SelectList(_context.Pessoa, "Id", "Nome");
            return View();
        }         

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
