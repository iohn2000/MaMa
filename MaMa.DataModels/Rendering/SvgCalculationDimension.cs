using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaMa.DataModels.Rendering
{
    /// <summary>
    /// how big is the finished rendered calculation in width and height
    /// </summary>
    public class SvgCalculationDimension
    {
        public int WidthPixel { get; set; }
        public int HeightPixel { get; set; }
        public int NumberOfRows { get;set; }
        public int NumberOfColumns { get;set;}
    }
}
