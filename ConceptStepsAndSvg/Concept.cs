using MaMa.DataModels;

namespace ConceptStepsAndSvg;

/// <summary>
/// all sort of PoC routines
/// </summary>
public class Concept
{
    private readonly CalculationSteps _stepsCalculator;
    private readonly SvgRenderer _svgRenderer;

    public Concept(CalculationSteps stepsCalculator, SvgRenderer svgRenderer)
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

