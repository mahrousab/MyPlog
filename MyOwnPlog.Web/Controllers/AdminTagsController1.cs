using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyOwnPlog.Web.Data;
using MyOwnPlog.Web.Models.Domain;
using MyOwnPlog.Web.Models.ViewModel;
using MyOwnPlog.Web.Repositories;

namespace MyOwnPlog.Web.Controllers
{

	[Authorize(Roles ="Admin")]
	public class AdminTagsController1 : Controller
	{
		private readonly ITagRepository tagRepository;
        public AdminTagsController1(ITagRepository tagRepository)
		{

			this.tagRepository = tagRepository;
		}


		[Authorize(Roles ="Admin")]
        [HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		[ActionName("Add")]
		public async Task<IActionResult>Add(AddTagRequest addTagRequest)
		{

			if (!ModelState.IsValid)
			{
				return View();
			}
			var tag = new Tag
			{
				Name = addTagRequest.Name,
				DisplayName = addTagRequest.DisplayName,


			};
			
			return RedirectToAction("List");

		}
		[HttpGet]
		[ActionName("List")]
		public async Task<IActionResult> List()
		{
			var tags = await tagRepository.GetAllAsync();
			return View(tags);
		}
		[HttpGet]
		public async Task <IActionResult> Edit(Guid id)
		{

		  var tag =	await tagRepository.GetAsync(id);

			 if(tag != null)
			{

				var editTagRequest = new EditTagRequest
				{
					Id = tag.Id,
					Name = tag.Name,
					DisplayName = tag.DisplayName
				};

				return View(editTagRequest);
			}
			return View(null);
		}

		[HttpPost]
		public async Task <IActionResult> Edit (EditTagRequest editTagRequest)
		{
			var tag = new Tag
			{
				Id = editTagRequest.Id,
				Name = editTagRequest.Name,
				DisplayName = editTagRequest.DisplayName,

			};

			await tagRepository.UpdateAsync(tag);
			return RedirectToAction("Edit", new {id = editTagRequest.Id});
		}

		[HttpPost]
		public async Task< IActionResult >Delete(EditTagRequest editTagRequest)
		{
			var deletedTag = tagRepository.DeleteAsync(editTagRequest.Id);

			if(deletedTag != null)
			{
				return RedirectToAction("List");
			}
			return RedirectToAction("Edit", new { id = editTagRequest.Id });
		}

	}
}
