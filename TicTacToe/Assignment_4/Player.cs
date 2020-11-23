using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_4
{
    class Player
    {
        #region Members
        /// <summary>
        /// Each player object will have a Symbol and Track their own #of Wins
        /// </summary>
        private int Number_Of_Wins;
        private Symbol playerSymbol;
        #endregion

        #region Public Methods
        /// <summary>
        /// Pass the assigned player Symbol to the player when created.
        /// </summary>
        /// <param name="Symbol"></param>
        public Player(Symbol Symbol)
        {
            Number_Of_Wins = 0;
            playerSymbol = Symbol;
        }

        /// <summary>
        /// return the number of times this player has won as an int
        /// </summary>
        /// <returns></returns>
        public int getNumber_Of_Wins()
        {
            return Number_Of_Wins;
        }

        /// <summary>
        /// returns the players Symbol
        /// </summary>
        /// <returns></returns>
        public Symbol GetPlayerSymbol()
        {
            return playerSymbol;
        }

        /// <summary>
        /// increase this players win count by 1
        /// </summary>
        public void Wins()
        {
            Number_Of_Wins++;
        }
        #endregion
    }

}
