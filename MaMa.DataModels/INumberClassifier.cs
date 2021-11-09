namespace MaMa.DataModels
{
    /// <summary>
    /// class to calculate classifications and other properties of numbers
    /// </summary>
    public interface INumberClassifier
    {
        EnumNumberClassification GetClassOfNumber(decimal theNumber);
    }
}
