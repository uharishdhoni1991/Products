namespace RandomTeamGenerator.Processors
{
	internal interface IFileReader<out T>
	{
		T FromPath(string path);
	}
}