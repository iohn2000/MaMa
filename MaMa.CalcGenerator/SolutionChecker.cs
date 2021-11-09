using MaMa.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaMa.CalcGenerator
{
    public class SolutionChecker : INumberClassifier
    {
        public EnumNumberClassification GetClassOfNumber(decimal theNumber)
        {
            if ((int)theNumber == theNumber)
            {
                return EnumNumberClassification.Integer;
            }
            else
            {
                return EnumNumberClassification.RationalNonPeriodic;
            }
            throw new Exception($"cannot calculate class of number: {theNumber}");
        }
    }
}
