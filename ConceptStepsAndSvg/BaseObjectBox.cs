using MaMa.DataModels.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptStepsAndSvg
{
    public abstract class BaseObjectBox : ISvgRenderer
    {
        /// <summary>
        /// Base coordinates for caluclation, every calculation is based internally on 0,0
        /// </summary>
        protected SvgCoord BoxOrigin;
        protected SvgCoord CarryOverOffset;
        protected SvgCoord DigitOffset;
        protected SvgCoord CommaOffset;
        protected string CarryOverStyle = """ font-family="monospace" font-size="10px" """;
        protected string DigitStyle = """ font-family="monospace" font-size="24px" """;

        public BaseObjectBox(SvgCoord boxOrigin)
        { 
            this.BoxOrigin = boxOrigin;    
            this.CarryOverOffset = new SvgCoord(0, 12);
            this.DigitOffset = new SvgCoord(5, 24);
            this.CommaOffset = new SvgCoord(13, this.DigitOffset.Y);
        }

        public abstract string GetSVG();
    }
}
