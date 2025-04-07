using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Handler;
using Prorent24.BLL.Services.CrewMember;
using Prorent24.BLL.Services.CrewPlanner;
using Prorent24.BLL.Services.General.Mail;
using Prorent24.BLL.Transfers.CrewMember;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Warehouse;
using Prorent24.Entity;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.General;
using Prorent24.Enum.Project;
using Prorent24.WebApp.Models.CrewMember;
using Prorent24.WebApp.Models.General.Filter;
using Prorent24.WebApp.Models.General.Period;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.CrewMember;
using Prorent24.WebApp.Transfers.General;
using Prorent24.WebApp.Transfers.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prorent24.BLL.Services;
using Prorent24.BLL.Services.AspUser;
using Prorent24.Common.Models.Filters;
using Microsoft.AspNetCore.Http;

namespace Prorent24.WebApp.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/crew_members")]
    [ApiController]
    public class CrewMembersController : ControllerBase
    {
        private IMailService _mailService;
        private readonly ICrewMemberService _crewMemberService;
        private readonly ICrewMemberRateService _crewMemberRateService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMediator _mediator;
        private readonly ICrewPlannerService _crewPlannerService;
        private readonly IPermissionService _permissionService;
        private readonly IUserService _userService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="crewMemberService"></param>
        /// <param name="crewMemberRateService"></param>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="mailService"></param>
        /// <param name="mediator"></param>
        /// <param name="crewPlannerService"></param>
        /// <param name="permissionService"></param>
        /// <param name="userService"></param>
        public CrewMembersController(ICrewMemberService crewMemberService,
                                     ICrewMemberRateService crewMemberRateService,
                                     UserManager<User> userManager,
                                     RoleManager<Role> roleManager,
                                     IMailService mailService,
                                     IMediator mediator, ICrewPlannerService crewPlannerService,
                                     IPermissionService permissionService,
                                     IUserService userService)
        {
            _crewMemberService = crewMemberService;
            _crewMemberRateService = crewMemberRateService;
            _userManager = userManager;
            _roleManager = roleManager;
            _mailService = mailService;
            _mediator = mediator;
            _crewPlannerService = crewPlannerService;
            _permissionService = permissionService;
            _userService = userService;
        }

        /// <summary>
        /// Get list CrewMembers
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCrewMemberList([FromQuery]string filters)
        {
            var permission = await _permissionService.GetFunctionPermissions(ModuleTypeEnum.CrewMembers, PermissionFieldEnum.DatabaseCrewMember);

            if (permission.View)
            {
                List<SelectedFilterViewModel> resultFilter = !string.IsNullOrWhiteSpace(filters) ?
                JsonConvert.DeserializeObject<List<SelectedFilterViewModel>>(filters) : new List<SelectedFilterViewModel>();
                BaseListDto dto = await _crewMemberService.GetPagedList(resultFilter.TransferToDtoModel());
                return Ok(dto.TransferToViewModel());
            }
            else
            {
                return Ok(new BaseListDto().TransferToViewModel());
            }
        }

        /// <summary>
        /// Get list CrewMembers for project planning
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("groups")]
        public async Task<IActionResult> GetCrewMwmberForProjectPlanning()
        {
            BaseListDto dto = await _crewMemberService.GetCrewMwmberForProjectPlanning();
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Serch CrewMembers by part of name
        /// </summary>
        /// <param name="search_text"></param>
        /// <returns></returns>
        [HttpGet("search/{search_text}")]
        public async Task<IActionResult> GetCrewMemberByName(string search_text)
        {
            if (search_text != null && !search_text.Equals("null"))
            {
                var text = search_text.ToLower();
                List<User> dtos = _userManager.Users.ToList();

                dtos = dtos.Where(x => x.FirstName?.IndexOf(search_text, StringComparison.OrdinalIgnoreCase) >= 0
                || x.LastName?.IndexOf(search_text, StringComparison.OrdinalIgnoreCase) >= 0
                || x.UserName?.IndexOf(search_text, StringComparison.OrdinalIgnoreCase) >= 0
                || x.Email?.IndexOf(search_text, StringComparison.OrdinalIgnoreCase) >= 0)
                .Select(x => x).ToList();

                return Ok(dtos.TransferToModelView());
            }
            else
            {
                List<User> dtos = _userManager.Users.ToList();
                return Ok(dtos.TransferToModelView());
            }
        }

        /// <summary>
        /// Get CrewMember by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCrewMemberById(string id)
        {
            var permission = await _permissionService.GetFunctionPermissions(ModuleTypeEnum.CrewMembers, PermissionFieldEnum.DatabaseCrewMember);
            var userId = _permissionService.GetUserId();
            if (permission.View || userId == id)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    CrewMemberDto dto = await _crewMemberService.GetById(id);
                    dto.Email = user.Email;
                    dto.Username = user.UserName;
                    dto.FirstName = user.FirstName;
                    dto.LastName = user.LastName;
                    dto.MiddleName = user.MiddleName;
                    dto.Phone = user.PhoneNumber;
                    dto.IsSystemUser = user.IsSystemUser;

                    try
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        var role = await _roleManager.FindByNameAsync(roles.FirstOrDefault());
                        dto.UserRoleId = role.Id;
                    }
                    catch (Exception ex)
                    {

                    }

                    return Ok(dto.TransferToViewModel());
                }
                else
                {
                    return NoContent();
                }
            }
            else
            {
                return Forbid();
            }
        }

        /// <summary>
        /// Get CrewMember Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetCrewMemberDetails(string id)
        {
            List<ModuleDetailDto> dtos = await _crewMemberService.GetDetails(id);
            return Ok(dtos?.TransferToModuleDetailViewModel());
        }

        /// <summary>
        /// Create CrewMember
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateCrewMember(CrewMemberViewModel model)
        {
            if (ModelState.IsValid)
            {
                var permission = await _permissionService.GetFunctionPermissions(ModuleTypeEnum.CrewMembers, PermissionFieldEnum.DatabaseCrewMember);
                User consistUser = model.Email != null ? await _userManager.FindByEmailAsync(model.Email) : null;
                if ((permission.Add && consistUser == null) || (consistUser != null && consistUser.Removed))
                {
                    string tempPassword = (1000000).GenerateUniqueId().ToString();

                    User user = new User
                    {
                        Email = model.Email,
                        UserName = model.Username,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        MiddleName = model.MiddleName,
                        PhoneNumber = model.Phone,
                        IsSystemUser = model.IsSystemUser.HasValue ? model.IsSystemUser : (model.Username != null ? true : false)
                    };
                    IdentityResult result = new IdentityResult();
                    User notSystemUser = new Entity.User();
                    if (user.IsSystemUser.HasValue && user.IsSystemUser.Value)
                    {
                        result = await _userManager.CreateAsync(user, tempPassword);
                        if (!result.Succeeded)
                        {
                            consistUser.Removed = false;
                            consistUser.UserName = model.Username;
                            consistUser.FirstName = model.FirstName;
                            consistUser.LastName = model.LastName;
                            consistUser.MiddleName = model.MiddleName;
                            consistUser.PhoneNumber = model.Phone;
                            consistUser.IsSystemUser = model.IsSystemUser.HasValue ? model.IsSystemUser : (model.Username != null ? true : false);
                            result = await _userManager.UpdateAsync(consistUser);
                            user = consistUser;
                        }
                    }
                    else
                    {
                        notSystemUser = await _userService.Create(user);
                    }

                    if (result.Succeeded || notSystemUser.Id != null)
                    {
                        var userId = await _userManager.GetUserIdAsync(user);
                        model.Id = userId;

                        CrewMemberDto crewMember = await _crewMemberService.GetById(userId);

                        if (crewMember != null && !string.IsNullOrEmpty(crewMember.Id))
                        {
                            crewMember = await _crewMemberService.Update(model.TransferToDto());
                        }
                        else
                        {
                            crewMember = await _crewMemberService.Create(model.TransferToDto());
                        }
                        Role role;
                        decimal dailyRate = 0;
                        decimal hourlyRate = 0;
                        if (user.IsSystemUser.HasValue && user.IsSystemUser.Value)
                        {
                            if (model.UserRoleId == null)
                            {
                                role = _roleManager.Roles.Where(x => x.IsDefault).FirstOrDefault() ?? _roleManager.Roles.FirstOrDefault();
                            }
                            else
                            {
                                role = await _roleManager.FindByIdAsync(model.UserRoleId);
                            }
                            dailyRate = (role.Rate ?? 0) / 20;
                            hourlyRate = (role?.Rate ?? 0) / 160;
                            try
                            {
                                IdentityResult identityResult = await _userManager.AddToRoleAsync(user, role.Name);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        CrewMemberRateDto crewMemberDto = new CrewMemberRateDto()
                        {
                            CrewMemberId = crewMember.Id,
                            DailyRate = dailyRate, // середня к-ть робочих днів
                            HourlyRate = hourlyRate,// середня к-ть робочих годин
                            Name = "Базовая ставка",
                            IsDefaultRate = true,


                        };
                        CrewMemberRateDto dto = await _crewMemberRateService.Create(crewMemberDto);

                        return await GetCrewMemberById(userId); //Ok(dto.TransferToViewModel());

                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    return !permission.Add ? Forbid() : ValidationProblem();
                }
            }

            var errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
            return BadRequest(errors);
        }

        /// <summary>
        /// Send New Login Info (Email)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}/send_login_info")]
        public async Task<IActionResult> SendLoginInfo(string id)
        {
            string tempPassword = (1000000).GenerateUniqueId().ToString();
            var user = await _userManager.FindByIdAsync(id);
            var _passwordHasher = HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;
            user.PasswordHash = _passwordHasher.HashPassword(user, tempPassword);
            await _userManager.UpdateAsync(user);
            await _mediator.Publish(new SendPasswordHandlerModel()
            {
                Url = string.Concat(this.HttpContext.Request.Scheme, ":\\", this.HttpContext.Request.Host.Value),
                Email = user.Email,
                Password = tempPassword
            });
            return Ok();
        }

        /// <summary>
        /// Update CrewMember
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateCrewMember(string id, CrewMemberViewModel model)
        {
            DateTime date = DateTime.Now;

            if (ModelState.IsValid)
            {
                var permission = await _permissionService.GetFunctionPermissions(ModuleTypeEnum.CrewMembers, PermissionFieldEnum.DatabaseCrewMember);

                if (permission.Edit)
                {
                    User user = await _userManager.FindByIdAsync(id);
                    if (user != null && _userManager.Users.Any(x=> x.Email.Equals(user.Email) || x.Email.Equals(model.Email)))
                    {
                        user.Email = model.Email;
                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;
                        user.MiddleName = model.MiddleName;
                        user.PhoneNumber = model.Phone;
                        user.IsSystemUser = model.IsSystemUser ?? model.Email != null ? true : false;
                        if (user.IsSystemUser.HasValue && user.IsSystemUser.Value)
                        {
                            user.UserName = model.Username;
                            user.NormalizedEmail = model.Email.ToUpper();
                            user.NormalizedUserName = model.Username.ToUpper();
                            await _userManager.UpdateSecurityStampAsync(user);
                            await _userManager.UpdateAsync(user);
                        }
                        if (user.IsSystemUser.HasValue && user.IsSystemUser.Value)
                        {
                            await _userManager.UpdateAsync(user);
                        }
                        else
                        {
                            var result = await _userService.Update(user);
                        }
                        model.Id = id;
                        string userId = _permissionService.GetUserId();
                        CrewMemberDto dto = await _crewMemberService.Update(model.TransferToDto(), userId);

                        if (model.UserRoleId != null && userId != model.Id && model.IsSystemUser.HasValue && model.IsSystemUser.Value)
                        {
                            var role = await _roleManager.FindByIdAsync(model.UserRoleId);
                            try
                            {
                                var roles = await _userManager.GetRolesAsync(user);
                                IdentityResult removerolesResult = await _userManager.RemoveFromRolesAsync(user, roles);
                                IdentityResult identityResult = await _userManager.AddToRoleAsync(user, role.Name);
                                dto.UserRoleId = role.Id;
                                await _crewMemberService.Update(dto, userId);
                            }
                            catch { }
                        }

                        return await GetCrewMemberById(user.Id);

                    }
                    return BadRequest();
                }
                else
                {
                    return Forbid();
                }
            }

            var errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
            return BadRequest(errors);
        }

        /// <summary>
        /// Delete CrewMember
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> DeleteCrewMember(string id)
        {
            var permission = await _permissionService.GetFunctionPermissions(ModuleTypeEnum.CrewMembers, PermissionFieldEnum.DatabaseCrewMember);
            string userId = _permissionService.GetUserId();
            if (permission.Delete && userId != id)
            {
                bool result = await _crewMemberService.Delete(id);
                if (result)
                {
                    User user = await _userManager.FindByIdAsync(id);
                    user.Removed = true;
                    await _userManager.UpdateAsync(user);
                }
                return Ok(result);
            }
            else
            {
                return Forbid();
            }
        }

        /// <summary>
        /// Get list CrewMemberRates
        /// </summary>
        /// <returns></returns>
        [HttpGet("{crewMemberId}/rates")]
        public async Task<IActionResult> GetCrewMwmberRateList(string crewMemberId) // List<FilterListViewModel> filters
        {
            BaseListDto dto = await _crewMemberRateService.GetPagedList(crewMemberId);
            return Ok(dto.TransferToViewModel());
        }
        /// <summary>
        /// Get CrewMemberRate by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{crewMemberId}/rates/{id}")]
        public async Task<IActionResult> GetCrewMemberRateById(string crewMemberId, int id)
        {
            if (ModelState.IsValid)
            {
                CrewMemberRateDto dto = await _crewMemberRateService.GetById(id);
                dto.CrewMemberId = crewMemberId;

                return Ok(dto.TransferToViewModel());
            }

            var errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
            return BadRequest(errors);
        }

        /// <summary>
        /// Create CrewMemberRate
        /// </summary>
        /// <param name="crewMemberId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{crewMemberId}/rates")]
        public async Task<IActionResult> CreateCrewMemberRate(string crewMemberId, CrewMemberRateViewModel model)
        {
            if (ModelState.IsValid)
            {

                CrewMemberRateDto crewMemberDto = model.TransferToDto();
                crewMemberDto.CrewMemberId = crewMemberId;

                CrewMemberRateDto dto = await _crewMemberRateService.Create(crewMemberDto);
                bool isDefaultRate = await _crewMemberService.UpdateDefaultRate(crewMemberId, dto.Id);
                dto.IsDefaultRate = isDefaultRate;

                return Ok(dto.TransferToViewModel());

            }

            var errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
            return BadRequest(errors);
        }

        /// <summary>
        /// Update CrewMemberRate
        /// </summary>
        /// <param name="crewMemberId"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{crewMemberId}/rates/{id}")]
        public async Task<IActionResult> UpdateCrewMemberRate(string crewMemberId, int id, CrewMemberRateViewModel model)
        {
            if (ModelState.IsValid)
            {
                CrewMemberRateDto crewMemberDto = model.TransferToDto();
                crewMemberDto.CrewMemberId = crewMemberId;
                crewMemberDto.Id = id;

                CrewMemberRateDto dto = await _crewMemberRateService.Update(crewMemberDto);
                return Ok(dto.TransferToViewModel());
            }

            var errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
            return BadRequest(errors);
        }

        /// <summary>
        /// Delete CrewMemberRates
        /// </summary>
        /// <param name="crewMemberId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{crewMemberId}/rates/{id}/delete")]
        public async Task<IActionResult> DeleteCrewMemberRate(string crewMemberId, int id)
        {
            bool result = await _crewMemberRateService.Delete(id);
            return Ok(result);
        }


        /// <summary>
        /// Get TimeLine
        /// </summary>
        /// <returns></returns>
        [HttpGet("timeLine")]
        public async Task<IActionResult> GetWarhouseEventResourseModel([FromQuery]string period, [FromQuery]List<string> ids)
        {
            ShortPeriodViewModel resultPeriod = !string.IsNullOrWhiteSpace(period) ? JsonConvert.DeserializeObject<ShortPeriodViewModel>(period) : new ShortPeriodViewModel();
            WarhouseEventResourseModel result = await _crewPlannerService.GetAllForModuleTimeLine(ProjectFunctionTypeEnum.Crew, ids, resultPeriod.FromDate, resultPeriod.ToDate);
            return Ok(result);
        }
    }
}