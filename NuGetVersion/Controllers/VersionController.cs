using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NuGetLib;
using NuGetVersion.ViewModels;

namespace NuGetVersion.Controllers
{
    [Authorize]
    public class VersionController : Controller
    {
        // GET: Version -- landing page where the upload or C&P of the file will occur
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Provides API access to the version interrogation logic.
        /// </summary>
        /// <param name="id">The NuGet Package Id.</param>
        /// <param name="version">The version from which "future" results should be calcualated.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PostSingleResult(string id, string version)
        {
            var interrogator = new Interrogate();
            return Json(interrogator.GetVersions(id, version));
        }

        [HttpGet]
        public ActionResult GetSingleResult(string id, string version)
        {
            var interrogator = new Interrogate();
            return View(interrogator.GetVersions(id, version));
        }

        [HttpPost]
        public ActionResult UploadPackages(PackageUploadVM upload)
        {
            if(string.IsNullOrEmpty(upload.TextAreaText))
                throw new Exception("Need data to upload");

            var interrogator = new Interrogate();
            return View(interrogator.GetVersionsForUpload(upload.TextAreaText));
        }
    }
}