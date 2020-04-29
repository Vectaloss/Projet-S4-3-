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
        /// <summary>
        /// Cette méthode permet de choisir une image parmis les différentes disponibles
        /// </summary>
        /// <param name="args"></param>
        static string choixImage()
        {
            ConsoleKeyInfo choixUtilisateur;
            Console.Clear();
            Console.WriteLine("Choissisez l'image à traiter :" +
                "\n- Coco                                                                   (a)" +
                "\n- Lena                                                                   (b)" +
                "\n- Lac en montagne                                                        (c)" +
                "\n- Le dernier résultat ou une image blanche pour fractale et histogramme  (d)"); 
            do
            {
                choixUtilisateur = Console.ReadKey(true);
            }
            while (choixUtilisateur.Key != ConsoleKey.A && choixUtilisateur.Key != ConsoleKey.B &&
            choixUtilisateur.Key != ConsoleKey.C && choixUtilisateur.Key != ConsoleKey.D);

            if (choixUtilisateur.Key == ConsoleKey.A)
            { return "coco.bmp"; }

            else if (choixUtilisateur.Key == ConsoleKey.B)
            { return "lena.bmp"; }

            else if (choixUtilisateur.Key == ConsoleKey.C)
            { return "lac_en_montagne.bmp"; }

            else 
            { return "newimage.bmp"; }

        }
        
        /// <summary>
        /// Main qui permet de lancer toutes les méthodes
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            bool execute = true; 
            while (execute == true)
            {
                #region Menu Textuel
                Console.Clear();
                Console.WriteLine("\nChoissisez au clavier une des sous-parties :\n" +
                    "\n- Opérations géometriques    (a)" +
                    "\n- Gris et couleurs           (b)" +
                    "\n- Matrices de convolution    (c)" +
                    "\n- Divers                     (d)" +
                    "\n- Innovations...             (e)" +
                  "\n\nFermer la console            (f)");
                ConsoleKeyInfo choixUtilisateur;
                do
                {
                    choixUtilisateur = Console.ReadKey(true);
                }
                #endregion

                while (choixUtilisateur.Key != ConsoleKey.A && choixUtilisateur.Key != ConsoleKey.B &&
                choixUtilisateur.Key != ConsoleKey.C && choixUtilisateur.Key != ConsoleKey.D && choixUtilisateur.Key != ConsoleKey.E && choixUtilisateur.Key != ConsoleKey.F);

                if (choixUtilisateur.Key == ConsoleKey.A)
                {
                    #region Menu Textuel
                    Console.Clear();
                    Console.WriteLine("\n     [Opérations géometriques]" +
                        "\n" +
                    "\n- Miroir                      (a)" +
                    "\n- Agrandir                    (b)" +
                    "\n- Rotation                    (c)" +
                    "\n- Rétrécir horizontallement   (d)" +
                    "\n- Rétrécir verticallement     (e)" +
                    "\n- Rétrécir globalement        (f)");
                    #endregion

                    #region Boutons pour lancer les méthodes
                    do
                    {
                        choixUtilisateur = Console.ReadKey(true);
                    }
                    while (choixUtilisateur.Key != ConsoleKey.A && choixUtilisateur.Key != ConsoleKey.B &&
                    choixUtilisateur.Key != ConsoleKey.C && choixUtilisateur.Key != ConsoleKey.D && choixUtilisateur.Key != ConsoleKey.E && choixUtilisateur.Key != ConsoleKey.F);

                    if (choixUtilisateur.Key == ConsoleKey.A)
                    {
                        MyImage image = new MyImage(choixImage());
                        image.Miroir();
                        Process.Start("newimage.bmp");
                    }///Miroir

                    if (choixUtilisateur.Key == ConsoleKey.B)
                    {
                        MyImage image = new MyImage(choixImage());
                        image.Agrandir2();
                        Process.Start("newimage.bmp");
                    }///Agrandir

                    if (choixUtilisateur.Key == ConsoleKey.C)
                    {
                        MyImage image = new MyImage(choixImage());
                        image.Rotation3(0);
                        Process.Start("newimage.bmp");
                    }///Rotations

                    if (choixUtilisateur.Key == ConsoleKey.D)
                    {
                        MyImage image = new MyImage(choixImage());
                        image.Retrecicement(true);
                        Process.Start("newimage.bmp");
                    }///Rétrécissement Horizontal

                    if (choixUtilisateur.Key == ConsoleKey.E)
                    {
                        MyImage image = new MyImage(choixImage());
                        image.Retrecicement(false);
                        Process.Start("newimage.bmp");
                    }///Rétrécissement Vertical

                    if (choixUtilisateur.Key == ConsoleKey.F)
                    {
                        MyImage image = new MyImage(choixImage());
                        image.Retrecicement(true);
                        image = new MyImage("newimage.bmp");
                        image.Retrecicement(false);
                        Process.Start("newimage.bmp");
                    }///Rétrécissement Global
                    #endregion

                }///Opérations géométriques

                if (choixUtilisateur.Key == ConsoleKey.B)
                {
                    #region Menu Textuel
                    Console.Clear();
                    Console.WriteLine("\n  [Gris et couleurs]" +
                         "\n" +
                     "\n- Noir et blanc               (a)" +
                     "\n- Nuances de gris             (b)" +
                     "\n- Filtre 4 couleur            (c)");
                    #endregion

                    #region Boutons pour lancer les méthodes
                    do
                    {
                        choixUtilisateur = Console.ReadKey(true);
                    }
                    while (choixUtilisateur.Key != ConsoleKey.A && choixUtilisateur.Key != ConsoleKey.B &&
                    choixUtilisateur.Key != ConsoleKey.C );

                    if (choixUtilisateur.Key == ConsoleKey.A)
                    {
                        MyImage image = new MyImage(choixImage());
                        image.NoirEtBlanc();
                        Process.Start("newimage.bmp");
                    }///Filtre noir et blanc

                    if (choixUtilisateur.Key == ConsoleKey.B)
                    {
                        MyImage image = new MyImage(choixImage());
                        image.NuancesDeGris();
                        Process.Start("newimage.bmp");
                    }///Filtre nuances de gris

                    if (choixUtilisateur.Key == ConsoleKey.C)
                    {
                        MyImage image = new MyImage(choixImage());
                        image.Innovation1();
                        Process.Start("newimage.bmp");
                    }
                    #endregion

                }///Gris et couleurs

                if (choixUtilisateur.Key == ConsoleKey.C)
                {
                    #region Menu Textuel
                    Console.Clear();
                    Console.WriteLine("\n  [Matrice de Convolution]" +
                         "\n" +
                     "\n- Détection des Contours      (a)" +
                     "\n- Flou                        (b)" +
                     "\n- Repoussage                  (c)" +
                     "\n- Renforcement                (d)");
                    #endregion

                    #region Boutons pour lancer les méthodes
                    do
                    {
                        choixUtilisateur = Console.ReadKey(true);
                    }
                    while (choixUtilisateur.Key != ConsoleKey.A && choixUtilisateur.Key != ConsoleKey.B &&
                    choixUtilisateur.Key != ConsoleKey.C && choixUtilisateur.Key != ConsoleKey.D );

                    if (choixUtilisateur.Key == ConsoleKey.A)
                    {
                        MyImage image = new MyImage(choixImage());
                        image.DetectionContours();
                        Process.Start("newimage.bmp");
                    } ///Détection des contours

                    if (choixUtilisateur.Key == ConsoleKey.B)
                    {
                        MyImage image = new MyImage(choixImage());
                        image.Flou();
                        Process.Start("newimage.bmp");
                    }///Flou

                    if (choixUtilisateur.Key == ConsoleKey.C)
                    {
                        MyImage image = new MyImage(choixImage());
                        image.Repoussage();
                        Process.Start("newimage.bmp");
                    }///Repoussage

                    if (choixUtilisateur.Key == ConsoleKey.D)
                    {
                        MyImage image = new MyImage(choixImage());
                        image.Renforcement();
                        Process.Start("newimage.bmp");
                    }///Renforcement
                    #endregion

                }///Matrice de convolution

                if (choixUtilisateur.Key == ConsoleKey.D)
                {
                    #region Menu Textuel
                    Console.Clear();
                    Console.WriteLine("\n  [Divers]" +
                         "\n" +
                     "\n- Histogramme                 (a)" +
                     "\n- Fractale                    (b)");
                    #endregion

                    #region Boutons pour lancer les méthodes
                    do
                    {
                        choixUtilisateur = Console.ReadKey(true);
                    }
                    while (choixUtilisateur.Key != ConsoleKey.A && choixUtilisateur.Key != ConsoleKey.B);

                    if (choixUtilisateur.Key == ConsoleKey.A)
                    {
                        MyImage image = new MyImage(choixImage());
                        Console.Clear();
                        Console.WriteLine("\nChargement... Veuillez patienter svp.");
                        image.Histogramme2();
                        Process.Start("newimage.bmp");
                    } ///Histogramme

                    if (choixUtilisateur.Key == ConsoleKey.B)
                    {
                        MyImage fractale = new MyImage(400, 400);
                        Console.Clear();
                        Console.WriteLine("\nChargement... Veuillez patienter svp.");
                        fractale.Fractaleee();
                        Process.Start("newimage.bmp");
                    }///Fractale
                    #endregion

                } ///Divers

                if (choixUtilisateur.Key == ConsoleKey.E)
                {
                    #region Menu Textuel
                    Console.Clear();
                    Console.WriteLine("\n  [Innovations]" +
                         "\n" +
                     "\n- Filtre 4 couleurs           (a)" +
                     "\n- Images aléatoires           (b)");
                    #endregion

                    #region Boutons pour lancer les méthodes
                    do
                    {
                        choixUtilisateur = Console.ReadKey(true);
                    }
                    while (choixUtilisateur.Key != ConsoleKey.A && choixUtilisateur.Key != ConsoleKey.B /*&&
                    choixUtilisateur.Key != ConsoleKey.C && choixUtilisateur.Key != ConsoleKey.D && choixUtilisateur.Key != ConsoleKey.E*/);

                    if (choixUtilisateur.Key == ConsoleKey.A) 
                    {
                        MyImage image = new MyImage(choixImage());
                        image.Innovation1();
                        Process.Start("newimage.bmp");
                    } /// Filtre 4 couleurs / Innovation

                    if (choixUtilisateur.Key == ConsoleKey.B)
                    {
                        MyImage image = new MyImage(500,500);
                        image.Innovation2();
                        Process.Start("newimage.bmp");
                    }
                    #endregion

                } ///Innovations

                if (choixUtilisateur.Key == ConsoleKey.F) /// Pour fermer la console et mettre fin au programme
                    execute = false;

            }
        }
    }
}
