using RandomTeamGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RandomTeamGenerator.Processors
{
	internal class RandomGenerator : IRandomGenerator
	{
		private readonly Random _random = new Random();

		public int GetRandomNumber(int min, int max)
        {
			return _random.Next(min, max);
        }

		public int GetRandomNumber(int min, int max, Dictionary<RandomScale, IEnumerable<int>> processedList, RandomScale randomScale)
		{
			int number;
			do
			{
				number = _random.Next(min, max);
			} while (processedList[randomScale].Any(item => item == number));

			return number;
		}

		public T GetRandomObject<T>(IEnumerable<T> source)
		{
			int sourceCount = source.Count();

			if (sourceCount == 1)
				return source.Single();

			int randomNumber = _random.Next(0, sourceCount-1);
			IEnumerable<T> filteredSource = source.Where((value, index) => index == randomNumber);

			return filteredSource.Count() == 1 ?
				filteredSource.Single() :
				GetRandomObject(filteredSource);
		}
	}
}