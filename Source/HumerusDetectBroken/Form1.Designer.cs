namespace HumerusDetectBroken
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ibImage = new Emgu.CV.UI.ImageBox();
            this.ofHarrHumerus = new System.Windows.Forms.OpenFileDialog();
            this.ofPiture = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.Lb_Detected_Contours = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Lb_Parallel_Line = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Lb_TakeTime = new System.Windows.Forms.Label();
            this.Lb_Time = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Lb_Broken = new System.Windows.Forms.Label();
            this.picTemp = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Lb_Rank = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ibImage)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTemp)).BeginInit();
            this.SuspendLayout();
            // 
            // ibImage
            // 
            this.ibImage.Location = new System.Drawing.Point(6, 133);
            this.ibImage.Name = "ibImage";
            this.ibImage.Size = new System.Drawing.Size(319, 113);
            this.ibImage.TabIndex = 2;
            this.ibImage.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(145, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Max Pixel ";
            // 
            // Lb_Detected_Contours
            // 
            this.Lb_Detected_Contours.AutoSize = true;
            this.Lb_Detected_Contours.Location = new System.Drawing.Point(221, 16);
            this.Lb_Detected_Contours.Name = "Lb_Detected_Contours";
            this.Lb_Detected_Contours.Size = new System.Drawing.Size(13, 13);
            this.Lb_Detected_Contours.TabIndex = 9;
            this.Lb_Detected_Contours.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Lb_Parallel_Line);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.Lb_TakeTime);
            this.groupBox2.Controls.Add(this.Lb_Detected_Contours);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 327);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(333, 47);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Result";
            // 
            // Lb_Parallel_Line
            // 
            this.Lb_Parallel_Line.AutoSize = true;
            this.Lb_Parallel_Line.Location = new System.Drawing.Point(89, 16);
            this.Lb_Parallel_Line.Name = "Lb_Parallel_Line";
            this.Lb_Parallel_Line.Size = new System.Drawing.Size(13, 13);
            this.Lb_Parallel_Line.TabIndex = 29;
            this.Lb_Parallel_Line.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Parallel line :";
            // 
            // Lb_TakeTime
            // 
            this.Lb_TakeTime.AutoSize = true;
            this.Lb_TakeTime.Location = new System.Drawing.Point(89, 64);
            this.Lb_TakeTime.Name = "Lb_TakeTime";
            this.Lb_TakeTime.Size = new System.Drawing.Size(0, 13);
            this.Lb_TakeTime.TabIndex = 12;
            // 
            // Lb_Time
            // 
            this.Lb_Time.AutoSize = true;
            this.Lb_Time.Location = new System.Drawing.Point(170, 9);
            this.Lb_Time.Name = "Lb_Time";
            this.Lb_Time.Size = new System.Drawing.Size(13, 13);
            this.Lb_Time.TabIndex = 26;
            this.Lb_Time.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(101, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Take time : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Lb_Broken);
            this.groupBox1.Controls.Add(this.picTemp);
            this.groupBox1.Controls.Add(this.ibImage);
            this.groupBox1.Location = new System.Drawing.Point(12, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 296);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            // 
            // Lb_Broken
            // 
            this.Lb_Broken.AutoSize = true;
            this.Lb_Broken.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lb_Broken.ForeColor = System.Drawing.Color.Black;
            this.Lb_Broken.Location = new System.Drawing.Point(88, 258);
            this.Lb_Broken.Name = "Lb_Broken";
            this.Lb_Broken.Size = new System.Drawing.Size(149, 24);
            this.Lb_Broken.TabIndex = 30;
            this.Lb_Broken.Text = "Can not Analysis";
            // 
            // picTemp
            // 
            this.picTemp.Location = new System.Drawing.Point(6, 19);
            this.picTemp.Name = "picTemp";
            this.picTemp.Size = new System.Drawing.Size(319, 108);
            this.picTemp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTemp.TabIndex = 26;
            this.picTemp.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Rank :";
            // 
            // Lb_Rank
            // 
            this.Lb_Rank.AutoSize = true;
            this.Lb_Rank.Location = new System.Drawing.Point(57, 9);
            this.Lb_Rank.Name = "Lb_Rank";
            this.Lb_Rank.Size = new System.Drawing.Size(13, 13);
            this.Lb_Rank.TabIndex = 28;
            this.Lb_Rank.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(356, 386);
            this.Controls.Add(this.Lb_Time);
            this.Controls.Add(this.Lb_Rank);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Humerus Analysis";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeBegin += new System.EventHandler(this.Form1_ResizeBegin);
            ((System.ComponentModel.ISupportInitialize)(this.ibImage)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTemp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox ibImage;
        private System.Windows.Forms.OpenFileDialog ofHarrHumerus;
        private System.Windows.Forms.OpenFileDialog ofPiture;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Lb_Detected_Contours;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label Lb_TakeTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label Lb_Time;
        private System.Windows.Forms.PictureBox picTemp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Lb_Rank;
        private System.Windows.Forms.Label Lb_Parallel_Line;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Lb_Broken;
    }
}

