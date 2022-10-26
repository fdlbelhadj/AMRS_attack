using FPRFramework.Core;
using FPRFramework.Matchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMRS_das2
{
    public static class Matcher
    {
        public static double Match(Fingerprint tFp, Fingerprint qFP, out List<MinutiaPair> matchingMtiae)
        {
            // Matching features
            matchingMtiae = null;
            double score = 0;

            IMinutiaMatcher minutiaMatcher = new MPN();
            if (minutiaMatcher != null)
            {
                score = minutiaMatcher.Match(tFp.features, qFP.features, out matchingMtiae);
            }
            return score;
        }
    }
}
