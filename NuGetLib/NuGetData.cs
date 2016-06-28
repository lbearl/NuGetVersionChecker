using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGetLib
{
    public class NuGetData
    {
        public string PackageId { get; set; }

        public DateTimeOffset? PublishedDate { get; set; }

        public string PackageSummary { get; set; }

        public Uri ProjectUrl { get; set; }
    }
}
