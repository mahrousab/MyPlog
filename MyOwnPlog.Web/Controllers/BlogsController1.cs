﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyOwnPlog.Web.Models.Domain;
using MyOwnPlog.Web.Models.ViewModel;
using MyOwnPlog.Web.Repositories;
using System.Runtime.CompilerServices;

namespace MyOwnPlog.Web.Controllers
{
    public class BlogsController1 : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly UserManager<IdentityUser> userManager;
		private readonly IBlogPostCommentRepository blogPostCommentRepository;
		private readonly SignInManager<IdentityUser> signInManager;
        private readonly IBlogPostLikeRepository blogPostLikeRepository;
        public  BlogsController1(IBlogPostRepository blogPostRepository,IBlogPostLikeRepository blogPostLikeRepository, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IBlogPostCommentRepository blogPostCommentRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.userManager = userManager;
			this.blogPostCommentRepository = blogPostCommentRepository;
			this.signInManager = signInManager;
            this.blogPostLikeRepository = blogPostLikeRepository;
        }

        [HttpGet]
		public async Task<IActionResult> Index(string urlHandle)
        {
			var liked = false;
			var blogPost = await blogPostRepository.GetByUrlHandleAsync(urlHandle);
			var blogDetailsViewModel = new BlogDetailsViewModel();

			if (blogPost != null)
			{
				var totalLikes = await blogPostLikeRepository.GetTotalLikes(blogPost.Id);

				if (signInManager.IsSignedIn(User))
				{
					// Get like for this blog for this user
					var likesForBlog = await blogPostLikeRepository.GetLikesForBlog(blogPost.Id);

					var userId = userManager.GetUserId(User);

					if (userId != null)
					{
						var likeFromUser = likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
						liked = likeFromUser != null;
					}


				}

				// Get comments for blog post
				var blogCommentsDomainModel = await blogPostCommentRepository.GetCommentsByBlogIdAsync(blogPost.Id);

				var blogCommentsForView = new List<BlogComment>();

				foreach (var blogComment in blogCommentsDomainModel)
				{
					blogCommentsForView.Add(new BlogComment
					{
						Description = blogComment.Description,
						DateAdded = blogComment.DateAdded,
						Username = (await userManager.FindByIdAsync(blogComment.UserId.ToString())).UserName
					});
				}

				blogDetailsViewModel = new BlogDetailsViewModel
				{
					Id = blogPost.Id,
					Content = blogPost.Content,
					PageTitle = blogPost.PageTitle,
					Author = blogPost.Author,
					FeaturedImagedUrl = blogPost.FeaturedImagedUrl,
					Heading = blogPost.Heading,
					PublishedDate = blogPost.PublishedDate,
					ShortDescription = blogPost.ShortDescription,
					UrlHandle = blogPost.UrlHandle,
					Visible = blogPost.Visible,
					Tags = blogPost.Tags,
					TotalLikes = totalLikes,
					Liked = liked,
					Comments = blogCommentsForView
				};

			}

			return View(blogDetailsViewModel);
		}
        
		
	/*
	public async Task <IActionResult> Index(string UrlHandle)
	{

		var blogPost = await blogPostRepository.GetByUrlHandleAsync(UrlHandle);
		return View(blogPost);
	}
	*/
	[HttpPost]

        public async Task<IActionResult> Index(BlogDetailsViewModel blogDetailsViewModel)
        {
            if (signInManager.IsSignedIn(User))
            {
                var model = new BlogPostComment
                {
                    BlogPostId = blogDetailsViewModel.Id,
                    Description = blogDetailsViewModel.CommentDescription,
                    UserId = Guid.Parse(userManager.GetUserId(User)),
                    DateAdded = DateTime.Now
                };

             await blogPostCommentRepository.AddAsync(model);
                return RedirectToAction("Index", "Blogs", new { UrlHandle = blogDetailsViewModel.UrlHandle}) ;
			}

            return View();

            

        }
    }
}
