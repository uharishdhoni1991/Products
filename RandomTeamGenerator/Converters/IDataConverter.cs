using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomTeamGenerator.Converters
{
	internal interface IDataConverter
	{
		string Number2String(int number, bool isCaps);
	}
}
