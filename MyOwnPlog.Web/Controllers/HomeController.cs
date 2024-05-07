using Microsoft.AspNetCore.Mvc;
using MyOwnPlog.Web.Models;
using MyOwnPlog.Web.Models.ViewModel;
using MyOwnPlog.Web.Repositories;
using System.Diagnostics;

namespace MyOwnPlog.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository blogPostRepository;
		private readonly ITagRepository tagRepository;

		public HomeController(ILogger<HomeController> logger,IBlogPostRepository blogPostRepository, ITagRepository tagRepository)
		{
			_logger = logger;
            this.blogPostRepository = blogPostRepository;
			this.tagRepository = tagRepository;
		}

		public async Task <IActionResult> Index()
		{
		   var taPost =	await tagRepository.GetAllAsync();
			var blogPost = await blogPostRepository.GetAllAsync();

			var model = new HomeViewModel
			{
				posts = blogPost,
				tags = taPost
			};
			return View(model);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		
		

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}