using QRCoder;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.IO;
using crud_dotnet_api.Model;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace crud_dotnet_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRCodeController : ControllerBase
    {
        [HttpPost("Generate")]
        public IActionResult GenerateQRCode([FromBody] PersonalInfo info)
        {
            string content = $"Name: {info.Name}\nEmail: {info.Email}\nPhone: {info.Phone}\nAddress: {info.Address }";

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
                using (BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData))
                {
                    byte[] qrCodeImage = qrCode.GetGraphic(20);
                    return File(qrCodeImage, "image/png");
                }
            }
        }

    }
}
