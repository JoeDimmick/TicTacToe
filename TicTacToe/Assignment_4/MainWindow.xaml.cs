using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Assignment_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Members
        /// <summary>
        /// Fields and objects
        /// </summary>
        private Tic_Tac_Toe gameLogic = new Tic_Tac_Toe();
        #endregion

        /// <summary>
        /// Builds the window UI and game logic to play Tic Tak Toe
        /// </summary>
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            NewGame();            
        }
        #endregion
        #region Private Methods
        /// <summary>
        /// Sets up a new game. Resets game logic and visual buttons.
        /// </summary>
        private void NewGame()
        {
            gameLogic.StartGame();
            VisualBoard.Children.Cast<Button>().ToList().ForEach(button => //grabbing each button.
            {
                button.Content = string.Empty;
                button.Background = Brushes.Gray;
                button.Foreground = Brushes.Black;
            });
        }
        /// <summary>
        /// when a tile has been selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (gameLogic.getGameStatus() == false)
            {
                //Don't do anything if the game is not active
                return;
            }
            //converting the 2dimensional location of the button to a single index.
            var button = (Button)sender; 
            int row = Grid.GetRow(button);
            int column = Grid.GetColumn(button);
            int tile = column + (row * 3);
            /* column + (row * numberOfRows) Thank you mahjong and CS 3230
             *  0 | 1 | 2           0,0 | 0,1 | 0,2
             *  3 | 4 | 5           1,0 | 1,1 | 1,2
             *  6 | 7 | 8           2,0 | 2,1 | 2,2
             */
            gameLogic.Declare_Move(tile);
            switch (gameLogic.getTile(tile))
            {
                case Symbol.CROSS: 
                    button.Content = "X";
                    button.Background = Brushes.Red;
                    button.Foreground = Brushes.White;
                    break;
                case Symbol.NAUGHT:
                    button.Content = "O";
                    button.Background = Brushes.Blue;
                    button.Foreground = Brushes.Yellow;
                    break;
                case Symbol.EMPTY:
                    break;
                default:
                    break;
            }

            UpdateUI();
        }
        /// <summary>
        /// handle th new game button click. creates a new game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
            UpdateUI();
        }
        /// <summary>
        /// Updates the game status and game statistics to reflect current values
        /// </summary>
        private void UpdateUI()
        {
            if (gameLogic.getGameStatus() == false)
            {
                if(gameLogic.checkTie() == true)
                {
                    gamestatuslabel.Content = $"Game has ended in a tie.";
                    VisualBoard.Children.Cast<Button>().ToList().ForEach(button => //grabbing each button.
                    {
                        button.Background = Brushes.Salmon;
                        button.Foreground = Brushes.Black;
                    });

                }
                else
                {
                    if (gameLogic.CurrentPlayer() == 1)
                    {
                        VisualBoard.Children.Cast<Button>().ToList().ForEach(button => //grabbing each button.
                        {
                            if ((string)button.Content == "X")
                            {
                                button.Background = Brushes.Yellow;
                                button.Foreground = Brushes.Black;
                            }
                        });
                        gamestatuslabel.Content = $"Player {gameLogic.CurrentPlayer()} Won!";
                    }
                    else
                    {
                        if ((gameLogic.CurrentPlayer() == 2))
                        {
                            VisualBoard.Children.Cast<Button>().ToList().ForEach(button => //grabbing each button.
                            {
                                if ((string)button.Content == "O")
                                {
                                    button.Background = Brushes.Yellow;
                                    button.Foreground = Brushes.Black;
                                }
                            });
                        }
                        gamestatuslabel.Content = $"Player {gameLogic.CurrentPlayer()} Won!";
                    }
                }
            }
            else
            {
                gamestatuslabel.Content = $" Player {gameLogic.CurrentPlayer()} turn";
            }            
            player1label.Content = $"{gameLogic.getPlayerWins(1)}";
            player2label.Content = $"{gameLogic.getPlayerWins(2)}";
            tieslabel.Content = $"{gameLogic.getTies()}";
        }
        #endregion
    }
}
