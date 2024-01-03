using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Service.Helpers
{
    public class Helper
    {
        public static void RemoveFile(string webRootPath, string folder, string filename)
        {
            File.Delete(Path.Combine(webRootPath, folder, filename));
        }
    }
}
