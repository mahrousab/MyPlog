using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyOwnPlog.Web.Models.ViewModel
{
    public class AddBlogPostRequest
    {

        public string Heading { get; set; }
        public string PageTitle { get; set; }

        public string Content { get; set; }

        public string ShortDescription { get; set; }

        public string FeaturedImagedUrl { get; set; }

        public string UrlHandle { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Author { get; set; }

        public bool Visible { get; set; }

        public IEnumerable<SelectListItem> Tags { get; set; }

        public string[] SelectedTags { get; set; } = Array.Empty<string>();

    }
}
