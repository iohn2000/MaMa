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
            string svgCarryOver = $"""<text x="{BoxOrigin.X+ base.CarryOverOffset.X}" y="{BoxOrigin.Y+base.CarryOverOffset.Y}" {CarryOverStyle}>{TheDigit.CarryOver}</text>""";
            string svgDigit = $"""<text x="{BoxOrigin.X+base.DigitOffset.X}" y="{BoxOrigin.Y+base.DigitOffset.Y}" {DigitStyle}>{TheDigit.DigitValue}</text>""";
            return svgCarryOver + Environment.NewLine + svgDigit;
        }
    }


}
