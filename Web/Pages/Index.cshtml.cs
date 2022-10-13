using ImageMagick;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }
        
        [BindProperty]
        [Required]
        public IFormFile? Upload { get; set; }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!IsImage(Upload!))
            {
                ModelState.AddModelError(nameof(Upload), "The file is not a valid image.");
            }

            try
            {
                // Read image from file
                using var image = new MagickImage(Upload!.OpenReadStream());
                
                // Sets the output format to jpeg
                image.Format = MagickFormat.Jpeg;

                // Create byte array that contains a jpeg file
                byte[] data = image.ToByteArray();

                return File(data, "image/jpeg", $"{Guid.NewGuid()}.jpg");
            }
            catch (Exception ex)
            {

                return Page();
            }
        }

        bool IsImage(IFormFile file)
        {
            try
            {
                using (var bitmap = new System.Drawing.Bitmap(file.OpenReadStream()))
                {
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}