using System.Collections.Generic;
using RandomTeamGenerator.Models;

namespace RandomTeamGenerator.Validators
{
	internal interface ITeamValidator
	{
		bool IsValid(IEnumerable<Player> team);
		bool IsSpecialityThresholdReached(IEnumerable<Player> team, Speciality speciality);
		bool HasSingleTeamThresholdReached(IEnumerable<Player> selectedPlayers, Team team);
	}
}
