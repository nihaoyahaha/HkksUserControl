using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomControlsImitatingUWP
{
	public class DataPager<T>
	{
		public IEnumerable<T> _dataSource;
		private int _currentPage = 1;
		public int CurrentPage
		{
			get { return _currentPage; }
		}

		private int _pageSize;

		private int _totalPages;

		public int TotalPages
		{
			get { return _totalPages; }
		}

		public DataPager(IEnumerable<T> values, int pageSize)
		{
			_dataSource = values ?? throw new ArgumentNullException(nameof(_dataSource)); ;
			_pageSize = pageSize;
			CalculateTotalPage();
		}

		private void CalculateTotalPage()
		{
			if (_dataSource == null || !_dataSource.Any())
			{
				_totalPages = 1;
				return;
			}

			_totalPages = (int)Math.Ceiling(_dataSource.Count() / (double)_pageSize);
		}

		/// <summary>
		/// 跳转
		/// </summary>
		/// <param name="page">目标页</param>
		public IEnumerable<T> GoToPage(int page)
		{
			_currentPage = page < 1 ? 1 : (page > _totalPages ? _totalPages : page);
			int startIdx = (_currentPage - 1) * _pageSize;
			return _dataSource.Skip(startIdx).Take(_pageSize);
		}

		/// <summary>
		/// //上页
		/// </summary>
		public IEnumerable<T> PreviousPage() => GoToPage(_currentPage - 1);

		/// <summary>
		/// 下页
		/// </summary>
		public IEnumerable<T> NextPage() => GoToPage(_currentPage + 1);

		/// <summary>
		/// 首页
		/// </summary>
		public IEnumerable<T> FirstPage() => GoToPage(1);

		/// <summary>
		/// 尾页
		/// </summary>
		public IEnumerable<T> LastPage() => GoToPage(_totalPages);

	}
}
