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
	public partial class PagedSvgGridViewTest : Form
	{
		public PagedSvgGridViewTest()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			InitData();
		}

		public void InitData()
		{
			List<SvgCompositionViewDto> dtos = new List<SvgCompositionViewDto>();
			for (int i = 0; i < 2; i++)
			{
				SvgCompositionViewDto dto = new SvgCompositionViewDto();
				dto.ContentImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.svg");
				dto.CheckResult = CheckResultIcon.CheckRsl_1;
				dto.Remark = $"{i + 1}";
				dtos.Add(dto);
			}
			svgGridView1.Bind(dtos.ToArray());
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			svgGridView1.SetPhotoVisible(checkBox1.Checked);
		}

		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			svgGridView1.SetBlackboardVisible(checkBox2.Checked);
		}

		private void checkBox3_CheckedChanged(object sender, EventArgs e)
		{
			svgGridView1.SetNotesVisible(checkBox3.Checked);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			bool ischanged = svgGridView1.HasDataChanged();
			MessageBox.Show(ischanged ? "发生修改":"未发生修改");
		}
	}
}
