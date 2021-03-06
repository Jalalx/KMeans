﻿using System;
using System.IO;

namespace kmeans
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nBegin k-means clustering demo\n");

            double[][] rawData = new double[10][];
            rawData[0] = new double[] { 73, 72.6 };
            rawData[1] = new double[] { 61, 54.4 };
            rawData[2] = new double[] { 67, 99.9 };
            rawData[3] = new double[] { 68, 97.3 };
            rawData[4] = new double[] { 62, 59.0 };
            rawData[5] = new double[] { 75, 81.6 };
            rawData[6] = new double[] { 74, 77.1 };
            rawData[7] = new double[] { 66, 97.3 };
            rawData[8] = new double[] { 68, 93.3 };
            rawData[9] = new double[] { 61, 59.0 };
            
            //double[][] rawData = LoadData("..\\..\\HeightWeight.txt", 10, 2, ',');
            
            Console.WriteLine("Raw unclustered height (in.) weight (kg.) data:\n");
            Console.WriteLine(" ID Height Weight");
            Console.WriteLine("---------------------");
            
            ShowData(rawData, 1, true, true);
            
            int numClusters = 3;
            Console.WriteLine("\nSetting numClusters to " + numClusters);
            Console.WriteLine("Starting clustering using k-means algorithm");
            
            Clusterer c = new Clusterer(numClusters);
            
            int[] clustering = c.Cluster(rawData);
            Console.WriteLine("Clustering complete\n");
            Console.WriteLine("Final clustering in internal form:\n");
            
            ShowVector(clustering, true);
            
            Console.WriteLine("Raw data by cluster:\n");
            
            ShowClustered(rawData, clustering, numClusters, 1);
            
            Console.WriteLine("\nEnd k-means clustering demo\n");
            Console.ReadLine();
        }

        static void ShowVector(int[] vector, bool newLine)
        {
            for (int i = 0; i < vector.Length; ++i)
                Console.Write(vector[i] + " ");
            if (newLine == true) Console.WriteLine("\n");
        }

        static void ShowData(double[][] data, int decimals, bool indices, bool newLine)
        {
            for (int i = 0; i < data.Length; ++i)
            {
                if (indices == true)
                    Console.Write(i.ToString().PadLeft(3) + " ");
                for (int j = 0; j < data[i].Length; ++j)
                {
                    double v = data[i][j];
                    Console.Write(v.ToString("F" + decimals) + " ");
                }
                Console.WriteLine("");
            }
            if (newLine == true)
                Console.WriteLine("");
        }

        static void ShowClustered(double[][] data, int[] clustering, int numClusters, int decimals)
        {
            for (int k = 0; k < numClusters; ++k)
            {
                Console.WriteLine("====================");
                for (int i = 0; i < data.Length; i++)
                {
                    var clusterId = clustering[i];
                    if (clusterId != k)
                        continue;

                    for (int j = 0; j < data[i].Length; ++j)
                    {
                        double v = data[i][j];
                        Console.Write(v.ToString("F" + decimals) + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("====================");
            } // k ends
        }

        static double[][] LoadData(string dataFile, int numRows, int numCols, char delimit)
        {
            double[][] result = new double[numRows][];
            using (var fileStream = new FileStream(dataFile, FileMode.Open))
            using (var reader = new StreamReader(fileStream))
            {
                string line = "";
                string[] tokens = null;
                int i = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    result[i] = new double[numCols];
                    tokens = line.Split(delimit);
                    for (int j = 0; j < numCols; ++j)
                        result[i][j] = double.Parse(tokens[j]);
                    ++i;
                }
            }
            return result;
        }
    }
}
