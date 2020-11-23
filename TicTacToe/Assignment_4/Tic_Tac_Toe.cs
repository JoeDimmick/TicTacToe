using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Assignment_4
{
    class Tic_Tac_Toe
    {
        #region Members;
        /// <summary>
        /// Single dimensional Array. Model for Tic Tak Toe
        /// [ 0 | 1 | 2 ] 
        /// [ 3 | 4 | 5 ] 
        /// [ 6 | 7 | 8 ]
        /// </summary>
        private Symbol[] Game_Board;
        /// <summary>
        /// Player objects so each player can track it's own number of wins and Symbol
        /// </summary>
        private Player x_Player1;
        private Player o_Player2;
        /// <summary>
        /// int to track number of tie games.
        /// </summary>
        private int number_of_Ties;
        /// <summary>
        /// Boolean to identify whos turn it is
        /// </summary>
        private bool IsPlayer1;
        /// <summary>
        /// Boolean to track if game is still playing
        /// </summary>
        private bool IsGamePlaying;
        #endregion

        #region Constructor
        /// <summary>
        /// intialized the game Logic
        /// </summary>
        public Tic_Tac_Toe()
        {
            x_Player1 = new Player(Symbol.CROSS);
            o_Player2 = new Player(Symbol.NAUGHT);
            StartGame();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets all tiles to empty and hands control to player 1
        /// </summary>
        public void StartGame()
        {
            IsPlayer1 = true;
            IsGamePlaying = true;

            Game_Board = new Symbol[9];

            for (int i = 0; i < Game_Board.Length; i++)
            {
                Game_Board[i] = Symbol.EMPTY;
            }
        }
        /// <summary>
        /// Method to designed to be called when player declares a tile.
        /// varifies game is active, tile is open.
        /// reserves tile for player in control, check if tile creates a tie,
        /// checks win conditions and awards player a win.
        /// toggle player control.
        /// </summary>
        /// <param name="tile"></param>
        public void Declare_Move(int tile)
        {
            //Is the game playing
            if (IsGamePlaying)
            { 
                ClaimTile(tile);//ClaimTile for the player
                if(checkTie() == true)//check if that tile creates a tie condition
                {
                    number_of_Ties++;
                    IsGamePlaying = false;
                }
                if (checkWinConditions() == true) //check if that tile creates a win condition
                {
                    // if player meets a win stop the game
                    if (IsPlayer1 == true)// and award player with a win
                    {
                        x_Player1.Wins();
                        IsGamePlaying = false;
                    }
                    else
                    {
                        o_Player2.Wins();
                        IsGamePlaying = false;
                    }
                }
                else
                {//if no win/tie condition is met switch player control.
                    SwitchPlayers();
                }
            }
        }
        /// <summary>
        /// returns the current player in control and player symbol as a string
        /// </summary>
        /// <returns></returns>
        public int CurrentPlayer()
        {
            if(IsPlayer1 == true)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        /// <summary>
        /// get the number of wins for the given player.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public int getPlayerWins(int player)
        {
            if(player == 1)
            {
                return x_Player1.getNumber_Of_Wins();
            }
            if (player == 2)
            {
                return o_Player2.getNumber_Of_Wins();
            }
            else return -1;
        }
        /// <summary>
        /// Accessor to get the status of the game for the UI.
        /// </summary>
        /// <returns></returns>
        public bool getGameStatus()
        {
            return IsGamePlaying;
        }
        /// <summary>
        /// gets the symbol of the given tile.
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        public Symbol getTile(int tile)
        {
            return Game_Board[tile];
        }
        /// <summary>
        /// returns the number of tie games
        /// </summary>
        /// <returns></returns>
        public int getTies()
        {
            return number_of_Ties;
        }
        /// <summary>
        /// Checkes if the game is a currently a tie. by seeing if there are
        /// any more empty tiles
        /// </summary>
        /// <returns></returns>
        public bool checkTie()
        {//if no tiles are empty and no win conditions are met game is a tie
            if ((!Game_Board.Any(tile => tile == Symbol.EMPTY)) &&
                checkWinConditions() == false) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// returns true if tile is available of the given
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        private bool isTileClaimed(int tile)
        {
            if (Game_Board[tile] == Symbol.EMPTY)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// works as a toggle for the player currently playing.
        /// </summary>
        private void SwitchPlayers()
        {
            IsPlayer1 = !IsPlayer1;
        }
        /// <summary>
        /// If selected Tile has not been claimed, then tile will be claimed for the corresponding player.
        /// increments number_of_moves if tile is claimed.
        /// </summary>
        /// <param name="tile"></param>
        private void ClaimTile(int tile)
        {
            if (isTileClaimed(tile) == false)
            {
                if (IsPlayer1 == true)
                {
                    Game_Board[tile] = x_Player1.GetPlayerSymbol();

                }
                else
                {
                    Game_Board[tile] = o_Player2.GetPlayerSymbol();
                }
            }
        }
        /// <summary>
        /// check the 3 possible win conditions if a player has
        /// 3 in a row across any row
        /// 3 in a row down any col
        /// 3 in a row diagonally
        /// </summary>
        /// <returns></returns>
        private bool checkWinConditions()
        { 

            if(checkDiagnolWin() == false && CheckRowWin() == false && 
                checkColumnWin() == false)
            {                 
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Check for diagonal win. Dialgnol wins {0, 4, 8} || {2, 4, 6}
        /// </summary>
        /// <returns></returns>
        private bool checkDiagnolWin()
        {
            if ((Game_Board[4] != Symbol.EMPTY) && ((Game_Board[0] == Game_Board[8]) && (Game_Board[4] == Game_Board[0])))
            {
                return true;
            }
            else if ((Game_Board[4] != Symbol.EMPTY) && ((Game_Board[2] == Game_Board[6]) && (Game_Board[4] == Game_Board[2])))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Check for a row win. Row wins {0, 1, 2} || {3, 4, 5} } || {6, 7, 8}
        /// </summary>
        /// <returns></returns>
        private bool CheckRowWin()
        {
            if ((Game_Board[0] != Symbol.EMPTY)&&((Game_Board[1] == Game_Board[2])&&(Game_Board[1]==Game_Board[0])))
            {
                return true;
            }
            else if((Game_Board[3] != Symbol.EMPTY) && ((Game_Board[4] == Game_Board[5]) && (Game_Board[4] == Game_Board[3])))
            {
                return true;
            }
            else if ((Game_Board[6] != Symbol.EMPTY) && ((Game_Board[7] == Game_Board[8]) && (Game_Board[7] == Game_Board[6])))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Check for win by column. Col wins {0, 3, 6} || {1, 4, 7} || {2, 5, 8}
        /// </summary>
        /// <returns></returns>
        private bool checkColumnWin()
        {
            if ((Game_Board[0] != Symbol.EMPTY) && ((Game_Board[3] == Game_Board[6]) && (Game_Board[3] == Game_Board[0])))
            {
                return true;
            }
            else if ((Game_Board[1] != Symbol.EMPTY) && ((Game_Board[4] == Game_Board[7]) && (Game_Board[4] == Game_Board[1])))
            {
                return true;
            }
            else if ((Game_Board[2] != Symbol.EMPTY) && ((Game_Board[5] == Game_Board[8]) && (Game_Board[5] == Game_Board[2])))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
