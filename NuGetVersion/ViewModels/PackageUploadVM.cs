using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NuGetVersion.ViewModels
{
    public class PackageUploadVM
    {
        [AllowHtml]
        public string TextAreaText { get; set; }
    }
}