using MaMa.DataModels.Rendering;

namespace ConceptStepsAndSvg
{
    public class SvgCharacterBox : BaseObjectBox
    {
        private readonly char TheChar;

        public SvgCharacterBox(SvgCoord boxOrigin, char theChar, bool needsComma = false) : base(boxOrigin)
        {
            this.TheChar = theChar;
        }

        public override string GetSVG()
        {
            string svgChar = string.Empty;
            if (TheChar == ',' || TheChar == '.')
            { 
                svgChar = $"""  <text x="{BoxOrigin.X + base.CommaOffset.X}" y="{BoxOrigin.Y + base.CommaOffset.Y}" {base.DigitStyle}>{TheChar}</text>""";
                
            }
            else
            {
                svgChar = $"""<text x="{BoxOrigin.X + base.DigitOffset.X}" y="{BoxOrigin.Y + base.DigitOffset.Y}" {base.DigitStyle}>{TheChar}</text>""";
            }
            
            return svgChar + Environment.NewLine;
        }
    }
}
