using MaMa.DataModels;

namespace ConceptStepsAndSvg;

public class SvgRenderer
{
    public void AddItem(object steps)
    {
        
    }
}
public class HtmlContent
{
    private static (int width, int height) viewBox = (2100,2970);
    private const int maxCols = 2;
    
    private int currentRow = 1;
    private int currentCol = 1;
    
    /// <summary>
    /// 297 x 210 time 10 to get more pixel
    /// </summary>
    private string HtmlBase = $"""
<html><body>
    <svg width="100%" height="100%" viewBox="0 0 {viewBox.width} {viewBox.height}">
    __svg-content__
    </svg>
</body></html>
""";

    // public void AddCalcItem(CalculationItem item)
    // {
    //     if (currentCol >= maxCols)
    //     {
    //         currentCol = 1;
    //         currentRow++;
    //     }
    //     
    //     item.
    // }
}