using Core.Entities;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebUI.Areas.Admin.ViewModels;
using WebUI.Utilites;

namespace WebUI.Areas.Admin.Controllers;

[Area("Admin")]
public class SpecialTeamController : Controller
{
	private readonly ISpecialTeamRepository _repository;
	private readonly IWebHostEnvironment _env;
	public SpecialTeamController(ISpecialTeamRepository repository, IWebHostEnvironment env)
	{
		_repository = repository;
		_env = env;
	}

	public async Task<IActionResult> Index()
	{
		return View(await _repository.GetAllAsync());
	}


	#region Detail
	public async Task<IActionResult> Detail(int id)
	{
		var model = await _repository.GetAsync(id);
		if (model == null) { return NotFound(); }
		return View(model);
	}
	#endregion

	#region Deleted
	public async Task<IActionResult> Delete(int id)
	{
		var model = await _repository.GetAsync(id);
		if (model == null) { return NotFound(); }
		return View(model);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[ActionName("Delete")]
	public async Task<IActionResult> DeletedPost(int id)
	{
		var model = await _repository.GetAsync(id);
		if (model == null) { return NotFound(); }
		_repository.Delete(model);
		await _repository.SaveAsync();
		return RedirectToAction(nameof(Index));
	}
	#endregion

	#region Created 
	public IActionResult Created()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Created(SpecialTeamCreateViewModel teamVM)
	{
		if (teamVM.Image == null)
		{
			ModelState.AddModelError("Image", "Bosh buraxma");
			return View(teamVM);
		}
		if (!teamVM.Image.CheckFileFormat("image/"))
		{
			ModelState.AddModelError("Image", "Fayl 'Image' fayli deyil");
			return View(teamVM);
		}
		if (!teamVM.Image.CheckFileSize(101))
		{
			ModelState.AddModelError("Image", "Faylin olcusu 101Kb-dan boyuk olmamalidir");
			return View(teamVM);
		}
		var fileName = String.Empty;
		try
		{
			fileName = await teamVM.Image.CopyFileAsync(_env.WebRootPath, "assets", "img", "team");
		}
		catch (Exception)
		{
			throw;
		}
		SpecialTeam specialTeam = new()
		{
			Fullname = teamVM.Fullname,
			Position = teamVM.Position,
			Image = fileName
		};
		await _repository.CreateAsync(specialTeam);
		await _repository.SaveAsync();
		return RedirectToAction(nameof(Index));
	}
	#endregion

	#region Update
	public async Task<IActionResult> Update(int id)
	{
		var model = await _repository.GetAsync(id);
		if (model == null) { return NotFound(); }
		SpecialTeamUpdateViewModel teamUpdateVM = new()
		{
			Fullname = model.Fullname,
			Position = model.Position,
			ImagePath = model.Image
		};
		return View(teamUpdateVM);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Update(int id, SpecialTeamUpdateViewModel updateVM)
	{
		if (id != updateVM.Id) { return BadRequest(); }
		if (!ModelState.IsValid) { return View(updateVM); }

		var model = await _repository.GetAsync(id);
		if (model == null) { return View(model); }

		model.Position = updateVM.Position;
		model.Fullname = updateVM.Fullname;

		if (updateVM.Image != null)
		{
			Helper.DeleteFile(_env.WebRootPath, "assets", "img", "team", model.Image);
			if (!updateVM.Image.CheckFileSize(100))
			{
				ModelState.AddModelError("Image", "Faylın ölçüsü 100KB-dan böyükdür");
				return View(updateVM);
			}
			if (!updateVM.Image.CheckFileFormat("image/"))
			{
				ModelState.AddModelError("Image", "Faylın 'Image' fayli deyil");
				return View(updateVM);
			}
			var fileName = String.Empty;
			try
			{
				fileName = await updateVM.Image.CopyFileAsync(_env.WebRootPath, "assets", "img", "team");
			}
			catch (Exception)
			{
				return View(updateVM);
			}
			model.Image = fileName;
		}
		_repository.Update(model);
		await _repository.SaveAsync();

		return RedirectToAction(nameof(Index));
	}
	#endregion

}
