using Microsoft.AspNetCore.Mvc;
using RoomCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoomCalculator.Controllers
{
    /// <summary>
    /// Calculate floor area, total wall area, volume and  for an irregularly shaped room
    /// </summary>
    public class ComplexRoomController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var model = new ComplexRoom();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(ComplexRoom model)
        {
            model.WallArea = CalculateWallArea(model.Walls, model.Height);
            model.PaintLitres = (model.WallArea / model.PaintCoverage) * model.PaintCoats;

            if (model.Walls.Count >= 3 && CalculateVertices(model.Walls))
            {
                model.FloorArea = CalculateFloorArea(model.Walls);
                model.Volume = model.FloorArea * model.Height;
            }
            else
            {
                ModelState.AddModelError("CustomError", "Wall lengths and corner angles entered do not form an enclosed space!");
            }

            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }

        /// <summary>
        /// Calculate total area of all the walls
        /// </summary>
        /// <param name="walls"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private double CalculateWallArea(List<Wall> walls, double height)
        {
            double area;
            area = walls.Sum(x => x.Length) * height;
            return area;
        }

        /// <summary>
        /// Calculate floor area of the room from the calculated corner points
        /// </summary>
        /// <param name="walls"></param>
        /// <returns></returns>
        private double CalculateFloorArea(List<Wall> walls)
        {
            double area = 0.0;
            int j = walls.Count - 1;

            for (int i = 0; i < walls.Count; i++)
            {
                area += ((walls[j].CornerX + walls[i].CornerX) * (walls[j].CornerY - walls[i].CornerY));
                j = i;
            }

            return Math.Abs(area/2.0); // Make sure value returned is positive.
        }

        /// <summary>
        /// Calculate the xy coordinates of each corner of the room
        /// </summary>
        /// <param name="walls"></param>
        private bool CalculateVertices(List<Wall> walls)
        {
            double currentAngle = 0.0;
            double currentX = 0.0;
            double currentY = 0.0;

            for (int i = 0; i < walls.Count; i++)
            {
                currentAngle += 180.0 - walls[i].CornerAngle;

                double radians = Math.PI * currentAngle / 180.0;
                double distX = walls[i].Length * Math.Cos(radians);
                double distY = walls[i].Length * Math.Sin(radians);

                walls[i].CornerX = Math.Round(currentX + distX, 2);
                walls[i].CornerY = Math.Round(currentY + distY, 2);

                currentX = walls[i].CornerX;
                currentY = walls[i].CornerY;
            }

            return ValidateShape(walls);
        }

        /// <summary>
        /// Validate that the collection of walls forms an enclosed shape
        /// </summary>
        /// <param name="walls"></param>
        /// <param name="totalAngle"></param>
        /// <returns></returns>
        private bool ValidateShape(List<Wall> walls)
        {
            double expectedAngles = (walls.Count - 2) * 180.0;
            double sumAngles = walls.Sum(x => x.CornerAngle);
            bool valid = (walls.LastOrDefault().CornerX == 0 && walls.LastOrDefault().CornerY == 0 && sumAngles == expectedAngles);
            return valid;
        }
    }
}