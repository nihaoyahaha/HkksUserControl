using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKKS
{
	//SvgCompositionView控件的数据传输对象
	public class SvgCompositionViewDto : ICloneable
	{
		public int Id { get; set; } = -1;
		public string ContentImagePath { get; set; }

		/// <summary>
		/// svg层显示
		///    写真      绿板      注释
		/// 7:  ⭕        ⭕        ⭕
		/// 3:  ⭕        ⭕        ✖
		/// 5:  ⭕        ✖        ⭕
		/// 1:  ⭕        ✖        ✖
		/// </summary>
		public SvgLayerDisplay LayerDisplay { get; set; } = SvgLayerDisplay.ShowAll;

		public CheckResultIcon CheckResult { get; set; }

		public string Remark { get; set; }

		public string CreateTime { get; set; }

		public string Orientation { get; set; }

		public ChangeType ChangeType { get; set; } = ChangeType.None;

		public object Clone()
		{
			return new SvgCompositionViewDto
			{
				Id = Id,
				ContentImagePath = ContentImagePath,
				CheckResult = CheckResult,
				Remark = Remark,
				CreateTime = CreateTime,
				Orientation = Orientation,
				ChangeType = ChangeType,
				LayerDisplay = LayerDisplay
			};
		}
	}
}
