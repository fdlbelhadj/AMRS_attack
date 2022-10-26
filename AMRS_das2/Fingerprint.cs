using FPRFramework.Core;
using FPRFramework.FeatureExtractors;
using FPRFramework.FeatureRepresentation;
using FPRFramework.ResourceProviders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMRS_das2
{

    public class Fingerprint
    {

        public int templateID { get; }

        private bool loadfromfile;
        string fpPath;
        public MtripletsFeature features;
        public List<Minutia> OriginalMinutiaeList { get => features == null ? null : features.Minutiae; }
        public List<Tuple<int,float>> transformedTemplate { get => Mdg; set => Mdg = value; }

        List<Tuple<int, float>> Mdg;
        public Minutia corePoint;

        private Hashtable minutiaeHash;
        public Fingerprint(string path, int tempId, bool loadfromfile)
        {
            this.templateID = tempId;
            this.loadfromfile = loadfromfile;
            this.fpPath = path.Replace("#",""+tempId);
            getOriginalFeatures();
            hashAlllMinutiae();   
            transformedTemplate = getTransformedTemplate();
        }

        private Minutia getCorePoint()
        {
            string str = Path.GetFileNameWithoutExtension(fpPath);
            string dir = Path.GetDirectoryName(fpPath);
            using (TextReader textReader = new StreamReader(dir + "\\All_CorePointsList.txt"))
            {
                var s = textReader.ReadLine();
                while (!string.IsNullOrEmpty(s))
                {
                    string[] ss = s.Split(new char[] { '\t' });
                    if (ss[0] == str) return new Minutia(short.Parse(ss[1]), short.Parse(ss[2]), 0);
                    s = textReader.ReadLine();

                }
            }
            return null;
        }

        internal Image LoadBitmap()
        {
            return Bitmap.FromFile(fpPath);
        }

        private void getOriginalFeatures()
        {
            if (!loadfromfile)
            {
                string resourcePath = Path.GetDirectoryName(fpPath);
                if (!Directory.Exists(resourcePath))
                {
                    throw new Exception("Unable to locate db path: Invalid resource path!");
                }

                var repository = new ResourceRepository(resourcePath);

                string shortFileName = Path.GetFileNameWithoutExtension(fpPath);
                try
                {
                    features = GetResource(shortFileName, repository);
                    corePoint = getCorePoint();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("An error ocurred while loading features with message: " + exc.Message,
                                    "Feature Loading Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } 
            }
            else
            {
                List<Minutia> MinutiaeList = loadResources();
                var mTripletsCalculator = new MTripletsExtractor();
                features = mTripletsCalculator.ExtractFeatures(MinutiaeList);

            }
        }

        private List<Minutia> loadResources()
        {
            List<Minutia> list = new List<Minutia>();
            string str = Path.GetFileNameWithoutExtension(fpPath);
            string dir = Path.GetDirectoryName(fpPath);
            using (TextReader textReader = new StreamReader(dir + "\\"+ str + ".txt"))
            {
                var cpx = textReader.ReadLine();
                var cpy = textReader.ReadLine();
                corePoint = new Minutia(short.Parse(cpx), short.Parse(cpy),0);
                textReader.ReadLine();
                int nbmnt = int.Parse(textReader.ReadLine());

                for (int i = 0; i < nbmnt; i++)
                {
                    var s = textReader.ReadLine();
                    string[] ss = s.Split(new char[] { ' ' });
                    try
                    {
                        list.Add(new Minutia(short.Parse(ss[0]), short.Parse(ss[1]), double.Parse(ss[2])));

                    }
                    catch (Exception)
                    {

                        list.Add(new Minutia(short.Parse(ss[0]), short.Parse(ss[1]), double.Parse(ss[2].Replace('.',','))));
                    }
                }
                
            }
            return list;
        }

        private void hashAlllMinutiae()
        {
            if (OriginalMinutiaeList == null) return;
            minutiaeHash = new Hashtable();
            for (int i= 0; i< OriginalMinutiaeList.Count; i++)
                minutiaeHash.Add(OriginalMinutiaeList[i].GetHashCode(), i);
        }


        private List<Tuple<int, float>> getTransformedTemplate()
        {
            if (this.OriginalMinutiaeList == null) return null;
            var list = new List<Minutia>(OriginalMinutiaeList);

            List<Tuple<int, float>> mdg = new List<Tuple<int, float>>();

            Minutia currentMnt = corePoint;
            mdg.Add(new Tuple<int, float>(-1,0));// -1 = index of the corepoint
            float ed = 0;
            int idex = FindNearest(list, currentMnt, ref ed);//TO_CORE_POINT;
            while (idex != -1)
            {
                var mnt = list[idex];
                mdg.Add(new Tuple<int, float>(getMinutiaIndex(mnt.GetHashCode()), ed));
                currentMnt = mnt;
                list.RemoveAt(idex);
                idex = FindNearest(list, currentMnt, ref ed);//TO_CORE_POINT;

            }
            return mdg;
        }

        private int FindNearest(List<Minutia> list, Minutia currentMnt, ref float ed)
        {
            int idex = -1;
            ed = float.MaxValue;
            for (int i = 0; i < list.Count; i++)
            {
                Minutia mnt = list[i];
                float edd = (currentMnt.X - mnt.X) * (currentMnt.X - mnt.X) + (currentMnt.Y - mnt.Y) * (currentMnt.Y - mnt.Y);
                if (edd < ed)
                {
                    ed = edd;
                    idex = i;
                }
            }
            return idex;
        }
        private MtripletsFeature GetResource(string shortFileName, ResourceRepository repository)
        {

            string resourceName = string.Format("{0}.Delaunay(Ratha1995MinutiaeExtractorFingerprint).mtp", shortFileName);
            if (repository.ResourceExists(resourceName))
                return repository.RetrieveObjectResource(resourceName) as MtripletsFeature;
            return null;

        }

        public int getMinutiaIndex(int hashCode)
        {
            return (int)minutiaeHash[hashCode];
        }

        public void DrawTransformedTemplate(Graphics g, Color col, bool drawLine = true)
        {
            if (transformedTemplate == null) return;

            g.Clear(Color.White);
            var prec = corePoint;
            helperClass.DrawPoint(g, new PointF(prec.X, prec.Y), Color.Aqua);

            int i = 1;
            while (i < transformedTemplate.Count)
            {
                var nd = transformedTemplate[i];
                helperClass.DrawPoint(g, new PointF( OriginalMinutiaeList[nd.Item1].X, OriginalMinutiaeList[nd.Item1].Y), col);
                if(drawLine)
                    helperClass.DrawLine(g, new PointF( prec.X, prec.Y), new PointF(OriginalMinutiaeList[nd.Item1].X, OriginalMinutiaeList[nd.Item1].Y), Color.Blue);

                prec = OriginalMinutiaeList[nd.Item1];
                i++;
            }

        }

     
    }
}
