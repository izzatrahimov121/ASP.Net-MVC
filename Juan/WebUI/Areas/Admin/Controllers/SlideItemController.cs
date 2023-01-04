using Core.Entities;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using WebUI.Areas.Admin.ViewsModels.Slider;
using WebUI.Utilites;

namespace WebUI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles ="Admin")]//login olmayanlari admin panele buraxmayacaq
public class SlideItemController : Controller
{
	private readonly ISlideItemRepository _repository;
	private readonly IWebHostEnvironment _env;

	public SlideItemController(ISlideItemRepository repository, IWebHostEnvironment env)
	{
		_repository = repository;
		_env = env;
	}

	public async Task<IActionResult> Index()
	{
		return View(await _repository.GetAllAsync());
	}


	#region Detail slide
	public async Task<IActionResult> Detail(int id)
	{
		var model = await _repository.GetAsync(id);
		if (model == null) { return NotFound(); }
		return View(model);
	}
	#endregion

	#region Create Slide
	public IActionResult Create()
	{
		return View();
	}
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(SlideCreateVM item)
	{
		if (item.Image == null)
		{
			ModelState.AddModelError("Image", "Şekilin daxil edilmeyib");
			return View(item);
		}
		if (!item.Image.CheckFileSize(100))
		{
			ModelState.AddModelError("Image", "Şekilin ölçüsü 100 KB-dan böyükdür");
			return View(item);
		}
		if (!ModelState.IsValid) { return View(item); }
		if (!item.Image.CheckFileFormat("image/"))
		{
			ModelState.AddModelError("Image", "Seçilən fayl 'Image' faylı deyil");
			return View(item);
		}

		var fileName = String.Empty;
		try
		{
			fileName = await item.Image.CopyFileAsync(_env.WebRootPath, "assets", "img", "slider");
		}
		catch (Exception)
		{
			throw;
		}

		SlideItem slide = new()
		{
			Title = item.Title,
			Category = item.Category,
			Image = fileName,
			Description = item.Description
		};
		await _repository.CreateAsync(slide);
		await _repository.SaveAsync();
		return RedirectToAction(nameof(Index));
	}
	#endregion

	#region Update Slide
	public async Task<IActionResult> Update(int id)
	{
		var model = await _repository.GetAsync(id);
		if (model == null) { return NotFound(); }
		SlideUpdateVM updateVM = new()
		{
			Title = model.Title,
			Category = model.Category,
			Description = model.Description,
			ImagePath = model.Image
		};
		return View(updateVM);
	}
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Update(int id, SlideUpdateVM slide)
	{
		if (id != slide.Id) { return BadRequest(); }
		if (!ModelState.IsValid) { return View(slide); }
		var model = await _repository.GetAsync(id);
		if (model == null) { return View(model); }
		//SlideItem item = new()
		model.Category = slide.Category;
		model.Title = slide.Title;
		model.Description = slide.Description;
		if (slide.Image != null)
		{
			//folderden shekili silmek
			Helper.DeleteFile(_env.WebRootPath, "assets", "img", "slider",model.Image);
			if (!slide.Image.CheckFileSize(100))
			{
				ModelState.AddModelError("Image", "Faylın ölçüsü 100KB-dan böyükdür");
				return View(slide);
			}
			if (!slide.Image.CheckFileFormat("image/"))
			{
				ModelState.AddModelError("Image", "Faylın 'Image' fayli deyil");
				return View(slide);
			}
			var fileName = String.Empty;
			try
			{
				fileName = await slide.Image.CopyFileAsync(_env.WebRootPath, "assets", "img", "slider");
			}
			catch (Exception)
			{
				return View(slide);
			}
			model.Image = fileName;
		}
		_repository.Update(model);
		await _repository.SaveAsync();

		return RedirectToAction(nameof(Index));
	}
	#endregion

	#region Delete Slide
	public IActionResult Delete(int id)
	{
		var slide = _repository.GetAsync(id);
		if (slide == null) { return Content("Slide null"); }
		return View(slide);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[ActionName("Delete")]
	public async Task<IActionResult> DeleteSlide(int id)
	{
		var slide = await _repository.GetAsync(id);
		if (slide != null) { return BadRequest(); }

		//folderden shekili silmek
		Helper.DeleteFile(_env.WebRootPath, "assets", "img", "slider", slide.Image);

		//Db-én silmek
		_repository.Delete(slide);
		await _repository.SaveAsync();
		return RedirectToAction(nameof(Index));

	}
	#endregion
}
