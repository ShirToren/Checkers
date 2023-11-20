using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic
{
    public class Soldier
    {
        private readonly PositionInBoard r_CurrentPosition;
        private readonly List<PositionInBoard> r_PossibleRegularMoves;
        private readonly List<PositionInBoard> r_PossibleEatMoves;
        private char m_Type;

        internal Soldier(int i_StartPositionRow, int i_StartPositionCol, char i_Type)
        {
            r_CurrentPosition = new PositionInBoard(i_StartPositionRow, i_StartPositionCol);
            r_PossibleRegularMoves = new List<PositionInBoard>();
            r_PossibleEatMoves = new List<PositionInBoard>();
            m_Type = i_Type;
        }

        public List<PositionInBoard> PossibleRegularMoves
        {
            get { return r_PossibleRegularMoves; }
        }

        public char Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }

        public List<PositionInBoard> PossibleEatMoves
        {
            get { return r_PossibleEatMoves; }
        }

        public PositionInBoard CurrentPosition
        {
            get { return r_CurrentPosition; }
        }

        public bool CheckIfSoldierCanMoveToPosition(PositionInBoard i_NextPosition)
        {
            bool canMove = false;

            foreach (PositionInBoard possiblePosition in r_PossibleRegularMoves)
            {
                if (possiblePosition.Row == i_NextPosition.Row && possiblePosition.Col == i_NextPosition.Col)
                {
                    canMove = true;
                }
            }

            foreach (PositionInBoard possiblePosition in r_PossibleEatMoves)
            {
                if (possiblePosition.Row == i_NextPosition.Row && possiblePosition.Col == i_NextPosition.Col)
                {
                    canMove = true;
                }
            }

            return canMove;
        }

        public bool CheckIfAnyPossibleMoves()
        {
            bool isPossibleMove = CheckIfSoldierCanEat() || r_PossibleRegularMoves.Count() > 0;

            return isPossibleMove;
        }

        public bool CheckIfSoldierCanEat()
        {
            bool canEat = false;

            if (r_PossibleEatMoves.Count > 0)
            {
                canEat = true;
            }

            return canEat;
        }
    }
}
