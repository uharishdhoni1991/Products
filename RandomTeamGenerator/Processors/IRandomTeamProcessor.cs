using System.Collections;
using System.Collections.Generic;
using System.Text;
using RandomTeamGenerator.Models;

namespace RandomTeamGenerator.Processors
{
	internal interface IRandomTeamProcessor
	{
		IEnumerable<Player> Team { get; }
		void Process();
	}
}