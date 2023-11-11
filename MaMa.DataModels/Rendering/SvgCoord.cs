using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaMa.DataModels.Rendering
{
    public class SvgCoord
    {
        public int X { get; set; }
        public int Y { get; set; }

        public SvgCoord(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
