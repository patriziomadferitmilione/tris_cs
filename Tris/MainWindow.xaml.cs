using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Private Members

        // holds the current result of cells in the active game
        private MarkType[] mResults;

        // True if it is player 1's turn (X) or player 2
        private bool mPlayer1Turn;

        // True if the game has ended
        private bool mGameEnded;


        #endregion

        #region Constructor

        //<summary>
        //Default constructor
        //</summary>

        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion

        // Starts a new game and clears all values back to the start
        private void NewGame()
        {
            // Create new blank array of free cells
            mResults = new MarkType[9];

            for (var i = 0; i > mResults.Length; i++)
                mResults[i] = MarkType.Free;

            // Make sure Player 1 is current player
            mPlayer1Turn = true;

            // Iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                // Change background and foreground to default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            // Make sure the game hasn't finished
            mGameEnded = false;
        }

        // Handles a button click event
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Start a new game on the click after it finished
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            // Cast the sender to a button
            var button = (Button)sender;
            
            // Find the buttons position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);
            
            // Don't do anything if the cell already has a value in it
            if (mResults[index] != MarkType.Free)
                return;

            // Set the cell value based on which players turn it is
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            // Set the button text to the result
            button.Content = mPlayer1Turn ? "X" : "O";

            // Change Noughts to red
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;

            // Toggle the players turns
            mPlayer1Turn ^= true;

            // Check for a winner
            CheckForWinner();

        }

        // Checks if theere is a winner of a 3 line straight
        private void CheckForWinner()
        {
            #region Horizontal Wins
            // Check for horizontal wins
            // Row 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in greens
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }
            // Row 1
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in greens
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            // Row 2
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in greens
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Vertical Wins
            // Check for vertical wins
            // Col 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in greens
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }
            // Col 1
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in greens
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }
            // Col 2
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in greens
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Diagonal Wins

            // Check for diagonal wins
            // Top left - Bottom Right
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in greens
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }
            // Top Right - Bottom Left
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in greens
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }

            #endregion

            #region No winners

            // Check for no winner and full board
            if (!mResults.Any(result => result == MarkType.Free))
            {
                // Game ended
                mGameEnded = true;

                // Turn cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }

            #endregion

        }
    }
}
