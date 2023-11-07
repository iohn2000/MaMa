using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaMa.DataModels.MultiplicationSteps
{
    public struct MuiltiplicationStepsSolution
    {
        public List<RowMultiplication> Steps { get; set; }
        public int CommaMoveCount { get; set; }

        public decimal GetProduct()
        {
            return (decimal)Steps.Sum(s => s.RowValueWithStellenwert) / (decimal)Math.Pow(10f, CommaMoveCount);
        }
    }
}
