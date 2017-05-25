using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.Models.DataContracts
{
    public class SavePermissionRequest
    {
        public string FunctionId { set; get; }

        public IList<PermissionViewModel> Permissions { get; set; }
    }
}