using MaMa.DataModels;
using MaMa.DataModels.MultiplicationSteps;
using MaMa.DataModels.Rendering;
using MaMa.MultiplicationSteps;
using System.ComponentModel.Design;

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
        _svgRenderer.StartNewPage();

        int idx=0;
        foreach (CalculationItem calcItem in calcList)
        {
            idx++;
            switch (calcItem.RechenArt)
            {
                case EnumRechenArt.Multiplikation:
                {
                    MuiltiplicationStepsSolution solution = _stepsCalculator.CalculateMultiplicationSteps(calcItem);
                        _ = _svgRenderer.AddCalculation(solution);
                        break;
                }
            }
            //if (idx == 3) break;
        }
        File.WriteAllText(@"C:\dev\MaMa\ConceptStepsAndSvg\temp.html", _svgRenderer.GetRenderedHtmlPage());
    }
 }

