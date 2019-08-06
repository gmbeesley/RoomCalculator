using Microsoft.AspNetCore.Mvc;
using RoomCalculator.Models;

namespace RoomCalculator.Controllers
{
    /// <summary>
    /// Calculate values for a regular quadrilateral shaped room
    /// </summary>
    public class SimpleRoomController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(SimpleRoom model)
        {
            model.FloorArea = model.Width * model.Length;
            model.Volume = model.Width * model.Length * model.Height;
            model.WallArea = (2 * (model.Width * model.Height)) + (2 * (model.Length * model.Height));
            model.PaintLitres = (model.WallArea / model.PaintCoverage) * model.PaintCoats;

            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}