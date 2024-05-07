using MyOwnPlog.Web.Models.Domain;

namespace MyOwnPlog.Web.Models.ViewModel
{
	public class HomeViewModel
	{

	  public IEnumerable<Tag> tags { get; set; }	

	  public IEnumerable<BlogPost> posts { get; set; }

	}
}
