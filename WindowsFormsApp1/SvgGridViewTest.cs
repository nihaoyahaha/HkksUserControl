using HKKS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class SvgGridViewTest: Form
    {
        public SvgGridViewTest()
        {
            InitializeComponent();
        }

		private async void SvgGridViewTest_Load(object sender, EventArgs e)
		{
			await InitDataAsync();
			svgGridView1.ClickItem += (a, b) => {
				var item = a as SvgCompositionView;
				//MessageBox.Show(item.Remark);
			};
		}

		public async Task InitDataAsync()
		{
			List<SvgCompositionViewDto> dtos = new List<SvgCompositionViewDto>();
			for (int i = 0; i < 2; i++)
			{
				SvgCompositionViewDto dto = new SvgCompositionViewDto();
				dto.LayerDisplay = SvgLayerDisplay.OnlyNotesHide;
				dto.ContentImagePath = @"C:\Users\whatr\Desktop\test.svg"; //Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.svg");
				dto.CheckResult = CheckResultIcon.CheckRsl_1;
				dto.Remark = $"{i + 1}";
				dto.CreateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
				dto.Orientation = "Left";
				dtos.Add(dto);
			}
			await svgGridView1.BindAsync(dtos);
		}


		private async void button1_Click_1(object sender, EventArgs e)
		{
			SvgCompositionViewDto dto = new SvgCompositionViewDto();
			dto.LayerDisplay = SvgLayerDisplay.OnlyNotesHide;
			dto.CheckResult = CheckResultIcon.CheckRsl_1;
			dto.Remark = "1";
			dto.Orientation = "Left";
			await svgGridView1.UpdateItem(dto);
		}

		private async void button2_Click(object sender, EventArgs e)
		{
			await svgGridView1.RefreshItemContentImageAsync();
			//bool ischanged = svgGridView1.IsDataChanged();
			//MessageBox.Show(ischanged ? "发生修改" : "未发生修改");
		}

		private void svgGridView1_Load(object sender, EventArgs e)
		{

		}
	}
}
