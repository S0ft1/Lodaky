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
        Image fireImage = Image.FromFile(@"..\..\pics\fire.png");
        Image fogImage = Image.FromFile(@"..\..\pics\fog.png");
        Image seaImage = Image.FromFile(@"..\..\pics\sea.png");
        Image background = Image.FromFile(@"..\..\pics\background.jpg");

        public Form1()
        {
            InitializeComponent();
            updateField();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            requestManaging(btn);
           // bkg = Image.FromFile(@"..\..\pics\USN\ca"+xd+".jpg");
          //  btn.BackgroundImage = bkg;
          //  xd++;
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
                game.chosenShip = FieldTypes.SEA;
            }
            else
            {
                if (game.chosenShip != FieldTypes.SEA)
                {
                    game.battleRequest(getButtonPosition(btn));
                    updateField();
                }
                updateReload();
            }
        }

        private void aiPlanningManagement()
        {
            game.AiTurn = true;
            game.aiPlanning();
            prepareFormForBatlle();
            game.AiTurn = false;
        }

        private void updateReload()
        {
            TextBox[] reloadButtons = getReloadButtons();
            for(int i = 0; i < reloadButtons.Length; ++i)
            {
                if (game.getAllyFleet()[i].getDestroyed())
                {
                    reloadButtons[i].Text = "Dead";
                }
                else if (game.getAllyFleet()[i].getReloadCooldown() == 0)
                {
                    reloadButtons[i].Text = "Rdy!";
                }
                else
                {
                    reloadButtons[i].Text = game.getAllyFleet()[i].getReloadCooldown().ToString();
                }
            }
        }

        private TextBox[] getReloadButtons()
        {
            TextBox[] reloadButtons = new TextBox[4];
            ushort counter = 0;
            foreach (var textbox in tableLayoutPanel3.Controls.OfType<TextBox>())
            {
                reloadButtons[counter] = textbox;
                ++counter;
            }
            return reloadButtons;
        }

        private void prepareFormForBatlle()
        {
            enableFields();
            foreach (var textbox in tableLayoutPanel3.Controls.OfType<TextBox>())
            {
                textbox.Text = "Rdy!";
            }
            game.rotation = false;
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
                drawButtonBackGround(button, game.playersField,true);
            }
            foreach (var button in tableLayoutPanel1.Controls.OfType<Button>())
            {
                drawButtonBackGround(button, game.enemysField,false);
            }
            TextBox[] reloadButtons = getReloadButtons();
            ushort counter = 0;
            foreach (var button in tableLayoutPanel3.Controls.OfType<Button>())
            {
                if (counter > 3)
                {
                    break;
                }
                if (game.currentGameState != GameStates.PLANNING)
                {
                    button.Enabled = reloadButtons[counter].Text == "Rdy!" ? true : false;
                }
                ++counter;
            }
            output.Text = game.playersOutput;
            output2.Text = game.enemyOutput;
        }

        private void drawButtonBackGround(Button button, Field[,] field,bool table)
        {
            Position pos = getButtonPosition(button);
            switch (field[pos.X, pos.Y].type)
            {
                case FieldTypes.BB:
                    button.Text = "BB";
                    break;
                case FieldTypes.CV:
                    button.Text = "CV";
                    break;
                case FieldTypes.CA:
                    button.Text = "CA";
                    break;
                case FieldTypes.DD:
                    button.Text = "DD";
                    break;
                case FieldTypes.FIRE:
                    button.Text = "";
                    button.BackgroundImage = fireImage;
                    break;
            }

            if(!field[pos.X, pos.Y].spotted&&!table)
            {
                button.Text = "";
                button.BackgroundImage = fogImage;
            }
            if (field[pos.X, pos.Y].spotted && field[pos.X, pos.Y].type != FieldTypes.FIRE)
            {
                button.BackgroundImage = seaImage;
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
            //turbohnus
            Position btnPosition = new Position(0,0);
            btnPosition.X = btn.Location.X-1;
            btnPosition.X = (btnPosition.X / 64);
            btnPosition.Y = btn.Location.Y;
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

        private void Form1_KeyPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.R:
                    game.rotation = !game.rotation;
                    break;
                case Keys.B:
                    game.chooseShip(FieldTypes.BB);
                    break;
                case Keys.A:
                    game.chooseShip(FieldTypes.CV);
                    break;
                case Keys.C:
                    game.chooseShip(FieldTypes.CA);
                    break;
                case Keys.D:
                    game.chooseShip(FieldTypes.DD);
                    break;
            }          
        }
        #endregion

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {
        }

    }
}
