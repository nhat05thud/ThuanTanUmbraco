using ThuanTanUmbraco.ClassHelper;

namespace ThuanTanUmbraco.Models
{
    public class ContactForm
    {
        [UmbracoDisplay("Form.Field.Name")]
        [UmbracoRequired("Form.Field.Name.Required")]
        public string Name { get; set; }
        [UmbracoDisplay("Form.Field.Email")]
        [UmbracoRequired("Form.Field.Email.Required")]
        [UmbracoEmail("Form.Field.Email.Validation")]
        public string Email { get; set; }
        [UmbracoDisplay("Form.Field.Phone")]
        [UmbracoRequired("Form.Field.Phone.Required")]
        public string Phone { get; set; }
        [UmbracoDisplay("Form.Field.Message")]
        [UmbracoRequired("Form.Field.Message.Required")]
        public string Message { get; set; }
        public string ErrorMsg { get; set; }
        public int CultureLCID { get; set; }
    }
}