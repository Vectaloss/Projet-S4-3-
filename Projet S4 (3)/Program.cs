using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Projet_S4__3_
{
    class Program
    {

        static void Lecture(string nom)
        {
            byte[] myfile = File.ReadAllBytes(nom);
            Console.WriteLine("\n Entête\n");
            for (int i = 0; i < 14; i++)
                Console.Write(myfile[i] + " ");
            Console.WriteLine("\n infos entête\n");
            for (int i = 14; i < 54; i++)
                Console.Write(myfile[i] + " ");
            Console.Write("\n Image \n");
            for (int i = 54; i < myfile.Length; i++)
            {
                // Console.Write(myfile[i] + " ");
            }
        }
        static string decToBin(int dec)
        {
            string a = Convert.ToString(dec, 2);
            while (a.Length < 8)
                a = "0" + a;
            return a;
        }
        static int Puissance(int puisance, int n)
        {
            if (n == 0)
            {
                return 1;
            }
            else
            {
                return puisance * Puissance(puisance, n - 1);
            }
        }
        static int binToDec(string bin, string bin2)
        {
            int nombre = 0;
            bin = bin + bin2;
            for (int i = 0; i < bin.Length; i++)
            {

                if (bin[i] == '1')
                    nombre += Puissance(2, bin.Length - i - 1);


            }
            return nombre;
        }

        static void Main(string[] args)
        {
            MyImage image = new MyImage("coco.bmp");
            Process.Start("coco.bmp");
            Console.ReadKey();
            image.Miroir();
            image = new MyImage("newimage.bmp");
            image.Flou();
            Process.Start("newimage.bmp");
            Console.ReadKey();
        }

    }
}
