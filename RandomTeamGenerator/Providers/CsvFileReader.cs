using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomTeamGenerator.Models;
using RandomTeamGenerator.Processors;

namespace RandomTeamGenerator.Providers
{
	internal class CsvFileReader : IFileReader<IEnumerable<Player>>
	{
		public IEnumerable<Player> FromPath(string path)
		{
			return	File.ReadAllLines(path)
				.Skip(1)
				.Select(line => Player.FromCsv(line))
				.ToList();
		}
	}
}
