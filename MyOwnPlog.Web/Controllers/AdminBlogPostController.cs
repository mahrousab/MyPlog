using Microsoft.AspNetCore.Mvc;
using MyOwnPlog.Web.Models.ViewModel;
using MyOwnPlog.Web.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyOwnPlog.Web.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using static Microsoft.VisualStudio.Services.Notifications.VssNotificationEvent;

namespace MyOwnPlog.Web.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminBlogPostController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;

        private readonly ITagRepository tagRepository;

        public AdminBlogPostController(IBlogPostRepository blogPostRepository, ITagRepository tagRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public async Task <IActionResult> Add()
        {
        var tags =  await tagRepository.GetAllAsync();

            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem{ Text = x.Name, Value = x.Id.ToString()})
            };
            return View(model);
        }
        [HttpPost]
        public async Task <IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {

            var blogpost = new BlogPost
            {
                 Heading = addBlogPostRequest.Heading,
                 Content = addBlogPostRequest.Content,
                 Author = addBlogPostRequest.Author,
                 UrlHandle = addBlogPostRequest.UrlHandle,
                 FeaturedImagedUrl = addBlogPostRequest.FeaturedImagedUrl,
                 ShortDescription = addBlogPostRequest.ShortDescription,
                 PublishedDate = addBlogPostRequest.PublishedDate,
                 PageTitle = addBlogPostRequest.PageTitle,
                 Visible = addBlogPostRequest.Visible
                

            };

            // map tag form selected 


            var SelectedTags = new List<Tag>();
            foreach(var SelectedTagId in addBlogPostRequest.SelectedTags)
            {
                var SelectedTagIdAsGuid = Guid.Parse(SelectedTagId);
                var existingTag = await tagRepository.GetAsync(SelectedTagIdAsGuid);

                if(existingTag != null)
                {
                    SelectedTags.Add(existingTag);
                }
            }
            blogpost.Tags = SelectedTags;
            await blogPostRepository.AddAsync(blogpost);
            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
           var blogpost =  await blogPostRepository.GetAllAsync();

            return View(blogpost);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // map model to view model 

            
            var blogpost = await blogPostRepository.GetAsync(id);
            var tagsDomainModel = await tagRepository.GetAllAsync();

            if(blogpost != null)
            {
                var model = new EditBlogPostRequest
                {


                    Id = blogpost.Id,
                    Heading = blogpost.Heading,
                    PageTitle = blogpost.PageTitle,
                    Content = blogpost.Content,
                    Visible = blogpost.Visible,
                    Author = blogpost.Author,
                    UrlHandle = blogpost.UrlHandle,
                    FeaturedImagedUrl = blogpost.FeaturedImagedUrl,
                    ShortDescription = blogpost.ShortDescription,
                    PublishedDate = blogpost.PublishedDate,

                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {

                        Text = x.Name,
                        Value = x.Id.ToString()

                    }),

                    SelectedTags = blogpost.Tags.Select(x => x.Id.ToString()).ToArray()

                };
                return View(model);
            }
            
            return View(null);
        }

        [HttpPost]

        public async Task<IActionResult> Edit (EditBlogPostRequest editBlogPostRequest)
        {

            // first we will maping the model to view model 


            var blogPostDomainModel = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                Author = editBlogPostRequest.Author,
                Content = editBlogPostRequest.Content,
                PageTitle = editBlogPostRequest.PageTitle,
                ShortDescription = editBlogPostRequest.ShortDescription,
                FeaturedImagedUrl = editBlogPostRequest.FeaturedImagedUrl,
                PublishedDate = editBlogPostRequest.PublishedDate,
                UrlHandle = editBlogPostRequest.UrlHandle,
                Visible = editBlogPostRequest.Visible


            };

            var selectedTags = new List<Tag>();
            foreach(var selectedTag in editBlogPostRequest.SelectedTags)
            {
                if (Guid.TryParse(selectedTag, out var tag))
                {
                    var foundTag = await tagRepository.GetAsync(tag);
                    if (foundTag != null)
                    {

                        selectedTags.Add(foundTag);
                    }
                }
            }

            blogPostDomainModel.Tags = selectedTags;

            var UpdateBlog = await blogPostRepository.UpdateAsync(blogPostDomainModel);

            if(UpdateBlog!= null)
            {
                return RedirectToAction("Edit");
            }
            return RedirectToAction("Edit");
        }

        [HttpPost]

        public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
        {
            var deletedBlogPost = await blogPostRepository.DeleteAsync(editBlogPostRequest.Id);

            if (deletedBlogPost != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new {id = editBlogPostRequest.Id});
        }
    }
}
