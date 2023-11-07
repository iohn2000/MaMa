using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaMa.DataModels.MultiplicationSteps
{
    public record DigitMultiplication
    {
        public int Order { get; set; }
        public int DigitValue { get; set; }
        public int CarryOver { get; set; }
    }
}
