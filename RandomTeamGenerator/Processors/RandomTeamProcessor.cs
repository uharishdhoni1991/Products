using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomTeamGenerator.Converters;
using RandomTeamGenerator.Models;
using RandomTeamGenerator.Validators;

namespace RandomTeamGenerator.Processors
{
	class RandomTeamProcessor : IRandomTeamProcessor
	{
		private readonly IConfigProvider _config;
		private readonly IRandomGenerator _random;
		private readonly IDataConverter _dataConverter;
		private readonly ITeamValidator _teamValidator;
		private IEnumerable<Player> _team;

		public IEnumerable<Player> Team
		{
			get
			{
				return _team;
			}
		}

		public RandomTeamProcessor(
			IConfigProvider config,
			IRandomGenerator randomNumberGenerator,
			IDataConverter dataConverter,
			ITeamValidator teamValidator)
		{
			_config = config ?? throw new ArgumentNullException(nameof(config));
			_random = randomNumberGenerator ?? throw new ArgumentNullException(nameof(randomNumberGenerator));
			_dataConverter = dataConverter ?? throw new ArgumentNullException(nameof(dataConverter));
			_teamValidator = teamValidator ?? throw new ArgumentNullException(nameof(teamValidator));
		}

		public void Process()
		{
			List<Player> playerList = _config.Players.ToList();
			IEnumerable<RandomScale> randomScaleParametes = _config.RandomScaleParameters;
			List<Player> selectedPlayers = new List<Player>();

			Player selectedPlayer = default(Player);

			Dictionary<RandomScale, IEnumerable<int>> processedItems = new Dictionary<RandomScale, IEnumerable<int>>();
			processedItems[RandomScale.Alphabet] = new List<int>();
			processedItems[RandomScale.Name] = new List<int>();

			while (selectedPlayers.Count < 11)
			{
				int randomNumber = _random.GetRandomNumber(
					min: 0,
					max: randomScaleParametes.Count() - 1);

				RandomScale randomScale = randomScaleParametes
					.Where((value, index) => index == randomNumber).Single();

				switch (randomScale)
				{
					case RandomScale.Name:						
						int randomAlphabetIndex = _random.GetRandomNumber(
							min: 1,
							max: 26,
							processedList: processedItems,
							random: RandomScale.Alphabet);

						var filteredPlayers = playerList
							.Where(player => player
							.Name.StartsWith(_dataConverter.Number2String(
								number: randomAlphabetIndex,
								isCaps: true)))
							.Select((player, index) => new { Player = player, Index = index });

						int count = filteredPlayers.Count();

						if (count < 1)
							continue;

						int l2randomPlayerIndex = count == 1 ? 0 : _random.GetRandomNumber(
							min: 1,
							max: count,
							processedList: processedItems,
							random: RandomScale.Name);

						var selectedPlayerWrapper = filteredPlayers.SingleOrDefault(player=> player.Index == l2randomPlayerIndex);
						selectedPlayer = selectedPlayerWrapper.Player;

						var list = processedItems[RandomScale.Name].ToList();
						list.Add(selectedPlayerWrapper.Index);
						processedItems[RandomScale.Name] = list;

						var alphaList = processedItems[RandomScale.Alphabet].ToList();
						alphaList.Add(randomAlphabetIndex);
						processedItems[RandomScale.Alphabet] = alphaList;
						break;

					case RandomScale.Credits:
						var creditsList = playerList
							.Select(player =>new { Name = player.Name, Credits = player.Credits });
						var randomPlayerCredits = _random.GetRandomObject(creditsList);

						selectedPlayer = playerList
							.AsEnumerable()
							.Single(player => player.Name == randomPlayerCredits.Name);
						break;

					case RandomScale.Points:
						var pointList = playerList
							.Select(player =>new { Name = player.Name, PointsEarned = player.PointsEarned });
						var randomPlayerPoints = _random.GetRandomObject(pointList);

						selectedPlayer = playerList
							.AsEnumerable()
							.Single(player => player.Name == randomPlayerPoints.Name);
						break;

					case RandomScale.SelectedBy:
						var selectedByList = playerList
							.Select(player => new { Name = player.Name, PointsEarned = player.SelectedBy });
						var randomPlayerSelectedBy = _random.GetRandomObject(selectedByList);

						selectedPlayer = playerList
							.AsEnumerable()
							.Single(player => player.Name == randomPlayerSelectedBy.Name);
						break;
				}

				if (_teamValidator.IsSpecialityThresholdReached(selectedPlayers, selectedPlayer.Speciality))
				{
					playerList.RemoveAll(player => player.Speciality == selectedPlayer.Speciality);
					continue;
				}

				if (_teamValidator.IsNationalityThresholdReached(selectedPlayers, selectedPlayer.Nationality))
				{
					playerList.RemoveAll(player => player.Nationality == selectedPlayer.Nationality);
					continue;
				}

				if (!selectedPlayers.Any(Player => Player.Name == selectedPlayer.Name))
				{
					selectedPlayers.Add(selectedPlayer);
					playerList.RemoveAll(player => player.Name == selectedPlayer.Name);
				}

				if (selectedPlayers.Count == 11 && !_teamValidator.IsValid(selectedPlayers))
					selectedPlayers.Clear();

				_team = selectedPlayers.ToList();
			}

			int captainRandomNumber = _random.GetRandomNumber(1, 11);
			_team.Where((player, index) => index == captainRandomNumber).Single().SetCaptain();

			int viceCaptainRandomNumber = _random.GetRandomNumber(1, 11);
			do
			{
				viceCaptainRandomNumber = _random.GetRandomNumber(1, 11);

				if (viceCaptainRandomNumber == captainRandomNumber)
					continue;

				_team.Where((player, index) => index == viceCaptainRandomNumber).Single().SetViceCaptain();
			} while (viceCaptainRandomNumber == captainRandomNumber);

			_team = _team.OrderBy(player => player.Speciality);
		}
	}
}
