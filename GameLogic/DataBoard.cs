using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic
{
    public class DataBoard
    {
        private readonly Cell[,] r_Board;
        private readonly int r_SizeOfBoard;

        public DataBoard(int i_SizeOfBoard)
        {
            r_SizeOfBoard = i_SizeOfBoard;
            r_Board = new Cell[i_SizeOfBoard, i_SizeOfBoard];
            for (int i = 0; i < r_SizeOfBoard; i++)
            {
                for (int j = 0; j < r_SizeOfBoard; j++)
                {
                    r_Board[i, j] = new Cell(i, j);
                }
            }
        }

        public void InitDataBoard()
        {
            makeEmptyBoard();
            for (int i = 0; i < (r_SizeOfBoard / 2 - 1); i++)
            {
                for (int j = 0; j < r_SizeOfBoard; j++)
                {
                    if ((j % 2 != 0 && i % 2 == 0 && i < (r_SizeOfBoard / 2 - 1)) || (j % 2 == 0 && i % 2 != 0 && i < (r_SizeOfBoard / 2 - 1)))
                    {
                        r_Board[i, j].ChangeCell('O');
                    }
                }
            }

            for (int i = (r_SizeOfBoard / 2 - 1); i < r_SizeOfBoard; i++)
            {
                for (int j = 0; j < r_SizeOfBoard; j++)
                {
                    if ((j % 2 == 0 && i % 2 != 0 && i > (r_SizeOfBoard / 2)) || (j % 2 != 0 && i % 2 == 0 && i > (r_SizeOfBoard / 2)))
                    {
                        r_Board[i, j].ChangeCell('X');
                    }
                }
            }
        }

        private void makeEmptyBoard()
        {
            for (int i = 0; i < r_SizeOfBoard; i++)
            {
                for (int j = 0; j < r_SizeOfBoard; j++)
                {
                    r_Board[i, j].ChangeCell(' ');
                }
            }
        }

        public Cell[,] FullBoard
        {
            get { return r_Board; }
        }

        public int SizeOfBoard
        {
            get { return r_SizeOfBoard; }
        }

        public Cell GetDataFromPosition(int i_Row, int i_Col)
        {
            return r_Board[i_Row, i_Col];
        }

        public Cell GetDataFromPosition(PositionInBoard i_Position)
        {
            return r_Board[i_Position.Row, i_Position.Col];
        }

        public void SetDataInPosition(char i_DataToSet, PositionInBoard i_PositionToSet)
        {
            r_Board[i_PositionToSet.Row, i_PositionToSet.Col].ChangeCell(i_DataToSet);
        }

        public void UpdateBoard(PositionInBoard i_CurrentPosition, PositionInBoard i_NextPosition, char i_TypeOfMen)
        {
            SetDataInPosition(' ', i_CurrentPosition);
            SetDataInPosition(i_TypeOfMen, i_NextPosition);
        }
    }
}

