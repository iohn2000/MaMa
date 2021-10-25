using MaMa.DataModels;

namespace MaMa.CalcGenerator
{
    public interface IRandomNumber
    {
        decimal GetRandomNr(NumberProperties nrCfg, out int rawNumber);
    }
}