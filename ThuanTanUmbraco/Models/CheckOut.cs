using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThuanTanUmbraco.ClassHelper;

namespace ThuanTanUmbraco.Models
{
    public class CheckOut
    {
        public CheckOut()
        {
            IsSuccess = false;
        }
        public int MemberId { get; set; }
        public string PaymentMethods { get; set; }
        [UmbracoRequired("Form.Field.Name.Required")]
        public string Name { get; set; }
        [UmbracoRequired("Form.Field.PhoneNumber.Required")]
        public string PhoneNumber { get; set; }
        [UmbracoRequired("Form.Field.Address.Required")]
        public string Address { get; set; }
        [UmbracoRequired("Form.Field.Email.Required")]
        [UmbracoEmail("Form.Field.Email.Validation")]
        public string Email { get; set; }
        public string Note { get; set; }
        public List<CartItem> CartItems { get; set; }
        public bool IsSuccess { get; set; }
    }
}