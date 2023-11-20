using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic
{
    public class PositionInBoard
    {
        private int m_RowPosition;
        private int m_ColPosition;

        public PositionInBoard(int i_Row, int i_Col)
        {
            m_RowPosition = i_Row;
            m_ColPosition = i_Col;
        }

        public int Row
        {
            get { return m_RowPosition; }
            set { m_RowPosition = value; }
        }

        public int Col
        {
            get { return m_ColPosition; }
            set { m_ColPosition = value; }
        }
    }
}
