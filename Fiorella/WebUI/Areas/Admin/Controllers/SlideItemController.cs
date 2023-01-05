﻿using Core.Entities;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebUI.Areas.Admin.ViewsModels.Slider;
using WebUI.Utilites;

namespace WebUI.Areas.Admin.Controllers;
[Area("Admin")]
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


    #region Create Slide
    public  IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SlideItemViewModel slideVM)
    {

		if (slideVM.Image == null)
		{
			ModelState.AddModelError("Image", "Şekilin daxil edilmeyib");
			return View(slideVM);
		}
		if (!slideVM.Image.CheckFileSize(100))
		{
			ModelState.AddModelError("Image", "Şekilin ölçüsü 100 KB-dan böyükdür");
			return View(slideVM);
		}
		if (!ModelState.IsValid) { return View(slideVM); }
		if (!slideVM.Image.CheckFileFormat("image/"))
		{
			ModelState.AddModelError("Image", "Seçilən fayl 'Image' faylı deyil");
			return View(slideVM);
		}

		var fileName = String.Empty;
		try
		{
			fileName = await slideVM.Image.CopyFileAsync(_env.WebRootPath, "assets", "img", "slider");
		}
		catch (Exception)
		{
			throw;
		}

		SlideItem slide = new()
		{
			Title = slideVM.Title,
			Image = fileName,
			Description = slideVM.Description
		};
		await _repository.CreateAsync(slide);
		await _repository.SaveAsync();
		return RedirectToAction(nameof(Index));
	}
    #endregion
}
