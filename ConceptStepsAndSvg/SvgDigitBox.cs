using MaMa.DataModels.MultiplicationSteps;
using MaMa.DataModels.Rendering;

namespace ConceptStepsAndSvg
{
    public class SvgDigitBox : BaseObjectBox
    {
        private DigitMultiplication TheDigit { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="digitBoxOrigin">Base coordinates for caluclation in pixel</param>
        /// <param name="theDigit">the Digit</param>
        public SvgDigitBox(SvgCoord digitBoxOrigin, DigitMultiplication theDigit) : base(digitBoxOrigin)
        {
            this.TheDigit = theDigit;
        }

        override public string GetSVG()
        { 
            string svgCarryOver = $"""<text x="{BoxOrigin.X+0}" y="{BoxOrigin.Y+12}" font-family="monospace" font-size="10px">{TheDigit.CarryOver}</text>""";
            string svgDigit = $"""<text x="{BoxOrigin.X+5}" y="{BoxOrigin.Y+24}" font-family="monospace" font-size="24px">{TheDigit.DigitValue}</text>""";
            return svgCarryOver + Environment.NewLine + svgDigit;
        }
    }


}
