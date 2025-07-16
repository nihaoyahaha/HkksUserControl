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
			await InitData();
		}

		public async Task InitData()
		{
			List<SvgCompositionViewDto> dtos = new List<SvgCompositionViewDto>();
			for (int i = 0; i < 10; i++)
			{
				SvgCompositionViewDto dto = new SvgCompositionViewDto();
				dto.ContentImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"test.svg");
				dto.IconImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "checkRsl_5.png"); 
				dto.Remark = $"{i + 1}";
				dtos.Add(dto);
			}
			await svgGridView1.BindAsync(dtos.ToArray());
		}


	}
}
