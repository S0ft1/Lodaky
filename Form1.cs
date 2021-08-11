using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lodaky
{
    public partial class Form1 : Form
    {
        Game game = new Game();
        public Form1()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            requestManaging(btn);
        }

        private void requestManaging(Button btn)
        {
            if (game.currentGameState == GameStates.PLANNING)
            {
                if (game.planningRequest(getButtonPosition(btn)))
                {
                    disableButtons();
                    if (game.currentGameState != GameStates.PLANNING)
                    {
                        aiPlanningManagement();
                    }
                    updateField();
                   
                }
            }
            else
            {
                game.battleRequest(getButtonPosition(btn));
            }
        }
        private void aiPlanningManagement()
        {
            game.AiTurn = true;
            game.aiPlanning();
            prepareFormForBatlle();
        }
        private void prepareFormForBatlle()
        {
            enableFields();
            Rotation.Enabled = false;
            Rotation.Visible = false;
            tableLayoutPanel3.RowStyles[4].Height = 0;
            foreach (var textbox in tableLayoutPanel3.Controls.OfType<TextBox>())
            {
                textbox.Text = "Rdy!";
            }
        }

        private void enableFields()
        {
            foreach (var button in tableLayoutPanel1.Controls.OfType<Button>())
            {
                button.Enabled = true;
            }
            foreach (var button in tableLayoutPanel3.Controls.OfType<Button>())
            {
                button.Enabled = true;
            }
            foreach (var button in tableLayoutPanel4.Controls.OfType<Button>())
            {
                button.Enabled = false;
            }
        }

        private void updateField()
        {
            foreach (var button in tableLayoutPanel4.Controls.OfType<Button>())
            {
                Position pos = getButtonPosition(button);
                
                if (game.playersField[pos.X, pos.Y] != FieldTypes.SEA)
                {
                    button.Text = "X";
                }
            }
            foreach (var button in tableLayoutPanel1.Controls.OfType<Button>())
            {
                Position pos = getButtonPosition(button);
                if (game.enemysField[pos.X, pos.Y] != FieldTypes.SEA)
                {
                    button.Text = "X";
                }
            }
            
        }
        private void disableButtons()
        {
            switch (game.chosenShip)
            {
                case FieldTypes.BB:
                    var bb = tableLayoutPanel3.Controls.OfType<Button>().Where(r => r.Name == "BBButton");
                    Button bbbutt = bb.ToList()[0];
                    bbbutt.Enabled = false;
                    bbtxt.Text = "0";
                    break;
                case FieldTypes.CV:
                    var cv = tableLayoutPanel3.Controls.OfType<Button>().Where(r => r.Name == "CVButton");
                    Button cvbutt = cv.ToList()[0];
                    cvbutt.Enabled = false;
                    cvtxt.Text = "0";
                    break;
                case FieldTypes.CA:
                    var ca = tableLayoutPanel3.Controls.OfType<Button>().Where(r => r.Name == "CAButton");
                    Button cabutt = ca.ToList()[0];
                    cabutt.Enabled = false;
                    catxt.Text = "0";
                    break;
                case FieldTypes.DD:
                    var dd = tableLayoutPanel3.Controls.OfType<Button>().Where(r => r.Name == "DDButton");
                    Button ddbutt = dd.ToList()[0];
                    ddbutt.Enabled = false;
                    ddtxt.Text = "0";
                    break;
            }
            game.chosenShip = FieldTypes.SEA;
        }

        private Position getButtonPosition(Button btn)
        {
            //hnus
            Position btnPosition = new Position(0,0);
            btnPosition.X = btn.Location.X - 4;
            btnPosition.X = (btnPosition.X / 64);
            btnPosition.Y = btn.Location.Y - 4;
            btnPosition.Y = (btnPosition.Y / 59);
            return btnPosition;
        }
        #region eventHandlers
        private void Rotation_Click(object sender, EventArgs e)
        {          
            game.rotation=!game.rotation;
        }
        private void BB_Click(object sender, EventArgs e)
        {
            game.chooseShip(FieldTypes.BB);
        }
        private void CV_Click(object sender, EventArgs e)
        {
            game.chooseShip(FieldTypes.CV);
        }
        private void CA_Click(object sender, EventArgs e)
        {
            game.chooseShip(FieldTypes.CA);
        }
        private void DD_Click(object sender, EventArgs e)
        {
            game.chooseShip(FieldTypes.DD);
        }
        #endregion
        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {
        }

    }
}
