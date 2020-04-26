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
        static string choixImage ()
        {
            ConsoleKeyInfo choixUtilisateur;
            Console.Clear();
            Console.WriteLine("Choissisez l'image à traiter :" +
                "\n- Coco                            (a)" +
                "\n- Lena                            (b)" +
                "\n- Lac en montagne                 (c)" +
                "\n- Le dernier résultat             (d)");
            do
            {
                choixUtilisateur = Console.ReadKey(true);
            }
            while (choixUtilisateur.Key != ConsoleKey.A && choixUtilisateur.Key != ConsoleKey.B &&
            choixUtilisateur.Key != ConsoleKey.C && choixUtilisateur.Key != ConsoleKey.D );

            if (choixUtilisateur.Key == ConsoleKey.A)
            { return "coco.bmp"; }

            else if (choixUtilisateur.Key == ConsoleKey.B)
            { return "lena.bmp"; }

            else if (choixUtilisateur.Key == ConsoleKey.C)
            { return "lac_en_montagne.bmp"; }

            else 
            { return "newimage.bmp"; }


            
        }
           
        static void Fractaleee()
        {
            int largeur_cadre = 400;
            int hauteur_cadre = 400;
            MyImage fractale = new MyImage(largeur_cadre, hauteur_cadre);//ecrire ce constructeur avec une image blanche 
            for (int i = 0; i < largeur_cadre; i++)
            {
                for (int j = 0; j < hauteur_cadre; j++)
                {
                    double a = (double)(i - (largeur_cadre / 2)) / (double)(largeur_cadre / 4);
                    double b = (double)(j - (hauteur_cadre / 2)) / (double)(hauteur_cadre / 4);
                    Complexe c = new Complexe(a, b);
                    Complexe z = new Complexe(0, 0);
                    int it = 0;
                    int it_max = 50;
                    do
                    {
                        it++;
                        z.Carre();
                        z.Addition(c);
                        /*if (z.Norme() > 2.0)
                        {
                            break;
                        }*/
                    }
                    while (it < it_max);
                    if ((it == it_max) && (z.Norme() < 6))
                    {
                        fractale.Image[j, i].Rouge = 0;
                        fractale.Image[j, i].Vert = 0;
                        fractale.Image[j, i].Bleu = 0;
                    }
                }

            }
            fractale.Enregistrement(fractale.Image);

        }

        static void Main(string[] args)
        {
            /* MyImage image = new MyImage("coco.bmp");
             Process.Start("coco.bmp");
             Console.ReadKey();
             image.Agrandir2();
             image = new MyImage("newimage.bmp");
             image.Flou();
             Process.Start("newimage.bmp"); 

            //
            MyImage image = new MyImage("coco.bmp");
            image.Flou();
            //Fractaleee();
            Process.Start("newimage.bmp");
            Console.ReadKey(); */
            Console.Clear();
            Console.WriteLine("Choissisez au clavier une des sous-parties :" +
                "\n- Opérations géometriques    (a)" +
                "\n- Gris et couleurs           (b)" +
                "\n- Matrices de convolution    (c)" +
                "\n- Innovations...             (d)" +
                "\n- QR codes                   (e)"+
              "\n\nFermer la console            (f)" );

            ConsoleKeyInfo choixUtilisateur;
            do
            {
                choixUtilisateur = Console.ReadKey(true);
            }
            while (choixUtilisateur.Key != ConsoleKey.A && choixUtilisateur.Key != ConsoleKey.B &&
            choixUtilisateur.Key != ConsoleKey.C && choixUtilisateur.Key != ConsoleKey.D && choixUtilisateur.Key != ConsoleKey.E);

            if (choixUtilisateur.Key == ConsoleKey.A)
            {
                Console.Clear();
                Console.WriteLine("\n  [Opérations géometriques]" +
                    "\n" + 
                "\n- Miroir                      (a)" +
                "\n- Agrandir                    (b)" +
                "\n- Rotation                    (c)" +
                "\n- Rétrécir horizontallement   (d)" +
                "\n- Rétrécir verticallement     (e)" +
                "\n- Rétrécir globalement        (f)");
                do
                {
                    choixUtilisateur = Console.ReadKey(true);
                }
                while (choixUtilisateur.Key != ConsoleKey.A && choixUtilisateur.Key != ConsoleKey.B &&
                choixUtilisateur.Key != ConsoleKey.C && choixUtilisateur.Key != ConsoleKey.D && choixUtilisateur.Key != ConsoleKey.E);

                if (choixUtilisateur.Key == ConsoleKey.A)
                { 
                    MyImage image = new MyImage(choixImage());
                    image.Miroir();
                    Process.Start("newimage.bmp");
                }

                if (choixUtilisateur.Key == ConsoleKey.B)
                {
                    MyImage image = new MyImage(choixImage());
                    image.Agrandir2();
                    Process.Start("newimage.bmp");
                }

                if (choixUtilisateur.Key == ConsoleKey.C)
                {
                    MyImage image = new MyImage(choixImage());
                    image.Rotation(90);
                    Process.Start("newimage.bmp");
                }

                if (choixUtilisateur.Key == ConsoleKey.D)
                {
                    MyImage image = new MyImage(choixImage());
                    image.Retrecicement(true);
                    Process.Start("newimage.bmp");
                }

                if (choixUtilisateur.Key == ConsoleKey.E)
                {
                    MyImage image = new MyImage(choixImage());
                    image.Retrecicement(false);
                    Process.Start("newimage.bmp");
                }

                if (choixUtilisateur.Key == ConsoleKey.F)
                {
                    MyImage image = new MyImage(choixImage());
                    image.Retrecicement(true);
                    image = new MyImage("newimage.bmp");
                    image.Retrecicement(false);
                    Process.Start("newimage.bmp");
                }
            }

            if (choixUtilisateur.Key == ConsoleKey.B)
            {
                Console.Clear();
                Console.WriteLine("");
                do
                {
                    choixUtilisateur = Console.ReadKey(true);
                }
                while (choixUtilisateur.Key != ConsoleKey.A && choixUtilisateur.Key != ConsoleKey.B &&
                choixUtilisateur.Key != ConsoleKey.C && choixUtilisateur.Key != ConsoleKey.D && choixUtilisateur.Key != ConsoleKey.E);

                if (choixUtilisateur.Key == ConsoleKey.A)
                { }

                if (choixUtilisateur.Key == ConsoleKey.B)
                { }

                if (choixUtilisateur.Key == ConsoleKey.C)
                { }

                if (choixUtilisateur.Key == ConsoleKey.D)
                { }

                if (choixUtilisateur.Key == ConsoleKey.E)
                { }
            }

            if (choixUtilisateur.Key == ConsoleKey.C)
            {
                Console.Clear();
                Console.WriteLine("");
                do
                {
                    choixUtilisateur = Console.ReadKey(true);
                }
                while (choixUtilisateur.Key != ConsoleKey.A && choixUtilisateur.Key != ConsoleKey.B &&
                choixUtilisateur.Key != ConsoleKey.C && choixUtilisateur.Key != ConsoleKey.D && choixUtilisateur.Key != ConsoleKey.E);

                if (choixUtilisateur.Key == ConsoleKey.A)
                { }

                if (choixUtilisateur.Key == ConsoleKey.B)
                { }

                if (choixUtilisateur.Key == ConsoleKey.C)
                { }

                if (choixUtilisateur.Key == ConsoleKey.D)
                { }

                if (choixUtilisateur.Key == ConsoleKey.E)
                { }
            }

            if (choixUtilisateur.Key == ConsoleKey.D)
            {
                Console.Clear();
                Console.WriteLine("");
                do
                {
                    choixUtilisateur = Console.ReadKey(true);
                }
                while (choixUtilisateur.Key != ConsoleKey.A && choixUtilisateur.Key != ConsoleKey.B &&
                choixUtilisateur.Key != ConsoleKey.C && choixUtilisateur.Key != ConsoleKey.D && choixUtilisateur.Key != ConsoleKey.E);

                if (choixUtilisateur.Key == ConsoleKey.A)
                { }

                if (choixUtilisateur.Key == ConsoleKey.B)
                { }

                if (choixUtilisateur.Key == ConsoleKey.C)
                { }

                if (choixUtilisateur.Key == ConsoleKey.D)
                { }

                if (choixUtilisateur.Key == ConsoleKey.E)
                { }
            }

            if (choixUtilisateur.Key == ConsoleKey.E)
            {
                Console.Clear();
                Console.WriteLine("");
                do
                {
                    choixUtilisateur = Console.ReadKey(true);
                }
                while (choixUtilisateur.Key != ConsoleKey.A && choixUtilisateur.Key != ConsoleKey.B &&
                choixUtilisateur.Key != ConsoleKey.C && choixUtilisateur.Key != ConsoleKey.D && choixUtilisateur.Key != ConsoleKey.E);

                if (choixUtilisateur.Key == ConsoleKey.A)
                { }

                if (choixUtilisateur.Key == ConsoleKey.B)
                { }

                if (choixUtilisateur.Key == ConsoleKey.C)
                { }

                if (choixUtilisateur.Key == ConsoleKey.D)
                { }

                if (choixUtilisateur.Key == ConsoleKey.E)
                { }
            }

            Console.ReadKey();
        }

    }
}
