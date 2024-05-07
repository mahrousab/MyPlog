using System.ComponentModel.DataAnnotations;

namespace MyOwnPlog.Web.Models.ViewModel
{
    public class AddTagRequest
    {

        [Required]
        public string Name { get; set; }
        [Required] 
        public string DisplayName { get; set; }

    }
}
