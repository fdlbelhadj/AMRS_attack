
namespace AMRS_das2
{
    partial class MainForm1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.pathTxtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nbprintsTxt = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.loadFromFilChk = new System.Windows.Forms.CheckBox();
            this.showGraphChk = new System.Windows.Forms.CheckBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button3 = new System.Windows.Forms.Button();
            this.whichGraph = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.fingerNb = new System.Windows.Forms.NumericUpDown();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.distorsionChk = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fingerNb)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(148, 101);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 42);
            this.button1.TabIndex = 0;
            this.button1.Text = "Create Repr struct";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pathTxtBox
            // 
            this.pathTxtBox.Location = new System.Drawing.Point(11, 28);
            this.pathTxtBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pathTxtBox.Name = "pathTxtBox";
            this.pathTxtBox.Size = new System.Drawing.Size(689, 22);
            this.pathTxtBox.TabIndex = 1;
            this.pathTxtBox.Text = "E:\\Recherche\\Doctorat\\ARTICLE\\APPLICATION\\FVC2002\\DB1_B";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Dataset path";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 148);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(691, 601);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox2_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Nb prints per finger";
            // 
            // nbprintsTxt
            // 
            this.nbprintsTxt.Location = new System.Drawing.Point(148, 66);
            this.nbprintsTxt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nbprintsTxt.Name = "nbprintsTxt";
            this.nbprintsTxt.Size = new System.Drawing.Size(164, 22);
            this.nbprintsTxt.TabIndex = 4;
            this.nbprintsTxt.Text = "8";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(295, 101);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 42);
            this.button2.TabIndex = 7;
            this.button2.Text = "Create Graph";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(765, 82);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(871, 62);
            this.panel1.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "label3";
            // 
            // loadFromFilChk
            // 
            this.loadFromFilChk.AutoSize = true;
            this.loadFromFilChk.Location = new System.Drawing.Point(765, 28);
            this.loadFromFilChk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loadFromFilChk.Name = "loadFromFilChk";
            this.loadFromFilChk.Size = new System.Drawing.Size(116, 21);
            this.loadFromFilChk.TabIndex = 9;
            this.loadFromFilChk.Text = "Load from file";
            this.loadFromFilChk.UseVisualStyleBackColor = true;
            // 
            // showGraphChk
            // 
            this.showGraphChk.AutoSize = true;
            this.showGraphChk.Location = new System.Drawing.Point(765, 55);
            this.showGraphChk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.showGraphChk.Name = "showGraphChk";
            this.showGraphChk.Size = new System.Drawing.Size(105, 21);
            this.showGraphChk.TabIndex = 10;
            this.showGraphChk.Text = "Show Umdg";
            this.showGraphChk.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Location = new System.Drawing.Point(765, 149);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(799, 601);
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.DoubleClick += new System.EventHandler(this.pictureBox2_DoubleClick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(423, 101);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(109, 42);
            this.button3.TabIndex = 12;
            this.button3.Text = "Solve";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // whichGraph
            // 
            this.whichGraph.AutoSize = true;
            this.whichGraph.Checked = true;
            this.whichGraph.CheckState = System.Windows.Forms.CheckState.Checked;
            this.whichGraph.Location = new System.Drawing.Point(935, 28);
            this.whichGraph.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.whichGraph.Name = "whichGraph";
            this.whichGraph.Size = new System.Drawing.Size(150, 21);
            this.whichGraph.TabIndex = 13;
            this.whichGraph.Text = "Graph for only FP0";
            this.whichGraph.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1238, 27);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(151, 42);
            this.button4.TabIndex = 14;
            this.button4.Text = "getMatlab Sol";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(427, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "Finger To Load";
            // 
            // fingerNb
            // 
            this.fingerNb.Location = new System.Drawing.Point(539, 66);
            this.fingerNb.Margin = new System.Windows.Forms.Padding(4);
            this.fingerNb.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.fingerNb.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.fingerNb.Name = "fingerNb";
            this.fingerNb.Size = new System.Drawing.Size(160, 22);
            this.fingerNb.TabIndex = 17;
            this.fingerNb.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(593, 101);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(109, 42);
            this.button5.TabIndex = 18;
            this.button5.Text = "DB stats";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(1413, 27);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(223, 42);
            this.button6.TabIndex = 19;
            this.button6.Text = "launch Optimizer";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // distorsionChk
            // 
            this.distorsionChk.AutoSize = true;
            this.distorsionChk.Checked = true;
            this.distorsionChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.distorsionChk.Location = new System.Drawing.Point(935, 55);
            this.distorsionChk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.distorsionChk.Name = "distorsionChk";
            this.distorsionChk.Size = new System.Drawing.Size(148, 21);
            this.distorsionChk.TabIndex = 20;
            this.distorsionChk.Text = "Tolearte distorsion";
            this.distorsionChk.UseVisualStyleBackColor = true;
            // 
            // MainForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1648, 746);
            this.Controls.Add(this.distorsionChk);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.fingerNb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.whichGraph);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.showGraphChk);
            this.Controls.Add(this.loadFromFilChk);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nbprintsTxt);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pathTxtBox);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fingerNb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox pathTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nbprintsTxt;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox loadFromFilChk;
        private System.Windows.Forms.CheckBox showGraphChk;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox whichGraph;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown fingerNb;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.CheckBox distorsionChk;
    }
}

