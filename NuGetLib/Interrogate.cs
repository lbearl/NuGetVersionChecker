using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using NuGet;

namespace NuGetLib
{
    public class Interrogate
    {
        public IEnumerable<NuGetData> GetVersions(string id, string version)
        {
            if(string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));
            if(string.IsNullOrEmpty(version))
                throw new ArgumentNullException(nameof(version));

            var repo =
                PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");

            //Get the list of all NuGet packages with ID 'EntityFramework'       
            var packages = repo.FindPackagesById(id).ToList();

            var semVer = new SemanticVersion(version);

            //Filter the list of packages that are not Release (Stable) versions
            packages = packages.Where(item => item.Version >= semVer && item.IsReleaseVersion()).ToList();

            foreach (var p in packages)
            {
                yield return
                    new NuGetData
                    {
                        PackageId = p.GetFullName(),
                        PackageSummary = p.Summary,
                        PublishedDate = p.Published,
                        ProjectUrl = p.ProjectUrl
                    };
            }
        }

        public Dictionary<string, IEnumerable<NuGetData>> GetVersionsForUpload(string xmlString)
        {
            var xmlDoc = new XmlDocument();
            var retDict = new Dictionary<string, IEnumerable<NuGetData>>();
            xmlDoc.LoadXml(xmlString);
            var packagesNode = xmlDoc.GetElementsByTagName("packages");
            foreach (var x in packagesNode.Item(0))
            {
                var node = x as XmlNode;
                if (node == null) continue;
                var y = node;
                var id = y.Attributes?["id"]?.Value;
                var version = y.Attributes?["version"]?.Value;
                if (id != null && version != null)
                {
                    var data = GetVersions(id, version);
                    retDict.Add(id, data);
                }
            }
            return retDict;
        }
    }
}
