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

        public BaseObjectBox(SvgCoord boxOrigin)
        { 
            this.BoxOrigin = boxOrigin;    
        }

        public abstract string GetSVG();
    }
}
