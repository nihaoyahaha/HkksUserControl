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
using CustomControlsImitatingUWP.Properties;

namespace HkksUserControl
{
	public partial class SvgCompositionView : UserControl
	{
		private const string _blackboardID = "blackboard";
		private const string _notesID = "notes";
		private bool _isHovered = false;
		private Color _borderColor;
		private SvgDocument _svgDoc;

		//图片类型
		private ImageType _imageType = ImageType.Svg;
		public ImageType ImageType
		{
			get { return _imageType; }
		}

		//内容图片路径
		private string _contentImagePath;
		public string GetContentImagePath
		{
			get { return _contentImagePath; }
		}

		//检查结果图标
		private CheckResultIcon _resultIcon = CheckResultIcon.Null;
		public CheckResultIcon ResultIcon
		{
			get { return _resultIcon; }
			private set
			{
				_resultIcon = value;
				switch (value)
				{
					case CheckResultIcon.CheckRsl_1: pic_Icon.Image = Resources.checkRsl_1; break;
					case CheckResultIcon.CheckRsl_2: pic_Icon.Image = Resources.checkRsl_2; break;
					case CheckResultIcon.CheckRsl_4: pic_Icon.Image = Resources.checkRsl_4; break;
					case CheckResultIcon.CheckRsl_5: pic_Icon.Image = Resources.checkRsl_5; break;
				}
			}
		}

		//创建时间
		public string _date;
		public string Date
		{
			get { return _date; }
			private set
			{
				_date = value;
				lb_Date.Text = value;
			}
		}

		//摄影方向
		private string _orientation = "";
		public string Orientation
		{
			get { return _orientation; }
			private set
			{
				_orientation = value;
				lb_Orientation.Text = value;
			}
		}

		//备注
		private string _remark = "";
		public string Remark
		{
			get { return _remark; }
			private set
			{
				_remark = value;
				lb_remark.Text = value;
			}
		}

		//选中状态
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

		//svg 图层的显示状态
		private SvgLayerDisplay _layerDisplay = SvgLayerDisplay.ShowAll;

		/// <summary>
		///    写真      黑板      注释
		/// 7:  ⭕        ⭕        ⭕
		/// 3:  ⭕        ⭕        ✖
		/// 5:  ⭕        ✖        ⭕
		/// 1:  ⭕        ✖        ✖
		/// </summary>
		public SvgLayerDisplay LayerDisplay
		{
			get { return _layerDisplay; }
			private set
			{
				_layerDisplay = value;
			}
		}

		//控件索引
		public int Id { get; private set; }

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

		public SvgCompositionView(SvgCompositionViewDto dto) : this() => Create(dto);

		//初始化内容图片
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
				_svgDoc = null;
				pic_svgImage.Image = await Task.Run(() =>
				{
					_svgDoc = SvgDocument.Open(_contentImagePath);
					return _svgDoc.Draw();
				});
			}
			else
			{
				pic_svgImage.Image = new Bitmap(_contentImagePath);
			}
			SetLabelBackColor();
		}

		//根据显示svg图层显示状态设置控件背景颜色
		private void SetLabelBackColor()
		{
			if (_imageType == ImageType.Svg)
			{
				lb_greenbk.Visible = true;
				lb_inkCanvas.Visible = true;
				switch (_layerDisplay)
				{
					case SvgLayerDisplay.OnlyNotesHide:
						lb_photo.BackColor = Color.FromArgb(255, 192, 192);
						lb_greenbk.BackColor = Color.FromArgb(255, 192, 192);
						lb_inkCanvas.BackColor = SystemColors.ControlLight;
						break;
					case SvgLayerDisplay.OnlyGreenBoardHide:
						lb_photo.BackColor = Color.FromArgb(255, 192, 192);
						lb_greenbk.BackColor = SystemColors.ControlLight;
						lb_inkCanvas.BackColor = Color.FromArgb(255, 192, 192);
						break;
					case SvgLayerDisplay.GreenBoardAndNotesHide:
						lb_photo.BackColor = Color.FromArgb(255, 192, 192);
						lb_greenbk.BackColor = SystemColors.ControlLight;
						lb_inkCanvas.BackColor = SystemColors.ControlLight;
						break;
					default:
						lb_photo.BackColor = Color.FromArgb(255, 192, 192);
						lb_greenbk.BackColor = Color.FromArgb(255, 192, 192);
						lb_inkCanvas.BackColor = Color.FromArgb(255, 192, 192);
						break;
				}
			}
			else
			{
				lb_photo.BackColor = Color.FromArgb(255, 192, 192);
				lb_greenbk.Visible = false;
				lb_inkCanvas.Visible = false;
			}
		}

		//注册事件
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

		//绘制选中样式
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

		//绘制删除样式(仅PagedSvgGridView 可用)
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

		//设置svg绿板图层的显示隐藏
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
			SetSvgLayerDisplay();
		}

		//设置svg注释图层的显示隐藏
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
			SetSvgLayerDisplay();
		}

		//设置svg图层显示状态
		private void SetSvgLayerDisplay()
		{
			if (lb_photo.Visible && lb_greenbk.Visible && lb_inkCanvas.Visible)
			{
				_layerDisplay = SvgLayerDisplay.ShowAll;
			}
			else if (lb_photo.Visible && lb_greenbk.Visible && !lb_inkCanvas.Visible)
			{
				_layerDisplay = SvgLayerDisplay.OnlyNotesHide;
			}
			else if (lb_photo.Visible && !lb_greenbk.Visible && lb_inkCanvas.Visible)
			{
				_layerDisplay = SvgLayerDisplay.OnlyGreenBoardHide;
			}
			else if (lb_photo.Visible && !lb_greenbk.Visible && !lb_inkCanvas.Visible)
			{
				_layerDisplay = SvgLayerDisplay.GreenBoardAndNotesHide;
			}
		}		

		public ChangeType SetDeletedState()
		{
			panel_Del.Visible = !panel_Del.Visible;
			return panel_Del.Visible ? ChangeType.Deleted : ChangeType.None;
		}

		//设置内容图片
		public async Task SetContentImageAsync(string path) => await InitContentImageAsync(path);

		//绘制删除时的状态(仅PagedSvgGridView可用)
		private void panel_Del_Paint(object sender, PaintEventArgs e) => DrawDeleteOverlay(e.Graphics);

		private void SvgCompositionView_DragEnter(object sender, DragEventArgs e) => SvgCompositionViewDragEnter?.Invoke(this, e);

		private void SvgCompositionView_DragDrop(object sender, DragEventArgs e) => SvgCompositionViewDragDrop?.Invoke(this, e);

		private void SvgCompositionView_DragOver(object sender, DragEventArgs e) => SvgCompositionViewDragOver?.Invoke(this, e);

		private void SvgCompositionView_DragLeave(object sender, EventArgs e) => SvgCompositionViewDragLeave?.Invoke(this, e);

		public async Task RefreshContentImageAsync() => await InitContentImageAsync(_contentImagePath);

		//设置检查结果图标
		public void SetResultIcon(CheckResultIcon resultIcon) => ResultIcon = resultIcon;

		public SvgCompositionView Create(SvgCompositionViewDto dto)
		{
			SvgCompositionView svgComposition = new SvgCompositionView();
			svgComposition.Id = dto.Id;
			svgComposition.ResultIcon = dto.CheckResult;
			svgComposition.Remark = dto.Remark;
			svgComposition.Date = dto.CreateTime;
			svgComposition.Orientation = dto.Orientation;
			svgComposition.LayerDisplay = dto.LayerDisplay;
			if (dto.ChangeType == ChangeType.Deleted) panel_Del.Visible = false;
			return svgComposition;
		}

		public void Update(SvgCompositionViewDto dto)
		{
			ResultIcon = dto.CheckResult;
			Remark = dto.Remark;
			Orientation = dto.Orientation;
			LayerDisplay = dto.LayerDisplay;
		}
	}
}

