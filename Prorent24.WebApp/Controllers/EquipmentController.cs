using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Equipment;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Services.Equipment;
using Prorent24.BLL.Services.Equipment.Accessory;
using Prorent24.BLL.Services.Equipment.Alternative;
using Prorent24.BLL.Services.Equipment.Content;
using Prorent24.BLL.Services.Equipment.PeriodicInspection;
using Prorent24.BLL.Services.Equipment.SerialNumber;
using Prorent24.BLL.Services.Equipment.SerialNumber.QRCode;
using Prorent24.BLL.Services.Equipment.Stock;
using Prorent24.BLL.Services.Equipment.Supplier;
using Prorent24.BLL.Services.Equipment.Webshop;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.WebApp.Models.Equipment;
using Prorent24.WebApp.Models.General.Filter;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.Equipment;
using Prorent24.WebApp.Transfers.General;
using Prorent24.WebApp.Transfers.Modules;

namespace Prorent24.WebApp.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/equipments")]
    [ApiController]
    public partial class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;
        private readonly IEquipmentAccessoryService _equipmentAccessoryService;
        private readonly IEquipmentAlternativeService _equipmentAlternativeService;
        private readonly IEquipmentContentService _equipmentContentService;
        private readonly IEquipmentSupplierService _equipmentSupplierService;
        private readonly IEquipmentSerialNumberService _equipmentSerialNumberService;
        private readonly IEquipmentQRCodeService _equipmentQRCodeService;
        private readonly IEquipmentPeriodicInspectionService _equipmentPeriodicInspectionService;
        private readonly IEquipmentWebShopService _equipmentWebShopService;
        private readonly IEquipmentStockService _equipmentStockService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="equipmentService"></param>
        /// <param name="equipmentAccessoryService"></param>
        /// <param name="equipmentAlternativeService"></param>
        /// <param name="equipmentContentService"></param>
        /// <param name="equipmentSupplierService"></param>
        /// <param name="equipmentSerialNumberService"></param>
        /// <param name="equipmentQRCodeService"></param>
        /// <param name="equipmentPeriodicInspectionService"></param>
        /// <param name="equipmentWebShopService"></param>
        /// <param name="equipmentStockService"></param>
        public EquipmentController(IEquipmentService equipmentService, IEquipmentAccessoryService equipmentAccessoryService,
            IEquipmentAlternativeService equipmentAlternativeService, IEquipmentContentService equipmentContentService,
            IEquipmentSupplierService equipmentSupplierService, IEquipmentSerialNumberService equipmentSerialNumberService,
            IEquipmentQRCodeService equipmentQRCodeService, IEquipmentPeriodicInspectionService equipmentPeriodicInspectionService,
            IEquipmentWebShopService equipmentWebShopService, IEquipmentStockService equipmentStockService)
        {
            _equipmentService = equipmentService ?? throw new ArgumentNullException(nameof(equipmentService));
            _equipmentAccessoryService = equipmentAccessoryService ?? throw new ArgumentNullException(nameof(equipmentAccessoryService));
            _equipmentAlternativeService = equipmentAlternativeService ?? throw new ArgumentNullException(nameof(equipmentAlternativeService));
            _equipmentContentService = equipmentContentService ?? throw new ArgumentNullException(nameof(equipmentContentService));
            _equipmentSupplierService = equipmentSupplierService ?? throw new ArgumentNullException(nameof(equipmentSupplierService));
            _equipmentSerialNumberService = equipmentSerialNumberService ?? throw new ArgumentNullException(nameof(equipmentSerialNumberService));
            _equipmentQRCodeService = equipmentQRCodeService ?? throw new ArgumentNullException(nameof(equipmentQRCodeService));
            _equipmentPeriodicInspectionService = equipmentPeriodicInspectionService ?? throw new ArgumentNullException(nameof(equipmentPeriodicInspectionService));
            _equipmentWebShopService = equipmentWebShopService ?? throw new ArgumentNullException(nameof(equipmentWebShopService));
            _equipmentStockService = equipmentStockService ?? throw new ArgumentNullException(nameof(equipmentStockService));
        }

        #region EQUPMENTS
        /// <summary>
        /// Get Equipments list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery]string filters)
        {
            List<SelectedFilterViewModel> resultFilter = !string.IsNullOrWhiteSpace(filters) ? JsonConvert.DeserializeObject<List<SelectedFilterViewModel>>(filters) : new List<SelectedFilterViewModel>();
            BaseListDto dto = await _equipmentService.GetPagedList(resultFilter.TransferToDtoModel());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get Equipments list
        /// </summary>
        /// <returns></returns>
        [HttpGet("groups")]
        public async Task<IActionResult> GetListGroupsByFolder([FromQuery]string filters)
        {
            List<SelectedFilterViewModel> resultFilter = !string.IsNullOrWhiteSpace(filters) ? JsonConvert.DeserializeObject<List<SelectedFilterViewModel>>(filters) : new List<SelectedFilterViewModel>();
            BaseListDto dto = await _equipmentService.GetEquipmentGroupsByFolder(resultFilter.TransferToDtoModel());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get Equipment by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            EquipmentDto dto = await _equipmentService.GetById(id);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Search Equipmentss
        /// </summary>
        /// <param name="search_text"></param>
        /// <returns></returns>
        [HttpGet("search/{search_text}")] // ?search={search_string}
        public async Task<IActionResult> SearchTag([FromRoute]string search_text)
        {
            List<EquipmentDto> dtos = await _equipmentService.Search(search_text);
            return Ok(dtos.TransferToViewModel());
        }

        /// <summary>
        /// Get Equipment Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetEquipmentDetails(int id)
        {
            List<ModuleDetailDto> dtos = await _equipmentService.GetDetails(id);
            return Ok(dtos?.TransferToModuleDetailViewModel());
        }


        /// <summary>
        /// Create Equipment
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(EquipmentViewModel model)
        {
            EquipmentDto dto = await _equipmentService.Create(model.TransferToDto());
            return Ok(dto?.TransferToViewModel());
        }

        /// <summary>
        /// Update Equipment
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> Update(EquipmentViewModel model)
        {
            EquipmentDto dto = await _equipmentService.Update(model.TransferToDto());
            return Ok(dto?.TransferToViewModel());
        }

        /// <summary>
        /// Delete Equipment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _equipmentService.Delete(id);
            return Ok(result);
        }

        /// <summary>
        /// Set folder for Equipment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="folderId"></param>
        /// <returns></returns>
        [HttpPost("{id}/set_folder/{folderId}")]
        public async Task<IActionResult> SetFolder(int id, int folderId)
        {
            await _equipmentService.SetFolderId(id, folderId);
            return Ok();
        }

        /// <summary>
        /// Export Filtered Equipments
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost("export")]
        public async Task<IActionResult> ExportEquipments([FromForm] IFormCollection form)
        {
            List<SelectedFilter> filters = new List<SelectedFilter>();
            if (form.TryGetValue("filters", out StringValues formFilters))
            {
                string _filters = formFilters.ToString();
                filters = (JsonConvert.DeserializeObject<List<SelectedFilterViewModel>>(_filters)).TransferToDtoModel();
            }

            string[] columns = new string[0];
            if (form.TryGetValue("columns", out StringValues formColumns))
            {
                string _columns = formColumns.ToString();
                columns = JsonConvert.DeserializeObject<string[]>(_columns);
            }

            string path = await _equipmentService.Export(columns, filters);

            if (!string.IsNullOrWhiteSpace(path))
            {
                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, FileExtention.GetMimeType(path), Path.GetFileName(path));
            }
            else
            {
                return BadRequest();
            }
        }

        #endregion

    }
}