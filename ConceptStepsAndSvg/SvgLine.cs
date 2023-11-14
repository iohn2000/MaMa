using MaMa.DataModels.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptStepsAndSvg 
{
    public class SvgLine : ISvgRenderer
    {
        private readonly SvgCoord svgCoord1;
        private readonly SvgCoord svgCoord2;

        public SvgLine(SvgCoord svgCoord1, SvgCoord svgCoord2)
        {
            this.svgCoord1 = svgCoord1;
            this.svgCoord2 = svgCoord2;
        }

        public string GetSVG()
        {
            return $"""<line x1="{svgCoord1.X}" y1="{svgCoord1.Y}" x2="{svgCoord2.X}" y2="{svgCoord2.Y}" style="stroke:rgb(0, 0, 0);stroke-width:1" />""";
        }
    }
}
