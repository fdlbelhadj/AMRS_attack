using Microsoft.Glee.Drawing;
using System;

namespace AMRS_das2
{
    public class MdgUnion
    {

        public float[,] umdg { get; set; }
        private byte[,] nbArcsArray;

        private Fingerprint[] fps;

        private MinutiaeReprsStruct mntRepStruct;

        public MdgUnion(Fingerprint[] fps, MinutiaeReprsStruct mntRepStruct)
        {
            this.fps = fps;
            this.mntRepStruct = mntRepStruct;
        }

        public void ReconstructUMDG(bool tolerateDistorsion)
        {
            umdg = new float[mntRepStruct.Count(), mntRepStruct.Count()];
            nbArcsArray = new byte[mntRepStruct.Count(), mntRepStruct.Count()];

            for (int i = 0; i < fps.Length; i++)
            {
                mergMdg(fps[i], tolerateDistorsion);
            }

            // merge edges between two nodes
            if (tolerateDistorsion)
            {
                for (int i = 0; i < umdg.GetLength(0); i++)
                {
                    for (int j = 0; j < umdg.GetLength(1); j++)
                    {
                        if (nbArcsArray[i, j] > 1) umdg[i, j] /= nbArcsArray[i, j];
                    }
                }
                // reduce bi-loops
                for (int i = 0; i < umdg.GetLength(0); i++)
                {
                    for (int j = i + 1; j < umdg.GetLength(1); j++)
                    {
                        if (umdg[i, j] != 0)
                        {
                            if (umdg[j, i] != 0) //    throw new Exception("i,j !=0   ====  j;i != 0  !!! ");
                            {
                                umdg[j, i] = (umdg[j, i] + umdg[i, j]) / 2.0f;
                                umdg[i, j] = 0;
                            }
                            //else umdg[j, i] = umdg[i, j];
                            //umdg[i, j] = 0;

                        }
                    }
                }
            }

            // eliminate self loops
            for (int i = 0; i < umdg.GetLength(0); i++)
            {
                umdg[i, i] = 0;
            }


           
        }

        public Graph ToGraph(Fingerprint fp) // null for all mdgs
        {
            if (umdg == null) return null;
            var graph = new Graph("DAS");
            int nbNodes = fp == null ? umdg.GetLength(0) : fp.OriginalMinutiaeList.Count + 1;//Math.Min(fps[0].OriginalMinutiaeList.Count + 1, umdg.GetLength(0));
            for (int i = 0; i < nbNodes; i++)
            {
                for (int j = 0; j < nbNodes; j++)//graphMat.GetLength(1)
                {
                    if (umdg[i, j] != 0)
                    {
                        Edge edge = graph.AddEdge("" + i, "" + umdg[i, j], "" + j);
                        edge.UserData = umdg[i, j];
                    }
                }
            }
            return graph;
        }

        private void mergMdg(Fingerprint fp, bool tolerateDistorsion)
        {
            //var prevNode = new MinutiaReprNode(-1, -1);
            int prevIndex = 0;
            int templateNum = fp.templateID;
            for (int i = 1; i < fp.transformedTemplate.Count; i++)
            {
                int mntIndex = fp.transformedTemplate[i].Item1;
                float ed = fp.transformedTemplate[i].Item2;

                var curNode = new MinutiaReprNode(mntIndex, templateNum);
                int curIndex = mntRepStruct.getRepresentative(curNode);
                if (curIndex == -1)
                    throw new Exception("a minutiae found with no representative !! :( ");

                ////umdg[prevIndex, curIndex] += ed;
                //if (umdg[prevIndex, curIndex] == 0)
                //{
                //    umdg[prevIndex, curIndex] = ed;

                //    nbArcsArray[prevIndex, curIndex]++;
                //}

                if (umdg[prevIndex, curIndex] + umdg[curIndex, prevIndex] == 0)
                {
                    umdg[prevIndex, curIndex] = ed;
                    nbArcsArray[prevIndex, curIndex]++;
                }
                else
                {
                    if (tolerateDistorsion)
                    {
                        umdg[prevIndex, curIndex] += ed;
                        nbArcsArray[prevIndex, curIndex]++;
                    }
                }

                prevIndex = curIndex;
            }
        }


    }
}
