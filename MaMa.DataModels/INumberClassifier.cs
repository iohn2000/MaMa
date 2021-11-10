namespace MaMa.DataModels
{
    /// <summary>
    /// class to calculate classifications and other properties of numbers
    /// </summary>
    public interface INumberClassifier
    {
        EnumNumberClassification GetClassOfNumber(decimal theNumber);

        (bool isNonPeriodic, int commaCount) CalcPeriodicity(decimal dividend, decimal divisor);

        (int integerNr, int potenzenCount) MakeInteger(decimal divisor);
    }
}
