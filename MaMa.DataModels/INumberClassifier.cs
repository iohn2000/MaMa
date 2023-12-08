namespace MaMa.DataModels
{
    /// <summary>
    /// class to calculate classifications and other properties of numbers
    /// </summary>
    public interface INumberClassifier
    {
        //EnumNumberClassification GetClassOfNumber(decimal theNumber);

        (bool isNonPeriodic, int commaCount) CalcPeriodicity(decimal dividend, decimal divisor);

        (int integerNr, int potenzenCount) MakeInteger(decimal divisor);

        /// <summary>
        /// check if <paramref name="theNumber"/> is in the specified range of <paramref name="theRange"/>
        /// </summary>
        /// <param name="theNumber">string in the format min-max; e.g. "2-5"</param>
        /// <param name="theRange"></param>
        /// <returns></returns>
        bool IsInRange(int theNumber, string theRange);
    }
}
