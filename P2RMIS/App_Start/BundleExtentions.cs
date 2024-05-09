using System;
using System.Collections.Generic;
using System.Web.Optimization;

namespace Sra.P2rmis.Web
{
    static class BundleExtentions
    {
        /// <summary>
        /// Bypasses the default ordering mechanism for explicit ordering
        /// </summary>
        /// <param name="bundle"></param>
        /// <returns></returns>
        public static Bundle NonOrdering(this Bundle bundle)
        {
            bundle.Orderer = new NonOrderingBundleOrderer();
            return bundle;
        }
    }
    class NonOrderingBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}