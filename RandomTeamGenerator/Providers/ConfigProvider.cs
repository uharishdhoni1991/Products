using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RandomTeamGenerator.Models;

namespace RandomTeamGenerator.Processors
{
	internal class ConfigProvider : IConfigProvider
	{
		private readonly IEnumerable<Player> _players;
		private const string _path = "DataPath";

		public IEnumerable<Player> Players
		{
			get
			{
				return _players;
			}
		}

		public IEnumerable<RandomScale> RandomScaleParameters
		{
			get
			{
				yield return RandomScale.Name;
				yield return RandomScale.Credits;
				yield return RandomScale.Points;
				yield return RandomScale.SelectedBy;
			}
		}

		public ConfigProvider(IFileReader<IEnumerable<Player>> fileReader)
		{
			IFileReader<IEnumerable<Player>> fileRead =
				fileReader ?? throw new ArgumentNullException(nameof(fileReader));

			_players = fileRead.FromPath(ConfigurationManager.AppSettings[_path]);
		}
	}
}
