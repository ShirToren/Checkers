using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GameLogic;

namespace UserInterface
{
    internal class GameSoldier : Button
    {
        private Cell m_Cell;

        public Cell Cell
        {
            get { return m_Cell; }
            set { m_Cell = value;}
        }
    }
}
