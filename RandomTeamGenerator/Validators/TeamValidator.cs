using System;
using System.Collections.Generic;
using System.Linq;
using RandomTeamGenerator.Models;

namespace RandomTeamGenerator.Validators
{
	internal class TeamValidator : ITeamValidator
	{
		private int _allRounderCount;
		private int _batsmanCount;
		private int _bowlerCount;
		private int _wicketKeeperCount;

		public bool IsNationalityThresholdReached(IEnumerable<Player> selectedPlayers, Nationality nationality)
		{
			return selectedPlayers
				.GroupBy(player => nationality)
				.Any(player => player.Count() > 7);
		}

		public bool IsSpecialityThresholdReached(IEnumerable<Player> team, Speciality speciality)
		{
			PopulateCount(team);

			if (speciality == Speciality.AllRounder)
				return _allRounderCount == Constants.MaxAllRounders;
			else if (speciality == Speciality.Batsman)
				return _batsmanCount == Constants.MaxBatsmen;
			else if (speciality == Speciality.Bowler)
				return _bowlerCount == Constants.MaximumBowlers;
			else if (speciality == Speciality.WicketKeeper)
				return _wicketKeeperCount == Constants.MaxWicketKeepers;

			return false;
		}

		public bool IsValid(IEnumerable<Player> team)
		{
			PopulateCount(team);

			return team.Sum(player => player.Credits) <= Constants.MaxCredits
				&& _wicketKeeperCount >= Constants.MinWicketKeepers && _wicketKeeperCount <= Constants.MaxWicketKeepers
				&& _batsmanCount >= Constants.MinBatsmen && _batsmanCount <= Constants.MaxBatsmen
				&& _allRounderCount >= Constants.MinimumAllRounders && _allRounderCount <= Constants.MaxAllRounders
				&& _bowlerCount >= Constants.MinimumBowlers && _bowlerCount <= Constants.MaximumBowlers;
		}

		private void PopulateCount(IEnumerable<Player> team)
		{
			_allRounderCount = team.Count(Player => Player.Speciality == Speciality.AllRounder);
			_batsmanCount = team.Count(Player => Player.Speciality == Speciality.Batsman);
			_bowlerCount = team.Count(Player => Player.Speciality == Speciality.Bowler);
			_wicketKeeperCount = team.Count(Player => Player.Speciality == Speciality.WicketKeeper);
		}
	}
}
