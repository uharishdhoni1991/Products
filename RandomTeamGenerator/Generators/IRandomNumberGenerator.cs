using RandomTeamGenerator.Models;
using System.Collections.Generic;

namespace RandomTeamGenerator.Processors
{
    internal interface IRandomGenerator
	{
		int GetRandomNumber(int min, int max);
		int GetRandomNumber(int min, int max, Dictionary<RandomScale, IEnumerable<int>> processedList, RandomScale random);
		T GetRandomObject<T>(IEnumerable<T> source);
	}
}
