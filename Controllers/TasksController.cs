using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using A01.Report.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace A01.Report.Controllers
{
    public class TasksController : Controller
    {
        private readonly ResultsContext _context;

        public TasksController(ResultsContext context) => _context = context;

        [HttpGet]
        [Route("/t/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            return View(new RunTaskViewModel(await _context.Tasks.Where(t => t.Id == id).Include(t => t.Run).FirstAsync()));
        }
    }
}