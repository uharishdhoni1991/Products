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

		public bool HasSingleTeamThresholdReached(IEnumerable<Player> selectedPlayers, Team team)
		{
			return selectedPlayers
				.GroupBy(player => player.Team)
				.Select(p => p.Count())
				.Any(p => p > 7); 
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

			bool isValid = false;
			Player currentPlayerAdded = team.Last();
			Speciality currentPlayerSpeciality = currentPlayerAdded.Speciality;
			int totalCount = team.Count();

			isValid = team.Sum(player => player.Credits) <= Constants.MaxCredits;

			switch (currentPlayerSpeciality)
			{
				case Speciality.WicketKeeper:
					isValid = isValid && _wicketKeeperCount <= Constants.MaxWicketKeepers;
					break;
				case Speciality.Batsman:
					isValid = isValid && _batsmanCount <= Constants.MaxBatsmen;
					break;
				case Speciality.AllRounder:
					isValid = isValid && _allRounderCount <= Constants.MaxAllRounders;
					break;
				case Speciality.Bowler:
					isValid = isValid && _bowlerCount <= Constants.MaximumBowlers;
					break;
			}

			return isValid;
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
