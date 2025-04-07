//using Prorent24.BLL.Models.Menu;
//using Prorent24.WebApp.Models.Setting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Prorent24.WebApp.Transfer.Setting
//{
//    public static class MenuTransfer
//    {
//        /// <summary>
//        /// Transfer from List<MenuDto> to List<MenuViewModel>
//        /// </summary>
//        /// <param name="menuDto"></param>
//        /// <returns></returns>
//        public static List<MenuViewModel> TransferToMenuViewModel(this List<MenuDto> menuDto)
//        {
//            List<MenuViewModel> list = menuDto.Select(x => new MenuViewModel()
//            {
//                Key = x.Key,
//                Name = x.Name,
//                Order = x.Order
//            }).ToList();

//            return list;
//        }
//    }
//}
