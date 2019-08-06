using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RoomCalculator.Models
{
    public class Wall
    {
        public double Length { get; set; }

        public double CornerAngle { get; set; }

        public double CornerX { get; set; }
        
        public double CornerY { get; set; }
    }

    public class ComplexRoom
    {
        public ComplexRoom()
        {
            Walls = new List<Wall>();
        }

        [Required]
        public List<Wall> Walls { get; set; }

        [Required]
        [Display(Name = "Room Height", Description = "Height of the room in metres")]
        public double Height { get; set; }

        [Required]
        [Display(Name = "Paint Coverage", Description = "Area that can be covered by a litre of paint")]
        public double PaintCoverage { get; set; }

        [Required]
        [Display(Name = "Paint Coats Required", Description = "Number of coats of paint required")]
        public int PaintCoats { get; set; }

        [Display(Name = "Floor Area", Description = "Total floor area of the room in square metres")]
        public double FloorArea { get; set; }

        [Display(Name = "Room Volume", Description = "Total volume of the room in cubic metres")]
        public double Volume { get; set; }

        [Display(Name = "Total Wall Area", Description = "Total area of all the walls of the room in square metres")]
        public double WallArea { get; set; }

        [Display(Name = "Paint Required", Description = "Total amount of paint required in litres")]
        public double PaintLitres { get; set; }
    }
}