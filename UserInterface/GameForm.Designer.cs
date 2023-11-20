namespace UserInterface
{
    partial class GameForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent(int i_BoardSize)
        {
            bool isEnabledButton;

            initForm();
            labelPlayer1 = new System.Windows.Forms.Label();
            labelPlayer2 = new System.Windows.Forms.Label();
            timerComputer = new System.Windows.Forms.Timer(this.components);
            timerComputer.Interval = 500;
            timerComputer.Tick += new System.EventHandler(this.timerComputer_Tick);
            for (int i = 0; i < r_BoardSize; i++)
            {
                isEnabledButton = (i % 2 == 0) ? false : true;
                for (int j = 0; j < r_BoardSize; j++)
                {
                    r_ButtonsBoard[i, j] = new GameSoldier();
                    if (isEnabledButton == false)
                    {
                        r_ButtonsBoard[i, j].BackColor = System.Drawing.Color.Gray;
                        r_ButtonsBoard[i, j].Enabled = false;
                    }
                    else
                    {
                        r_ButtonsBoard[i, j].BackColor = System.Drawing.Color.White;
                    }

                    isEnabledButton = !isEnabledButton;
                    r_ButtonsBoard[i, j].Location = new System.Drawing.Point(j * 50, (i * 50) + 50);
                    r_ButtonsBoard[i, j].Size = new System.Drawing.Size(50, 50);
                    r_ButtonsBoard[i, j].Cell = r_GameLogicManger.DataBoard.GetDataFromPosition(i, j);
                    r_ButtonsBoard[i, j].Cell.Changed += Cell_Changed;
                    this.Controls.Add(r_ButtonsBoard[i, j]);
                    r_ButtonsBoard[i, j].Click += new System.EventHandler(gameSoldier_Clicked);
                }
            }

            initLabels();
        }

        private void initForm()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Size = new System.Drawing.Size(r_BoardSize * 53, r_BoardSize * 55 + 65);
            this.Text = "Damka";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        private void initLabels()
        {
            if (r_Settings.Player1Name.Length == 0)
            {
                labelPlayer1.Text = "Player 1: 0";
            }
            else
            {
                labelPlayer1.Text = string.Format(r_Settings.Player1Name + ": 0");
            }

            if (r_Settings.Player2Name.Length == 0)
            {
                labelPlayer2.Text = "Player 2: 0";
            }
            else
            {
                labelPlayer2.Text = string.Format(r_Settings.Player2Name + ": 0");
            }

            labelPlayer1.BackColor = System.Drawing.Color.LightPink;
            labelPlayer1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            labelPlayer1.Location = new System.Drawing.Point(((r_BoardSize / 2) * 25) / 2, 20);
            labelPlayer2.Location = new System.Drawing.Point((r_BoardSize * 50) / 2 + (r_BoardSize * 25) / 4, 20);
            labelPlayer2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Controls.Add(labelPlayer1);
            this.Controls.Add(labelPlayer2);
        }

        #endregion

        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.Label labelPlayer2;
        private System.Windows.Forms.Timer timerComputer;
    }
}