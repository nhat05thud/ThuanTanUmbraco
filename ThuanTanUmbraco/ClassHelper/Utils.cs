using System;
using System.Drawing;
using System.IO;
using System.Web;

namespace ThuanTanUmbraco.ClassHelper
{
    public static class Utils
    {
        public static string ConvertImageToBase64(string imagePath)
        {
            using (Image image = Image.FromFile(HttpContext.Current.Server.MapPath("~" + imagePath)))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
    }
}