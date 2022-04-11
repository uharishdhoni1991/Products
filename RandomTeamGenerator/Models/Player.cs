using RandomTeamGenerator.Helpers;

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
			player.IsPlaying = int.Parse(isPlaying) == 0 ? false : true;				

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
