using UnityEngine;

namespace Board{
    public class GapController{
        // V: Black bars are a background image, to show "black bars", tiles need to be downsized and their position adjusted
        private const float barSizeToTilePercent = 0.05f;
        private readonly float barSize;
        private readonly float overallBarsSizeVertical;

        // V: Same is applied to the gaps between tiles
        private const float gapToBarPercent = 0.33f;
        private readonly float gapSize;
        private readonly float overallGapsSizeVertical;

        private readonly float totalWidthReduction;
        private readonly float totalHeightReduction;
        
        public GapController(Vector2Int boardSize, Vector2Int squareLayout, float tileWidth) {
            barSize = tileWidth * barSizeToTilePercent;
            // V: Calculate the total black bars width for horizontal and vertical bars
            float overallBlackBarsSizeHorizontal = barSize * ((float)boardSize.x / squareLayout.x + 1);
            overallBarsSizeVertical = barSize * ((float)boardSize.y / squareLayout.y + 1);

            // V: Find the width and height that needs to be deducted from the tiles to compensate for black bars
            float widthOfBarsToDeduct = overallBlackBarsSizeHorizontal / boardSize.x;
            float heightOfBarsToDeduct = overallBarsSizeVertical / boardSize.y;

            totalWidthReduction += widthOfBarsToDeduct;
            totalHeightReduction += heightOfBarsToDeduct;
            
            gapSize = barSize * gapToBarPercent;
            // V: Calculate number total number of horizontal and vertical gaps
            int totalNumberOfGapsHorizontal = (squareLayout.x - 1) * (boardSize.x / squareLayout.x);
            int totalNumberOfGapsVertical = (squareLayout.y - 1) * (boardSize.y / squareLayout.y);
            // V: Find overall width/height that all the gaps will take
            float overallGapsSizeHorizontal = totalNumberOfGapsHorizontal * gapSize;
            overallGapsSizeVertical = totalNumberOfGapsVertical * gapSize;
            // V: Calculate how much width/height to deduct from tile to compensate for gaps
            float widthOfGapToDeduct = overallGapsSizeHorizontal / boardSize.x;
            float heightOfGapToDeduct = overallGapsSizeVertical / boardSize.y;
            
            totalWidthReduction += widthOfGapToDeduct;
            totalHeightReduction += heightOfGapToDeduct;
        }

        public Vector2 GetPositionAdjustment(int x, int y, Vector2Int squareLayout) {
            Vector2 result = new();
            result += GetBarsAdjustmentPosition(x, y, squareLayout);
            result += GetGapAdjustmentPosition(x, y, squareLayout);
            return result;
        }
        
        private Vector2 GetBarsAdjustmentPosition(int x, int y, Vector2Int squareLayout) {
            float additionalXPosition = Mathf.FloorToInt((float)x / squareLayout.x) * barSize + barSize;
            float additionalYPosition = Mathf.FloorToInt((float)y / squareLayout.y) * barSize + barSize;
            Vector2 result = new(additionalXPosition, additionalYPosition - overallBarsSizeVertical);
            return result;
        }

        private Vector2 GetGapAdjustmentPosition(int x, int y, Vector2Int squareLayout) {
            float xAdjustment = (x - Mathf.FloorToInt((float)x / squareLayout.x)) * gapSize;
            float yAdjustment = (y - Mathf.FloorToInt((float)y / squareLayout.y)) * gapSize;
            Vector2 result = new(xAdjustment, yAdjustment - overallGapsSizeVertical);
            return result;
        }

        public float GetTotalWidthDeduction() {
            return totalWidthReduction;
        }

        public float GetTotalHeightDeduction() {
            return totalHeightReduction;
        }
    }
}