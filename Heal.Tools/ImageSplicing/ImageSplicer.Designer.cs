namespace Heal.Tools.ImageSplicing
{
    partial class ImageSplicer
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
            this.components = new System.ComponentModel.Container();
            this.lstFiles = new System.Windows.Forms.ListBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer( this.components );
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            ( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).BeginInit();
            this.SuspendLayout();
            // 
            // lstFiles
            // 
            this.lstFiles.FormattingEnabled = true;
            this.lstFiles.ItemHeight = 12;
            this.lstFiles.Location = new System.Drawing.Point( 12, 12 );
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size( 256, 400 );
            this.lstFiles.TabIndex = 0;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point( 274, 12 );
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size( 75, 23 );
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Add";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler( this.btnOpen_Click );
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point( 274, 387 );
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size( 75, 23 );
            this.button2.TabIndex = 2;
            this.button2.Text = "Process";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler( this.button2_Click );
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point( 274, 41 );
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size( 75, 23 );
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler( this.btnClear_Click );
            // 
            // timer1
            // 
            this.timer1.Interval = 70;
            this.timer1.Tick += new System.EventHandler( this.timer1_Tick );
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point( 355, 12 );
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size( 329, 400 );
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point( 275, 101 );
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size( 74, 21 );
            this.textBox1.TabIndex = 5;
            this.textBox1.TextChanged += new System.EventHandler( this.textBox1_TextChanged );
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point( 275, 128 );
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size( 74, 21 );
            this.textBox2.TabIndex = 5;
            this.textBox2.TextChanged += new System.EventHandler( this.textBox1_TextChanged );
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point( 275, 155 );
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size( 74, 21 );
            this.textBox3.TabIndex = 5;
            this.textBox3.TextChanged += new System.EventHandler( this.textBox1_TextChanged );
            // 
            // ImageSplicer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 12F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 696, 422 );
            this.Controls.Add( this.textBox3 );
            this.Controls.Add( this.textBox2 );
            this.Controls.Add( this.textBox1 );
            this.Controls.Add( this.pictureBox1 );
            this.Controls.Add( this.btnClear );
            this.Controls.Add( this.button2 );
            this.Controls.Add( this.btnOpen );
            this.Controls.Add( this.lstFiles );
            this.Name = "ImageSplicer";
            this.Text = "Form1";
            this.Load += new System.EventHandler( this.ImageSplicer_Load );
            ( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).EndInit();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstFiles;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
    }
}