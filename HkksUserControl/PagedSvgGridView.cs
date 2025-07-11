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

namespace CustomControlsImitatingUWP
{
	public partial class PagedSvgGridView : UserControl
	{
		private DataPager<SvgCompositionViewDto> _dataPager;

		private int _pageSize = 9;

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

		private SvgCompositionViewDto[] _dtos;  

		public PagedSvgGridView()
		{
			InitializeComponent();
			DoubleBuffered = true;
		}

		private void SetNumInfo()
		{
			lb_numInfo.Text = $"{_selectedIndex + 1}/{_dataSouce.Count}";
		}

		private void OnSelected(object sender, MouseEventArgs e)
		{
			var obj = _dataSouce.FirstOrDefault(x => x.Selected && !ReferenceEquals(x, sender));
			if (obj != null) obj.Selected = false;
			_selectedItem = (SvgCompositionView)sender;
			_selectedItem.Selected = true;
			_selectedIndex = _dataSouce.IndexOf(_selectedItem);
			SetNumInfo();
		}

		private void Add(SvgCompositionView item, bool isSetSelectedIndex = true)
		{
			if (item == null) return;
			item.SelectionChanged += OnSelected;
			_dataSouce.Add(item);
			panel_Main.Controls.Add(item);
			if (isSetSelectedIndex) SetSelectedIndex(_dataSouce.Count - 1);
			SetNumInfo();
		}

		private void SetItemDeletedState(SvgCompositionView item) 
		{
			if (item == null) return;
			item.SetDeletedState();
			_dtos.ToList().FirstOrDefault(x => x.Id == item.Id).IsDeleted = item.IsDeleted;
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
			SetItemDeletedState(_selectedItem);
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

		private async Task AddSvgCompositionViewAsync(SvgCompositionViewDto dto)
		{
			SvgCompositionView svgComposition = SetSvgCompositionViewProperties(dto);
			await svgComposition.SetContentImageAsync(dto.ContentImagePath);
			Add(svgComposition, false);
		}

		public void Bind(params SvgCompositionViewDto[] dtos)
		{
			int index = 0;
			dtos.ToList().ForEach(x => {
				x.Id = index;
				index++;
			});
			_dtos = dtos;
			_dataPager = new DataPager<SvgCompositionViewDto>(dtos, _pageSize);
			pic_PageHome_Click(null, null);
		}

		private void DisposeDataSourceItem()
		{
			if (_dataSouce != null)
			{
				_dataSouce.ForEach(x =>
				{
					x.SelectionChanged -= OnSelected;
					x.DisposeImage();
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

		private async Task GoToAsync(int page)
		{
			if (_dataPager == null) return;
			var data = _dataPager.GoToPage(page);
			 await InitDataSourceAsync(data.ToArray());
			lb_PageInfo.Text = $"{_dataPager.CurrentPage}/{_dataPager.TotalPages}";
		}

		private  void txt_Goto_KeyPress(object sender, KeyPressEventArgs e)
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

		//当前页内数据前移
		private void MoveSelectedItemsForward() => MoveSelectedItems(_selectedIndex - 1);

		//跨页数据前移
		private async Task MoveSelectedItemsAcrossPagesForwardAsync()
		{
			var dto = _dtos.FirstOrDefault(x => x.Id == _selectedItem.Id);			
			int dto_selectedIndex = _dtos.ToList().IndexOf(dto);

			int dto_targetIndex = dto_selectedIndex-1;

			var dto_temp = _dtos[dto_selectedIndex];
			_dtos[dto_selectedIndex] = _dtos[dto_targetIndex];
			_dtos[dto_targetIndex] = dto_temp;

			var data = _dataPager.PreviousPage();
			await InitDataSourceAsync(data.ToArray());
			SetSelectedIndex(_dataSouce.Count-1);
		}

		//向后移动
		private async void pic_backward_Click(object sender, EventArgs e)
		{
			if (_dataPager == null) return;
			if (_selectedItem == null) return;
			if (_selectedIndex < 0) return;
			if (_currentPage == _totalPages && _selectedIndex == _dataSouce.Count-1) return;
			if (_selectedIndex < _dataSouce.Count - 1)
			{
				MoveSelectedItemsBackForward();
			}
			else
			{
				await MoveSelectedItemsAcrossPagesBackForwardAsync();
			}
		}

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

		//当前页内数据后移
		private void MoveSelectedItemsBackForward() => MoveSelectedItems(_selectedIndex + 1);
		
		//跨页数据后移
		private async Task MoveSelectedItemsAcrossPagesBackForwardAsync()
		{
			var dto = _dtos.FirstOrDefault(x => x.Id == _selectedItem.Id);
			int dto_selectedIndex = _dtos.ToList().IndexOf(dto);

			int dto_targetIndex = dto_selectedIndex + 1;

			var dto_temp = _dtos[dto_selectedIndex];
			_dtos[dto_selectedIndex] = _dtos[dto_targetIndex];
			_dtos[dto_targetIndex] = dto_temp;

			var data = _dataPager.NextPage();
			await InitDataSourceAsync(data.ToArray());
		}
	}
}
