using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Svg;
using System.IO;
using System.Web;
using System.Diagnostics;

namespace CustomControlsImitatingUWP
{
	public partial class SvgCompositionView : UserControl
	{
		private const string _photoID = "photo";
		private const string _blackboardID = "blackboard";
		private const string _notesID = "notes";
		private bool _isHovered = false;
		private Color _borderColor;
		private SvgDocument _svgDoc;

		private ImageType _imageType = ImageType.Svg;
		public ImageType ImageType
		{
			get { return _imageType; }
		}

		private string _contentImagePath;
		public string GetContentImagePath
		{
			get { return _contentImagePath; }
		}

		public Bitmap Icon
		{
			get { return (Bitmap)pic_Icon.Image.Clone(); }
		}

		public string _date;
		public string Date
		{
			get { return _date; }
			set
			{
				_date = value;
				lb_Date.Text = value;
			}
		}

		public string _orientation;
		public string Orientation
		{
			get { return _orientation; }
			set
			{
				_orientation = value;
				lb_Orientation.Text = value;
			}
		}

		public string _remark;
		public string Remark
		{
			get { return _remark; }
			set
			{
				_remark = value;
				lb_remark.Text = value;
			}
		}

		private bool _isSelected = false;
		public bool Selected
		{
			get { return _isSelected; }
			set
			{
				_isSelected = value;
				_borderColor = Color.FromArgb(0, 120, 215);
				Invalidate();
			}
		}

		private bool _isDeleted = false;
		public bool IsDeleted
		{
			get { return _isDeleted; }
			set
			{
				_isDeleted = value;
				panel_Del.Visible = value;
			}
		}

		public int Id { get; set; }

		public event EventHandler<MouseEventArgs> SelectionChanged;
		public event EventHandler<DragEventArgs> SvgCompositionViewDragDrop;
		public event EventHandler<DragEventArgs> SvgCompositionViewDragEnter;
		public event EventHandler<DragEventArgs> SvgCompositionViewDragOver;
		public event EventHandler<EventArgs> SvgCompositionViewDragLeave;

		public SvgCompositionView()
		{
			InitializeComponent();
			InitializeHoverEffect();
			DoubleBuffered = true;
			panel_Del.Size = new Size(Width - 4, Height - 4);
			panel_Del.Location = new Point(1, 1);
		}

		private void InitContentImage(string filePath)
		{
			_contentImagePath = filePath;
			pic_svgImage.Image?.Dispose();
			pic_svgImage.Image = null;
			if (string.IsNullOrEmpty(_contentImagePath)) return;
			if (!File.Exists(_contentImagePath)) return;
			_imageType = _contentImagePath.EndsWith(".svg") ? ImageType.Svg : ImageType.Jpg;
			if (_imageType == ImageType.Svg)
			{
				lb_greenbk.Visible = true;
				lb_inkCanvas.Visible = true;
				_svgDoc = null;
				_svgDoc = SvgDocument.Open(_contentImagePath);
				pic_svgImage.Image = _svgDoc.Draw();
				_svgDoc.Children.ToList().ForEach(x =>
				{
					if (x is SvgImage)
					{
						if (x.ID == _photoID)
						{
							lb_photo.BackColor = x.Display == "inline" ? Color.FromArgb(255, 192, 192) : SystemColors.ControlLight;
						}
						if (x.ID == _blackboardID)
						{
							lb_greenbk.BackColor = x.Display == "inline" ? Color.FromArgb(255, 192, 192) : SystemColors.ControlLight;
						}
						if (x.ID == _notesID)
						{
							lb_inkCanvas.BackColor = x.Display == "inline" ? Color.FromArgb(255, 192, 192) : SystemColors.ControlLight;
						}
					}

					if (x is SvgPath)
					{
						lb_inkCanvas.BackColor = x.Display == "inline" ? Color.FromArgb(255, 192, 192) : SystemColors.ControlLight;
					}
				});
			}
			else
			{
				pic_svgImage.Image = new Bitmap(_contentImagePath);
				lb_photo.BackColor = Color.FromArgb(255, 192, 192);
				lb_greenbk.Visible = false;
				lb_inkCanvas.Visible = false;
			}
		}

		private async Task InitContentImageAsync(string filePath)
		{
			_contentImagePath = filePath;
			pic_svgImage.Image?.Dispose();
			pic_svgImage.Image = null;
			if (string.IsNullOrEmpty(_contentImagePath)) return;
			if (!File.Exists(_contentImagePath)) return;
			_imageType = _contentImagePath.EndsWith(".svg") ? ImageType.Svg : ImageType.Jpg;
			if (_imageType == ImageType.Svg)
			{
				lb_greenbk.Visible = true;
				lb_inkCanvas.Visible = true;
				_svgDoc = null;
				await Task.Run(() =>
				{
					_svgDoc = SvgDocument.Open(_contentImagePath);
					pic_svgImage.Image = _svgDoc.Draw();
					_svgDoc.Children.ToList().ForEach(x =>
					{
						if (x is SvgImage)
						{
							if (x.ID == _photoID)
							{
								lb_photo.BackColor = x.Display == "inline" ? Color.FromArgb(255, 192, 192) : SystemColors.ControlLight;
							}
							if (x.ID == _blackboardID)
							{
								lb_greenbk.BackColor = x.Display == "inline" ? Color.FromArgb(255, 192, 192) : SystemColors.ControlLight;
							}
							if (x.ID == _notesID)
							{
								lb_inkCanvas.BackColor = x.Display == "inline" ? Color.FromArgb(255, 192, 192) : SystemColors.ControlLight;
							}
						}

						if (x is SvgPath)
						{
							lb_inkCanvas.BackColor = x.Display == "inline" ? Color.FromArgb(255, 192, 192) : SystemColors.ControlLight;
						}
					});
				});

			}
			else
			{
				pic_svgImage.Image = new Bitmap(_contentImagePath);
				lb_photo.BackColor = Color.FromArgb(255, 192, 192);
				lb_greenbk.Visible = false;
				lb_inkCanvas.Visible = false;
			}
		}

		private void InitIcon(Bitmap bitmap)
		{
			if (bitmap == null) return;
			pic_Icon.Image?.Dispose();
			pic_Icon.Image = null;
			pic_Icon.Image = bitmap;
		}

		private void InitializeHoverEffect()
		{
			MouseEnter += OnControlMouseEnter;
			MouseLeave += OnControlMouseLeave;
			MouseDown += OnControlMouseDown;
			foreach (Control ctrl in GetAllControls(this))
			{
				ctrl.MouseEnter += OnControlMouseEnter;
				ctrl.MouseLeave += OnControlMouseLeave;
				ctrl.MouseDown += OnControlMouseDown;
			}
		}

		private IEnumerable<Control> GetAllControls(Control control)
		{
			List<Control> controls = new List<Control>();
			foreach (Control ctrl in control.Controls)
			{
				controls.Add(ctrl);
				controls.AddRange(GetAllControls(ctrl));
			}
			return controls;
		}

		private void OnControlMouseEnter(object sender, EventArgs e)
		{
			if (_isSelected) return;
			_isHovered = true;
			_borderColor = Color.FromArgb(102, 174, 231);
			Invalidate();
		}

		private void OnControlMouseLeave(object sender, EventArgs e)
		{
			_isHovered = false;
			BorderStyle = BorderStyle.FixedSingle;
			Invalidate();
		}

		private void OnControlMouseDown(object sender, MouseEventArgs e)
		{
			_isHovered = true;
			_isSelected = true;
			_borderColor = Color.FromArgb(0, 120, 215);
			SelectionChanged?.Invoke(this, e);
			Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (_isHovered)
			{
				DrawBorder(e.Graphics);
			}
			if (_isSelected)
			{
				DrawBorder(e.Graphics);
			}
		}

		private void DrawBorder(Graphics graphics)
		{
			using (Pen pen = new Pen(_borderColor, 2))
			{
				Rectangle rect = new Rectangle(
					1, 1,
					this.ClientSize.Width - 2,
					this.ClientSize.Height - 2);
				graphics.DrawRectangle(pen, rect);
			}
		}

		private void DrawDeleteOverlay(Graphics g)
		{
			// 创建灰色半透明遮罩
			using (Brush overlayBrush = new SolidBrush(Color.FromArgb(100, Color.Gray)))
			{
				g.FillRectangle(overlayBrush, this.ClientRectangle);
			}

			// 绘制红色对角线交叉线（左上到右下、右上到左下）
			using (Pen crossPen = new Pen(Color.Red, 3))
			{
				Point p1 = new Point(0, 0);
				Point p2 = new Point(this.ClientSize.Width, this.ClientSize.Height);
				g.DrawLine(crossPen, p1, p2);

				Point p3 = new Point(this.ClientSize.Width, 0);
				Point p4 = new Point(0, this.ClientSize.Height);
				g.DrawLine(crossPen, p3, p4);
			}
		}

		public void SetPhotoVisible(bool visible)
		{
			if (_imageType == ImageType.Jpg) return;
			if (_svgDoc == null) return;
			var item = _svgDoc.Children.FirstOrDefault(x => x is SvgImage && x.ID == _photoID);
			if (item == null) return;
			item.Display = visible ? "inline" : "none";
			lb_photo.BackColor = visible ? Color.FromArgb(255, 192, 192) : SystemColors.ControlLight;
			_svgDoc.Write(_contentImagePath);
			pic_svgImage.Image = _svgDoc.Draw();
		}

		public void SetBlackboardVisible(bool visible)
		{
			if (_imageType == ImageType.Jpg) return;
			if (_svgDoc == null) return;
			var item = _svgDoc.Children.FirstOrDefault(x => x is SvgImage && x.ID == _blackboardID);
			if (item == null) return;
			item.Display = visible ? "inline" : "none";
			lb_greenbk.BackColor = visible ? Color.FromArgb(255, 192, 192) : SystemColors.ControlLight;
			_svgDoc.Write(_contentImagePath);
			pic_svgImage.Image = _svgDoc.Draw();
		}

		public void SetNotesVisible(bool visible)
		{
			if (_imageType == ImageType.Jpg) return;
			if (_svgDoc == null) return;
			var item = _svgDoc.Children.FirstOrDefault(x => x is SvgImage && x.ID == _notesID);
			if (item == null) return;
			item.Display = visible ? "inline" : "none";
			lb_inkCanvas.BackColor = visible ? Color.FromArgb(255, 192, 192) : SystemColors.ControlLight;
			_svgDoc.Write(_contentImagePath);
			pic_svgImage.Image = _svgDoc.Draw();
		}

		public void SetContentImage(string path)
		{
			InitContentImage(path);
		}

		public async Task SetContentImageAsync(string path)
		{
			await InitContentImageAsync(path);
		}

		public void SetIcon(Bitmap bitmap)
		{
			InitIcon(bitmap);
		}

		public void DisposeImage()
		{
			pic_svgImage.Image?.Dispose();
			pic_svgImage.Image = null;
			pic_Icon.Image?.Dispose();
			pic_Icon.Image = null;
		}

		private void panel_Del_Paint(object sender, PaintEventArgs e)
		{
			DrawDeleteOverlay(e.Graphics);
		}

		public void SetDeletedState()
		{
			IsDeleted = !IsDeleted;
			panel_Del.Visible = IsDeleted;
		}

		private void SvgCompositionView_DragEnter(object sender, DragEventArgs e)
		{
			SvgCompositionViewDragEnter?.Invoke(this, e);
		}

		private void SvgCompositionView_DragDrop(object sender, DragEventArgs e)
		{
			SvgCompositionViewDragDrop?.Invoke(this, e);
		}

		private void SvgCompositionView_DragOver(object sender, DragEventArgs e)
		{
			SvgCompositionViewDragOver?.Invoke(this,e);
		}

		private void SvgCompositionView_DragLeave(object sender, EventArgs e)
		{
			SvgCompositionViewDragLeave?.Invoke(this,e);
		}
	}

	public enum SvgCompositionViewInsertDirection
	{
		Null,
		Left,
		Right
	}

	public enum CheckResultIcon
	{ 
	   CheckRsl_1,
	   CheckRsl_2,
	   CheckRsl_4,
	   CheckRsl_5
	}

	public class SvgCompositionViewDto: ICloneable
	{
		public int Id { get; set; } = -1;
		public string ContentImagePath { get; set; }

		public CheckResultIcon CheckResult { get; set; }

		public string Remark { get; set; }
		public string CreateTime { get; set; }

		public string Orientation { get; set; }

		public bool IsDeleted { get; set; } = false;

		public object Clone()
		{
			return new SvgCompositionViewDto
			{
				Id = this.Id,
				ContentImagePath = ContentImagePath,
				CheckResult= CheckResult,
				Remark= Remark,
				CreateTime= CreateTime,
				Orientation= Orientation,
				IsDeleted= IsDeleted
			};
		}
	}
}

