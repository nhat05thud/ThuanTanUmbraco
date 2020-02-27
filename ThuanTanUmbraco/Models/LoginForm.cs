using System.ComponentModel.DataAnnotations;
using ThuanTanUmbraco.ClassHelper;

namespace ThuanTanUmbraco.Models
{
    public class LoginForm
    {
        public LoginForm()
        {
            IsRunScripts = false;
        }

        [UmbracoRequired("Username is required")]
        public string Username { get; set; }
        [UmbracoRequired("PassWord is required")]
        [MinLength(10, ErrorMessage = "Minimum 10 character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ResponseText { get; set; }
        public string ResponseHeader { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsRunScripts { get; set; }
        public string RedirectLink { get; set; }
    }
}