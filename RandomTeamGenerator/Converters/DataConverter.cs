using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomTeamGenerator.Converters
{
	internal class DataConverter : IDataConverter
	{
		public String Number2String(int number, bool isCaps)
		{
			Char c = (Char)((isCaps ? 65 : 97) + (number - 1));
			return c.ToString();
		}
	}
}
