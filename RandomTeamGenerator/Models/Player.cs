using RandomTeamGenerator.Helpers;
using RandomTeamGenerator.Validators;

namespace RandomTeamGenerator.Models
{
	public class Player
	{
		public string Name { get; private set; }
		public decimal Credits { get; private set; }
		public decimal PointsEarned { get; private set; }
		public decimal SelectedBy { get; private set; }
		public bool IsCaptain { get; private set; }
		public bool IsViceCaptain { get; private set; }
		public Speciality Speciality { get; private set; }
		public Nationality Nationality { get; private set; }
		public Team Team { get; private set; }
        public bool IsPlaying { get; private set; }

        public override string ToString()
		{
			string CVC = IsCaptain ? "C" : IsViceCaptain ? "V" : "";
			string speciality = Speciality.GetDescription();
			return $"Name:{Name}--Credits--{Credits}--speciality -- {speciality}"; 
		}

		internal static Player FromCsv(string line)
		{
			string[] values = line.Split(',');
			Player player = new Player();
			player.Name = values[0];
			player.Credits = decimal.Parse(values[1]);
			player.PointsEarned = decimal.Parse(values[2]);
			player.SelectedBy = decimal.Parse(values[3]);

			string speciality = values[4];
			if (speciality == EnumExtensions.GetDescription(Speciality.WicketKeeper))
				player.Speciality = Speciality.WicketKeeper;
			else if (speciality == EnumExtensions.GetDescription(Speciality.Batsman))
				player.Speciality = Speciality.Batsman;
			else if (speciality == EnumExtensions.GetDescription(Speciality.Bowler))
				player.Speciality = Speciality.Bowler;
			else if (speciality == EnumExtensions.GetDescription(Speciality.AllRounder))
				player.Speciality = Speciality.AllRounder;

			string isPlaying = values[5];
			player.IsPlaying = isPlaying == "NO" ? false : true;				

			string nationality = values[6];
			if (nationality == EnumExtensions.GetDescription(Nationality.India))
				player.Nationality = Nationality.India;
			else if (nationality == EnumExtensions.GetDescription(Nationality.Australia))
				player.Nationality = Nationality.Australia;
			else if (nationality == EnumExtensions.GetDescription(Nationality.Afganistan))
				player.Nationality = Nationality.Afganistan;
			else if (nationality == EnumExtensions.GetDescription(Nationality.Bangladesh))
				player.Nationality = Nationality.Bangladesh;
			else if (nationality == EnumExtensions.GetDescription(Nationality.SouthAfrica))
				player.Nationality = Nationality.SouthAfrica;
			else if (nationality == EnumExtensions.GetDescription(Nationality.NewZealand))
				player.Nationality = Nationality.NewZealand;

			string team = values[7];
			if (team == EnumExtensions.GetDescription(Team.RR))
				player.Team = Team.RR;
			else if(team == EnumExtensions.GetDescription(Team.KKR))
				player.Team = Team.KKR;
			else if (team == EnumExtensions.GetDescription(Team.RCB))
				player.Team = Team.RCB;
			else if (team == EnumExtensions.GetDescription(Team.LSG))
				player.Team = Team.LSG;
			else if (team == EnumExtensions.GetDescription(Team.GT))
				player.Team = Team.GT;
			else if (team == EnumExtensions.GetDescription(Team.DC))
				player.Team = Team.DC;
			if (team == EnumExtensions.GetDescription(Team.PBKS))
				player.Team = Team.PBKS;
			if (team == EnumExtensions.GetDescription(Team.SRH))
				player.Team = Team.SRH;
			if (team == EnumExtensions.GetDescription(Team.MI))
				player.Team = Team.MI;
			else if (team == EnumExtensions.GetDescription(Team.CSK))
				player.Team = Team.CSK;

			return player;
		}

		public void SetCaptain()
		{
			IsCaptain = true;
		}

		public void SetViceCaptain()
		{
			IsViceCaptain = true;
		}
	}
}
