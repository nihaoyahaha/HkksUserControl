namespace CustomControlsImitatingUWP
{
	partial class SvgCompositionView
	{
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.lb_Date = new System.Windows.Forms.Label();
			this.lb_Orientation = new System.Windows.Forms.Label();
			this.lb_remark = new System.Windows.Forms.Label();
			this.lb_photo = new System.Windows.Forms.Label();
			this.lb_greenbk = new System.Windows.Forms.Label();
			this.lb_inkCanvas = new System.Windows.Forms.Label();
			this.pic_svgImage = new System.Windows.Forms.PictureBox();
			this.pic_Icon = new System.Windows.Forms.PictureBox();
			this.panel_Del = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.pic_svgImage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_Icon)).BeginInit();
			this.SuspendLayout();
			// 
			// lb_Date
			// 
			this.lb_Date.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lb_Date.AutoSize = true;
			this.lb_Date.Location = new System.Drawing.Point(3, 148);
			this.lb_Date.Name = "lb_Date";
			this.lb_Date.Size = new System.Drawing.Size(41, 12);
			this.lb_Date.TabIndex = 2;
			this.lb_Date.Text = "撮影日";
			// 
			// lb_Orientation
			// 
			this.lb_Orientation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lb_Orientation.AutoSize = true;
			this.lb_Orientation.Location = new System.Drawing.Point(3, 165);
			this.lb_Orientation.Name = "lb_Orientation";
			this.lb_Orientation.Size = new System.Drawing.Size(53, 12);
			this.lb_Orientation.TabIndex = 2;
			this.lb_Orientation.Text = "撮影方向";
			// 
			// lb_remark
			// 
			this.lb_remark.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lb_remark.AutoSize = true;
			this.lb_remark.Location = new System.Drawing.Point(3, 181);
			this.lb_remark.Name = "lb_remark";
			this.lb_remark.Size = new System.Drawing.Size(53, 12);
			this.lb_remark.TabIndex = 2;
			this.lb_remark.Text = "コメント";
			// 
			// lb_photo
			// 
			this.lb_photo.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lb_photo.Location = new System.Drawing.Point(29, 6);
			this.lb_photo.Name = "lb_photo";
			this.lb_photo.Size = new System.Drawing.Size(36, 20);
			this.lb_photo.TabIndex = 3;
			this.lb_photo.Text = "写真";
			this.lb_photo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lb_greenbk
			// 
			this.lb_greenbk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lb_greenbk.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lb_greenbk.Location = new System.Drawing.Point(73, 6);
			this.lb_greenbk.Name = "lb_greenbk";
			this.lb_greenbk.Size = new System.Drawing.Size(36, 20);
			this.lb_greenbk.TabIndex = 3;
			this.lb_greenbk.Text = "黒板";
			this.lb_greenbk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lb_inkCanvas
			// 
			this.lb_inkCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lb_inkCanvas.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lb_inkCanvas.Location = new System.Drawing.Point(118, 6);
			this.lb_inkCanvas.Name = "lb_inkCanvas";
			this.lb_inkCanvas.Size = new System.Drawing.Size(36, 20);
			this.lb_inkCanvas.TabIndex = 3;
			this.lb_inkCanvas.Text = "注釈";
			this.lb_inkCanvas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pic_svgImage
			// 
			this.pic_svgImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pic_svgImage.Location = new System.Drawing.Point(4, 32);
			this.pic_svgImage.Name = "pic_svgImage";
			this.pic_svgImage.Size = new System.Drawing.Size(150, 110);
			this.pic_svgImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pic_svgImage.TabIndex = 0;
			this.pic_svgImage.TabStop = false;
			// 
			// pic_Icon
			// 
			this.pic_Icon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pic_Icon.Location = new System.Drawing.Point(5, 7);
			this.pic_Icon.Name = "pic_Icon";
			this.pic_Icon.Size = new System.Drawing.Size(20, 20);
			this.pic_Icon.TabIndex = 0;
			this.pic_Icon.TabStop = false;
			// 
			// panel_Del
			// 
			this.panel_Del.Location = new System.Drawing.Point(92, 148);
			this.panel_Del.Name = "panel_Del";
			this.panel_Del.Size = new System.Drawing.Size(51, 45);
			this.panel_Del.TabIndex = 4;
			this.panel_Del.Visible = false;
			this.panel_Del.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Del_Paint);
			// 
			// SvgCompositionView
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.panel_Del);
			this.Controls.Add(this.lb_inkCanvas);
			this.Controls.Add(this.lb_greenbk);
			this.Controls.Add(this.lb_photo);
			this.Controls.Add(this.lb_remark);
			this.Controls.Add(this.lb_Orientation);
			this.Controls.Add(this.lb_Date);
			this.Controls.Add(this.pic_svgImage);
			this.Controls.Add(this.pic_Icon);
			this.Name = "SvgCompositionView";
			this.Size = new System.Drawing.Size(161, 200);
			((System.ComponentModel.ISupportInitialize)(this.pic_svgImage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_Icon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private System.Windows.Forms.PictureBox pic_Icon;
		private System.Windows.Forms.PictureBox pic_svgImage;
		private System.Windows.Forms.Label lb_Date;
		private System.Windows.Forms.Label lb_Orientation;
		private System.Windows.Forms.Label lb_remark;
		private System.Windows.Forms.Label lb_photo;
		private System.Windows.Forms.Label lb_greenbk;
		private System.Windows.Forms.Label lb_inkCanvas;

		#endregion

		private System.Windows.Forms.Panel panel_Del;
	}
}
