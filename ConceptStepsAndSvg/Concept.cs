﻿using MaMa.DataModels;
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
       MuiltiplicationStepsSolution solution = _stepsCalculator.CalculateMultiplicationSteps(calcItem);
       
       _svgRenderer.RenderNewPage();
       string svg = _svgRenderer.RenderCalculation(solution);
       
        File.WriteAllText(@"C:\dev\MaMa\ConceptStepsAndSvg\temp.html", svg);
    }
}

