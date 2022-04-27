using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PUSL2020.Web.Pages;

public class UploadImage : PageModel
{
    public  string[] AllowedExtensions => new[] { "jpg", "png" };
    public bool IsLimited => true;
    
    


    public void OnGet()
    {
        
    }

    public void OnPostUpload(IEnumerable<IFormFile> files)
    {
        
    }
}