using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaMa.DataModels.MultiplicationSteps
{
    public record RowMultiplication
    {
        public int OrderRevers { get; set; }
        public int RowValueWithStellenwert { get; set; }
        public int RowValue { get; set; }
        public List<DigitMultiplication> Digits { get; set; }
    }
}
