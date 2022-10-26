using Microsoft.Glee.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AMRS_das2
{
    public static class nonLinearSolver
    {
        static MLApp.MLApp matlab = new MLApp.MLApp();
        private static string[] freeVarsTabNames;
        private static double[] freeVarsDistances;

        public static List<PointF> solveSytsem(Graph uMdgGraph)
        {
            object resultt = null;

            //MLApp.MLApp matlab = new MLApp.MLApp();

            string cmd1 = generateNonLinearEquations(uMdgGraph);

            // generate inequalities distance 
            string cmd3 = generateNonLinearConstraints(uMdgGraph);


            string cmd2 = "X0 = rand(1," + (2 * uMdgGraph.NodeCount) + "); [x, fval] = fsolve(f, X0)";
            

            matlab.Execute(cmd1);
            matlab.Execute(cmd2);

            matlab.GetWorkspaceData("x", "base", out resultt);
            
            var res = (double[,])resultt;
            if (res == null) return null;
            var listpts = new List<PointF>(res.GetLength(1));
            for (int i = 0; i < res.GetLength(1); i += 2)
            {
                listpts.Add(new PointF((float)res[0, i], (float)res[0, i + 1]));
            }

            return listpts;

        }


        public static List<Node> getFreeVars(Graph uMdgGraph)
        {
            List<Node> freevars =  new List<Node>();
            for (int i = 0; i < uMdgGraph.NodeMap.Count; i++)
            {
                var node = (Node)uMdgGraph.NodeMap[""+i];
                if (node.Attr.Label == "0") continue;
                int result = 0;
                using (IEnumerator<Edge> enumerator = node.InEdges.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                        result++;
                }
                //if (result == 0) throw new Exception("node without inner edges !!!");
                if (result == 1) freevars.Add(node);
            }
            
            
            return freevars;

        }

         private static string generateNonLinearConstraints(Graph uMdgGraph)
        {
            string cmd = "g = [";
            foreach (Edge edge in uMdgGraph.Edges)
            {
                if (edge.SourceNode.Id == "0") continue;
                var endNode = edge.TargetNode;
                string ed = edge.Attr.Label;
                int i = int.Parse(edge.SourceNode.Id);

                foreach (Edge outer in endNode.OutEdges)
                {
                    int j = int.Parse(outer.Target);
                    cmd += "" + ed + " - (x(" + (2 * i + 1) + ")-x(" + (2 * j + 1) + "))^2 - (x(" + (2 * i + 2) + ")-x(" + (2 * j + 2) + "))^2 ;\n";
                }
            }
            if (cmd[cmd.Length - 1] == ';') cmd = cmd.Remove(cmd.Length - 1);
            cmd += "];";

            return cmd;
        }
       private static string generateNonLinearEquations(Graph uMdgGraph)
        {
            string cmd = "f = @(x) [";
            foreach (Edge edge in uMdgGraph.Edges)
            {
                int i = int.Parse(edge.SourceNode.Id);
                int j = int.Parse(edge.TargetNode.Id);

                string ss = edge.Attr.Label;
                cmd += "(x(" + (2 * i + 1) + ")-x(" + (2 * j + 1) + "))^2 + (x(" + (2 * i + 2) + ")-x(" + (2 * j + 2) + "))^2 - " + ss + ";\n";

            }
            if (cmd[cmd.Length - 1] == ';') cmd = cmd.Remove(cmd.Length - 1);
            cmd += "];";

            return cmd;
        }

        public static List<PointF> getAndPlotSolutions(out double objectiveValue)
        {
            string cmd = @"cd 'E:\Recherche\cancelable bio\optimization using matlab\PSO like'";
            matlab.Execute(cmd);
            cmd = "PSOLiveWithParams";
            matlab.Execute(cmd);

            object resultt, O_value;
            matlab.GetWorkspaceData("solution", "base", out resultt);
            matlab.GetWorkspaceData("objectiveValue", "base", out O_value);
            objectiveValue = (double) O_value;

            var res = (double[,])resultt;
            if (res == null) return null;
            var listpts = new List<PointF>(res.GetLength(1));
            for (int i = 0; i < res.GetLength(1); i += 2)
            {
                listpts.Add(new PointF((float)res[0, i], (float)res[0, i + 1]));
            }

            return listpts;

        }

        public static List<PointF> launchOptimizer(Graph uMdgGraph, out double objectiveValue)
        {
            string cmd = @"cd 'E:\Recherche\cancelable bio\optimization using matlab\PSO like'";
            matlab.Execute(cmd);

            matlab.PutWorkspaceData("nbVars", "base", uMdgGraph.NodeCount*2);
            //matlab.PutWorkspaceData("x0", "base", cs);

            cmd = "PSOLiveWithParams";
            matlab.Execute(cmd);

            object resultt, O_value;
            matlab.GetWorkspaceData("solution", "base", out resultt);
            matlab.GetWorkspaceData("objectiveValue", "base", out O_value);
            objectiveValue = (double)O_value;


            var res = (double[,])resultt;
            if (res == null) return null;
            var listpts = new List<PointF>(res.GetLength(1));
            //float minx = float.MaxValue, miny = float.MaxValue;
            for (int i = 0; i < res.GetLength(1); i += 2)
            {
                //if (minx > (float)res[0, i]) minx = (float)res[0, i];
                //if (miny > (float)res[0, i + 1]) miny = (float)res[0, i + 1];
                listpts.Add(new PointF((float)res[0, i], (float)res[0, i + 1]));
            }

            return listpts;

        }

        public static List<PointF> BruteForceAttack(Graph uMdgGraph)
        {
            object resultt = null;

            var freeVarsList = getFreeVars(uMdgGraph);
            setFreeVarsNamesAndDistances(freeVarsList);

            doBruteForceAttack(0, freeVarsList.Count, freeVarsTabNames, freeVarsDistances);
            string cmd1 = generateNonLinearEquations(uMdgGraph);

            // generate inequalities distance 
            string cmd3 = generateNonLinearConstraints(uMdgGraph);



            string cmd2 = "X0 = rand(1," + (2 * uMdgGraph.NodeCount) + "); [x, fval] = fsolve(f, X0)";


            matlab.Execute(cmd1);
            matlab.Execute(cmd2);

            matlab.GetWorkspaceData("x", "base", out resultt);

            var res = (double[,])resultt;
            if (res == null) return null;
            var listpts = new List<PointF>(res.GetLength(1));
            for (int i = 0; i < res.GetLength(1); i += 2)
            {
                listpts.Add(new PointF((float)res[0, i], (float)res[0, i + 1]));
            }

            return listpts;

        }

        private static void doBruteForceAttack(int pass, int count, string[] freeVars, double[] freeDists)
        {
            double[] generatedValues;
            if (pass == count)
            {
                // resolve equations
            }
            else
            {
                if(pass == 0)
                {
                    generatedValues = new double[count];


                }
            }
        }

        private static void setFreeVarsNamesAndDistances(List<Node> freeVarsList)
        {
            freeVarsTabNames = new string[freeVarsList.Count];
            freeVarsDistances = new double[freeVarsList.Count];
            for (int i = 0; i < freeVarsList.Count; i++)
            {
                freeVarsTabNames[i] = freeVarsList[i].Attr.Label;
                var edge = freeVarsList[i].InEdges.GetEnumerator().Current;
                freeVarsDistances[i] = double.Parse(edge.Attr.Label);
            }
        }
    }
}
