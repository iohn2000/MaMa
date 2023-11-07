using MaMa.DataModels;
using MaMa.MultiplicationSteps;

namespace ConceptStepsAndSvg;

/// <summary>
/// all sort of PoC routines
/// </summary>
public class Concept
{
    private readonly StepsCalculator _stepsCalculator;
    private readonly SvgRenderer _svgRenderer;

    public Concept(StepsCalculator stepsCalculator, SvgRenderer svgRenderer)
    {
        _stepsCalculator = stepsCalculator;
        _svgRenderer = svgRenderer;
    }
    public void Start(List<CalculationItem> calcList)
    {
        
        foreach (CalculationItem calcItem in calcList)
        {
            switch (calcItem.RechenArt)
            {
                case EnumRechenArt.Multiplikation:
                {
                    PrepareDivisionForRender(calcItem);
                    break;
                }
            }
        }
    }
    public void PrepareDivisionForRender(CalculationItem calcItem)
    {
        var steps = _stepsCalculator.CalculateMultiplicationSteps(calcItem);
        //_svgRenderer.AddItem(steps);
    }
}

