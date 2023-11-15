using MaMa.DataModels;
using MaMa.DataModels.MultiplicationSteps;
using MaMa.DataModels.Rendering;
using System.Transactions;

namespace ConceptStepsAndSvg;

public class SvgRenderer
{
    private const string textLine = "__textline__";

    //private static (int Width, int Height) viewBox = (620, 877);
    private static (int Width, int Height) viewBox = (1240, 1754);
    private static (int Width, int Height) svgSize = (1240, 1754);

    private SvgCoord currentPos = new SvgCoord(0, 0);
    private SvgCoord currentPx = new SvgCoord(0, 0);

    private const int rowHeight = 24;
    private const int colWidth = 20;
    private const int xPosStartOfAngabe = 1;
    private readonly int maxCols = svgSize.Width / colWidth;
    private readonly int maxRows = svgSize.Height / rowHeight;

    private SvgCalculationDimension lastCalculationDimension = new SvgCalculationDimension { HeightPixel = 0, WidthPixel = 0, NumberOfColumns = 0, NumberOfRows = 0 };

    private string RenderedHtml = "";
    private string HtmlTemplate = $"""
<html>
<body>
    <svg viewBox="0 0 {viewBox.Width} {viewBox.Height}">
        <!-- A4 : 0 0 1240 1754 : 150 dpi -->
        <!-- https://superuser.com/questions/161313/print-a-huge-svg -->
        <rect x="0" y="0" width="{svgSize.Width}" height="{svgSize.Height}" style="stroke:red; fill:none" />
        {textLine}
    </svg>
</body></html>
""";

    public SvgRenderer()
    {
        StartNewPage();
    }

    /// <summary>
    /// reset and start a new page on top left
    /// </summary>
    public void StartNewPage()
    {
        currentPos = new SvgCoord(0, 0);
        currentPx = new SvgCoord(0, 0);
        RenderedHtml = HtmlTemplate;
    }

    public string GetRenderedHtmlPage()
    {
        return RenderedHtml;
    }

    public SvgCalculationDimension AddCalculation(MuiltiplicationStepsSolution solution)
    {
        SvgCoord firstDigitFaktor1 = new SvgCoord(currentPos);

        int startXForRows = this.RenderAngabe(solution);
        MoveOneRowDown();

        // set cursor to last digit of faktor1
        currentPos.X = startXForRows - 1;
        currentPx.X = XPixelFromPos(currentPos.X);

        solution.Steps.Reverse();
        foreach ((RowMultiplication? row, int index) in solution.Steps.Select((item, index) => (item, index)))
        {
            foreach (DigitMultiplication? digit in row.Digits)
            {
                string d = new SvgDigitBox(currentPx, digit).GetSVG();
                AddToHtml(d);
                MoveOneColumnLeft();
            }
            AddToHtml("<!-- new row -->");
            MoveOneRowDown();
            // reset cols to last digit previouse row + 1
            currentPos.X = startXForRows + index;
            currentPx.X = XPixelFromPos(currentPos.X);
        }
        /*
        AddToHtml(new SvgLine(
            new SvgCoord(firstDigitFaktor1.X + 2, currentPx.Y + 3),
            new SvgCoord(XPixelFromPos(currentPos.X), currentPx.Y + 3)).GetSVG());
        */
        this.RenderSolution(solution.CalcItem.Solution);

        SvgCalculationDimension dimensions = new SvgCalculationDimension
        {
            NumberOfRows = currentPos.Y,
            NumberOfColumns = GetXposRightMostOfAngabe(solution.CalcItem),
            WidthPixel = XPixelFromPos(GetXposRightMostOfAngabe(solution.CalcItem)),
            HeightPixel = YPixelFromPos(currentPos.Y + 1)
        };

        AddToHtml(new SvgRect(
            new SvgCoord(firstDigitFaktor1.X, firstDigitFaktor1.Y),
            new SvgCoord(dimensions.WidthPixel, dimensions.HeightPixel)
            ).GetSVG());

        this.lastCalculationDimension = dimensions;
        return dimensions;
    }

    /// <summary>
    /// how long is angabe string minus commas (the dont count for length)
    /// </summary>
    /// <param name="calc"></param>
    /// <returns></returns>
    private int GetXposRightMostOfAngabe(CalculationItem calc)
    {
        string angabeStr = GetAngabeString(calc);
        int length = xPosStartOfAngabe + angabeStr.Length - angabeStr.Count(c => c == '.' || c == ',');
        return length;
    }

    private string GetAngabeString(CalculationItem calc)
    {
        return calc.FirstNumber.ToString() + "x" + calc.SecondNumber.ToString();
    }

    /// <summary>
    /// render angabe, e.g. 123x42
    /// </summary>
    /// <param name="solution"></param>
    /// <returns>tuple : (pos of last digit faktor1 , pos of last digit faktor2)</returns>
    public int RenderAngabe(MuiltiplicationStepsSolution solution)
    {
        string angabeStr = GetAngabeString(solution.CalcItem);
        int currentXPos = currentPos.X;

        // use digitbox to render each number (char)
        foreach (char item in angabeStr)
        {
            if (item != ',' && item != '.')
            {
                MoveOneColumnRight(); // angabe starts one col right of start
            }
            string c = new SvgCharacterBox(currentPx, item).GetSVG();
            AddToHtml(c);
        }

        int pos = this.HasComma(solution.CalcItem.FirstNumber) ?
            solution.CalcItem.FirstNumber.ToString().Length + xPosStartOfAngabe - 1 :
            solution.CalcItem.FirstNumber.ToString().Length + xPosStartOfAngabe;

        AddToHtml(new SvgLine(
            new SvgCoord(XPixelFromPos(currentXPos + xPosStartOfAngabe), currentPx.Y + 3 + rowHeight),
            new SvgCoord(XPixelFromPos(GetXposRightMostOfAngabe(solution.CalcItem)), currentPx.Y + 3 + rowHeight)).GetSVG());

        return pos; // we start at second row not first
    }

    /// <summary>
    /// render the product of calculation at bottom
    /// </summary>
    /// <param name="solution"></param>
    public void RenderSolution(decimal solution)
    {
        int slnLength = this.HasComma(solution) ?
           solution.ToString().Length + xPosStartOfAngabe - 1 :
           solution.ToString().Length + xPosStartOfAngabe;

        AddToHtml(new SvgLine(
            new SvgCoord(XPixelFromPos(currentPos.X - slnLength - 1), currentPx.Y + 3),
            new SvgCoord(XPixelFromPos(currentPos.X), currentPx.Y + 3)).GetSVG());

        MoveOneColumnLeft();
        var solutionReverse = solution.ToString().ToList();
        solutionReverse.Reverse();
        foreach (char item in solutionReverse)
        {
            string c = new SvgCharacterBox(currentPx, item).GetSVG();
            AddToHtml(c);
            if (item != ',' && item != '.')
            {
                MoveOneColumnLeft(); // angabe starts one col right of start
            }
        }

    }

    private void AddToHtml(string angabe)
    {
        RenderedHtml = RenderedHtml.Replace(textLine, angabe + Environment.NewLine + textLine);
    }

    private void MoveOneColumnRight()
    {
        currentPos.X += 1;
        currentPx.X = XPixelFromPos(currentPos.X);
    }
    private void MoveOneColumnLeft()
    {
        currentPos.X -= 1;
        currentPx.X = XPixelFromPos(currentPos.X);
    }
    private void MoveOneRowDown()
    {
        currentPos.Y += 1;
        //currentPx.Y += rowHeight;
        currentPx.Y = YPixelFromPos(currentPos.Y);
    }

    private bool HasComma(decimal theNumber)
    {
        return theNumber.ToString().Contains(',') || theNumber.ToString().Contains('.');
    }

    private int XPixelFromPos(int xpos) => (xpos) * colWidth;

    private int YPixelFromPos(int ypos) => ypos * rowHeight;
}