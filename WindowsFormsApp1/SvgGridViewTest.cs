using CustomControlsImitatingUWP;
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
		}

		public async Task InitDataAsync()
		{
			List<SvgCompositionViewDto> dtos = new List<SvgCompositionViewDto>();
			for (int i = 0; i < 20; i++)
			{
				SvgCompositionViewDto dto = new SvgCompositionViewDto();
				dto.ContentImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"test.svg");
				dto.CheckResult = CheckResultIcon.CheckRsl_1; 
				dto.Remark = $"{i + 1}";
				dtos.Add(dto);
			}
			await svgGridView1.BindAsync(dtos.ToArray());
		}

		private  void button1_Click(object sender, EventArgs e)
		{
			bool ischanged = svgGridView1.HasDataChanged();
			MessageBox.Show(ischanged ? "发生修改" : "未发生修改");
		}
	}
}
