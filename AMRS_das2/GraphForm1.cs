using System;
using System.Windows.Forms;
using Microsoft.Glee.Drawing;

namespace AMRS_das2
{
    public partial class GraphForm1 : Form
    {
        Graph graph;


        public GraphForm1(Graph graph):this()
        {
            this.graph = graph;
            gViewer.Graph = graph;

        }

        
        public GraphForm1()
        {
            InitializeComponent();
        }

        internal int getNodeCount()
        {
           return  graph == null ? -1 : graph.NodeCount;
        }

        internal int EdgeCount()
        {
            return graph == null ? -1 : graph.EdgeCount;
        }

        internal int getInequationsNumber()
        {
            if (graph == null) return -1;
            var nbineq = 0;
            foreach (Edge edge in graph.Edges)
            {
                if (edge.SourceNode.Id == "0") continue;
                var endNode = edge.TargetNode;

                foreach (Edge outer in endNode.OutEdges)
                {
                    nbineq++;
                }
            }
            return nbineq;
        }
    }
}
