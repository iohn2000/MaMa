using MaMa.DataModels.MultiplicationSteps;
using MaMa.DataModels.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptStepsAndSvg
{
    public class SvgCharacterBox : BaseObjectBox
    {
        private readonly char TheChar;

        public SvgCharacterBox(SvgCoord boxOrigin, char theChar) : base(boxOrigin)
        {
            this.TheChar = theChar;
        }

        public override string GetSVG()
        {
            string svgChar = $"""<text x="{BoxOrigin.X + 5}" y="{BoxOrigin.Y + 24}" font-family="monospace" font-size="24px">{TheChar}</text>""";
            return svgChar + Environment.NewLine;
        }
    }
}
