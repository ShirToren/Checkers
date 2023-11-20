using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GameLogic;
using System.IO;

namespace UserInterface
{
    public partial class GameForm : Form
    {
        private readonly GameManagement r_GameLogicManger;
        private readonly EntryForm r_Settings;
        private readonly int r_BoardSize;
        private readonly GameSoldier[,] r_ButtonsBoard;
        private readonly string r_Player1Name;
        private readonly string r_Player2Name;
        private PositionInBoard m_CurrentSoldierToMove;
        private PositionInBoard m_NextPosition;

        public GameForm()
        {
            r_Settings = new EntryForm();
            r_Settings.ShowDialog();
            if (r_Settings.DialogResult == DialogResult.OK)
            {
                r_Player1Name = r_Settings.Player1Name;
                r_Player2Name = r_Settings.Player2Name;
                r_BoardSize = r_Settings.SelectedRadioSize;
                r_GameLogicManger = new GameManagement(r_BoardSize);
                r_GameLogicManger.PlayWithComputer = !r_Settings.TwoPlayers;
                r_ButtonsBoard = new GameSoldier[r_BoardSize, r_BoardSize];
                InitializeComponent(r_BoardSize);
                r_GameLogicManger.DataBoard.InitDataBoard();
                r_GameLogicManger.InitFirstPossibleMoves();
            }
            else
            {
                this.Close();
            }
        }

        public void Cell_Changed(Cell i_Cell)
        {
            switch (i_Cell.Type)
            {
                case 'O':
                    r_ButtonsBoard[i_Cell.Row, i_Cell.Col].BackgroundImage = Properties.Resources.RedSoldier;
                    r_ButtonsBoard[i_Cell.Row, i_Cell.Col].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    break;
                case 'X':
                    r_ButtonsBoard[i_Cell.Row, i_Cell.Col].BackgroundImage = Properties.Resources.BlackSoldier;
                    r_ButtonsBoard[i_Cell.Row, i_Cell.Col].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    break;
                case ' ':
                    r_ButtonsBoard[i_Cell.Row, i_Cell.Col].BackgroundImage = null;
                    break;
                case 'K':
                    r_ButtonsBoard[i_Cell.Row, i_Cell.Col].BackgroundImage = Properties.Resources.BlackKing;
                    r_ButtonsBoard[i_Cell.Row, i_Cell.Col].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    break;
                case 'U':
                    r_ButtonsBoard[i_Cell.Row, i_Cell.Col].BackgroundImage = Properties.Resources.RedKing;
                    r_ButtonsBoard[i_Cell.Row, i_Cell.Col].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    break;
            }
        }

        private void timerComputer_Tick(object sender, EventArgs e)
        {
            bool tryMove;

            if(r_GameLogicManger.PlayWithComputer == true && r_GameLogicManger.CurrentTurnPlayer == r_GameLogicManger.XPlayer)
            {
                r_GameLogicManger.ManageMove(m_CurrentSoldierToMove, m_NextPosition, out tryMove);
                setTurnColor();
                (sender as Timer).Stop();
                checkEndOfGame();
            }
        }

        private void gameSoldier_Clicked(object sender, EventArgs e)
        {
            bool tryMove;

            if (m_CurrentSoldierToMove == null)
            {
                m_CurrentSoldierToMove = new PositionInBoard((sender as GameSoldier).Cell.Row, (sender as GameSoldier).Cell.Col);
                (sender as Button).BackColor = Color.LightBlue;
            }
            else
            {
                m_NextPosition = new PositionInBoard((sender as GameSoldier).Cell.Row, (sender as GameSoldier).Cell.Col);
                if (m_NextPosition.Col == m_CurrentSoldierToMove.Col && m_NextPosition.Row == m_CurrentSoldierToMove.Row)
                {
                    (sender as Button).BackColor = Color.White;
                    m_CurrentSoldierToMove = null;
                }
                else
                {
                    r_GameLogicManger.ManageMove(m_CurrentSoldierToMove, m_NextPosition, out tryMove);
                    invalidMoveCase(tryMove);
                    r_ButtonsBoard[m_CurrentSoldierToMove.Row, m_CurrentSoldierToMove.Col].BackColor = Color.White;
                    m_CurrentSoldierToMove = null;
                    m_NextPosition = null;
                    setTurnColor();
                    if (r_GameLogicManger.PlayWithComputer == true)
                    {
                        timerComputer.Start();
                    }
                }
            }
        }

        private bool checkEndOfGame()
        {
            DialogResult result;
            StringBuilder message = new StringBuilder();
            bool isEndOfGame = false;
            MessageBoxButtons button;

            if (r_GameLogicManger.IsEndOfGame() == true)
            {
                isEndOfGame = true;
                if(r_Player1Name.Length == 0)
                {
                    labelPlayer1.Text = string.Format("Player 1:" + r_GameLogicManger.OPlayerScore);
                }
                else
                {
                    labelPlayer1.Text = string.Format(r_Player1Name + ":" + r_GameLogicManger.OPlayerScore);
                }

                if(r_Player2Name.Length == 0)
                {
                    labelPlayer2.Text = string.Format("Player 2:" + r_GameLogicManger.XPlayerScore);
                }
                else
                {
                    labelPlayer2.Text = string.Format(r_Player2Name + ":" + r_GameLogicManger.XPlayerScore);
                }

                button = MessageBoxButtons.YesNo;
                switch(r_GameLogicManger.Winner)
                {
                 case GameManagement.eWinner.OPlayer:
                        message.AppendFormat("{0} Won!", r_Player1Name.Length == 0 ? "Player 1" : r_Player1Name);
                        message.Append(Environment.NewLine);
                        message.Append("Another round?");
                        break;
                 case GameManagement.eWinner.XPlayer:
                        message.AppendFormat("{0} Won!", r_Player2Name.Length == 0 ? "Player 2" : r_Player2Name);
                        message.Append(Environment.NewLine);
                        message.Append("Another round?");
                        break;
                 case GameManagement.eWinner.Tie:
                        message.Append("Tie");
                        message.Append(Environment.NewLine);
                        message.Append("Another round?");
                        break;
                }

                result = MessageBox.Show(message.ToString(), "Damka", button, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    initNewRound();
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                }
            }

            return isEndOfGame;
        }

        private void initNewRound()
        {
            r_GameLogicManger.InitPlayers();
            r_GameLogicManger.DataBoard.InitDataBoard();
            r_GameLogicManger.InitFirstPossibleMoves();
            m_CurrentSoldierToMove = null;
            setTurnColor();
        }

        private void invalidMoveCase(bool i_Valid)
        {
            MessageBoxButtons button;

            if (i_Valid == false)
            {
                button = MessageBoxButtons.OK;
                MessageBox.Show("Invalid move", "Damka", button,MessageBoxIcon.Error);
            }
        }

        private void setTurnColor()
        {
            if (r_GameLogicManger.CurrentTurnType == 'O')
            {
                labelPlayer1.BackColor = System.Drawing.Color.LightPink;
                labelPlayer2.BackColor = System.Drawing.Color.Transparent;
            }
            else
            {
                labelPlayer2.BackColor = System.Drawing.Color.LightPink;
                labelPlayer1.BackColor = System.Drawing.Color.Transparent;
            }
        }
    }
}
