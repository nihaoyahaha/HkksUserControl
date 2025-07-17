namespace CustomControlsImitatingUWP
{
	partial class SvgGridView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SvgGridView));
			this.panel_Main = new System.Windows.Forms.FlowLayoutPanel();
			this.panel_Top = new System.Windows.Forms.Panel();
			this.pic_triangle = new System.Windows.Forms.PictureBox();
			this.pic_tick = new System.Windows.Forms.PictureBox();
			this.pic_crosses = new System.Windows.Forms.PictureBox();
			this.pic_rect = new System.Windows.Forms.PictureBox();
			this.pic_remove = new System.Windows.Forms.PictureBox();
			this.lb_numInfo = new System.Windows.Forms.Label();
			this.pic_backward = new System.Windows.Forms.PictureBox();
			this.pic_forward = new System.Windows.Forms.PictureBox();
			this.panel_Top.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pic_triangle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_tick)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_crosses)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_rect)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_remove)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_backward)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_forward)).BeginInit();
			this.SuspendLayout();
			// 
			// panel_Main
			// 
			this.panel_Main.AllowDrop = true;
			this.panel_Main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel_Main.AutoScroll = true;
			this.panel_Main.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel_Main.Location = new System.Drawing.Point(3, 32);
			this.panel_Main.Name = "panel_Main";
			this.panel_Main.Padding = new System.Windows.Forms.Padding(3);
			this.panel_Main.Size = new System.Drawing.Size(382, 269);
			this.panel_Main.TabIndex = 0;
			this.panel_Main.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Main_Paint);
			// 
			// panel_Top
			// 
			this.panel_Top.Controls.Add(this.pic_triangle);
			this.panel_Top.Controls.Add(this.pic_tick);
			this.panel_Top.Controls.Add(this.pic_crosses);
			this.panel_Top.Controls.Add(this.pic_rect);
			this.panel_Top.Location = new System.Drawing.Point(3, 5);
			this.panel_Top.Name = "panel_Top";
			this.panel_Top.Size = new System.Drawing.Size(137, 27);
			this.panel_Top.TabIndex = 1;
			// 
			// pic_triangle
			// 
			this.pic_triangle.Image = ((System.Drawing.Image)(resources.GetObject("pic_triangle.Image")));
			this.pic_triangle.Location = new System.Drawing.Point(113, 5);
			this.pic_triangle.Name = "pic_triangle";
			this.pic_triangle.Size = new System.Drawing.Size(16, 16);
			this.pic_triangle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pic_triangle.TabIndex = 0;
			this.pic_triangle.TabStop = false;
			this.pic_triangle.Click += new System.EventHandler(this.pic_triangle_Click);
			// 
			// pic_tick
			// 
			this.pic_tick.Image = global::CustomControlsImitatingUWP.Properties.Resources.checkRsl_4;
			this.pic_tick.Location = new System.Drawing.Point(77, 4);
			this.pic_tick.Name = "pic_tick";
			this.pic_tick.Size = new System.Drawing.Size(16, 16);
			this.pic_tick.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pic_tick.TabIndex = 0;
			this.pic_tick.TabStop = false;
			this.pic_tick.Click += new System.EventHandler(this.pic_tick_Click);
			// 
			// pic_crosses
			// 
			this.pic_crosses.Image = global::CustomControlsImitatingUWP.Properties.Resources.checkRsl_2;
			this.pic_crosses.Location = new System.Drawing.Point(41, 4);
			this.pic_crosses.Name = "pic_crosses";
			this.pic_crosses.Size = new System.Drawing.Size(16, 16);
			this.pic_crosses.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pic_crosses.TabIndex = 0;
			this.pic_crosses.TabStop = false;
			this.pic_crosses.Click += new System.EventHandler(this.pic_crosses_Click);
			// 
			// pic_rect
			// 
			this.pic_rect.Image = global::CustomControlsImitatingUWP.Properties.Resources.checkRsl_1;
			this.pic_rect.Location = new System.Drawing.Point(3, 5);
			this.pic_rect.Name = "pic_rect";
			this.pic_rect.Size = new System.Drawing.Size(16, 16);
			this.pic_rect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pic_rect.TabIndex = 0;
			this.pic_rect.TabStop = false;
			this.pic_rect.Click += new System.EventHandler(this.pic_rect_Click);
			// 
			// pic_remove
			// 
			this.pic_remove.Image = global::CustomControlsImitatingUWP.Properties.Resources.Del;
			this.pic_remove.Location = new System.Drawing.Point(168, 10);
			this.pic_remove.Name = "pic_remove";
			this.pic_remove.Size = new System.Drawing.Size(16, 16);
			this.pic_remove.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pic_remove.TabIndex = 0;
			this.pic_remove.TabStop = false;
			this.pic_remove.Click += new System.EventHandler(this.pic_remove_Click);
			// 
			// lb_numInfo
			// 
			this.lb_numInfo.AutoSize = true;
			this.lb_numInfo.Location = new System.Drawing.Point(221, 13);
			this.lb_numInfo.Name = "lb_numInfo";
			this.lb_numInfo.Size = new System.Drawing.Size(23, 12);
			this.lb_numInfo.TabIndex = 2;
			this.lb_numInfo.Text = "0/0";
			// 
			// pic_backward
			// 
			this.pic_backward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pic_backward.Image = global::CustomControlsImitatingUWP.Properties.Resources.backward;
			this.pic_backward.Location = new System.Drawing.Point(360, 9);
			this.pic_backward.Name = "pic_backward";
			this.pic_backward.Size = new System.Drawing.Size(16, 16);
			this.pic_backward.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pic_backward.TabIndex = 13;
			this.pic_backward.TabStop = false;
			this.pic_backward.Click += new System.EventHandler(this.pic_backward_Click);
			// 
			// pic_forward
			// 
			this.pic_forward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pic_forward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pic_forward.Image = global::CustomControlsImitatingUWP.Properties.Resources.forward;
			this.pic_forward.Location = new System.Drawing.Point(336, 9);
			this.pic_forward.Name = "pic_forward";
			this.pic_forward.Size = new System.Drawing.Size(16, 16);
			this.pic_forward.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pic_forward.TabIndex = 14;
			this.pic_forward.TabStop = false;
			this.pic_forward.Click += new System.EventHandler(this.pic_forward_Click);
			// 
			// SvgGridView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.pic_backward);
			this.Controls.Add(this.pic_forward);
			this.Controls.Add(this.lb_numInfo);
			this.Controls.Add(this.pic_remove);
			this.Controls.Add(this.panel_Top);
			this.Controls.Add(this.panel_Main);
			this.Name = "SvgGridView";
			this.Size = new System.Drawing.Size(388, 304);
			this.panel_Top.ResumeLayout(false);
			this.panel_Top.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pic_triangle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_tick)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_crosses)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_rect)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_remove)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_backward)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_forward)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel panel_Main;
		private System.Windows.Forms.Panel panel_Top;
		private System.Windows.Forms.PictureBox pic_rect;
		private System.Windows.Forms.PictureBox pic_triangle;
		private System.Windows.Forms.PictureBox pic_tick;
		private System.Windows.Forms.PictureBox pic_crosses;
		private System.Windows.Forms.PictureBox pic_remove;
		private System.Windows.Forms.Label lb_numInfo;
		private System.Windows.Forms.PictureBox pic_backward;
		private System.Windows.Forms.PictureBox pic_forward;
	}
}
