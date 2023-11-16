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

    private SvgCoord currentRelativePos = new SvgCoord(0, 0);
    private SvgCoord currentAbsolutePx = new SvgCoord(0, 0);
    private SvgCoord originPx = new SvgCoord(0, 0);

    private const int rowHeight = 24;
    private const int colWidth = 20;
    private const int xPosOffsetForAngabe = 1;
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
        currentRelativePos = new SvgCoord(0, 0);
        originPx = new SvgCoord(0, 0);
        currentAbsolutePx = new SvgCoord(XPixelFromRelativePos(0), YPixelFromRelativePos(0));
        this.lastCalculationDimension = new SvgCalculationDimension
        {
            HeightPixel = 0,
            WidthPixel = 0,
            NumberOfColumns = 0,
            NumberOfRows = 0,
        };
        RenderedHtml = HtmlTemplate;
    }

    public string GetRenderedHtmlPage()
    {
        return RenderedHtml;
    }

    public SvgCalculationDimension AddCalculation(MuiltiplicationStepsSolution solution)
    {
        // reset relative pos
        currentRelativePos = new SvgCoord(0,0);
        
        // set new origin taking last runs dimensions

        // move 1 col right and 1 row down
        //this.originPx.Add(this.lastCalculationDimension.WidthPixel, this.lastCalculationDimension.HeightPixel);
        
        // stay in row move to next column
        //this.originPx.Add(this.lastCalculationDimension.WidthPixel, 0);

        // stay in col move to next row
        this.originPx.Add(0, this.lastCalculationDimension.HeightPixel);


        // set abs px now
        currentAbsolutePx = new SvgCoord(XPixelFromRelativePos(0), YPixelFromRelativePos(0));

        int startXForRows = this.RenderAngabe(solution);
        MoveOneRowDown();

        // set cursor to last digit of faktor1
        currentRelativePos.X = startXForRows - 1;
        currentAbsolutePx.X = XPixelFromRelativePos(currentRelativePos.X);

        solution.Steps.Reverse();
        foreach ((RowMultiplication? row, int index) in solution.Steps.Select((item, index) => (item, index)))
        {
            foreach (DigitMultiplication? digit in row.Digits)
            {
                string d = new SvgDigitBox(currentAbsolutePx, digit).GetSVG();
                AddToHtml(d);
                MoveOneColumnLeft();
            }
            MoveOneRowDown();

            // reset cols to last digit previouse row + 1
            currentRelativePos.X = startXForRows + index;
            currentAbsolutePx.X = XPixelFromRelativePos(currentRelativePos.X);
        }

        this.RenderSolution(solution.CalcItem.Solution);

        SvgCalculationDimension dimensions = new SvgCalculationDimension
        {
            NumberOfRows = currentRelativePos.Y,
            NumberOfColumns = GetXposRightMostOfAngabe(solution.CalcItem),
            WidthPixel = XPixelFromRelativePos(GetXposRightMostOfAngabe(solution.CalcItem)) - originPx.X,
            HeightPixel = YPixelFromRelativePos(currentRelativePos.Y + 1) + 10 - originPx.Y
        };

        /*
        AddToHtml(new SvgRect(
            new SvgCoord(XPixelFromRelativePos(0), YPixelFromRelativePos(0)),
            new SvgCoord(dimensions.WidthPixel, dimensions.HeightPixel)
            ).GetSVG());
        */

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
        int length = xPosOffsetForAngabe + angabeStr.Length - angabeStr.Count(c => c == '.' || c == ',');
        return length;
    }

    /// <summary>
    /// render angabe, e.g. 123x42
    /// </summary>
    /// <param name="solution"></param>
    /// <returns>pos of last digit faktor1</returns>
    public int RenderAngabe(MuiltiplicationStepsSolution solution)
    {
        string angabeStr = GetAngabeString(solution.CalcItem);
        int currentXPos = currentRelativePos.X;
        
        // move xPosOffsetForAngabe amount of times to right to position cursor correcly with offest
        foreach (int _ in Enumerable.Range(0, xPosOffsetForAngabe))
        {
            MoveOneColumnRight();
        }

        // use charbox to render each number (char)
        foreach (char item in angabeStr)
        {
            string c = new SvgCharacterBox(currentAbsolutePx, item).GetSVG();
            AddToHtml(c);
            if (IsNotAComma(item)) // comma doesnt count as its own character dont move cursor
            {
                MoveOneColumnRight();
            }
        }

        int lastDigitOfFactor1 = this.GetDigitCounthWithoutCommas(solution.CalcItem.FirstNumber) + xPosOffsetForAngabe;

        AddToHtml(new SvgLine(
            new SvgCoord(XPixelFromRelativePos(currentXPos + xPosOffsetForAngabe), currentAbsolutePx.Y + 3 + rowHeight),
            new SvgCoord(XPixelFromRelativePos(GetXposRightMostOfAngabe(solution.CalcItem)), currentAbsolutePx.Y + 3 + rowHeight)).GetSVG());

        return lastDigitOfFactor1; // we start at second row not first
    }

    /// <summary>
    /// render the product of calculation at bottom
    /// </summary>
    /// <param name="solution"></param>
    public void RenderSolution(decimal solution)
    {
        int slnLength = this.GetDigitCounthWithoutCommas(solution) + xPosOffsetForAngabe;

        AddToHtml(new SvgLine(
            new SvgCoord(XPixelFromRelativePos(currentRelativePos.X - slnLength + 1), currentAbsolutePx.Y + 3),
            new SvgCoord(XPixelFromRelativePos(currentRelativePos.X), currentAbsolutePx.Y + 3)).GetSVG());

        MoveOneColumnLeft(); // cursor if one too far right because of row logic, fix it

        var solutionReverse = solution.ToString().ToList();
        solutionReverse.Reverse();
        foreach (char item in solutionReverse)
        {
            string c = new SvgCharacterBox(currentAbsolutePx, item).GetSVG();
            AddToHtml(c);
            if (IsNotAComma(item))
            {
                MoveOneColumnLeft(); // angabe starts one col right of start
            }
        }
    }

    private bool IsNotAComma(char item)
    {
        return item != ',' && item != '.';
    }

    private int GetDigitCounthWithoutCommas(decimal theNumber)
    {
        string strNumer = theNumber.ToString();
        return strNumer.Length - strNumer.Count(c => c == '.' || c == ',');
    }

    private string GetAngabeString(CalculationItem calc)
    {
        return calc.FirstNumber.ToString() + "x" + calc.SecondNumber.ToString();
    }

    private void AddToHtml(string angabe)
    {
        RenderedHtml = RenderedHtml.Replace(textLine, angabe + Environment.NewLine + textLine);
    }

    private void MoveOneColumnRight()
    {
        currentRelativePos.X += 1;
        currentAbsolutePx.X = XPixelFromRelativePos(currentRelativePos.X);
    }
    private void MoveOneColumnLeft()
    {
        currentRelativePos.X -= 1;
        currentAbsolutePx.X = XPixelFromRelativePos(currentRelativePos.X);
    }
    private void MoveOneRowDown()
    {
        currentRelativePos.Y += 1;
        currentAbsolutePx.Y = YPixelFromRelativePos(currentRelativePos.Y);
    }

    private bool HasComma(decimal theNumber)
    {
        return theNumber.ToString().Contains(',') || theNumber.ToString().Contains('.');
    }

    private int XPixelFromRelativePos(int xpos) => originPx.X + (xpos * colWidth);

    private int YPixelFromRelativePos(int ypos) => originPx.Y + (ypos * rowHeight);
}