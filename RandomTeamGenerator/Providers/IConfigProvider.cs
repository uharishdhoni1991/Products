using System.Collections.Generic;
using RandomTeamGenerator.Models;

namespace RandomTeamGenerator.Processors
{
	internal interface IConfigProvider
	{
		IEnumerable<Player> Players { get; }
		IEnumerable<RandomScale> RandomScaleParameters { get; }
	}
}