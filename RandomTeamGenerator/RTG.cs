using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RandomTeamGenerator.Converters;
using RandomTeamGenerator.Processors;
using RandomTeamGenerator.Providers;
using RandomTeamGenerator.Validators;

namespace RandomTeamGenerator
{
	public partial class RTG : Form
	{
		private readonly RandomTeamProcessor _randomTeamProcessor;

		public RTG()
		{
			InitializeComponent();
			backgroundWorker1.RunWorkerAsync();

			_randomTeamProcessor = new RandomTeamProcessor(
				new ConfigProvider(new CsvFileReader()),
				new RandomGenerator(),
				new DataConverter(),
				new TeamValidator());
			//dataGridView1.CellFormatting += DataGridView1_CellFormatting;
		}

		private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			foreach (DataGridViewRow Myrow in dataGridView1.Rows)
			{
				if (bool.Parse(Myrow.Cells[6].Value.ToString()))
					Myrow.DefaultCellStyle.BackColor = Color.AliceBlue;
			}
		}

		private void LoadPlayers()
		{	
			_randomTeamProcessor.Process();
			dataGridView1.DataSource = _randomTeamProcessor.Team.ToList();
		}

		private void btnGenerate_Click(object sender, EventArgs e)
		{
			btnGenerate.Enabled = false;
			try
			{
				LoadPlayers();
			}
			finally
			{
				btnGenerate.Enabled = true;
			}
		}
	}
}
