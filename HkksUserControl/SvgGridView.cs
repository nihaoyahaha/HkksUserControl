using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace CustomControlsImitatingUWP
{
	public partial class SvgGridView : UserControl
	{
		private Color _insertCursorColor = Color.Coral;
		private SvgCompositionViewDto[] _dtos;

		private int _selectedIndex = -1;
		public int SelectedIndex
		{
			get { return _selectedIndex; }
		}

		private SvgCompositionView _selectedItem;
		public SvgCompositionView SelectedItem
		{
			get { return _selectedItem; }
		}

		private List<SvgCompositionView> _dataSouce = new List<SvgCompositionView>();

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<SvgCompositionView> DataSource
		{
			get
			{
				return _dataSouce;
			}
			set
			{
				if (value == null || value.Count == 0) return;
				_dataSouce = new List<SvgCompositionView>();
				_dataSouce = value;
				panel_Main.Controls.Clear();
				_dataSouce.ForEach(x =>
				{
					if (x != null)
					{
						x.SelectionChanged += OnSelected;
						x.SvgCompositionViewDragDrop += OnDragDrop;
						x.SvgCompositionViewDragEnter += OnDragEnter;
						x.SvgCompositionViewDragOver += OnDragOver;
						x.SvgCompositionViewDragLeave += OnDragLeave;
						panel_Main.Controls.Add(x);
					}
				});
				SetNumInfo();
			}
		}

		private Rectangle _rectDragEnter = Rectangle.Empty;

		private SvgCompositionViewInsertDirection _insertDirection = SvgCompositionViewInsertDirection.Null;

		private bool _isDragOver = false;

		public SvgGridView()
		{
			InitializeComponent();
			DoubleBuffered = true;
			typeof(Panel).InvokeMember(
			"DoubleBuffered",
			System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
			null,
			panel_Main,
			new object[] { true });
		}

		private void SetNumInfo()
		{
			lb_numInfo.Text = $"{_selectedIndex + 1}/{_dataSouce.Count}";
		}

		private void MoveSelectedItem(int indexToMove, int targetIndex, bool insertAfter)
		{
			var item = _dataSouce[indexToMove];
			var control = panel_Main.Controls[indexToMove];

			_dataSouce.RemoveAt(indexToMove);
			panel_Main.Controls.RemoveAt(indexToMove);

			int insertPosition = targetIndex;
			if (insertAfter)
			{
				if (indexToMove > targetIndex)
				{
					insertPosition++;
				}
			}
			else
			{
				if (indexToMove < targetIndex)
				{
					insertPosition--;
				}
			}
			_dataSouce.Insert(insertPosition, item);
			panel_Main.Controls.Add(item);
			panel_Main.Controls.SetChildIndex(control, insertPosition);
			SetSelectedIndex(insertPosition);
		}

		public void Add(SvgCompositionView item)
		{
			if (item == null) return;
			item.SelectionChanged += OnSelected;
			item.SvgCompositionViewDragDrop += OnDragDrop;
			item.SvgCompositionViewDragEnter += OnDragEnter;
			item.SvgCompositionViewDragOver += OnDragOver;
			item.SvgCompositionViewDragLeave += OnDragLeave;
			_dataSouce.Add(item);
			panel_Main.Controls.Add(item);
			SetNumInfo();
		}

		private void SetItemDeletedState(SvgCompositionView item)
		{
			if (item == null) return;
			item.SetDeletedState();
			_dataSouce.FirstOrDefault(x => x.Id == item.Id).IsDeleted = item.IsDeleted;
		}

		private void Remove(SvgCompositionView item)
		{
			if (item == null) return;
			SetItemDeletedState(item);
			if (_dataSouce.Contains(item))
			{
				_dataSouce.Remove(item);
				panel_Main.Controls.Remove(item);
				item.SelectionChanged -= OnSelected;
				item.SvgCompositionViewDragDrop -= OnDragDrop;
				item.SvgCompositionViewDragEnter -= OnDragEnter;
				item.SvgCompositionViewDragOver -= OnDragOver;
				item.SvgCompositionViewDragLeave -= OnDragLeave;
				item.DisposeImage();
				item.Dispose();
				item = null;
				_selectedItem = null;
				GC.Collect();
				if (_dataSouce.Count > 0)
				{
					var index = _selectedIndex == _dataSouce.Count ? (_dataSouce.Count > 1 ? _selectedIndex - 1 : 0) : _selectedIndex;
					_selectedItem = _dataSouce[index];
					SetNumInfo();
					SetSelectedIndex(index);
				}
				else
				{
					_selectedIndex = -1;
					_selectedItem = null;
					SetNumInfo();
				}
			}
		}

		private void CheckPicture_Click(object sender, EventArgs e)
		{
			if (_selectedItem == null) return;
			var picturebox = sender as PictureBox;
			_selectedItem.SetIcon((Bitmap)picturebox.Image.Clone());
		}

		public void SetSelectedIndex(int index)
		{
			if (index < 0) return;
			if (index >= _dataSouce.Count) return;
			_selectedIndex = index;
			if (_selectedItem != null) _selectedItem.Selected = false;
			_selectedItem = _dataSouce[index];
			_selectedItem.Selected = true;
			SetNumInfo();
		}

		private void pic_remove_Click(object sender, EventArgs e)
		{
			if (_selectedItem == null) return;
			Remove(_selectedItem);
		}

		public void SetPhotoVisible(bool visible)
		{
			if (_selectedItem == null) return;
			_selectedItem.SetPhotoVisible(visible);
		}

		public void SetBlackboardVisible(bool visible)
		{
			if (_selectedItem == null) return;
			_selectedItem.SetBlackboardVisible(visible);
		}

		public void SetNotesVisible(bool visible)
		{
			if (_selectedItem == null) return;
			_selectedItem.SetNotesVisible(visible);
		}

		public async Task BindAsync(params SvgCompositionViewDto[] dtos)
		{
			int index = 0;
			dtos.ToList().ForEach(x =>
			{
				x.Id = index;
				index++;
			});
			_dtos = dtos;
			await InitDataSourceAsync(_dtos);
		}

		private async Task InitDataSourceAsync(params SvgCompositionViewDto[] dtos)
		{
			SetControlEnable(false);
			Cursor = Cursors.WaitCursor;
			foreach (var item in dtos)
			{
				await AddSvgCompositionViewAsync(item);
			}
			SetControlEnable(true);
			Cursor = Cursors.Default;
			SetSelectedIndex(0);
		}

		private async Task AddSvgCompositionViewAsync(SvgCompositionViewDto dto)
		{
			SvgCompositionView svgComposition = SetSvgCompositionViewProperties(dto);
			await svgComposition.SetContentImageAsync(dto.ContentImagePath);
			Add(svgComposition);
		}

		private SvgCompositionView SetSvgCompositionViewProperties(SvgCompositionViewDto dto)
		{
			SvgCompositionView svgComposition = new SvgCompositionView();
			svgComposition.Id = dto.Id;
			svgComposition.IsDeleted = dto.IsDeleted;
			svgComposition.SetIcon(dto.IconImagePath);
			svgComposition.Remark = dto.Remark;
			svgComposition.Date = dto.CreateTime;
			svgComposition.Orientation = dto.Orientation;

			return svgComposition;
		}

		private void SetControlEnable(bool enable)
		{
			pic_rect.Enabled = enable;
			pic_crosses.Enabled = enable;
			pic_tick.Enabled = enable;
			pic_triangle.Enabled = enable;
			pic_remove.Enabled = enable;
			panel_Main.Enabled = enable;
		}

		private void pic_forward_Click(object sender, EventArgs e)
		{
			if (_selectedItem == null) return;
			if (_selectedIndex <= 0) return;
			MoveSelectedItemsForward();
		}

		//当前页内数据前移
		private void MoveSelectedItemsForward() => MoveSelectedItems(_selectedIndex - 1);

		//当前页内数据移动
		private void MoveSelectedItems(int targetIndex)
		{
			var targetItem = _dataSouce[targetIndex];

			int dto_selectedIndex = _dtos.ToList().IndexOf(_dtos.FirstOrDefault(x => x.Id == _selectedItem.Id));
			int dto_targetIndex = _dtos.ToList().IndexOf(_dtos.FirstOrDefault(x => x.Id == targetItem.Id));

			panel_Main.Controls.SetChildIndex(_selectedItem, targetIndex);
			panel_Main.Controls.SetChildIndex(targetItem, _selectedIndex);

			var temp = _selectedItem;
			var dto_temp = _dtos[dto_selectedIndex];

			_dataSouce[_selectedIndex] = _dataSouce[targetIndex];
			_dtos[dto_selectedIndex] = _dtos[dto_targetIndex];

			_dataSouce[targetIndex] = temp;
			_dtos[dto_targetIndex] = dto_temp;

			SetSelectedIndex(targetIndex);
		}

		private void pic_backward_Click(object sender, EventArgs e)
		{
			if (_selectedItem == null) return;
			if (_selectedIndex < 0) return;
			if (_selectedIndex == _dataSouce.Count - 1) return;
			MoveSelectedItemsBackForward();
		}

		//当前页内数据后移
		private void MoveSelectedItemsBackForward() => MoveSelectedItems(_selectedIndex + 1);

		private void panel_Main_Paint(object sender, PaintEventArgs e)
		{
			if (_rectDragEnter != Rectangle.Empty)
			{
				DrawInsertCursor(e.Graphics);
			}
		}

		private void DrawInsertCursor(Graphics g)
		{
			if (!_isDragOver) return;
			using (Pen crossPen = new Pen(_insertCursorColor, 3))
			{
				Point p1 = Point.Empty;
				Point p2 = Point.Empty;
				if (_insertDirection == SvgCompositionViewInsertDirection.Left)
				{
					p1 = new Point(_rectDragEnter.X - 4, _rectDragEnter.Y);
					p2 = new Point(_rectDragEnter.X - 4, _rectDragEnter.Y + _rectDragEnter.Height);
				}
				else
				{
					p1 = new Point(_rectDragEnter.X + _rectDragEnter.Width + 2, _rectDragEnter.Y);
					p2 = new Point(_rectDragEnter.X + _rectDragEnter.Width + 2, _rectDragEnter.Y + _rectDragEnter.Height);
				}

				g.DrawLine(crossPen, p1, p2);
			}
		}

		private void OnSelected(object sender, MouseEventArgs e)
		{
			var obj = _dataSouce.FirstOrDefault(x => x.Selected && !ReferenceEquals(x, sender));
			if (obj != null) obj.Selected = false;
			_selectedItem = sender as SvgCompositionView;
			_selectedItem.Selected = true;
			_selectedIndex = _dataSouce.IndexOf(_selectedItem);
			SetNumInfo();

			panel_Main.DoDragDrop(_selectedItem, DragDropEffects.Move);
		}

		private void OnDragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(SvgCompositionView)))
			{
				e.Effect = DragDropEffects.Move;
			}
		}

		private void OnDragDrop(object sender, DragEventArgs e)
		{
			var targetItem = sender as SvgCompositionView;
			int targetIndex = _dataSouce.IndexOf(targetItem);
			if (targetIndex == _selectedIndex) return;

			Point clientPoint = targetItem.PointToClient(new Point(e.X, e.Y));
			bool isLeftSide = clientPoint.X < targetItem.Width / 2;
			MoveSelectedItem(_selectedIndex, targetIndex, !isLeftSide);
			_insertDirection = SvgCompositionViewInsertDirection.Null;
			_rectDragEnter = Rectangle.Empty;
			_isDragOver = false;
			panel_Main.Invalidate();
		}

		private void OnDragOver(object sender, DragEventArgs e)
		{
			if (!e.Data.GetDataPresent(typeof(SvgCompositionView))) 
			{ 
				return; 
			}
			_isDragOver = true;
			var targetItem = sender as SvgCompositionView;
			if (targetItem.Id == _selectedItem.Id) return;

			_insertDirection = SvgCompositionViewInsertDirection.Null;
			_rectDragEnter = Rectangle.Empty;
			
			Point clientPoint = targetItem.PointToClient(new Point(e.X, e.Y));
			bool isLeftSide = clientPoint.X < targetItem.Width / 2;
			_insertDirection = isLeftSide ? SvgCompositionViewInsertDirection.Left : SvgCompositionViewInsertDirection.Right;
			_rectDragEnter = new Rectangle(targetItem.Location, targetItem.Size);
			panel_Main.Invalidate();
		}

		private void OnDragLeave(object sender, EventArgs e)
		{
			_isDragOver = false;
			panel_Main.Invalidate();
		}


	}
}
