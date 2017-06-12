using BotDetect.Web.Mvc;
using Ecommerce.Common;
using Ecommerce.Model.Models;
using Ecommerce.Web.App_Start;
using Ecommerce.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController(ApplicationSignInManager signInManager, ApplicationUserManager userManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Account
        public ActionResult Login(string returnUrl)
        {
            if (Request.IsAuthenticated)
            {
                return Redirect("/");
            }
            else
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    ClaimsIdentity identity = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationProperties props = new AuthenticationProperties();
                    props.IsPersistent = model.RememberMe;
                    authenticationManager.SignIn(props, identity);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }else
                    {
                        return Redirect("/");
                    }
                }else
                {
                    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không chính xác");
                }
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // GET: Account
        public ActionResult Register()
        {
            if (Request.IsAuthenticated)
            {
                return Redirect("/");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "registerCaptcha", "Mã xác nhận không chính xác")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = await _userManager.FindByEmailAsync(model.Email);
                if (email != null)
                {
                    ModelState.AddModelError("email", "Email đã tồn tại");
                    return View(model);
                }

                var username = await _userManager.FindByNameAsync(model.UserName);
                if (username != null)
                {
                    ModelState.AddModelError("username", "Tài khoản đã tồn tại");
                    return View(model);
                }

                var user = new AppUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    EmailConfirmed = true,
                    BirthDay = DateTime.Now,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address
                };
                await _userManager.CreateAsync(user, model.Password);
                var adminUser = await _userManager.FindByEmailAsync(model.Email);
                if (adminUser != null)
                {
                    await _userManager.AddToRolesAsync(adminUser.Id, new string[] { "User" });
                }

                string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/template/newuser.html"));
                content = content.Replace("{{ UserName }}", adminUser.FullName);
                content = content.Replace("{{ Link }}", ConfigHelper.GetByKey("CurrentLink") + "dang-nhap.html");
                MailHelper.SendMail(adminUser.Email, "Đăng Ký Thành Công", content);

                ViewData["SuccessMessage"] = "Đăng ký thành công";
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return Redirect("/");
        }

    }
}