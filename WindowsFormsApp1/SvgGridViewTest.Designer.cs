namespace WindowsFormsApp1
{
	partial class SvgGridViewTest
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
			this.button1 = new System.Windows.Forms.Button();
			this.svgGridView1 = new CustomControlsImitatingUWP.SvgGridView();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(826, 29);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// svgGridView1
			// 
			this.svgGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.svgGridView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.svgGridView1.Location = new System.Drawing.Point(12, 12);
			this.svgGridView1.Name = "svgGridView1";
			this.svgGridView1.Size = new System.Drawing.Size(743, 525);
			this.svgGridView1.TabIndex = 2;
			// 
			// SvgGridViewTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(913, 584);
			this.Controls.Add(this.svgGridView1);
			this.Controls.Add(this.button1);
			this.Name = "SvgGridViewTest";
			this.Text = "SvgGridViewTest";
			this.Load += new System.EventHandler(this.SvgGridViewTest_Load);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button button1;
		private CustomControlsImitatingUWP.SvgGridView svgGridView1;
	}
}