using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic
{
    public class Cell
    {
        private char m_Type;
        private readonly int r_Row;
        private readonly int r_Col;

        public event Action<Cell> Changed;

        public Cell(int i_Row , int i_Col)
        {
            r_Row = i_Row;
            r_Col = i_Col;
            m_Type = ' ';
        }

        public int Row
        {
            get { return r_Row; }
        }

        public int Col
        {
            get { return r_Col; }
        }

        public char Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }

        public void ChangeCell(char i_Type)
        {
            m_Type = i_Type;
            OnChanged();
        }

        protected virtual void OnChanged()
        {
            if (Changed != null)
            {
                Changed.Invoke(this);
            }
        }
    }
}
