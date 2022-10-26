using FPRFramework.Core;
using System.Collections.Generic;

namespace AMRS_das2
{
    public class MinutiaeReprsStruct
    {
        List<MinutiaReprNode> minutiaeReprStruct;
        public MinutiaeReprsStruct()
        {
        }

        public List<MinutiaReprNode> CreateMinutiaeRepresentativeStructure(Fingerprint[] fps, bool oneFp = false)
        {
            if (fps == null) return null;
            minutiaeReprStruct = new List<MinutiaReprNode>();
            int nbFps = oneFp? 1: fps.Length;
            
            // insert corepoints
            minutiaeReprStruct.Add(new MinutiaReprNode(-1, 1));
            for (int i = 1; i < fps.Length; i++) minutiaeReprStruct[0].enqueuMinutia(new MinutiaReprNode(-1,fps[i].templateID));

            for (int i = 0; i < nbFps ; i++)
            {
                Fingerprint tFp = fps[i];

                for (int j = i + 1; j < fps.Length; j++)
                {
                    Fingerprint qFp = fps[j];

                    Matcher.Match(qFp, tFp, out List<MinutiaPair> pairedList);
                    foreach (MinutiaPair minutiaPair in pairedList)
                    {
                        MinutiaReprNode m_repT = new MinutiaReprNode(tFp.getMinutiaIndex( minutiaPair.TemplateMtia.GetHashCode()), tFp.templateID);
                        MinutiaReprNode m_repQ = new MinutiaReprNode(qFp.getMinutiaIndex( minutiaPair.QueryMtia.GetHashCode()), qFp.templateID);

                        int index = getRepresentative( m_repT);
                        if (index == -1) // no represnetative for this minutia
                        {
                            minutiaeReprStruct.Add(m_repT);
                            index = minutiaeReprStruct.Count - 1;
                        }

                        if (minutiaeReprStruct[index].isInMyClass(m_repQ) == -1)
                            minutiaeReprStruct[index].enqueuMinutia(m_repQ);
                    }
                }


                for (int k = 0; k < tFp.OriginalMinutiaeList.Count; k++)
                {
                    var node = new MinutiaReprNode(k, tFp.templateID);
                    if (getRepresentative(node) == -1) minutiaeReprStruct.Add(node);
                }
            }

            return minutiaeReprStruct;
        }

        internal int Count()
        {
            return minutiaeReprStruct == null ? -1 : minutiaeReprStruct.Count;
        }

        public MinutiaReprNode this[int i]
        {
            get => minutiaeReprStruct == null? null: minutiaeReprStruct[i];
        }

        public int getRepresentative( MinutiaReprNode nodeRech)
        {
            for (int i = 0; i < minutiaeReprStruct.Count; i++)
            {
                var node = minutiaeReprStruct[i];
                if (nodeRech.Equals(node))
                    return i;
                if (node.isInMyClass(nodeRech) != -1) return i;
            }

            return -1;
        }
    }
}
