using Prorent24.Common.Models.Columns;
using Prorent24.Entity.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.General
{
    public static class UserColumnTransfer
    {
        public static List<UserColumn> TransferToList(this List<UserColumnEntity> columns)
        {
            List<UserColumn> userColumns = columns.Select(x => new UserColumn()
            {
                Column = x.Column,
                Order = x.Order,
                ShowColumn = x.ShowColumn,
                WidthColumn = x.WidthColumn
                
            }).ToList();

            return userColumns;
        }
    }
}
