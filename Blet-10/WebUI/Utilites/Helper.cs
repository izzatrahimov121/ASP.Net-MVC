namespace WebUI.Utilites;

public class Helper
{
	public static bool DeleteFile(params string[] path)
	{
		var resultPath = String.Empty;
		foreach (var item in path)
		{
			resultPath = Path.Combine(resultPath, item);
		}
		if (File.Exists(resultPath))
		{
			File.Delete(resultPath);
			return true;
		}
		return false;
	}
}
