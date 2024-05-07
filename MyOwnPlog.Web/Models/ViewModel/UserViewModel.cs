namespace MyOwnPlog.Web.Models.ViewModel
{
    public class UserViewModel
    {

      public List<User> users {  get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool AdminRoleCheckbox { get; set; }
    }
}
