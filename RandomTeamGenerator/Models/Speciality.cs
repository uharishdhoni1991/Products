using System.ComponentModel;

namespace RandomTeamGenerator.Models
{
	public enum Speciality
	{
		[Description("ALL")]
		AllRounder,
		[Description("BAT")]
		Batsman,
		[Description("BOWL")]
		Bowler,
		[Description("WK")]
		WicketKeeper
	}
}
