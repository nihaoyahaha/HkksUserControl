namespace HKKS
{
	partial class PagedSvgGridView
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PagedSvgGridView));
			this.panel_Top = new System.Windows.Forms.Panel();
			this.pic_triangle = new System.Windows.Forms.PictureBox();
			this.pic_tick = new System.Windows.Forms.PictureBox();
			this.pic_crosses = new System.Windows.Forms.PictureBox();
			this.pic_rect = new System.Windows.Forms.PictureBox();
			this.lb_numInfo = new System.Windows.Forms.Label();
			this.panel_Main = new System.Windows.Forms.FlowLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.lb_PageInfo = new System.Windows.Forms.Label();
			this.txt_Goto = new System.Windows.Forms.TextBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.pic_PageEnd = new System.Windows.Forms.PictureBox();
			this.pic_PageDown = new System.Windows.Forms.PictureBox();
			this.pic_PageHome = new System.Windows.Forms.PictureBox();
			this.pic_PageUp = new System.Windows.Forms.PictureBox();
			this.pic_backward = new System.Windows.Forms.PictureBox();
			this.pic_forward = new System.Windows.Forms.PictureBox();
			this.pic_remove = new System.Windows.Forms.PictureBox();
			this.lb_order = new System.Windows.Forms.Label();
			this.panel_Top.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pic_triangle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_tick)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_crosses)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_rect)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_PageEnd)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_PageDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_PageHome)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_PageUp)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_backward)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_forward)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_remove)).BeginInit();
			this.SuspendLayout();
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
			this.pic_tick.Image = global::HKKS.Properties.Resources.checkRsl_4;
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
			this.pic_crosses.Image = global::HKKS.Properties.Resources.checkRsl_2;
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
			this.pic_rect.Image = global::HKKS.Properties.Resources.checkRsl_1;
			this.pic_rect.Location = new System.Drawing.Point(3, 5);
			this.pic_rect.Name = "pic_rect";
			this.pic_rect.Size = new System.Drawing.Size(16, 16);
			this.pic_rect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pic_rect.TabIndex = 0;
			this.pic_rect.TabStop = false;
			this.pic_rect.Click += new System.EventHandler(this.pic_rect_Click);
			// 
			// lb_numInfo
			// 
			this.lb_numInfo.AutoSize = true;
			this.lb_numInfo.Location = new System.Drawing.Point(206, 13);
			this.lb_numInfo.Name = "lb_numInfo";
			this.lb_numInfo.Size = new System.Drawing.Size(23, 12);
			this.lb_numInfo.TabIndex = 2;
			this.lb_numInfo.Text = "0/0";
			// 
			// panel_Main
			// 
			this.panel_Main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel_Main.AutoScroll = true;
			this.panel_Main.Location = new System.Drawing.Point(3, 38);
			this.panel_Main.Name = "panel_Main";
			this.panel_Main.Padding = new System.Windows.Forms.Padding(3);
			this.panel_Main.Size = new System.Drawing.Size(496, 266);
			this.panel_Main.TabIndex = 4;
			this.panel_Main.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Main_Paint);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(476, 310);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(20, 16);
			this.label1.TabIndex = 11;
			this.label1.Text = "GO";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lb_PageInfo
			// 
			this.lb_PageInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lb_PageInfo.Location = new System.Drawing.Point(319, 310);
			this.lb_PageInfo.Name = "lb_PageInfo";
			this.lb_PageInfo.Size = new System.Drawing.Size(60, 16);
			this.lb_PageInfo.TabIndex = 10;
			this.lb_PageInfo.Text = "1/1";
			this.lb_PageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txt_Goto
			// 
			this.txt_Goto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.txt_Goto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt_Goto.Location = new System.Drawing.Point(439, 309);
			this.txt_Goto.Multiline = true;
			this.txt_Goto.Name = "txt_Goto";
			this.txt_Goto.Size = new System.Drawing.Size(33, 18);
			this.txt_Goto.TabIndex = 9;
			this.toolTip1.SetToolTip(this.txt_Goto, "指定されたページにジャンプ");
			this.txt_Goto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Goto_KeyDown);
			this.txt_Goto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Goto_KeyPress);
			// 
			// pic_PageEnd
			// 
			this.pic_PageEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pic_PageEnd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pic_PageEnd.Image = global::HKKS.Properties.Resources.pageEnd;
			this.pic_PageEnd.Location = new System.Drawing.Point(415, 310);
			this.pic_PageEnd.Name = "pic_PageEnd";
			this.pic_PageEnd.Size = new System.Drawing.Size(16, 16);
			this.pic_PageEnd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pic_PageEnd.TabIndex = 5;
			this.pic_PageEnd.TabStop = false;
			this.toolTip1.SetToolTip(this.pic_PageEnd, "最後のページ");
			this.pic_PageEnd.Click += new System.EventHandler(this.pic_PageEnd_Click);
			// 
			// pic_PageDown
			// 
			this.pic_PageDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pic_PageDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pic_PageDown.Image = global::HKKS.Properties.Resources.pageRight;
			this.pic_PageDown.Location = new System.Drawing.Point(385, 310);
			this.pic_PageDown.Name = "pic_PageDown";
			this.pic_PageDown.Size = new System.Drawing.Size(16, 16);
			this.pic_PageDown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pic_PageDown.TabIndex = 6;
			this.pic_PageDown.TabStop = false;
			this.toolTip1.SetToolTip(this.pic_PageDown, "次のページ");
			this.pic_PageDown.Click += new System.EventHandler(this.pic_PageDown_Click);
			// 
			// pic_PageHome
			// 
			this.pic_PageHome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pic_PageHome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pic_PageHome.Image = global::HKKS.Properties.Resources.pageHome;
			this.pic_PageHome.Location = new System.Drawing.Point(267, 310);
			this.pic_PageHome.Name = "pic_PageHome";
			this.pic_PageHome.Size = new System.Drawing.Size(16, 16);
			this.pic_PageHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pic_PageHome.TabIndex = 7;
			this.pic_PageHome.TabStop = false;
			this.toolTip1.SetToolTip(this.pic_PageHome, "ホーム");
			this.pic_PageHome.Click += new System.EventHandler(this.pic_PageHome_Click);
			// 
			// pic_PageUp
			// 
			this.pic_PageUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pic_PageUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pic_PageUp.Image = global::HKKS.Properties.Resources.pageLeft;
			this.pic_PageUp.Location = new System.Drawing.Point(297, 310);
			this.pic_PageUp.Name = "pic_PageUp";
			this.pic_PageUp.Size = new System.Drawing.Size(16, 16);
			this.pic_PageUp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pic_PageUp.TabIndex = 8;
			this.pic_PageUp.TabStop = false;
			this.toolTip1.SetToolTip(this.pic_PageUp, "前のページ");
			this.pic_PageUp.Click += new System.EventHandler(this.pic_PageUp_Click);
			// 
			// pic_backward
			// 
			this.pic_backward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pic_backward.Image = ((System.Drawing.Image)(resources.GetObject("pic_backward.Image")));
			this.pic_backward.Location = new System.Drawing.Point(456, 10);
			this.pic_backward.Name = "pic_backward";
			this.pic_backward.Size = new System.Drawing.Size(16, 16);
			this.pic_backward.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pic_backward.TabIndex = 12;
			this.pic_backward.TabStop = false;
			this.pic_backward.Click += new System.EventHandler(this.pic_backward_Click);
			// 
			// pic_forward
			// 
			this.pic_forward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pic_forward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pic_forward.Image = ((System.Drawing.Image)(resources.GetObject("pic_forward.Image")));
			this.pic_forward.Location = new System.Drawing.Point(432, 10);
			this.pic_forward.Name = "pic_forward";
			this.pic_forward.Size = new System.Drawing.Size(16, 16);
			this.pic_forward.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pic_forward.TabIndex = 12;
			this.pic_forward.TabStop = false;
			this.pic_forward.Click += new System.EventHandler(this.pic_forward_Click);
			// 
			// pic_remove
			// 
			this.pic_remove.Image = global::HKKS.Properties.Resources.Del;
			this.pic_remove.Location = new System.Drawing.Point(168, 10);
			this.pic_remove.Name = "pic_remove";
			this.pic_remove.Size = new System.Drawing.Size(16, 16);
			this.pic_remove.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pic_remove.TabIndex = 0;
			this.pic_remove.TabStop = false;
			this.pic_remove.Click += new System.EventHandler(this.pic_remove_Click);
			// 
			// lb_order
			// 
			this.lb_order.AutoSize = true;
			this.lb_order.Location = new System.Drawing.Point(265, 12);
			this.lb_order.Name = "lb_order";
			this.lb_order.Size = new System.Drawing.Size(53, 12);
			this.lb_order.TabIndex = 16;
			this.lb_order.Text = "並び順▲";
			this.lb_order.Click += new System.EventHandler(this.lb_order_Click);
			// 
			// PagedSvgGridView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.lb_order);
			this.Controls.Add(this.pic_backward);
			this.Controls.Add(this.pic_forward);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lb_PageInfo);
			this.Controls.Add(this.txt_Goto);
			this.Controls.Add(this.pic_PageEnd);
			this.Controls.Add(this.pic_PageDown);
			this.Controls.Add(this.pic_PageHome);
			this.Controls.Add(this.pic_PageUp);
			this.Controls.Add(this.panel_Main);
			this.Controls.Add(this.lb_numInfo);
			this.Controls.Add(this.pic_remove);
			this.Controls.Add(this.panel_Top);
			this.Name = "PagedSvgGridView";
			this.Size = new System.Drawing.Size(502, 336);
			this.panel_Top.ResumeLayout(false);
			this.panel_Top.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pic_triangle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_tick)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_crosses)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_rect)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_PageEnd)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_PageDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_PageHome)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_PageUp)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_backward)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_forward)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_remove)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Panel panel_Top;
		private System.Windows.Forms.PictureBox pic_rect;
		private System.Windows.Forms.PictureBox pic_triangle;
		private System.Windows.Forms.PictureBox pic_tick;
		private System.Windows.Forms.PictureBox pic_crosses;
		private System.Windows.Forms.PictureBox pic_remove;
		private System.Windows.Forms.Label lb_numInfo;
		private System.Windows.Forms.FlowLayoutPanel panel_Main;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lb_PageInfo;
		private System.Windows.Forms.TextBox txt_Goto;
		private System.Windows.Forms.PictureBox pic_PageEnd;
		private System.Windows.Forms.PictureBox pic_PageDown;
		private System.Windows.Forms.PictureBox pic_PageHome;
		private System.Windows.Forms.PictureBox pic_PageUp;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.PictureBox pic_forward;
		private System.Windows.Forms.PictureBox pic_backward;
		private System.Windows.Forms.Label lb_order;
	}
}
