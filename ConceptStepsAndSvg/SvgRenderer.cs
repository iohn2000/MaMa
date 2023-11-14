using MaMa.DataModels;
using MaMa.DataModels.MultiplicationSteps;
using MaMa.DataModels.Rendering;

namespace ConceptStepsAndSvg;

public class SvgRenderer
{
    private static (int Width, int Height) viewBox = (620, 877);
    private const int maxCols = 2;
    private const string textLine = "__textline__";

    private SvgCoord currentPos = new SvgCoord(0, 0);
    private SvgCoord currentPx = new SvgCoord(0, 0);

    private const int rowHeight = 24;
    private const int colWidth = 20;
    
    private string HtmlTemplate = $"""
<html>
<body>
    <svg viewBox="0 0 {viewBox.Width} {viewBox.Height}">
        <!-- A4 : 0 0 1240 1754 : 150 dpi -->
        <!-- https://superuser.com/questions/161313/print-a-huge-svg -->
        <rect x="0" y="0" width="1240" height="1754" style="stroke:red; fill:none" />
        {textLine}
    </svg>
</body></html>
""";
    
    /// <summary>
    /// reset and start a new page on top left
    /// </summary>
    public void RenderNewPage()
    {
        currentPos = new SvgCoord(0, 0);
        currentPx = new SvgCoord(0, 0);
    }

    public string RenderCalculation(MuiltiplicationStepsSolution solution)
    {
        SvgCoord firstDigitFaktor1 = new SvgCoord(currentPos);
        (int startXForRows, int endPosAngabe) = this.RenderAngabe(solution);
        AddToHtml(new SvgLine(
            new SvgCoord(XPixelFromPos(firstDigitFaktor1.X+2),currentPx.Y+3+rowHeight), 
            new SvgCoord(XPixelFromPos(endPosAngabe + 1),currentPx.Y+3+rowHeight)).GetSVG());
        MoveOneRowDown();
        currentPos.X = startXForRows;
        currentPx.X = XPixelFromPos(startXForRows);

        solution.Steps.Reverse();
        foreach ( (RowMultiplication? row, int index) in solution.Steps.Select((item, index) => (item, index)))
        {
            foreach (DigitMultiplication? digit in row.Digits)
            {
                string d = new SvgDigitBox(currentPx, digit).GetSVG();
                AddToHtml(d);
                MoveOneColumnLeft();
            }
            AddToHtml("<!-- new row -->");
            MoveOneRowDown();
            // reset cols
            currentPos.X = startXForRows + index + 1;
            currentPx.X = XPixelFromPos(currentPos.X);
        }
        AddToHtml(new SvgLine(
            new SvgCoord(firstDigitFaktor1.X + 2, currentPx.Y+3),
            new SvgCoord(XPixelFromPos(currentPos.X),currentPx.Y+3)).GetSVG());

        this.RenderSolution(solution.CalcItem.Solution);

        return HtmlTemplate;
    }

    public void RenderSolution(decimal solution)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// render angabe, e.g. 123x42=5166
    /// </summary>
    /// <param name="solution"></param>
    /// <returns>x pos for start of first step row, under first digit of first faktor</returns>
    public (int posOfFaktor1Einer, int posOfAngabeLine) RenderAngabe(MuiltiplicationStepsSolution solution)
    {
        string angabeStr = solution.CalcItem.FirstNumber.ToString() + "x" + solution.CalcItem.SecondNumber.ToString();

        // use digitbox to render each number (char)
        foreach (char item in angabeStr)
        {
            MoveOneColumnRight(); // angaber start one col right of start
            string c = new SvgCharacterBox(currentPx, item).GetSVG();
            AddToHtml(c);
        }
        return (solution.CalcItem.FirstNumber.ToString().Length + 1,angabeStr.Length +1); // we start at second row not first
    }

    private void AddToHtml(string angabe)
    {
        HtmlTemplate = HtmlTemplate.Replace(textLine, angabe + Environment.NewLine + textLine);
    }

    private void MoveOneColumnRight()
    {
        currentPos.X += 1;
        currentPx.X += colWidth;
    }
    private void MoveOneColumnLeft()
    {
        currentPos.X -= 1;
        currentPx.X -= colWidth;
    }
    private void MoveOneRowDown()
    {
        currentPos.Y += 1;
        currentPx.Y += rowHeight;
        /*currentPos.Y += 1;
        currentPx.Y = currentPos.Y * rowHeight;*/
    }

    private int XPixelFromPos(int xpos) => (xpos-1) * colWidth;
}