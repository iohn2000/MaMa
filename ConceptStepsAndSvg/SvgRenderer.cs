using MaMa.DataModels;
using MaMa.DataModels.MultiplicationSteps;
using MaMa.DataModels.Rendering;

namespace ConceptStepsAndSvg;

public class SvgRenderer
{
    private static (int Width, int Height) viewBox = (620,877);
    private const int maxCols = 2;
    private const string textLine = "__textline__";

    private SvgCoord currentPos = new SvgCoord(0,0);
    private SvgCoord currentPx = new SvgCoord(0, 0);

    private const int rowHeight = 24;
    private const int colWidth = 20;

    private string HtmlBase = $"""
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

    public string RenderSolution(MuiltiplicationStepsSolution solution)
    {
        foreach (RowMultiplication? row in solution.Steps)
        {
            foreach (DigitMultiplication? digit in row.Digits)
            {
                string d = new SvgDigitBox(currentPx,digit).GetSVG();
                HtmlBase = HtmlBase.Replace(textLine,d + Environment.NewLine + textLine);
                currentPos.X += 1;
                currentPx.X += colWidth;
            }
            HtmlBase = HtmlBase.Replace(textLine, "<!-- new row -->" + Environment.NewLine + textLine);
            // next row
            currentPos.Y += 1;
            currentPx.Y += rowHeight;
            // reset cols
            currentPos.X = currentPos.Y;
            currentPx.X = currentPos.Y * colWidth;
        }
        return HtmlBase;
    }
}