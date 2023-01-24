using Core.Entities;
using DataAccess.Exceptions;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebUI.Areas.Admin.ViewModel.Employee;
using WebUI.Utilites;

namespace WebUI.Areas.Admin.Controllers;

[Area("Admin")]
public class EmployeeController : Controller
{
	//private readonly AppDbContext _context;
	private readonly IEmployeeRepository _employeeRepository;
	private readonly IWebHostEnvironment _env;
	public EmployeeController(IEmployeeRepository employeeRepository, IWebHostEnvironment env)
	{
		_employeeRepository = employeeRepository;
		_env = env;
	}

	public async Task<IActionResult> Index()
	{
		return View(await _employeeRepository.GetAllAsync());
	}

	#region Detail
	[HttpGet]
	public async Task<IActionResult> Detail(int id)
	{
		var model = await _employeeRepository.GetAsync(id);
		if (model == null)
		{
			return NoContent();
		}
		return View(model);
	}
	#endregion

	#region Deleted
	public async Task<IActionResult> Delete(int id)
	{
		var model =await _employeeRepository.GetAsync(id);
		if (model == null) { return NoContent(); }
		return View(model);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[ActionName("Delete")]
	public async Task<IActionResult> DeletePost(int id)
	{
		var model = await _employeeRepository.GetAsync(id);
		if (model == null) { return NotFound(); }

		Helper.DeleteFile(_env.WebRootPath, "assets", "img", "team", model.Image);
		_employeeRepository.Delete(model);
		await _employeeRepository.SaveAsync();
		return RedirectToAction(nameof(Index));
	}
	#endregion

	#region Update
	public async Task<IActionResult> Update(int id)
	{
		var model = await _employeeRepository.GetAsync(id);
		if (model == null) { return NotFound(); }
		EmployeeUpdateViewModel updateVM = new()
		{
			Fullname= model.Fullname,
			Position=model.Position,
			Description=model.Description,
			ImagePath=model.Image
		};
		return View(updateVM);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Update(int id, EmployeeUpdateViewModel updateVM)
	{
		if (id != updateVM.Id) { return BadRequest(); }
		if (!ModelState.IsValid) { return View(updateVM); }
		var model = await _employeeRepository.GetAsync(id);
		if (model == null) { return View(model); }

		model.Position = updateVM.Position;
		model.Description = updateVM.Description;
		model.Fullname= updateVM.Fullname;
		if (updateVM.Image != null)
		{
			//folderden shekili silmek
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
		_employeeRepository.Update(model);
		await _employeeRepository.SaveAsync();
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
	public async Task<IActionResult> Created(EmployeeCreateViewModel createdVM)
	{
		if (createdVM.Image == null)
		{
			ModelState.AddModelError("Image", "Şəkil daxil edimləlidir");
			return View(createdVM);
		}
		if (!createdVM.Image.CheckFileSize(100))
		{
			ModelState.AddModelError("Image", "Şəklin ölçüsü 100KB-dan böyükdür");
			return View(createdVM);
		}
		if (!createdVM.Image.CheckFileFormat("image/"))
		{
			ModelState.AddModelError("Image", "Fayıl 'Image' formatında deyildir");
			return View(createdVM);
		}
		if (!ModelState.IsValid)
		{
			return View(createdVM);
		}
		var fileName = String.Empty;
		try
		{
			fileName = await createdVM.Image.CopyFileAsync(_env.WebRootPath, "assets", "img", "team");
		}
		catch (Exception ex)
		{
			throw new FileCreatedNotFoundException("Created file fail");
		}

		Employee employee = new()
		{
			Image = fileName,
			Position = createdVM.Position,
			Fullname = createdVM.Fullname,
			Description = createdVM.Description
		};
		await _employeeRepository.CreateAsync(employee);
		await _employeeRepository.SaveAsync();
		return RedirectToAction(nameof(Index));
	}
	#endregion
}
