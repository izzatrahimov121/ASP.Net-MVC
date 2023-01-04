using Core.Entities;

namespace WebUI.ViewsModels;

public class HomeViewModel
{
	public IEnumerable<SlideItem> SlideItems { get; set; } = null!;
}
