using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Handler;
using Prorent24.BLL.Services;
using Prorent24.BLL.Services.Configuration.AccountConfiguration.UserRole;
using Prorent24.BLL.Services.CrewMember;
using Prorent24.BLL.Services.General.Module;
using Prorent24.Common.ApplicationSettings;
using Prorent24.Common.Extentions;
using Prorent24.Context;
using Prorent24.Entity;
using Prorent24.WebApp.AppData.Resources;
using Prorent24.WebApp.Extentions;
using Prorent24.WebApp.Models.Account;
using Prorent24.WebApp.Models.Modules;
using Prorent24.WebApp.Transfers.Configurations.Account;
using Prorent24.WebApp.Transfers.CrewMember;
using Prorent24.WebApp.Transfers.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Prorent24.WebApp.Controllers
{
    /// <summary>
    /// Account
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IHostingEnvironment _currentEnvironment;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ICrewMemberService _crewMemberService;
        private readonly IUserRoleService _userRoleService;
        private readonly IModuleService _moduleService;
        private readonly IMediator _mediator;
        private IMemoryCache _cache;
        private readonly IPermissionService _permissionService;
        private readonly LogProrentContext _contextLog;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="currentEnvironment"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="crewMemberService"></param>
        /// <param name="userRoleService"></param>
        /// <param name="moduleService"></param>
        /// <param name="mediator"></param>
        /// <param name="cache"></param>
        public AccountController(
            IHostingEnvironment currentEnvironment,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            ICrewMemberService crewMemberService,
            IUserRoleService userRoleService,
            IModuleService moduleService,
            IMediator mediator,
            IMemoryCache cache,
            LogProrentContext contextLog)
        {
            _currentEnvironment = currentEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _crewMemberService = crewMemberService;
            _userRoleService = userRoleService;
            _moduleService = moduleService;
            _mediator = mediator;
            _cache = cache;
            _contextLog = contextLog;
        }


        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                var source = HttpContext.Request.Host.Value;

                if (result.Succeeded)
                    {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    var crewMember = await _crewMemberService.GetById(user.Id);
                    
                    LogManage.AddLogAuth(_contextLog, model.Email, true, source, user.Id, String.Concat(user.LastName, " ", user.FirstName, " ", user.MiddleName));
                    
                    if (crewMember.Active)
                    {
                        SetUserClaimsCookie();

                        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        else
                        {
                            return Ok();
                        }
                    }
                    else
                    {
                        return Forbid(new AuthenticationProperties(new Dictionary<string, string> { { "error", LocalizedKeys.Accesaccess_denied_contact_administrator } }));// (new Exception(LocalizedKeys.Accesaccess_denied_contact_administrator));
                    }
                }
                else
                {
                    LogManage.AddLogAuth(_contextLog, model.Email, false, source);
                    ModelState.AddModelError("Unauthorized", "Incorrect username and/or password");
                    return Forbid(new AuthenticationProperties(new Dictionary<string, string> { { "error", "Incorrect username and/or password" } }));// (new Exception(LocalizedKeys.Accesaccess_denied_contact_administrator));
                }
            }

            var errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
            return BadRequest(errors);
        }

        /// <summary>
        /// Registration
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isUsersAlreadyExists = _userManager.Users.Count() > 0;
                if (isUsersAlreadyExists)
                {
                    return BadRequest();
                }

                User user = new User
                {
                    Email = model.Email,
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var crewMemberModel = model.TransferToModelView();
                    crewMemberModel.Id = user.Id;
                    crewMemberModel.Active = true;
                    await _crewMemberService.Create(crewMemberModel.TransferToDto());

                    await _userManager.AddToRoleAsync(user, "Administrator");
                    return Ok();
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            var errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
            return BadRequest(errors);
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            RemoveUserClaimsCookie();
            return Ok();
        }

        /// <summary>
        /// Change passwoed
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("change_password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                User user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var _passwordValidator =
                        HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                    var _passwordHasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                    IdentityResult result = await _passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);
                    if (result.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                        await _userManager.UpdateAsync(user);
                        return Ok();
                    }
                    else
                    {
                        return BadRequest(result.Errors);
                    }
                }
            }

            var errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
            return BadRequest(errors);
        }

        /// <summary>
        /// Forgot Password
        /// </summary>
        /// <returns></returns>
        [HttpPost("forgot_password/{email}")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            User user = _userManager.Users.SingleOrDefault(x => x.Email.ToLower() == email.ToLower());

            if (user != null)
            {
                string tempPassword = (1000000).GenerateUniqueId().ToString();
                var _passwordValidator =
                       HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                var _passwordHasher =
                    HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                IdentityResult result = await _passwordValidator.ValidateAsync(_userManager, user, tempPassword);

                if (result.Succeeded)
                {
                    user.PasswordHash = _passwordHasher.HashPassword(user, tempPassword);
                    await _userManager.UpdateAsync(user);
                    await _mediator.Publish(new SendPasswordHandlerModel()
                    {
                        Url = string.Concat("https://", this.HttpContext.Request.Host.Value),
                        Email = email,
                        Password = tempPassword
                    });

                    return Ok();
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Check user authorize
        /// </summary>
        /// <returns></returns>
        [HttpGet("check")]
        public async Task<IActionResult> Check()
        {
            bool _isEmpty = false;
            bool _isAuthenticated = HttpContext.User.Identity.IsAuthenticated;
            var _user = SetUserClaimsCookie();

            bool existsCashe = _cache.TryGetValue(string.Concat("dbIsEmpty", _currentEnvironment.EnvironmentName), out _isEmpty);
            _isEmpty = !existsCashe ? true : _isEmpty;

            string logo;
            _cache.TryGetValue<string>(_currentEnvironment.EnvironmentName, out logo);

            if (_isEmpty)
            {
                _isEmpty = _userManager.Users.Count() == 0;
                _cache.Set(string.Concat("dbIsEmpty", _currentEnvironment.EnvironmentName), _isEmpty,
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(1)));
            }

            if (_user != null && !_isEmpty)
            {
                List<ModuleDto> modules = await _moduleService.GetModuleItemsBaseOnPermission();
                //List<ModuleDto> modules = await _moduleService.GetModuleItems(); всі модулі
                var moduleList = modules.TransferToModuleViewModel();

                return Ok(new
                {
                    isEmpty = _isEmpty,
                    isAuthenticated = _isAuthenticated,
                    user = _user,
                    permission = new List<ModuleViewModel>(), // selected,
                    modules = moduleList,
                    logo = this.HttpContext.GetCompanyLogo(_currentEnvironment.EnvironmentName)
                });
            }
            else
            {
                return Ok(new
                {
                    isEmpty = _isEmpty,
                    isAuthenticated = _isAuthenticated,
                    user = new
                    {
                        id = "",
                        email = "",
                        name = "",
                        surname = ""
                    },
                    permission = new List<ModuleViewModel>(),
                    modules = new List<ModuleViewModel>(),
                    logo = this.HttpContext.GetCompanyLogo(_currentEnvironment.EnvironmentName)
                });
            }
        }

        /// <summary>
        /// Check is empty system - could register new admin user
        /// </summary>
        /// <returns></returns>
        [HttpGet("check-isempty")]
        public IActionResult CheckSystemIsEmpty()
        {
            var result = _userManager.Users.Count() == 0;
            return Ok(result);
        }

        /// <summary>
        /// Upload profile image
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("upload/profile-image")]
        public async Task<IActionResult> UploadProfileImage([FromForm]IFormFile file)
        {
            // your other code
            if (file.Length > 0)
            {

                var path = Path.Combine(
                           Directory.GetCurrentDirectory(), DirectorySettings.fileDirectory,
                           file.FileName);

                if (!System.IO.File.Exists(path))
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                User user = _userManager.Users.SingleOrDefault(x => x.Email.ToLower() == HttpContext.User.Identity.Name.ToLower());
                string imgPath = string.Concat(this.HttpContext.Request.Scheme, "://", this.HttpContext.Request.Host, $"/Files/{_currentEnvironment.EnvironmentName}/{file.FileName}");
                user.ProfileImage = imgPath;
                await _userManager.UpdateAsync(user);
                SetUserClaimsCookie(imgPath);
                return Ok(user.ProfileImage);
            }
            else
            {
                return NoContent();
            }

        }

        #region Helpers

        private object SetUserClaimsCookie(string profileImage = null)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                {
                string jsonUserClaim = Request.Cookies["claims"];

                UserClaimViewModel oldUserclaim = null;

                if (!string.IsNullOrWhiteSpace(jsonUserClaim))
                {
                    oldUserclaim = JsonConvert.DeserializeObject<UserClaimViewModel>(jsonUserClaim);

                    if (!string.IsNullOrEmpty(profileImage))
                    {
                        oldUserclaim.profileImage = profileImage;
                    }
                }

                var context = HttpContext.User;

                UserClaimViewModel _user = new UserClaimViewModel()
                {
                    id = context.FindFirst(ClaimTypes.NameIdentifier).Value,
                    email = context.Identity.Name,
                    name = context.FindFirst(ClaimTypes.GivenName)?.Value,
                    surname = context.FindFirst(ClaimTypes.Surname)?.Value,
                    profileImage = oldUserclaim != null ? oldUserclaim.profileImage : context.FindFirst(ClaimTypes.Anonymous)?.Value,
                };

                Response.Cookies.Append("claims", JsonConvert.SerializeObject(_user));

                return _user;
            }

            return null;
        }

        private void RemoveUserClaimsCookie()
        {
            Response.Cookies.Delete("claims");
        }

        #endregion
    }
}