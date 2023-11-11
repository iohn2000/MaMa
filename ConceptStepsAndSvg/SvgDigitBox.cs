using MaMa.DataModels.MultiplicationSteps;
using MaMa.DataModels.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptStepsAndSvg
{
    public class SvgDigitBox
    {
        /// <summary>
        /// Base coordinates for caluclation, every calculation is based internally on 0,0
        /// </summary>
        private SvgCoord DigitBoxOrigin { get; }
        private DigitMultiplication TheDigit { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="digitBoxOrigin">Base coordinates for caluclation</param>
        public SvgDigitBox(SvgCoord digitBoxOrigin, DigitMultiplication theDigit)
        {
            this.DigitBoxOrigin = digitBoxOrigin;
            this.TheDigit = theDigit;
        }

        public string GetSVG()
        { 
            string svgCarryOver = $"""<text x="{DigitBoxOrigin.X+0}" y="{DigitBoxOrigin.Y+12}" font-family="monospace" font-size="10px">{TheDigit.CarryOver}</text>""";
            string svgDigit = $"""<text x="{DigitBoxOrigin.X+5}" y="{DigitBoxOrigin.Y+24}" font-family="monospace" font-size="24px">{TheDigit.DigitValue}</text>""";
            return svgCarryOver + Environment.NewLine + svgDigit;
        }
    }


}
