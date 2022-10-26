using FPRFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMRS_das2
{
    public class MinutiaReprNode
    {
        int minutiaID;
        int templateNum;
        List<MinutiaReprNode> equiClass;

        public MinutiaReprNode(int mntID, int templateNum)
        {
            this.minutiaID = mntID;
            this.templateNum = templateNum;
        }

        public void enqueuMinutia(MinutiaReprNode node)
        {
            if (equiClass == null) 
                equiClass = new List<MinutiaReprNode>();
            equiClass.Add(node);
        }

        public int getTemplateNum()
        {
            return templateNum;
        }

        public int getEquiClassSize() => equiClass == null ? 0 : equiClass.Count;

        internal int getReprsentative_ID()
        {
            return minutiaID;
        }

        public bool Equals(MinutiaReprNode right)
        {
            //return (this.minutia.X == right.minutia.X) && (this.minutia.Y == right.minutia.Y)
            //        && (this.minutia.Angle == right.minutia.Angle)
            //        && (this.templateNum == right.templateNum)
            //        ;
            return (minutiaID == right.minutiaID) && (templateNum == right.templateNum);
        }


        public int isInMyClass(MinutiaReprNode node)
        {
            if (equiClass == null) return -1;
            for (int i = 0; i < equiClass.Count; i++)
            {
                var repNod = equiClass[i];
                if (node.Equals(repNod))
                    return i;
            }

            return -1;
        }
    }
}
