using MaMa.DataModels.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptStepsAndSvg
{
    public class SvgRect : ISvgRenderer
    {
        private readonly SvgCoord svgCoord1;
        private readonly SvgCoord svgCoord2;

        public SvgRect(SvgCoord svgCoord1, SvgCoord svgCoord2)
        {
            this.svgCoord1 = svgCoord1;
            this.svgCoord2 = svgCoord2;
        }

        public string GetSVG()
        {
            return $"""<rect x="{svgCoord1.X}" y="{svgCoord1.Y}" width="{svgCoord2.X}" height="{svgCoord2.Y}" style="fill:none;stroke:black;stroke-width:1;stroke-opacity:0.75" />""";
        }
    }
}
