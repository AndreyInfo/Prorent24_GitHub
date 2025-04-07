using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Configuration.AccountConfiguration
{
    public class FunctionPermissionDto
    {
        public bool Add { get; set; } = false;
        public bool Edit { get; set; } = false;
        public bool Delete { get; set; } = false;
        public bool View { get; set; } = false;

        //public bool ViewOther { get; set; } = false;
        //public bool EditOther { get; set; } = false;
        //public bool DeleteOther { get; set; } = false;

        public void OnlyView() {
            this.View = true;
        }
        public void ViewEdit() {
            this.View = true;
            this.Edit = true;
        }
        public void ViewEditDelete() {
            this.Add = true;
            this.View = true;
            this.Edit = true;
            this.Delete = true;
        }
    }
}
