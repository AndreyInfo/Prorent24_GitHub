using Prorent24.Enum.Directory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Directory
{
    public class DirectoryViewModel //: IEqualityComparer<DirectoryViewModel>
    {
        public int Id { get; set; }

        public DirectoryTypeEnum Type { get; set; }

        public bool IsActive { get; set; }

        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        //public List<DirectoryLocViewModel> Locs { get; set; }



        //public bool Equals(DirectoryViewModel x, DirectoryViewModel y)
        //{
        //    if (x == null || y == null) return false;

        //    return ReferenceEquals(x, y) || (x.Id == y.Id);
        //}

        //public int GetHashCode(DirectoryViewModel obj)
        //{
        //    return this.Id.GetHashCode();
        //}
    }
}
