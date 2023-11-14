using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaMa.DataModels.MultiplicationSteps
{
    public struct MuiltiplicationStepsSolution
    {
        /// <summary>
        /// each row of multiplication, comes in revers order first row is last in list
        /// </summary>
        public required List<RowMultiplication> Steps { get; set; }
        public required CalculationItem CalcItem { get; init; }

        public required int CommaMoveCount { get; init; }

        public decimal GetProduct()
        {
            return (decimal)Steps.Sum(s => s.RowValueWithStellenwert) / (decimal)Math.Pow(10f, CommaMoveCount);
        }
    }
}
