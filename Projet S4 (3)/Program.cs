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
             Process.Start("newimage.bmp"); */

            //
            MyImage image = new MyImage("coco.bmp");
            image.Flou();
            //Fractaleee();
            Process.Start("newimage.bmp");
            Console.ReadKey();

            Console.WriteLine("Choissisez au clavier une des sous-parties :" +
                "\n- Opérations géometriques    (a)" +
                "\n- Gris et couleurs           (b)" +
                "\n- Matrices de convolution    (c)" +
                "\n- Innovations...             (d)" +
                "\n- QR codes                   (e)");

            ConsoleKeyInfo choixUtilisateur;
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

            Console.ReadKey();
        }

    }
}
