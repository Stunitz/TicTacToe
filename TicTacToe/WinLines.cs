using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Configuration of the win lines to be implemented to the UI when a player win a game
    /// </summary>
    public class WinLines
    {
        #region Player 1 WinLines

        /// <summary>
        /// A blue horizontal line that will appear when the Player 1 win with a horizontal line
        /// </summary>
        public Line HorizontalLineBlue = new Line()
        {
            X1 = 1,
            Y1 = 0,
            X2 = 0,
            Y2 = 0,
            Stretch = Stretch.Fill,
            Stroke = Brushes.Blue,
            StrokeThickness = 10
        };

        /// <summary>
        /// A blue vertical line that will appear when the Player 1 win with a vertical line
        /// </summary>
        public Line VerticalLineBlue = new Line()
        {
            X1 = 0,
            Y1 = 1,
            X2 = 0,
            Y2 = 0,
            Stretch = Stretch.Fill,
            Stroke = Brushes.Blue,
            StrokeThickness = 10
        };

        /// <summary>
        /// A blue diagonal line that will appear from the top left to the right bottom of the grid when the Player 1 win with that type of diagonal line
        /// </summary>
        public Line NormalDiagonalLineBlue = new Line()
        {
            X1 = 1,
            Y1 = 1,
            X2 = 0,
            Y2 = 0,
            Stretch = Stretch.Fill,
            Stroke = Brushes.Blue,
            StrokeThickness = 10
        };

        /// <summary>
        /// A blue diagonal line that will appear from the bottom left to the right top of the grid when the Player 1 win with that type of diagonal line
        /// </summary>
        public Line InverseDiagonalLineBlue = new Line()
        {
            X1 = 0,
            Y1 = 1,
            X2 = 1,
            Y2 = 0,
            Stretch = Stretch.Fill,
            Stroke = Brushes.Blue,
            StrokeThickness = 10
        };

        #endregion

        #region Player 2 WinLines

        /// <summary>
        /// A red horizontal line that will appear when the Player 2 win with a horizontal line
        /// </summary>
        public Line HorizontalLineRed = new Line()
        {
            X1 = 1,
            Y1 = 0,
            X2 = 0,
            Y2 = 0,
            Stretch = Stretch.Fill,
            Stroke = Brushes.Red,
            StrokeThickness = 10
        };

        /// <summary>
        /// A red vertical line that will appear when the Player 2 win with a vertical line
        /// </summary>
        public Line VerticalLineRed = new Line()
        {
            X1 = 0,
            Y1 = 1,
            X2 = 0,
            Y2 = 0,
            Stretch = Stretch.Fill,
            Stroke = Brushes.Red,
            StrokeThickness = 10
        };

        /// <summary>
        /// A red diagonal line that will appear from the top left to the right bottom of the grid when the Player 2 win with that type of diagonal line
        /// </summary>
        public Line NormalDiagonalLineRed = new Line()
        {
            X1 = 1,
            Y1 = 1,
            X2 = 0,
            Y2 = 0,
            Stretch = Stretch.Fill,
            Stroke = Brushes.Red,
            StrokeThickness = 10
        };

        /// <summary>
        /// A red diagonal line that will appear from the bottom left to the right top of the grid when the Player 2 win with that type of diagonal line
        /// </summary>
        public Line InverseDiagonalLineRed = new Line()
        {
            X1 = 0,
            Y1 = 1,
            X2 = 1,
            Y2 = 0,
            Stretch = Stretch.Fill,
            Stroke = Brushes.Red,
            StrokeThickness = 10
        };

        #endregion

        #region Configuration for the position

        /// <summary>
        /// Determine the position on the grid to draw a specific type of line
        /// </summary>
        /// <param name="lineUiName">Name of the configuration of the line in the UI</param>
        /// <param name="lineType">The type of line to draw (can only be: Horizontal, Vertical or Diagonal)</param>
        /// <param name="lineNumber">The number of the position draw the line</param>
        /// <param name="cellNumberToWin">The number condicion will change how many cells will be draw</param>
        public void SetGridLine(Line lineUiName, string lineType, int lineNumber, int cellNumberToWin)
        {
            // Set the position of the win line and what rows and columns will be draw over
            switch (lineType)
            {
            // Check if the line to be draw is a horizontal
                case "Horizontal":
                    // Configurations of the position to draw
                    Grid.SetColumn(lineUiName, 0);
                    Grid.SetColumnSpan(lineUiName, cellNumberToWin);
                    Grid.SetRow(lineUiName, lineNumber);
                    break;
            // Check if the line to be draw is a vertical
                case "Vertical":
                    // Configurations of the position to draw
                    Grid.SetColumn(lineUiName, lineNumber);
                    Grid.SetRow(lineUiName, 0);
                    Grid.SetRowSpan(lineUiName, cellNumberToWin);
                    break;
            // Check if the line to be draw is a diagonal
                case "Diagonal":
                    // Configurations of the position to draw
                    Grid.SetColumn(lineUiName, 0);
                    Grid.SetColumnSpan(lineUiName, cellNumberToWin);
                    Grid.SetRow(lineUiName, 0);
                    Grid.SetRowSpan(lineUiName, cellNumberToWin);
                    break;
            }
        }

        #endregion
    }
}