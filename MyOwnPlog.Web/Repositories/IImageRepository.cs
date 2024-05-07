namespace MyOwnPlog.Web.Repositories
{
    public interface IImageRepository
    {

         Task<string> UploadAsync(IFormFile file);
    }
}
