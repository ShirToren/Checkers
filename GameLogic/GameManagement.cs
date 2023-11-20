using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic
{
    public class GameManagement
    {
        private readonly DataBoard r_DataBoard;
        private Player m_CurrentPlayer;
        private Player m_OPlayer;
        private Player m_XPlayer;
        private bool m_PlayWithComputer;
        private eWinner m_Winner;

        public enum eWinner
        {
            XPlayer,
            OPlayer,
            Tie
        }

        public GameManagement(int i_SizeOfBoard)
        {
            r_DataBoard = new DataBoard(i_SizeOfBoard);
            InitPlayers();
        }

        public eWinner Winner
        {
            get { return m_Winner;}
        }

        public char CurrentTurnType
        {
            get { return m_CurrentPlayer.TypeOfMen; }
        }

        public bool PlayWithComputer
        {
            get { return m_PlayWithComputer; }
            set { m_PlayWithComputer = value; }
        }

        public Player CurrentTurnPlayer
        {
            get { return m_CurrentPlayer; }
        }

        public bool CurrentTurnPossibleMoves
        {
            get { return m_CurrentPlayer.PossibleMove; }
            set { m_CurrentPlayer.PossibleMove = value; }
        }

        public int OPlayerScore
        {
            get { return m_OPlayer.Score; }
        }

        public int XPlayerScore
        {
            get { return m_XPlayer.Score; }
        }

        public Player XPlayer
        {
            get { return m_XPlayer; }
        }

        public DataBoard DataBoard
        {
            get { return r_DataBoard; }
        }

        public void InitPlayers()
        {
            m_OPlayer = new Player(r_DataBoard.SizeOfBoard, 'O');
            m_XPlayer = new Player(r_DataBoard.SizeOfBoard, 'X');
            m_CurrentPlayer = m_OPlayer;
        }

        public void InitFirstPossibleMoves()
        {
            for (int i = 0; i < r_DataBoard.SizeOfBoard; i++)
            {
                for (int j = 0; j < r_DataBoard.SizeOfBoard; j++)
                {
                    if (r_DataBoard.FullBoard[i, j].Type == 'O')
                    {
                        m_OPlayer.AddSoldierToList(new PositionInBoard(i, j));
                    }
                    else if (r_DataBoard.FullBoard[i, j].Type == 'X')
                    {
                        m_XPlayer.AddSoldierToList(new PositionInBoard(i, j));
                    }
                }
            }

            updatePossibleMoves();
        }

        public void DeleteRivelsMen(PositionInBoard i_CurrentPosition, PositionInBoard i_NextPossition)
        {
            PositionInBoard eatenRivelSoldierPosition;
            int row;
            int col;

            if (i_CurrentPosition.Row < i_NextPossition.Row)
            {
                row = i_CurrentPosition.Row + 1;
            }
            else
            {
                row = i_CurrentPosition.Row - 1;
            }

            if (i_CurrentPosition.Col < i_NextPossition.Col)
            {
                col = i_CurrentPosition.Col + 1;
            }
            else
            {
                col = i_CurrentPosition.Col - 1;
            }

            eatenRivelSoldierPosition = new PositionInBoard(row, col);
            if (m_CurrentPlayer == m_OPlayer)
            {
                m_XPlayer.DeleteSoldierFromList(eatenRivelSoldierPosition);
                if (r_DataBoard.GetDataFromPosition(eatenRivelSoldierPosition).Type == 'K')
                {
                    m_XPlayer.NumberOfKings--;
                }

                m_XPlayer.NumberOfMens--;
            }
            else
            {
                m_OPlayer.DeleteSoldierFromList(eatenRivelSoldierPosition);
                if (r_DataBoard.GetDataFromPosition(eatenRivelSoldierPosition).Type == 'U')
                {
                    m_OPlayer.NumberOfKings--;
                }

                m_OPlayer.NumberOfMens--;
            }

            r_DataBoard.SetDataInPosition(' ', eatenRivelSoldierPosition);
        }

        public void ManageMove(PositionInBoard i_CurrentPosition, PositionInBoard i_NextPosition, out bool io_TryMove)
        {
            bool mustEatMove;

            if (m_PlayWithComputer == true && m_CurrentPlayer == m_XPlayer)
            {
                if(m_XPlayer.PossibleMove == true)
                {
                    getComputersMove(out i_CurrentPosition, out i_NextPosition);
                }
            }

            mustEatMove = checkIfAnySoldierMustEat();
            if (CheckIfValidMove(i_CurrentPosition, i_NextPosition, mustEatMove) == true)
            {
                io_TryMove = true;
                r_DataBoard.UpdateBoard(i_CurrentPosition, i_NextPosition, m_CurrentPlayer.CurrentSoldierMove.Type);
                m_CurrentPlayer.UpdatePositionOfSoldierInList(i_CurrentPosition, i_NextPosition);
                checkAndChangeToAKing(i_NextPosition);
                if (Math.Abs(i_CurrentPosition.Row - i_NextPosition.Row) == 2)
                {
                    DeleteRivelsMen(i_CurrentPosition, i_NextPosition);
                    updatePossibleMoves();
                    if (checkIfEatMove(m_CurrentPlayer.CurrentSoldierMove) == false)
                    {
                        switchTurn();
                    }
                }
                else
                {
                    updatePossibleMoves();
                    switchTurn();
                }
            }
            else
            {
                io_TryMove = false;
            }
        }

        private void switchTurn()
        {
            if (m_CurrentPlayer == m_OPlayer)
            {
                m_CurrentPlayer = m_XPlayer;
            }
            else
            {
                m_CurrentPlayer = m_OPlayer;
            }
        }

        public bool CheckIfValidMove(PositionInBoard i_CurrentPosition, PositionInBoard i_NextPosition, bool i_mustEatMove)
        {
            bool isValidMove = false;

            if (m_CurrentPlayer.TypeOfMen == 'O')
            {
                isValidMove = m_OPlayer.CheckIfPlayerCanMove(i_CurrentPosition, i_NextPosition) && checkIfEaten(i_CurrentPosition, i_NextPosition, i_mustEatMove);
            }
            else
            {
                isValidMove = m_XPlayer.CheckIfPlayerCanMove(i_CurrentPosition, i_NextPosition) && checkIfEaten(i_CurrentPosition, i_NextPosition, i_mustEatMove);
            }

            return isValidMove && r_DataBoard.GetDataFromPosition(i_NextPosition).Type == ' ';
        }

        private bool checkIfEaten(PositionInBoard i_CurrentPosition, PositionInBoard i_NextPosition, bool i_mustEatMove)
        {
            bool isValidMove = true;

            if (i_mustEatMove == true && isEaten(i_CurrentPosition, i_NextPosition) ==  false)
            {
                isValidMove = false;
            }

            return isValidMove;
        }

        private bool isEaten(PositionInBoard i_CurrentPosition, PositionInBoard i_NextPosition)
        {
            bool valid = false;

            foreach(PositionInBoard position in m_CurrentPlayer.CurrentSoldierMove.PossibleEatMoves)
            {
                if(position.Row == i_NextPosition.Row && position.Col == i_NextPosition.Col)
                {
                    valid = true;
                }
            }

            return valid;
        }

        private bool checkIfAnySoldierMustEat()
        {
            bool isMustEat = false;

            foreach (Soldier playersSoldir in m_CurrentPlayer.SoldiersList)
            {
                if (checkIfEatMove(playersSoldir) == true)
                {
                    isMustEat = true;
                    break;
                }
            }

            return isMustEat;
        }

        private bool checkIfEatMove(Soldier i_soldierToMove)
        {
            bool isMustEat = false;

            if (i_soldierToMove.CheckIfSoldierCanEat() == true)
            {
                isMustEat = true;
            }

            return isMustEat;
        }

        public bool IsEndOfGame()
        {
            bool isEndOfGame = false;

            if (m_CurrentPlayer == m_OPlayer && (m_CurrentPlayer.NumberOfMens == 0 || m_CurrentPlayer.PossibleMove == false))
            {
                addScoreIfThereIsAWinner(ref m_OPlayer);
                isEndOfGame = true;
                m_Winner = eWinner.XPlayer;
            }
            else if (m_CurrentPlayer == m_XPlayer && (m_CurrentPlayer.NumberOfMens == 0 || m_CurrentPlayer.PossibleMove == false))
            {
                addScoreIfThereIsAWinner(ref m_XPlayer);
                isEndOfGame = true;
                m_Winner = eWinner.OPlayer;
            }
            else if (m_OPlayer.PossibleMove == false && m_XPlayer.PossibleMove == false)
            {
                m_XPlayer.Score += m_XPlayer.NumberOfMens - m_XPlayer.NumberOfKings + (m_XPlayer.NumberOfKings * 4);
                m_OPlayer.Score += m_OPlayer.NumberOfMens - m_OPlayer.NumberOfKings + (m_OPlayer.NumberOfKings * 4);
                isEndOfGame = true;
                m_Winner = eWinner.Tie;
            }

            return isEndOfGame;
        }

        private void addScoreIfThereIsAWinner(ref Player i_IsTheLooser)
        {
            if (i_IsTheLooser.Equals(m_OPlayer))
            {
                m_XPlayer.Score += (m_XPlayer.NumberOfMens - m_XPlayer.NumberOfKings) + (m_XPlayer.NumberOfKings * 4);
            }
            else
            {
                m_OPlayer.Score += (m_OPlayer.NumberOfMens - m_OPlayer.NumberOfKings) + (m_OPlayer.NumberOfKings * 4);
            }
        }

        private void checkAndChangeToAKing(PositionInBoard i_NextPosition)
        {
            if (m_CurrentPlayer.TypeOfMen == 'O' && i_NextPosition.Row == r_DataBoard.SizeOfBoard - 1)
            {
                r_DataBoard.SetDataInPosition('U', i_NextPosition);
                m_CurrentPlayer.CurrentSoldierMove.Type = 'U';
                m_CurrentPlayer.NumberOfKings++;
            }
            else if (m_CurrentPlayer.TypeOfMen == 'X' && i_NextPosition.Row == 0)
            {
                r_DataBoard.SetDataInPosition('K', i_NextPosition);
                m_CurrentPlayer.CurrentSoldierMove.Type = 'K';
                m_CurrentPlayer.NumberOfKings++;
            }
        }

        private void updatePossibleMoves()
        {
            findPossiblePlyersMoves(m_OPlayer);
            findPossiblePlyersMoves(m_XPlayer);
        }

        private void findPossiblePlyersMoves(Player i_PlayerToCheck)
        {
            bool isPossibleMoveForAPlayer = false;

            foreach (Soldier soldier in i_PlayerToCheck.SoldiersList)
            {
                updatePossibleMovesForOneSoldier(soldier);
                if (isPossibleMoveForAPlayer != true)
                {
                    isPossibleMoveForAPlayer = soldier.CheckIfAnyPossibleMoves();
                }
            }

            if (isPossibleMoveForAPlayer == false)
            {
                i_PlayerToCheck.PossibleMove = false;
            }
        }

        private void updatePossibleMovesForOneSoldier(Soldier i_SoldierToUpdate)
        {
            int border = r_DataBoard.SizeOfBoard - 1;

            i_SoldierToUpdate.PossibleRegularMoves.Clear();
            i_SoldierToUpdate.PossibleEatMoves.Clear();
            if (i_SoldierToUpdate.Type != 'X')
            {
                if (i_SoldierToUpdate.CurrentPosition.Row + 1 <= border && i_SoldierToUpdate.CurrentPosition.Col + 1 <= border &&
                    isEmptyPosition(i_SoldierToUpdate.CurrentPosition.Row + 1, i_SoldierToUpdate.CurrentPosition.Col + 1))
                {
                    i_SoldierToUpdate.PossibleRegularMoves.Add(new PositionInBoard(i_SoldierToUpdate.CurrentPosition.Row + 1, i_SoldierToUpdate.CurrentPosition.Col + 1));
                }

                if (i_SoldierToUpdate.CurrentPosition.Row + 1 <= border && i_SoldierToUpdate.CurrentPosition.Col - 1 >= 0 &&
                    isEmptyPosition(i_SoldierToUpdate.CurrentPosition.Row + 1, i_SoldierToUpdate.CurrentPosition.Col - 1))
                {
                    i_SoldierToUpdate.PossibleRegularMoves.Add(new PositionInBoard(i_SoldierToUpdate.CurrentPosition.Row + 1, i_SoldierToUpdate.CurrentPosition.Col - 1));
                }

                if (i_SoldierToUpdate.CurrentPosition.Row + 2 <= border && i_SoldierToUpdate.CurrentPosition.Col + 2 <= border &&
                    isEmptyPosition(i_SoldierToUpdate.CurrentPosition.Row + 2, i_SoldierToUpdate.CurrentPosition.Col + 2) &&
                    checkIfEatRivel(new PositionInBoard(i_SoldierToUpdate.CurrentPosition.Row + 1, i_SoldierToUpdate.CurrentPosition.Col + 1), i_SoldierToUpdate))
                {
                    i_SoldierToUpdate.PossibleEatMoves.Add(new PositionInBoard(i_SoldierToUpdate.CurrentPosition.Row + 2, i_SoldierToUpdate.CurrentPosition.Col + 2));
                }

                if (i_SoldierToUpdate.CurrentPosition.Row + 2 <= border && i_SoldierToUpdate.CurrentPosition.Col - 2 >= 0 &&
                     isEmptyPosition(i_SoldierToUpdate.CurrentPosition.Row + 2, i_SoldierToUpdate.CurrentPosition.Col - 2) &&
                     checkIfEatRivel(new PositionInBoard(i_SoldierToUpdate.CurrentPosition.Row + 1, i_SoldierToUpdate.CurrentPosition.Col - 1), i_SoldierToUpdate))
                {
                    i_SoldierToUpdate.PossibleEatMoves.Add(new PositionInBoard(i_SoldierToUpdate.CurrentPosition.Row + 2, i_SoldierToUpdate.CurrentPosition.Col - 2));
                }
            }

            if (i_SoldierToUpdate.Type != 'O')
            {
                if (i_SoldierToUpdate.CurrentPosition.Row - 1 >= 0 && i_SoldierToUpdate.CurrentPosition.Col + 1 <= border &&
                    isEmptyPosition(i_SoldierToUpdate.CurrentPosition.Row - 1, i_SoldierToUpdate.CurrentPosition.Col + 1))
                {
                    i_SoldierToUpdate.PossibleRegularMoves.Add(new PositionInBoard(i_SoldierToUpdate.CurrentPosition.Row - 1, i_SoldierToUpdate.CurrentPosition.Col + 1));
                }

                if (i_SoldierToUpdate.CurrentPosition.Row - 1 >= 0 && i_SoldierToUpdate.CurrentPosition.Col - 1 >= 0 &&
                    isEmptyPosition(i_SoldierToUpdate.CurrentPosition.Row - 1, i_SoldierToUpdate.CurrentPosition.Col - 1))
                {
                    i_SoldierToUpdate.PossibleRegularMoves.Add(new PositionInBoard(i_SoldierToUpdate.CurrentPosition.Row - 1, i_SoldierToUpdate.CurrentPosition.Col - 1));
                }

                if (i_SoldierToUpdate.CurrentPosition.Row - 2 >= 0 && i_SoldierToUpdate.CurrentPosition.Col + 2 <= border &&
                    isEmptyPosition(i_SoldierToUpdate.CurrentPosition.Row - 2, i_SoldierToUpdate.CurrentPosition.Col + 2) &&
                    checkIfEatRivel(new PositionInBoard(i_SoldierToUpdate.CurrentPosition.Row - 1, i_SoldierToUpdate.CurrentPosition.Col + 1), i_SoldierToUpdate))
                {
                    i_SoldierToUpdate.PossibleEatMoves.Add(new PositionInBoard(i_SoldierToUpdate.CurrentPosition.Row - 2, i_SoldierToUpdate.CurrentPosition.Col + 2));
                }

                if (i_SoldierToUpdate.CurrentPosition.Row - 2 >= 0 && i_SoldierToUpdate.CurrentPosition.Col - 2 >= 0 &&
                     isEmptyPosition(i_SoldierToUpdate.CurrentPosition.Row - 2, i_SoldierToUpdate.CurrentPosition.Col - 2) &&
                     checkIfEatRivel(new PositionInBoard(i_SoldierToUpdate.CurrentPosition.Row - 1, i_SoldierToUpdate.CurrentPosition.Col - 1), i_SoldierToUpdate))
                {
                    i_SoldierToUpdate.PossibleEatMoves.Add(new PositionInBoard(i_SoldierToUpdate.CurrentPosition.Row - 2, i_SoldierToUpdate.CurrentPosition.Col - 2));
                }
            }
        }

        private bool isEmptyPosition(int i_Row, int i_Col)
        {
            PositionInBoard positionToCheck = new PositionInBoard(i_Row, i_Col);

            return r_DataBoard.GetDataFromPosition(positionToCheck).Type == ' ';
        }

        private bool checkIfEatRivel(PositionInBoard i_SkipedMenPosition, Soldier i_SoldierToCheck)
        {
            bool eatMove = false;

            if (i_SoldierToCheck.Type == 'O' || i_SoldierToCheck.Type == 'U')
            {
                if (r_DataBoard.GetDataFromPosition(i_SkipedMenPosition).Type == 'X' || r_DataBoard.GetDataFromPosition(i_SkipedMenPosition).Type == 'K')
                {
                    eatMove = true;
                }
            }
            else
            {
                if (r_DataBoard.GetDataFromPosition(i_SkipedMenPosition).Type == 'O' || r_DataBoard.GetDataFromPosition(i_SkipedMenPosition).Type == 'U')
                {
                    eatMove = true;
                }
            }

            return eatMove;
        }

        private void getComputersMove(out PositionInBoard o_CurrentPosition, out PositionInBoard o_NextPosition)
        {
            int numOfSoldiers = m_XPlayer.NumberOfMens;
            Random random = new Random();
            int numberOfSoldier = random.Next(0, numOfSoldiers);

            if (checkIfAnySoldierMustEat())
            {
                while (m_XPlayer.SoldiersList[numberOfSoldier].PossibleEatMoves.Count() == 0)
                {
                    numberOfSoldier = random.Next(0, numOfSoldiers);
                }

                o_CurrentPosition = new PositionInBoard(m_XPlayer.SoldiersList[numberOfSoldier].CurrentPosition.Row, m_XPlayer.SoldiersList[numberOfSoldier].CurrentPosition.Col);
                o_NextPosition = new PositionInBoard(m_XPlayer.SoldiersList[numberOfSoldier].PossibleEatMoves[0].Row, m_XPlayer.SoldiersList[numberOfSoldier].PossibleEatMoves[0].Col);
            }
            else
            {
                while (m_XPlayer.SoldiersList[numberOfSoldier].PossibleRegularMoves.Count() == 0)
                {
                    numberOfSoldier = random.Next(0, numOfSoldiers);
                }

                o_CurrentPosition = new PositionInBoard(m_XPlayer.SoldiersList[numberOfSoldier].CurrentPosition.Row, m_XPlayer.SoldiersList[numberOfSoldier].CurrentPosition.Col);
                o_NextPosition = new PositionInBoard(m_XPlayer.SoldiersList[numberOfSoldier].PossibleRegularMoves[0].Row, m_XPlayer.SoldiersList[numberOfSoldier].PossibleRegularMoves[0].Col);
            }
        }
    }
}
