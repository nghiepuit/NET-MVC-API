using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Tên không được bỏ trống")]
        public string FullName { set; get; }

        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        public string Password { set; get; }

        [Required(ErrorMessage = "Email không được bỏ trống")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không đúng.")]
        public string Email { set; get; }

        public string Address { set; get; }

        [Required(ErrorMessage = "Số điện thoại không được bỏ trống")]
        public string PhoneNumber { set; get; }
    }
}