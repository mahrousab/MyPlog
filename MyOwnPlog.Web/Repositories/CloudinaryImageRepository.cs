using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Identity.Client;
using Microsoft.VisualStudio.Services.Account;

namespace MyOwnPlog.Web.Repositories
{
    public class CloudinaryImageRepository : IImageRepository
    {

        private readonly IConfiguration configuration;

        private readonly CloudinaryDotNet.Account account;

        public CloudinaryImageRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(account);

            var UploadParams = new ImageUploadParams()
            {

                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName


            };
            var UploadResult = await client.UploadAsync(UploadParams);
            if(UploadResult != null&& UploadResult.StatusCode == System.Net.HttpStatusCode.OK) {
               
                return UploadResult.SecureUri.ToString();
            
            }

            return null;
        }
    }
}
