using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HkksUserControl
{
    public enum ImageType
    {
        Jpg,
        Svg
    }

	//拖拽时在目标元素的位置
	public enum SvgCompositionViewInsertDirection
	{
		/// <summary>
		/// 无效位置
		/// </summary>
		Null,

		/// <summary>
		/// 左侧
		/// </summary>
		Left,

		/// <summary>
		/// 右侧
		/// </summary>
		Right
	}

	//检查结果
	public enum CheckResultIcon
	{
		Null,
		CheckRsl_1,
		CheckRsl_2,
		CheckRsl_4,
		CheckRsl_5
	}

	public enum ChangeType
	{
		/// <summary>
		/// 无变化
		/// </summary>
		None,

		/// <summary>
		/// 修改
		/// </summary>
		Modified,

		/// <summary>
		/// 删除
		/// </summary>
		Deleted
	}

	//SVG图层的显示隐藏
	public enum SvgLayerDisplay
	{
		/// <summary>
		/// 全部显示
		/// </summary>
		ShowAll = 7,

		/// <summary>
		/// 仅注释隐藏
		/// </summary>
		OnlyNotesHide = 3,

		/// <summary>
		/// 仅绿板隐藏
		/// </summary>
		OnlyGreenBoardHide = 5,

		/// <summary>
		/// 绿板和注释隐藏
		/// </summary>
		GreenBoardAndNotesHide = 1
	}
}
