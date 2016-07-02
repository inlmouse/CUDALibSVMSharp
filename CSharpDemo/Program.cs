using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using CPULibSvm;
using GPULibSvm;

namespace CSharpDemo
{
    class Program
    {
        static CPULibSvm.Model CPUClassifierModel;
        static GPULibSvm.Model GPUClassifierModel;
        static string SvmModelPath = @"C:\Research\LibSVMsharp-master\LibSVMsharp.Examples.Classification\bin\Debug\Dataset\TrainZ.txt";
        static void Main(string[] args)
        {
            
            GPUClassifierModel=GPULoadClassifier();
            
            //sw.Reset();
            //sw.Start();
            //CPUClassifierModel = CPULoadClassifier();
            //sw.Stop();
            //Console.WriteLine("CPU: "+sw.ElapsedMilliseconds.ToString());
            //Random rd = new Random();
            //double[] feature=new double[2622];
            //for (int i = 0; i < feature.Length; i++)
            //{
            //    feature[i] = Convert.ToDouble(rd.Next(0, 100)*1.0/100);
            //}
            //Console.WriteLine(Svm.Predict(ClassifierModel, TestSVMFormat(feature)));
            Console.WriteLine("!!!!");
            Console.ReadLine();
        }

        /// <summary>
        /// 加载分类器（CPU版，GPU版本测试中）   
        /// 如果人脸库没有变更，直接加载即可
        /// </summary>
        /// <returns>加载好的分类器</returns>
        static public GPULibSvm.Model GPULoadClassifier()
        {
            if (File.Exists(SvmModelPath))
            {
                GPULibSvm.Problem prob = GPULibSvm.Svm.ReadProblem(SvmModelPath);
                Stopwatch sw = new Stopwatch();
                sw.Start();
                GPULibSvm.Svm.CrossValidation(prob, GCreateParamCHelper(128), 0);
                sw.Stop();
                Console.WriteLine("GPU: " + sw.ElapsedMilliseconds.ToString());
                return GPULibSvm.Svm.Train(prob, GCreateParamCHelper(128));
            }
            else
            {
                //TODO:分类器未训练
                //下面的代码应该做异常判断
                GPULibSvm.Problem prob = GPULibSvm.Svm.ReadProblem(SvmModelPath);//fake code
                return GPULibSvm.Svm.Train(prob, GCreateParamCHelper(128));//fake code
            }
        }

        static public CPULibSvm.Model CPULoadClassifier()
        {
            if (File.Exists(SvmModelPath))
            {
                CPULibSvm.Problem prob = CPULibSvm.Svm.ReadProblem(SvmModelPath);
                return CPULibSvm.Svm.Train(prob, CCreateParamCHelper(128));
            }
            else
            {
                //TODO:分类器未训练
                //下面的代码应该做异常判断
                CPULibSvm.Problem prob = CPULibSvm.Svm.ReadProblem(SvmModelPath);//fake code
                return CPULibSvm.Svm.Train(prob, CCreateParamCHelper(128));//fake code
            }
        }

        #region 私有辅助方法 具体实现不重要
        private static GPULibSvm.Parameter GCreateParamCHelper(double c)
        {
            return new GPULibSvm.Parameter
            {
                SvmType = GPULibSvm.SvmType.C_SVC,
                KernelType = GPULibSvm.KernelType.RBF,                  //4 means precomputed kernel, see https://github.com/encog/libsvm-java
                Degree = 0,                       //polynom kernel degree - not used
                C = c,                            //C
                Gamma = 0.078125,                        //RBF gamma - not used
                Coef0 = 0,                        //polynom exponent - not used
                Nu = 0.0,                         //regression parameter - not used
                CacheSize = 100,                  //libsvm parameter
                Eps = 1e-3,                       //training parameter
                p = 0.1,                          //training parameter
                Shrinking = 1,                    //training optimization
                Probability = false ? 1 : 0,      //output
                WeightLabel = new int[0],         //label weightings - not used
                Weight = new double[0],
            };
        }

        private static CPULibSvm.Parameter CCreateParamCHelper(double c)
        {
            return new CPULibSvm.Parameter
            {
                SvmType = CPULibSvm.SvmType.C_SVC,
                KernelType = CPULibSvm.KernelType.RBF,                  //4 means precomputed kernel, see https://github.com/encog/libsvm-java
                Degree = 0,                       //polynom kernel degree - not used
                C = c,                            //C
                Gamma = 0.078125,                        //RBF gamma - not used
                Coef0 = 0,                        //polynom exponent - not used
                Nu = 0.0,                         //regression parameter - not used
                CacheSize = 100,                  //libsvm parameter
                Eps = 1e-3,                       //training parameter
                p = 0.1,                          //training parameter
                Shrinking = 1,                    //training optimization
                Probability = false ? 1 : 0,      //output
                WeightLabel = new int[0],         //label weightings - not used
                Weight = new double[0],
            };
        }

        static private GPULibSvm.Node[] TestSVMFormat(double[] feature)
        {
            GPULibSvm.Node[] x = new GPULibSvm.Node[feature.Length];
            for (int i = 0; i < feature.Length; i++)
            {
                x[i].Index = i + 1;
                //x[i].Value = feature[i];
            }
            return x;
        }

        public static void Write(string path, string label)
        {
            FileStream fs = new FileStream(path, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(label);
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        static private void GetLibSVMFormat(int label, float[] feature)
        {
            string featurestring = label.ToString() + " ";
            for (int i = 0; i < feature.Length; i++)
            {
                featurestring += (i + 1).ToString() + ":" + feature[i].ToString() + " ";
            }
            Write(SvmModelPath, featurestring);
        }
        #endregion
    }
}
