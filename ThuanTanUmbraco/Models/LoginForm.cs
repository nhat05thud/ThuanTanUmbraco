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

        [UmbracoRequired("Form.Field.Email.Required")]
        public string Username { get; set; }
        [UmbracoRequired("Form.Field.Password.Required")]
        [UmbracoRange(10, 15, "Form.Field.PasswordLength.Required")]
        [MinLength(10, ErrorMessage = "Minimum 10 character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ResponseText { get; set; }
        public string ResponseHeader { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsRunScripts { get; set; }
        public string RedirectLink { get; set; }
    }

    public class ResetPasswordForm
    {
        [UmbracoRequired("Form.Field.Email.Required")]
        public string Username { get; set; }
        public string ResponseText { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class RegisterForm
    {
        [UmbracoRequired("Form.Field.Email.Required")]
        public string Username { get; set; }
        [UmbracoRequired("Form.Field.Password.Required")]
        [UmbracoRange(10, 15, "Form.Field.PasswordLength.Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [System.Web.Mvc.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [UmbracoRequired("Form.Field.FirstName.Required")]
        public string FirstName { get; set; }
        [UmbracoRequired("Form.Field.LastName.Required")]
        public string LastName { get; set; }
        public string ResponseText { get; set; }
        public bool IsSuccess { get; set; }
    }
}