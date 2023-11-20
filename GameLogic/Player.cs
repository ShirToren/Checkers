using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic
{
    public class Player
    {
        private readonly List<Soldier> r_SoldiersList;
        private int m_NumberOfMens;
        private int m_NumberOfKings;
        private int m_Score;
        private char m_TypeOfMen;
        private bool m_PossibleMove;
        private Soldier m_CurrentSoldierMove;

        public Player(int i_SizeOfBoard, char i_TypeOfMen)
        {
            r_SoldiersList = new List<Soldier>();
            m_NumberOfMens = (i_SizeOfBoard / 2) * (i_SizeOfBoard / 2 - 1);
            m_NumberOfKings = 0;
            m_Score = 0;
            m_TypeOfMen = i_TypeOfMen;
            m_PossibleMove = true;
        }

        public bool PossibleMove
        {
            get { return m_PossibleMove; }
            set { m_PossibleMove = value; }
        }

        public Soldier CurrentSoldierMove
        {
            get { return m_CurrentSoldierMove; }
        }

        public List<Soldier> SoldiersList
        {
            get { return r_SoldiersList; }
        }

        public char TypeOfMen
        {
            get { return m_TypeOfMen; }
        }

        public int NumberOfKings
        {
            get { return m_NumberOfKings; }
            set { m_NumberOfKings = value; }
        }

        public int NumberOfMens
        {
            get { return m_NumberOfMens; }
            set { m_NumberOfMens = value; }
        }

        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

        public void UpdatePositionOfSoldierInList(PositionInBoard i_PositionToChange, PositionInBoard i_NewPosition)
        {
            for (int i = 0; i < r_SoldiersList.Count; i++)
            {
                if (r_SoldiersList[i].CurrentPosition.Col == i_PositionToChange.Col && r_SoldiersList[i].CurrentPosition.Row == i_PositionToChange.Row)
                {
                    r_SoldiersList[i].CurrentPosition.Row = i_NewPosition.Row;
                    r_SoldiersList[i].CurrentPosition.Col = i_NewPosition.Col;
                    m_CurrentSoldierMove.CurrentPosition.Row = i_NewPosition.Row;
                    m_CurrentSoldierMove.CurrentPosition.Col = i_NewPosition.Col;
                }
            }
        }

        public void DeleteSoldierFromList(PositionInBoard i_PositionToDelete)
        {
            Soldier soldierToDelete;

            foreach (Soldier soldier in r_SoldiersList)
            {
                if (soldier.CurrentPosition.Col == i_PositionToDelete.Col && soldier.CurrentPosition.Row == i_PositionToDelete.Row)
                {
                    soldierToDelete = soldier;
                    r_SoldiersList.Remove(soldierToDelete);
                    break;
                }
            }
        }

        public void AddSoldierToList(PositionInBoard i_PositionToAdd)
        {
            r_SoldiersList.Add(new Soldier(i_PositionToAdd.Row, i_PositionToAdd.Col, m_TypeOfMen));
        }

        public bool CheckIfPlayerCanMove(PositionInBoard i_CurrentPosition, PositionInBoard i_NextPosition)
        {
            bool canMove = false;

            foreach (Soldier onePlayersoldier in r_SoldiersList)
            {
                if (onePlayersoldier.CurrentPosition.Row == i_CurrentPosition.Row && onePlayersoldier.CurrentPosition.Col == i_CurrentPosition.Col && onePlayersoldier.CheckIfSoldierCanMoveToPosition(i_NextPosition))
                {
                    canMove = true;
                    m_CurrentSoldierMove = onePlayersoldier;
                    break;
                }
            }

            return canMove;
        }

        public bool CheckIfPlayerCanEat()
        {
            bool canEat = false;

            foreach (Soldier onePlayersoldier in r_SoldiersList)
            {
                if (onePlayersoldier.CheckIfSoldierCanEat() == true)
                {
                    canEat = true;
                }
            }

            return canEat;
        }
    }
}
