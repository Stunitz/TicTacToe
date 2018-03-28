using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        /// <summary>
        /// Holds the current results of cells in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True if it is player 1's turn (X) or false if it is player 2's turn (O)
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// True if the game has ended
        /// </summary>
        private bool mGameEnded;

        /// <summary>
        /// Create the lines that appears when a game is won
        /// </summary>
        private WinLines winLines = new WinLines();

        /// <summary>
        /// Holds the type of line that will be draw after a win game
        /// </summary>
        private UIElement winLineType;

        /// <summary>
        /// Count the number of buttons in the grid
        /// </summary>
        private int mNumberOfButtons;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion

        #region Game initializers and button methods

        /// <summary>
        /// Starts a new game and clears all values back to the start
        /// </summary>
        private void NewGame()
        {
            // Count the number of buttons in the grid
            mNumberOfButtons = Container.Children.OfType<Button>().Count();

            // Create a new blank array of free cells
            mResults = new MarkType[mNumberOfButtons];

            // Set initial value of free for the array (grid) in tic tac toe
            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            // In the first game the "winLineType" will be null, this prevents a error if it's the first game
            if (winLineType != null)
                Container.Children.Remove(winLineType);

            // Make sure Player 1 starts the game
            mPlayer1Turn = true;


            // Interate every button on the grid...
            Container.Children.OfType<Button>().Cast<Button>().ToList().ForEach(button =>
            {
                // Change background, foreground and content to default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            // Make sure the game hasn't finished
            mGameEnded = false;
        }

        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button that was been clicked</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Starts a new game if there is a winner
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            // Cast the sender to a button
            var button = (Button)sender;

            // Get the number of cell needed to win a game with a line
            int cellNumberToWin = (int)Math.Round(Math.Sqrt(mResults.Length));

            // Finds the buttons position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);
            var index = column + (row * cellNumberToWin);

            // Don't do anything if the cell already has a value in it
            if (mResults[index] != MarkType.Free)
                return;

            // Set the cell value based on which players turn it is
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            // Set button text to the result
            button.Content = mPlayer1Turn ? "X" : "O";

            // Change noughts to red
            if(!mPlayer1Turn)
                button.Foreground = Brushes.Red;

            // Toggle the players turns
            mPlayer1Turn ^= true;

            // Check for a winner
            CheckForWinner();
        }

        #endregion

        #region Checks

        /// <summary>
        /// Checks if there is a winner of a three line straight
        /// </summary>
        private void CheckForWinner()
        {
            #region Checkers initializers

            // Get the number of cell needed to win a game with a line
            int cellNumberToWin =  (int)Math.Round(Math.Sqrt(mResults.Length));
            // Generating variables to check if there is a win game player
            string rowCheck, colCheck, diagCheck, inverseDiagCheck;
            // Emptying the varibles for the checks
            rowCheck = colCheck = diagCheck = inverseDiagCheck = string.Empty;

            #endregion

            #region Winner check

            // Iterating throughout rows of the matrix of the grid to se if there is a winner ('i = row' and 'k = column')
            for (int i = 0; i < cellNumberToWin; i++)
            {
                // Clear the variables for a new check
                rowCheck = colCheck = string.Empty;

                // Iterating throughout columns of the matrix of the grid to se if there is a winner ('i = row' and 'k = column')
                for (int k = 0; k < cellNumberToWin; k++)
                {
                    #region Horizontal check

                    // Get the positions to check if we have horizontal winner 
                    colCheck += GetCellContent(i, k);
                    // Check for the horizontal winner
                    if ((!colCheck.Contains("X") || !colCheck.Contains("O")) & colCheck.Length >= cellNumberToWin)
                    {
                         
                        // Creates a line that will be drawn over the horizontal win condition
                        SetGridForWinLine("Horizontal", i);
                        // Set a end game
                        mGameEnded = true;
                        // Show message for the winner 
                        ShowWinnerMessage();
                    }

                    #endregion

                    #region Vertical check

                    // Get the positions to check if we have vertical winner
                    rowCheck += GetCellContent(k, i);
                    // Check for the vertical winner
                    if ((!rowCheck.Contains("X") || !rowCheck.Contains("O")) & rowCheck.Length >= cellNumberToWin)
                    {
                        // Creates a line that will be drawn over the vertical win condition
                        SetGridForWinLine("Vertical", i);
                        // Set a end game
                        mGameEnded = true;
                        // Show message for the winner 
                        ShowWinnerMessage();
                    }

                    #endregion
                }

                #region Diagonal check

                // Get the positions to check if we have normal diagonal winner
                diagCheck += GetCellContent(i, i);
                // Check for the normal diagonal winner
                if ((!diagCheck.Contains("X") || !diagCheck.Contains("O")) & diagCheck.Length >= cellNumberToWin)
                {
                    // Creates a line that will be drawn over the normal diagonal win condition
                    SetGridForWinLine("Diagonal", 0);
                    // Set a end game
                    mGameEnded = true;
                    // Show message for the winner 
                    ShowWinnerMessage();
                }

                // Get the positions to check if we have inverse diagonal winner
                inverseDiagCheck += GetCellContent(i, cellNumberToWin - 1 - i);
                // Check for the inverse diagonal winner
                if ((!inverseDiagCheck.Contains("X") || !inverseDiagCheck.Contains("O")) & inverseDiagCheck.Length >= cellNumberToWin)
                {
                    // Creates a line that will be drawn over the inverse diagonal win condition
                    SetGridForWinLine("Diagonal", 1);
                    // Set a end game
                    mGameEnded = true;
                    // Show message for the winner 
                    ShowWinnerMessage();
                }

                #endregion
            }

            #endregion

            #region No winners check

            if (!mResults.Any(result => result == MarkType.Free) && !mGameEnded)
            {
                // Gsme ended
                mGameEnded = true;

                // Turn all cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button => button.Background = Brushes.Orange);
            }

            #endregion
        }

        #endregion

        #region Helpers methods

        /// <summary>
        /// Will draw a line over the squares within the grid
        /// </summary>
        /// <param name="lineType">The type of line to draw (can only be: Horizontal, Vertical or Diagonal)</param>
        /// <param name="lineNumber">The number of the position draw the line</param>
        private void SetGridForWinLine(string lineType,int lineNumber)
        {
            // Get the number of cell needed to win a game with a line
            int cellNumberToWin = (int)Math.Round(Math.Sqrt(mResults.Length));

            // Check if the line to be draw is a horizontal
            if (lineType == "Horizontal")
            {
                // Check to know who had won the game
                if (!mPlayer1Turn == true)
                {
                    // Player 1 had won, so the grid will receive a blue horizontal line
                    Container.Children.Add(winLines.HorizontalLineBlue);
                    // Hold the name of the type of line to remove on a new game
                    winLineType = winLines.HorizontalLineBlue;
                }
                else
                {
                    // Player 2 had won, so the grid will receive a red horizontal line
                    Container.Children.Add(winLines.HorizontalLineRed);
                    // Hold the name of the the type of line to remove on a new game
                    winLineType = winLines.HorizontalLineRed;
                }

                // This will add the line of the player that had won to the right position
                winLines.SetGridLine((Line)winLineType, lineType, lineNumber, cellNumberToWin);
            }
            // Check if the line to be draw is a vertical
            else if (lineType == "Vertical")
            {
                // Check to know who had won the game
                if (!mPlayer1Turn == true)
                {
                    // Player 1 had won, so the grid will receive a blue vertical line
                    Container.Children.Add(winLines.VerticalLineBlue);
                    // Hold the name of the type of line to remove on a new game
                    winLineType = winLines.VerticalLineBlue;
                }
                else
                {
                    // Player 2 had won, so the grid will receive a red vertical line
                    Container.Children.Add(winLines.VerticalLineRed);
                    // Hold the name of the the type of line to remove on a new game
                    winLineType = winLines.VerticalLineRed;
                }

                // This will add the line of the player that had won to the right position
                winLines.SetGridLine((Line)winLineType, lineType, lineNumber, cellNumberToWin);
            }
            // Check if the line to be draw is a normal diagonal
            else if (lineType == "Diagonal" && lineNumber == 0)
            {
                // Check to know who had won the game
                if (!mPlayer1Turn == true)
                {
                    // Player 1 had won, so the grid will receive a blue normal diagonal line
                    Container.Children.Add(winLines.NormalDiagonalLineBlue);
                    // Holds the name of the type of line to remove on a new game
                    winLineType = winLines.NormalDiagonalLineBlue;
                }
                else
                {
                    // Player 2 had won, so the grid will receive a red normal diagonal line
                    Container.Children.Add(winLines.NormalDiagonalLineRed);
                    // Hold the name of the the type of line to remove on a new game
                    winLineType = winLines.NormalDiagonalLineRed;
                }

                // This will add the line of the player that had won to the right position
                winLines.SetGridLine((Line)winLineType, lineType, lineNumber, cellNumberToWin);
            }
            // Check if the line to be draw is a inverse diagonal
            else if (lineType == "Diagonal" && lineNumber == 1)
            {
                // Check to know who had won the game
                if (!mPlayer1Turn == true)
                {
                    // Player 1 had won, so the grid will receive a blue inverse diagonal line
                    Container.Children.Add(winLines.InverseDiagonalLineBlue);
                    // Hold the name of the type of line to remove on a new game
                    winLineType = winLines.InverseDiagonalLineBlue;
                }
                else
                {
                    // Player 2 had won, so the grid will receive a red inverse diagonal line
                    Container.Children.Add(winLines.InverseDiagonalLineRed);
                    // Hold the name of the the type of line to remove on a new game
                    winLineType = winLines.InverseDiagonalLineRed;
                }

                // This will add the line of the player that had won to the right position
                winLines.SetGridLine((Line)winLineType, lineType, lineNumber, cellNumberToWin);
            }

            // Paint the background to green after a won game
            /// PaintBackgroundCells(lineType, lineNumber);
        }

        /// <summary>
        /// Check who is the winner and return a message 
        /// </summary>
        private void ShowWinnerMessage()
        {
            // Check to know who had won the game
            if (mPlayer1Turn)
            {
                // Show the message that the player 2 had won
                MessageBox.Show("Player 2 Wins !!!");
            }
            else
            {
                // Show the message that the player 1 had won
                MessageBox.Show("Player 1 Wins !!!");
            }
        }

        /// <summary>
        /// Get cell value based on row/column 
        /// </summary>
        /// <param name="row">Get the row of the value</param>
        /// <param name="column">Column</param>
        /// <returns>The value of the cell</returns>
        private string GetCellContent(int row, int column)
        {
            // Search for the value of the cell with the same row and column sended by the varible, and returns
            return Container.Children.Cast<Button>().First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column).Content.ToString();
        }

        /// <summary>
        /// Paint the background of the game win line
        /// </summary>
        /// <param name="lineType">The type of line to draw (can only be: Horizontal, Vertical or Diagonal)</param>
        /// <param name="lineNumber">The number of the position draw the line</param>
        private void PaintBackgroundCells(string lineType,int lineNumber)
        {
            // Check what type of line to paint the background
            switch (lineType)
            {
                case "Horizontal":
                    // Check what row to paint the background
                    switch (lineNumber)
                    {
                        case 0:
                            // Paint the row 0
                            Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
                            break;
                        case 1:
                            // Paint the row 1
                            Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
                            break;
                        case 2:
                            // Paint the row 2
                            Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
                            break;
                    }
                    break;
                case "Vertical":
                    // Check what column to paint the background
                    switch (lineNumber)
                    {
                        case 0:
                            // Paint the column 0
                            Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
                            break;
                        case 1:
                            // Paint the column 1
                            Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
                            break;
                        case 2:
                            // Paint the column 2
                            Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
                            break;
                    }
                    break;
                case "Diagonal":
                    // Check what diagonal to paint the background
                    switch (lineNumber)
                    {
                        case 0:
                            // Paint the diagonal from the left top to the right bottom
                            Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
                            break;
                        case 1:
                            // Paint the diagonal from the left bottom to the right top
                            Button0_2.Background = Button1_1.Background = Button2_0.Background = Brushes.Green;
                            break;
                    }
                    break;
            }
        }

        #endregion
    }
}