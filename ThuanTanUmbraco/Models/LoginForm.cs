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
        [UmbracoEmail("Form.Field.Email.Validation")]
        public string Username { get; set; }
        [UmbracoRequired("Form.Field.Password.Required")]
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
        public RegisterForm()
        {
            IsRegisterSuccess = false;
            IsSendMail = false;
        }
        [UmbracoRequired("Form.Field.Email.Required")]
        [UmbracoEmail("Form.Field.Email.Validation")]
        public string Username { get; set; }
        [UmbracoRequired("Form.Field.Password.Required")]
        [MinLength(10, ErrorMessage = "Mật khẩu ít nhất 10 ký tự")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [System.Web.Mvc.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [UmbracoRequired("Form.Field.FirstName.Required")]
        public string FirstName { get; set; }
        [UmbracoRequired("Form.Field.LastName.Required")]
        public string LastName { get; set; }
        public string ResponseText { get; set; }
        public bool IsRegisterSuccess { get; set; }
        public bool IsSendMail { get; set; }
    }

    public class VerifyAccount
    {
        public string Email { get; set; }
        public bool IsSuccess { get; set; }
        public string ResponseText { get; set; }
    }
}