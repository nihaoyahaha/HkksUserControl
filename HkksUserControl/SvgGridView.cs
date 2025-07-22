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

namespace HkksUserControl
{
	public partial class SvgGridView : UserControl
	{
		//插入光标颜色
		private Color _insertCursorColor = Color.Coral;

		//元素控件数据
		private List<SvgCompositionViewDto> _dtos;

		//元素控件原始数据
		private List<SvgCompositionViewDto> _currentdtos;

		private int _selectedIndex = -1;

		//用于判断是否绘制插入光标
		private bool _isDragOver = false;

		//当前选中的元素
		private SvgCompositionView _selectedItem;

		//元素控件
		private List<SvgCompositionView> _dataSouce = new List<SvgCompositionView>();

		//目标元素尺寸
		private Rectangle _rectDragEnter = Rectangle.Empty;

		//拖拽时在目标元素上的位置
		private SvgCompositionViewInsertDirection _insertDirection = SvgCompositionViewInsertDirection.Null;

		//元素点击事件
		public event EventHandler<EventArgs> ClickItem;

		public SvgCompositionView SelectedItem
		{
			get { return _selectedItem; }
		}

		public List<SvgCompositionViewDto> DataSouce
		{
			get 
			{
				return _dtos; 
			}
		}

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

		private void SetItemDeletedState(SvgCompositionView item)
		{
			if (item == null) return;
			item.SetDeletedState();
			_dtos.FirstOrDefault(x => x.Id == item.Id).ChangeType = item.ChangeType;
		}

		//释放元素控件
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
				item.Dispose();
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
	
		//删除元素控件
		private void pic_remove_Click(object sender, EventArgs e)
		{
			if (_selectedItem == null) return;
			Remove(_selectedItem);
		}

		//添加元素控件
		private async Task InitDataSourceAsync(List<SvgCompositionViewDto> dtos)
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
			SvgCompositionView svgComposition = SvgCompositionView.Create(dto);
			await svgComposition.SetContentImageAsync(dto.ContentImagePath);
			AddItem(svgComposition);
		}

		private void AddItem(SvgCompositionView item)
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

		//设置按钮启用状态
		private void SetControlEnable(bool enable)
		{
			pic_rect.Enabled = enable;
			pic_crosses.Enabled = enable;
			pic_tick.Enabled = enable;
			pic_triangle.Enabled = enable;
			pic_remove.Enabled = enable;
			panel_Main.Enabled = enable;
		}

		//元素前移
		private void pic_forward_Click(object sender, EventArgs e)
		{
			if (_selectedItem == null) return;
			if (_selectedIndex <= 0) return;
			MoveSelectedItemsForward();
		}

		//元素后移
		private void pic_backward_Click(object sender, EventArgs e)
		{
			if (_selectedItem == null) return;
			if (_selectedIndex < 0) return;
			if (_selectedIndex == _dataSouce.Count - 1) return;
			MoveSelectedItemsBackForward();
		}

		//当前页内数据前移
		private void MoveSelectedItemsForward() => MoveSelectedItems(_selectedIndex - 1);

		//当前页内数据移动
		private void MoveSelectedItems(int targetIndex)
		{
			var targetItem = _dataSouce[targetIndex];

			int dto_selectedIndex = _dtos.IndexOf(_dtos.FirstOrDefault(x => x.Id == _selectedItem.Id));
			int dto_targetIndex = _dtos.IndexOf(_dtos.FirstOrDefault(x => x.Id == targetItem.Id));

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

		//当前页内数据后移
		private void MoveSelectedItemsBackForward() => MoveSelectedItems(_selectedIndex + 1);

		//绘制插入光标
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

		//元素选中事件
		private void OnSelected(object sender, MouseEventArgs e)
		{
			ClickItem?.Invoke(sender,e);
			var obj = _dataSouce.FirstOrDefault(x => x.Selected && !ReferenceEquals(x, sender));
			if (obj != null) obj.Selected = false;
			_selectedItem = sender as SvgCompositionView;
			_selectedItem.Selected = true;
			_selectedIndex = _dataSouce.IndexOf(_selectedItem);
			SetNumInfo();

			panel_Main.DoDragDrop(_selectedItem, DragDropEffects.Move);
			panel_Main.ScrollControlIntoView(_selectedItem);
		}

		//拖动进入元素时触发
		private void OnDragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(SvgCompositionView)))
			{
				e.Effect = DragDropEffects.Move;
			}
		}

		//拖动释放鼠标键时触发
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

		//拖动在元素内部移动时持续触发
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

		//拖动离开元素时触发
		private void OnDragLeave(object sender, EventArgs e)
		{
			_isDragOver = false;
			panel_Main.Invalidate();
		}

		//清除元素控件
		private void ClearDataSource()
		{
			if (_dataSouce == null) return;
			for (int i = _dataSouce.Count-1; i >=0; i--)
			{
				var item = _dataSouce[i];
				_dataSouce.Remove(item);
				panel_Main.Controls.Remove(item);
				item.SelectionChanged -= OnSelected;
				item.SvgCompositionViewDragDrop -= OnDragDrop;
				item.SvgCompositionViewDragEnter -= OnDragEnter;
				item.SvgCompositionViewDragOver -= OnDragOver;
				item.SvgCompositionViewDragLeave -= OnDragLeave;
				item.Dispose();
				item = null;
			}
			lb_numInfo.Text = "0/0";
			_selectedIndex = -1;
			GC.Collect();
		}

		//拖拽元素完成后修改数据位置
		private void MoveSelectedItem(int indexToMove, int targetIndex, bool insertAfter)
		{
			var item = _dataSouce[indexToMove];
			var control = panel_Main.Controls[indexToMove];
			var dtoItem = _dtos.FirstOrDefault(x => x.Id == item.Id);
			var currentIndex = _dtos.IndexOf(dtoItem);

			_dataSouce.RemoveAt(indexToMove);
			panel_Main.Controls.RemoveAt(indexToMove);
			_dtos.RemoveAt(currentIndex);

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


			if (insertPosition <= _dataSouce.Count - 2)
			{
				var nextItem = _dataSouce[insertPosition + 1];
				int insertIndex = _dtos.IndexOf(_dtos.FirstOrDefault(x => x.Id == nextItem.Id));
				_dtos.Insert(insertIndex, dtoItem);
			}
			else
			{
				var previousItem = _dataSouce[insertPosition - 1];
				int insertIndex = _dtos.IndexOf(_dtos.FirstOrDefault(x => x.Id == previousItem.Id));
				_dtos.Insert(insertIndex + 1, dtoItem);
			}

			SetSelectedIndex(insertPosition);
		}

		//设置检查结果
		private void SetResultIcon(CheckResultIcon resultIcon)
		{
			if (_selectedItem == null) return;
			_selectedItem.SetResultIcon(resultIcon);
			_dtos.FirstOrDefault(x => x.Id == _selectedItem.Id).CheckResult = resultIcon;
		}

		private void pic_rect_Click(object sender, EventArgs e) => SetResultIcon(CheckResultIcon.CheckRsl_1);
		
		private void pic_crosses_Click(object sender, EventArgs e) => SetResultIcon(CheckResultIcon.CheckRsl_2);
		
		private void pic_tick_Click(object sender, EventArgs e) => SetResultIcon(CheckResultIcon.CheckRsl_4);
		
		private void pic_triangle_Click(object sender, EventArgs e) => SetResultIcon(CheckResultIcon.CheckRsl_5);
		
	   //检查数据是否被排序过
		public bool IsOrderChanged()
		{
			bool isChanged = false;
			for (int i = 0; i < _dtos.Count; i++)
			{
				if (_dtos[i].Id != _currentdtos[i].Id) isChanged = true;
			}
			return isChanged;
		}

		private void DataCompare(SvgCompositionViewDto obj1, SvgCompositionViewDto obj2)
		{
			if (obj1.LayerDisplay != obj2.LayerDisplay) { obj1.ChangeType = ChangeType.Modified; }
			if (obj1.CheckResult != obj2.CheckResult) { obj1.ChangeType = ChangeType.Modified; }
			if (obj1.Remark != obj2.Remark) { obj1.ChangeType = ChangeType.Modified; }
			if (obj1.Orientation != obj2.Orientation) { obj1.ChangeType = ChangeType.Modified; }
		}

		//判断数据是否被修改
		public bool IsDataChanged()
		{
			if (_dtos.Count(x => x.ChangeType  != ChangeType.None) > 0) return true;
			if (IsOrderChanged()) return true;
			return false;
		}

		//设置当前选中的元素
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

		//设置绿板层显示状态
		public void SetBlackboardVisible(bool visible)
		{
			if (_selectedItem == null) return;
			_selectedItem.SetBlackboardVisible(visible);
		}

		//设置画图层显示状态
		public void SetNotesVisible(bool visible)
		{
			if (_selectedItem == null) return;
			_selectedItem.SetNotesVisible(visible);
		}

		//绑定数据
		public async Task BindAsync(params SvgCompositionViewDto[] dtos)
		{
			_dtos = null;
			ClearDataSource();
			int index = 0;
			dtos.ToList().ForEach(x =>
			{
				x.Id = index;
				index++;
			});
			_dtos = dtos.ToList();
			_currentdtos = _dtos.Select(d => (SvgCompositionViewDto)d.Clone()).ToList();
			await InitDataSourceAsync(_dtos);
		}

		//升序/降序排序
		private async void lb_order_Click(object sender, EventArgs e)
		{
			if (_dataSouce.Count==0) return;
			lb_order.Text = lb_order.Text.Contains("▼") ? "並び順▲" : "並び順▼";
			if (_dtos == null) return;
			_dtos.Reverse();
			ClearDataSource();
			await InitDataSourceAsync(_dtos);
		}

		//修改元素数据
		public void UpdateItem(SvgCompositionViewDto dto)
		{
			if (_selectedItem == null) return;
			_selectedItem.Update(dto);
			DataCompare(_dtos.FirstOrDefault(x=>x.Id == _selectedItem.Id),dto);
		}

	}
}
