namespace MyOwnPlog.Web.Models.ViewModel
{
    public class AddLikeRequest
    {

        public Guid BlogPostId { get; set; }

        public Guid UserId { get; set; }
    }
}
