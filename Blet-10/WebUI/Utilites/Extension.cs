namespace WebUI.Utilites;

public static class Extension
{
	public static bool CheckFileSize(this IFormFile formFile, long KByte)
	{
		return formFile.Length / 1024 < KByte;
	}

	public static bool CheckFileFormat( this IFormFile formFile ,string format)
	{
		return formFile.ContentType.Contains(format);
	}

	public static async Task<string> CopyFileAsync(this IFormFile file,string wwwroot , params string[] folders)
	{
		try
		{
			var fileName = Guid.NewGuid().ToString() + file.FileName;
			var resultPath = wwwroot;
			foreach (var folder in folders)
			{
				resultPath = Path.Combine(resultPath, folder);
			}
			resultPath = Path.Combine(resultPath, fileName);
			using (FileStream stream = new FileStream(resultPath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}
			return fileName;
		}
		catch (Exception)
		{
			throw;
		}
	}

}
