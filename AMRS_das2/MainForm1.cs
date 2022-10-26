using FPRFramework.Core;
using Microsoft.Glee.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Color = System.Drawing.Color;

namespace AMRS_das2
{
    public partial class MainForm1 : Form
    {

        private string resourcePath;
        private Fingerprint[] fps;
        private MinutiaeReprsStruct mntRepStruct;
        private MdgUnion mdgUnion;
        private Graph UMDGgraph;

        public MainForm1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            resourcePath = pathTxtBox.Text;
            if (!Directory.Exists(resourcePath))
            {
                MessageBox.Show("Unable to locate db path: Invalid resource path!", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ProcessFinger(resourcePath, loadFromFilChk.Checked, (int)fingerNb.Value);


            pictureBox1.Image = fps[0].LoadBitmap();
            pictureBox2.Image = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);
            fps[0].DrawTransformedTemplate(Graphics.FromImage(pictureBox2.Image), Color.Blue, false);
        }

       
        private Fingerprint[] getFingerpints(string resourcePath, int nB_PRINTS, bool loadfromfile, int fingerToLoad)
        {
            
            fps = new Fingerprint[nB_PRINTS];
            var str = resourcePath + "\\1" + string.Format("{0:00}", fingerToLoad) + "_#.tif";
            for (int i = 1; i <= nB_PRINTS; i++) fps[i-1] = new Fingerprint(str,i,loadfromfile);
            
            return fps;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mdgUnion = new MdgUnion(fps, mntRepStruct);
            mdgUnion.ReconstructUMDG(distorsionChk.Checked);
            Fingerprint fp = null;
            if (whichGraph.Checked) fp = fps[0];
            UMDGgraph = mdgUnion.ToGraph(fp);
            GraphForm1 graphForm = new GraphForm1(UMDGgraph);
            if(showGraphChk.Checked) graphForm.Show();
            int nbineqs = graphForm.getInequationsNumber();
            label3.Text =   "nodes = " + graphForm.getNodeCount() + 
                            "\t----- Edges = " + graphForm.EdgeCount() + 
                            "\t----- inequations = " + nbineqs + 
                            "\t----- freevars " + nonLinearSolver.getFreeVars(UMDGgraph).Count;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (UMDGgraph == null) throw new ArgumentNullException("UMDGgraph", "UMDG graph is null\n Reconstruct it First !");
            List<PointF> list = nonLinearSolver.solveSytsem(UMDGgraph);
            ProcessSolsList(list);
        }

        private void ProcessSolsList(List<PointF> list)
        {
            //helperClass.translatePoints(list, new PointF(fps[0].corePoint.X - list[0].X, fps[0].corePoint.Y - list[0].Y));
            list = helperClass.orderList(list.GetRange(0, fps[0].OriginalMinutiaeList.Count+1));

            Graphics.FromImage(pictureBox2.Image).DrawImage(pictureBox1.Image, new PointF(0,0));
            
            fps[0].DrawTransformedTemplate(Graphics.FromImage(pictureBox2.Image), Color.Blue, false);

            helperClass.DrawPointsList(Graphics.FromImage(pictureBox2.Image), list, Color.Red, Color.Red, false);

            pictureBox2.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double O_value;
            var list = nonLinearSolver.getAndPlotSolutions(out O_value);
            ProcessSolsList(list );

            label3.Text = "\t------ " + O_value;
        }

        private void ProcessFinger(string resPath, bool @checked, int value)
        {
            int NB_PRINTS = int.Parse(nbprintsTxt.Text);
            fps = getFingerpints(resPath, NB_PRINTS , @checked, value);
            mntRepStruct = new MinutiaeReprsStruct();
            mntRepStruct.CreateMinutiaeRepresentativeStructure(fps); // , whichGraph.Checked
        }


        private void button5_Click(object sender, EventArgs e)
        {
            using (var sw = new StreamWriter(resourcePath + "\\allStats.txt"))
            {
                sw.WriteLine("Finger\tnodes\tEdges\tinequations");
                for (int i = 1; i <= 10; i++)
                {
                    ProcessFinger(resourcePath, false, i);
                    UMDGgraph = mdgUnion.ToGraph(fps[0]);
                    GraphForm1 graphForm = new GraphForm1(UMDGgraph);

                    int nbineqs = graphForm.getInequationsNumber();
                    sw.WriteLine( graphForm.getNodeCount() + "\t" + graphForm.EdgeCount() + "\t" + nbineqs);

                }
                sw.Flush();
                sw.Close();
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //var cs = getFp0XY(1);
            double O_value = -1.0;
            var list = nonLinearSolver.launchOptimizer(UMDGgraph, out O_value);
            ProcessSolsList(list);

            label3.Text += "\t------ " + O_value;



        }

        private double[] getFp0XY(int tempNum = -1)
        {
            
            var coors = new double[tempNum == -1? 2*mntRepStruct.Count():2*fps[tempNum-1].OriginalMinutiaeList.Count+2];
            Minutia mnt;
            for (int i = 0; i < coors.Length/2; i++)
            {
                var tt = mntRepStruct[0].getTemplateNum();
                bool ok = true;
                if ((tempNum != -1)&&(tempNum != tt)) ok = false;
                if (ok) 
                {
                    if (i == 0) mnt = fps[mntRepStruct[0].getTemplateNum()-1].corePoint;
                    else mnt = fps[mntRepStruct[i].getTemplateNum() - 1].OriginalMinutiaeList[mntRepStruct[i].getReprsentative_ID()];
                    coors[2 * i] = mnt.X;
                    coors[2 * i + 1] = mnt.Y; 
                }
            }
            return coors;
        }

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if(sfd.ShowDialog() == DialogResult.OK)
                helperClass.savePicture((sender as PictureBox).Image, sfd.FileName);
        }
    }
}
