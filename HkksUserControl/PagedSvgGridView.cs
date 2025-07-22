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
using System.Linq.Expressions;

namespace HkksUserControl
{
	public partial class PagedSvgGridView : UserControl
	{
		private Color _insertCursorColor = Color.Coral;

		private Rectangle _rectDragEnter = Rectangle.Empty;

		private SvgCompositionViewInsertDirection _insertDirection = SvgCompositionViewInsertDirection.Null;

		private bool _isDragOver = false;

		private DataPager<SvgCompositionViewDto> _dataPager;

		private int _pageSize = 9;

		private int _selectedIndex = -1;

		private SvgCompositionView _selectedItem;

		private List<SvgCompositionView> _dataSouce = new List<SvgCompositionView>();

		private int _currentPage
		{
			get { return _dataPager.CurrentPage; }
		}

		private int _totalPages
		{
			get { return _dataPager.TotalPages; }
		}

		private List<SvgCompositionViewDto> _dtos;

		private List<SvgCompositionViewDto> _currentdtos;

		[Description("每页显示的记录数，取值范围为9到18。")]
		[Category("分页设置")]
		[DefaultValue(9)]
		public int PageSize
		{
			get { return _pageSize; }
			set
			{
				_pageSize = value < 9 ? 9 : (value > 18 ? 18 : value);
			}
		}

		public List<SvgCompositionViewDto> DataSource {
			get { return _dtos; }
		}

		public event EventHandler<EventArgs> ClickItem;

		public PagedSvgGridView()
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

		private void Add(SvgCompositionView item, bool isSetSelectedIndex = true)
		{
			if (item == null) return;
			item.SelectionChanged += OnSelected;
			item.SvgCompositionViewDragDrop += OnDragDrop;
			item.SvgCompositionViewDragEnter += OnDragEnter;
			item.SvgCompositionViewDragOver += OnDragOver;
			item.SvgCompositionViewDragLeave += OnDragLeave;
			_dataSouce.Add(item);
			panel_Main.Controls.Add(item);
			if (isSetSelectedIndex) SetSelectedIndex(_dataSouce.Count - 1);
			SetNumInfo();
		}

		private void SetItemDeletedState(SvgCompositionView item)
		{
			if (item == null) return;
			_dtos.FirstOrDefault(x => x.Id == item.Id).ChangeType = item.SetDeletedState();
		}

		private void pic_remove_Click(object sender, EventArgs e)
		{
			if (_selectedItem == null) return;
			SetItemDeletedState(_selectedItem);
		}

		private async Task AddSvgCompositionViewAsync(SvgCompositionViewDto dto)
		{
			SvgCompositionView svgComposition =new SvgCompositionView(dto);
			await svgComposition.SetContentImageAsync(dto.ContentImagePath);
			Add(svgComposition, false);
		}

		private void DisposeDataSourceItem()
		{
			if (_dataSouce != null)
			{
				_dataSouce.ForEach(x =>
				{
					x.SelectionChanged -= OnSelected;
					x.SvgCompositionViewDragDrop -= OnDragDrop;
					x.SvgCompositionViewDragEnter -= OnDragEnter;
					x.SvgCompositionViewDragOver -= OnDragOver;
					x.SvgCompositionViewDragLeave -= OnDragLeave;
					x.Dispose();
				});
			}
			_dataSouce.Clear();
			_dataSouce = new List<SvgCompositionView>();
			GC.Collect();
			panel_Main.Controls.Clear();
		}

		private async Task InitDataSourceAsync(params SvgCompositionViewDto[] dtos)
		{
			DisposeDataSourceItem();
			SetControlEnable(false);
			Cursor = Cursors.WaitCursor;
			lb_PageInfo.Text = $"{_currentPage}/{_totalPages}";
			foreach (var item in dtos)
			{
				await AddSvgCompositionViewAsync(item);
			}
			SetControlEnable(true);
			Cursor = Cursors.Default;
			SetSelectedIndex(0);
		}

		private void SetControlEnable(bool enable)
		{
			pic_rect.Enabled = enable;
			pic_crosses.Enabled = enable;
			pic_tick.Enabled = enable;
			pic_triangle.Enabled = enable;
			pic_remove.Enabled = enable;
			panel_Main.Enabled = enable;
			txt_Goto.Enabled = enable;
		}

		/// <summary>
		/// 首页
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void pic_PageHome_Click(object sender, EventArgs e)
		{
			if (_dataPager == null) return;
			var data = _dataPager.FirstPage();
			await InitDataSourceAsync(data.ToArray());
		}

		/// <summary>
		/// 上页
		/// </summary>
		private async void pic_PageUp_Click(object sender, EventArgs e)
		{
			if (_dataPager == null) return;
			var data = _dataPager.PreviousPage();
			await InitDataSourceAsync(data.ToArray());
		}

		/// <summary>
		/// 下页
		/// </summary>
		private async void pic_PageDown_Click(object sender, EventArgs e)
		{
			if (_dataPager == null) return;
			var data = _dataPager.NextPage();
			await InitDataSourceAsync(data.ToArray());
		}

		/// <summary>
		/// 尾页
		/// </summary>
		private async void pic_PageEnd_Click(object sender, EventArgs e)
		{
			if (_dataPager == null) return;
			var data = _dataPager.LastPage();
			await InitDataSourceAsync(data.ToArray());
		}

		/// <summary>
		/// 目标页跳转
		/// </summary>
		private async Task GoToAsync(int page)
		{
			if (_dataPager == null) return;
			var data = _dataPager.GoToPage(page);
			await InitDataSourceAsync(data.ToArray());
			lb_PageInfo.Text = $"{_dataPager.CurrentPage}/{_dataPager.TotalPages}";
		}

		private void txt_Goto_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (char.IsDigit(e.KeyChar) || e.KeyChar == '\b') return;

			e.Handled = true;
		}

		private async void txt_Goto_KeyDown(object sender, KeyEventArgs e)
		{
			if (string.IsNullOrEmpty(txt_Goto.Text)) return;
			if (_dataPager == null) return;
			if (e.KeyData == Keys.Enter)
			{
				int pageNum = int.Parse(txt_Goto.Text);
				if (pageNum > _totalPages) txt_Goto.Text = _totalPages.ToString();
				if (pageNum < 1) txt_Goto.Text = "1";
				await GoToAsync(int.Parse(txt_Goto.Text));
			}
		}

		//向前移动
		private async void pic_forward_Click(object sender, EventArgs e)
		{
			if (_dataPager == null) return;
			if (_selectedItem == null) return;
			if (_selectedIndex < 0) return;
			if (_currentPage == 1 && _selectedIndex == 0) return;
			if (_selectedIndex > 0)
			{
				MoveSelectedItemsForward();
			}
			else
			{
				await MoveSelectedItemsAcrossPagesForwardAsync();
			}

		}

		//向后移动
		private async void pic_backward_Click(object sender, EventArgs e)
		{
			if (_dataPager == null) return;
			if (_selectedItem == null) return;
			if (_selectedIndex < 0) return;
			if (_currentPage == _totalPages && _selectedIndex == _dataSouce.Count - 1) return;
			if (_selectedIndex < _dataSouce.Count - 1)
			{
				MoveSelectedItemsBackForward();
			}
			else
			{
				await MoveSelectedItemsAcrossPagesBackForwardAsync();
			}
		}

		//当前页内数据前移
		private void MoveSelectedItemsForward() => MoveSelectedItems(_selectedIndex - 1);

		//当前页内数据后移
		private void MoveSelectedItemsBackForward() => MoveSelectedItems(_selectedIndex + 1);

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

		//跨页数据前移
		private async Task MoveSelectedItemsAcrossPagesForwardAsync()
		{
			var dto = _dtos.FirstOrDefault(x => x.Id == _selectedItem.Id);
			int dto_selectedIndex = _dtos.IndexOf(dto);

			int dto_targetIndex = dto_selectedIndex - 1;

			var dto_temp = _dtos[dto_selectedIndex];
			_dtos[dto_selectedIndex] = _dtos[dto_targetIndex];
			_dtos[dto_targetIndex] = dto_temp;

			var data = _dataPager.PreviousPage();
			await InitDataSourceAsync(data.ToArray());
			SetSelectedIndex(_dataSouce.Count - 1);
		}

		//跨页数据后移
		private async Task MoveSelectedItemsAcrossPagesBackForwardAsync()
		{
			var dto = _dtos.FirstOrDefault(x => x.Id == _selectedItem.Id);
			int dto_selectedIndex = _dtos.IndexOf(dto);

			int dto_targetIndex = dto_selectedIndex + 1;

			var dto_temp = _dtos[dto_selectedIndex];
			_dtos[dto_selectedIndex] = _dtos[dto_targetIndex];
			_dtos[dto_targetIndex] = dto_temp;

			var data = _dataPager.NextPage();
			await InitDataSourceAsync(data.ToArray());
		}

		//拖拽完成后数据移动
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

		private void OnSelected(object sender, MouseEventArgs e)
		{
			var obj = _dataSouce.FirstOrDefault(x => x.Selected && !ReferenceEquals(x, sender));
			if (obj != null) obj.Selected = false;
			_selectedItem = (SvgCompositionView)sender;
			_selectedItem.Selected = true;

			if(_dtos.FirstOrDefault(x=>x.Id == _selectedItem.Id).ChangeType != ChangeType.Deleted) ClickItem?.Invoke(sender, e);

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
				else if(_insertDirection == SvgCompositionViewInsertDirection.Right)
				{
					p1 = new Point(_rectDragEnter.X + _rectDragEnter.Width + 2, _rectDragEnter.Y);
					p2 = new Point(_rectDragEnter.X + _rectDragEnter.Width + 2, _rectDragEnter.Y + _rectDragEnter.Height);
				}

				g.DrawLine(crossPen, p1, p2);
			}
		}

		public bool IsDataChanged()
		{
			if (_dtos.Count(x => x.ChangeType != ChangeType.None) > 0) return true;
			if (IsOrderChanged()) return true;
			return false;
		}

		public bool IsOrderChanged()
		{
			bool isChanged = false;
			for (int i = 0; i < _dtos.Count; i++)
			{
				if (_dtos[i].Id != _currentdtos[i].Id) isChanged = true;
			}
			return isChanged;
		}

		private void SetResultIcon(CheckResultIcon checkResult)
		{
			if (_selectedItem == null) return;
			_selectedItem.SetResultIcon(checkResult);
			_dtos.FirstOrDefault(x => x.Id == _selectedItem.Id).CheckResult = checkResult;
		}

		private void pic_rect_Click(object sender, EventArgs e)
		{
			SetResultIcon(CheckResultIcon.CheckRsl_1);
		}

		private void pic_crosses_Click(object sender, EventArgs e)
		{
			SetResultIcon(CheckResultIcon.CheckRsl_2);
		}

		private void pic_tick_Click(object sender, EventArgs e)
		{
			SetResultIcon(CheckResultIcon.CheckRsl_4);
		}

		private void pic_triangle_Click(object sender, EventArgs e)
		{
			SetResultIcon(CheckResultIcon.CheckRsl_5);
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

		public void Bind(List<SvgCompositionViewDto> dtos)
		{
			int index = 0;
			dtos.RemoveAll(x => x == null);
			dtos.ToList().ForEach(x =>
			{
				x.Id = index;
				index++;
			});
			_dtos = dtos;
			_currentdtos = _dtos.Select(d => (SvgCompositionViewDto)d.Clone()).ToList();
			_dataPager = new DataPager<SvgCompositionViewDto>(_dtos, _pageSize);
			pic_PageHome_Click(null, null);
		}

		private async void lb_order_Click(object sender, EventArgs e)
		{
			lb_order.Text = lb_order.Text.Contains("▼") ? "並び順▲" : "並び順▼";
			if (_dtos == null) return;
			_dtos.Reverse();
			await GoToAsync(_currentPage);
		}

		public void UpdateItem(SvgCompositionViewDto dto)
		{
			if (_selectedItem == null) return;
			_selectedItem.Update(dto);
			DataCompare(_dtos.FirstOrDefault(x => x.Id == _selectedItem.Id), dto);
		}

		private void DataCompare(SvgCompositionViewDto obj1, SvgCompositionViewDto obj2)
		{
			if (obj1.LayerDisplay != obj2.LayerDisplay) { obj1.ChangeType = ChangeType.Modified; }
			if (obj1.CheckResult != obj2.CheckResult) { obj1.ChangeType = ChangeType.Modified; }
			if (obj1.Remark != obj2.Remark) { obj1.ChangeType = ChangeType.Modified; }
			if (obj1.Orientation != obj2.Orientation) { obj1.ChangeType = ChangeType.Modified; }
		}

		public async Task RefreshItemContentImageAsync()
		{
			if (_selectedItem == null) return;
			await _selectedItem.RefreshContentImageAsync();
		}

	}
}
