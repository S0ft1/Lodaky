using System;
using System.Linq;
using System.Windows.Forms;

namespace Lodaky
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.BBButton = new System.Windows.Forms.Button();
            this.CVButton = new System.Windows.Forms.Button();
            this.CAButton = new System.Windows.Forms.Button();
            this.DDButton = new System.Windows.Forms.Button();
            this.Rotation = new System.Windows.Forms.Button();
            this.bbtxt = new System.Windows.Forms.TextBox();
            this.cvtxt = new System.Windows.Forms.TextBox();
            this.catxt = new System.Windows.Forms.TextBox();
            this.ddtxt = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout(); 
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            int columnNrowCount = 10;
            EventHandler button_Click = new System.EventHandler(this.button_Click);





            // 
            // tableLayoutPanel1
            // 
                 this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
                 this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
                 this.tableLayoutPanel1.ColumnCount = columnNrowCount;
                 this.tableLayoutPanel1.RowCount = columnNrowCount;
                 for (int i = 0; i < 10; ++i)
                 {
                     this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
                     this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
                 }
                 for (int i =0; i< 10; ++i)
                 {
                     for(int j =0; j<10; ++j)
                     {

                         this.tableLayoutPanel1.Controls.Add(new Button(), i, j);
                     }               
                 }
                 this.tableLayoutPanel1.Location = new System.Drawing.Point(850, 60);
                 this.tableLayoutPanel1.Name = "tableLayoutPanel1";

                 this.tableLayoutPanel1.Size = new System.Drawing.Size(650, 600);
                 this.tableLayoutPanel1.TabIndex = 0;
                 foreach (var button in this.tableLayoutPanel1.Controls.OfType<Button>())
                 {
                     button.Click += button_Click;
                     button.AutoSize = true;
                     
                     button.BackColor = System.Drawing.SystemColors.MenuHighlight;
                     button.Dock = System.Windows.Forms.DockStyle.Fill;
                     button.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
                     button.FlatAppearance.BorderSize = 0;
                     button.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.MenuHighlight;
                     button.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight;
                     button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                     button.Location = new System.Drawing.Point(0, 0);
                     button.Name = "button2";
                     button.Margin = new Padding(0,0,0,0);
                     button.Size = new System.Drawing.Size(80, 80);
                     button.TabIndex = 0;
                     button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                     button.UseVisualStyleBackColor = false;
                     button.Enabled = false;
                 } 


                 //tableLayout4
                 this.tableLayoutPanel4.BackColor = System.Drawing.SystemColors.MenuHighlight;
                 this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
                 this.tableLayoutPanel4.ColumnCount = columnNrowCount;
                 this.tableLayoutPanel4.RowCount = columnNrowCount;
                 for (int i = 0; i < 10; ++i)
                 {
                     this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
                     this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
                 }
                 for (int i = 0; i < 10; ++i)
                 {
                     for (int j = 0; j < 10; ++j)
                     {

                         this.tableLayoutPanel4.Controls.Add(new Button(), i, j);
                     }
                 }
                 this.tableLayoutPanel4.Location = new System.Drawing.Point(26, 60);
                 this.tableLayoutPanel4.Size = new System.Drawing.Size(650, 600);
                 this.tableLayoutPanel4.TabIndex = 0;
                 foreach (var button in this.tableLayoutPanel4.Controls.OfType<Button>())
                 {
                     button.Click += button_Click;
                     button.AutoSize = true;
                     button.BackColor = System.Drawing.SystemColors.MenuHighlight;
                     button.Dock = System.Windows.Forms.DockStyle.Fill;
                     button.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                     button.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;                  
                     button.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
                     button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                     button.FlatAppearance.BorderSize = 0;
                     button.Location = new System.Drawing.Point(0, 0);
                     button.Margin = new Padding(0,0,0,0);
                     button.Name = "button1";
                     //button.Size = new System.Drawing.Size(100,100);
                     button.Size = new System.Drawing.Size(65,60);
                     button.Padding = new Padding(0,0,0,0);
                     button.TabIndex = 0;
                     button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                     button.UseVisualStyleBackColor = false;
                 }
            
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Controls.Add(this.BBButton, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.CVButton, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.CAButton, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.DDButton, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.Rotation, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.bbtxt, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.cvtxt, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.catxt, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.ddtxt, 1, 3);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(680, 60);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(165, 303);
            this.tableLayoutPanel3.TabIndex = 3;
            this.tableLayoutPanel3.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel3_Paint);


          
            // 
            // BBButton
            // 
            this.BBButton.Location = new System.Drawing.Point(3, 3);
            this.BBButton.Name = "BBButton";
            this.BBButton.Size = new System.Drawing.Size(126, 69);
            this.BBButton.TabIndex = 0;
            this.BBButton.Text = "Battleship";
            this.BBButton.UseVisualStyleBackColor = true;
            this.BBButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BBButton.FlatAppearance.BorderSize = 0;
            this.BBButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDark;
            this.BBButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.BBButton.Click += new EventHandler(this.BB_Click);
            // 
            // CVButton
            // 
            this.CVButton.Location = new System.Drawing.Point(3, 78);
            this.CVButton.Name = "CVButton";
            this.CVButton.Size = new System.Drawing.Size(126, 69);
            this.CVButton.TabIndex = 1;
            this.CVButton.Text = "Aircraft Carrier";
            this.CVButton.UseVisualStyleBackColor = true;
            this.CVButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CVButton.FlatAppearance.BorderSize = 0;
            this.CVButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDark;
            this.CVButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.CVButton.Click += new EventHandler(this.CV_Click);
            // 
            // CAButton
            // 
            this.CAButton.Location = new System.Drawing.Point(3, 153);
            this.CAButton.Name = "CAButton";
            this.CAButton.Size = new System.Drawing.Size(126, 69);
            this.CAButton.TabIndex = 2;
            this.CAButton.Text = "Cruiser";
            this.CAButton.UseVisualStyleBackColor = true;
            this.CAButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CAButton.FlatAppearance.BorderSize = 0;
            this.CAButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDark;
            this.CAButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.CAButton.Click += new EventHandler(this.CA_Click);

            // 
            // DDButton
            // 
            this.DDButton.Location = new System.Drawing.Point(3, 228);
            this.DDButton.Name = "DDButton";
            this.DDButton.Size = new System.Drawing.Size(126, 69);
            this.DDButton.TabIndex = 3;
            this.DDButton.Text = "Destroyer";
            this.DDButton.UseVisualStyleBackColor = true;
            this.DDButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DDButton.FlatAppearance.BorderSize = 0;
            this.DDButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDark;
            this.DDButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.DDButton.Click += new EventHandler(this.DD_Click);
            
            //
            // Rotation
            //
            this.Rotation.Location = new System.Drawing.Point(3, 303);
            this.Rotation.Name = "Rotation";
            this.Rotation.Size = new System.Drawing.Size(126, 69);
            this.Rotation.TabIndex = 0;
            this.Rotation.Text = "Rotate";
            this.Rotation.UseVisualStyleBackColor = true;
            this.Rotation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Rotation.FlatAppearance.BorderSize = 0;
            this.Rotation.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDark;
            this.Rotation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.Rotation.Click += new EventHandler(this.Rotation_Click);


            //
            // txtboxy
            //

           
            this.bbtxt.Text = "1";
            this.bbtxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bbtxt.AutoSize = true;
            this.bbtxt.TextAlign = HorizontalAlignment.Center;
            this.bbtxt.Anchor = AnchorStyles.None;
            this.bbtxt.BackColor = System.Drawing.SystemColors.ControlDark;
            this.bbtxt.BorderStyle = BorderStyle.None;
            this.bbtxt.ForeColor = System.Drawing.Color.Black;
            this.bbtxt.ReadOnly = true;

            this.cvtxt.Text = "1";
            this.cvtxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cvtxt.AutoSize = true;
            this.cvtxt.TextAlign = HorizontalAlignment.Center;
            this.cvtxt.Anchor = AnchorStyles.None;
            this.cvtxt.BackColor = System.Drawing.SystemColors.ControlDark;
            this.cvtxt.BorderStyle = BorderStyle.None;
            this.cvtxt.ForeColor = System.Drawing.Color.Black;
            this.cvtxt.ReadOnly = true;

            this.catxt.Text = "1";
            this.catxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.catxt.AutoSize = true;
            this.catxt.TextAlign = HorizontalAlignment.Center;
            this.catxt.Anchor = AnchorStyles.None;
            this.catxt.BackColor = System.Drawing.SystemColors.ControlDark;
            this.catxt.BorderStyle = BorderStyle.None;
            this.catxt.ForeColor = System.Drawing.Color.Black;
            this.catxt.ReadOnly = true;

            this.ddtxt.Text = "1";
            this.ddtxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ddtxt.AutoSize = true;
            this.ddtxt.TextAlign = HorizontalAlignment.Center;
            this.ddtxt.Anchor = AnchorStyles.None;
            this.ddtxt.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ddtxt.BorderStyle = BorderStyle.None;
            this.ddtxt.ForeColor = System.Drawing.Color.Black;
            this.ddtxt.ReadOnly = true;

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1528, 682);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1; 
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private TextBox bbtxt;
        private TextBox cvtxt;
        private TextBox catxt;
        private TextBox ddtxt;
        private Button BBButton;
        private Button CVButton;
        private Button CAButton;
        private Button DDButton;
        private Button Rotation;
    }
}

