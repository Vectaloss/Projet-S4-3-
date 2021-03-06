﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Projet_S4__3_
{
    public class MyImage
    {
        #region Variables

        /// <summary>
        /// Variables
        /// </summary>
        private string typeImage;
        private int tailleFichier, tailleOffset, largeur, hauteur, nbBitCouleur;
        private Pixel[,] image;

        #endregion

        #region Propriétés

        /// <summary>
        /// Propriétés
        /// </summary>
        public string TypeImage
        {
            get { return this.typeImage; }
            set { this.typeImage = value; }
        }
        public int TailleFichier
        {
            get { return this.tailleFichier; }
            set { this.tailleFichier = value; }
        }
        public int TailleOffset
        {
            get { return this.tailleOffset; }
            set { this.tailleOffset = value; }
        }
        public int Largeur
        {
            get { return this.largeur; }
            set { this.largeur = value; }
        }
        public int Hauteur
        {
            get { return this.hauteur; }
            set { this.hauteur = value; }
        }
        public int NbBitCouleur
        {
            get { return this.nbBitCouleur; }
            set { this.nbBitCouleur = value; }
        }

        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeurs
        /// </summary>
        public Pixel[,] Image
        {
            get { return this.image; }
            set { this.image = value; }
        }

        /// <summary>
        /// Ce constructeur est utilisé pour lire des images
        /// </summary>
        /// <param name="fichier"></param>
        public MyImage(string fichier)
        {
            byte[] myfile = File.ReadAllBytes(fichier);
            this.tailleFichier = myfile.Length; ///taille de l'image en octet //c'est pas bon, bytes 2 à 5
            this.tailleOffset = Convert_Endian_To_Int(myfile, 10, 13); ///peut etre 14 à 17
            this.largeur = Convert_Endian_To_Int(myfile, 18, 21); ///Largeur de l'image en pixels
            this.hauteur = Convert_Endian_To_Int(myfile, 22, 25);///hauteur de l'image en pixels
            this.nbBitCouleur = Convert_Endian_To_Int(myfile, 28, 29);///Nombre de bits codant chaque couleur 
            Pixel[,] matRGB = new Pixel[this.largeur, this.hauteur];

            int index = 54; ///c'est l'index qui servira qui parcours le tableau "myfile"
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++) ///on utilise ici un double index pour parcourir toute la matrice 
                {
                    matRGB[j, i] = new Pixel(myfile[index], myfile[index + 1], myfile[index + 2]); ///l'index est la position de l'octet de la première couleur, et les "+1" et "+2" servent à acceder aux deux autres couleurs
                    index += 3; ///on ajoute 3 pour acceder à la première couleur du prochain pixel

                }
            }
            this.image = matRGB;

            if (myfile[0] == 66 && myfile[1] == 77) ///si les lettres encodées sont "B" et "M" c'est un fichier bitmap
                this.typeImage = ".bmp";

        }

        /// <summary>
        /// Ce constructeur permet de créer une nouvelle image d'une certain hauteur et largeur
        /// </summary>
        /// <param name="largeur"></param>
        /// <param name="hauteur"></param>
        public MyImage(int largeur, int hauteur)
        {
            this.hauteur = hauteur;
            this.largeur = largeur;
            this.tailleFichier = largeur * hauteur * 3 + 54;
            this.tailleOffset = 54;//rajoute
            this.nbBitCouleur = 24;
            Pixel[,] matRGB = new Pixel[this.largeur, this.hauteur];
            for (int i = 0; i < this.hauteur; i++)
            {
                for (int j = 0; j < this.largeur; j++) //on utilise ici un double index pour parcourir toute la matrice 
                {
                    matRGB[j, i] = new Pixel(255, 255, 255);
                }
            }
            this.image = matRGB;
        }

        #endregion

        #region Traitement d'image et convertions

        /// <summary>
        /// Méthodes qui permet d'enregistrer les différentes images
        /// </summary>
        /// <param name="newimage"></param>
        public void Enregistrement(Pixel[,] newimage)
        {

            int newHauteur = newimage.GetLength(1);
            int newLargeur = newimage.GetLength(0);
            int newTaille = newHauteur * newLargeur * 3 + 54;
            byte[] newfile = new byte[newTaille];
            for (int j = 0; j < 54; j++)
            {
                newfile[j] = 0;
            }
            int i;
            newfile[0] = Convert.ToByte(66); ///on considère uniquement le bitmap classique ici
            newfile[1] = Convert.ToByte(77);

            byte[] tabsize = Convert_Int_To_Endian(newTaille);
            for (i = 2; i < 6; i++)
            {
                newfile[i] = Convert.ToByte(tabsize[i - 2]);
            }
            byte[] taboffsetsize = Convert_Int_To_Endian(this.tailleOffset);
            for (i = 10; i < 14; i++)
            {
                newfile[i] = Convert.ToByte(taboffsetsize[i - 10]);
            }
            newfile[14] = 40;
            byte[] tablength = Convert_Int_To_Endian(newLargeur);
            for (i = 18; i < 22; i++)
            {
                newfile[i] = Convert.ToByte(tablength[i - 18]);
            }
            byte[] tabheight = Convert_Int_To_Endian(newHauteur);
            for (i = 22; i < 26; i++)
            {
                newfile[i] = Convert.ToByte(tabheight[i - 22]);
            }
            newfile[26] = 1;
            byte[] tabbitpercolor = Convert_Int_To_Endian(this.nbBitCouleur);
            for (i = 28; i < 30; i++)
            {
                newfile[i] = Convert.ToByte(tabbitpercolor[i - 28]);
            }
            int index = 54;
            for (int k = 0; k < newHauteur; k++)
            {
                for (int j = 0; j < newLargeur; j++)
                {
                    newfile[index] = Convert.ToByte(newimage[j, k].Rouge);
                    newfile[index + 1] = Convert.ToByte(newimage[j, k].Vert);
                    newfile[index + 2] = Convert.ToByte(newimage[j, k].Bleu);
                    index += 3;
                }
            }
            File.WriteAllBytes("newimage.bmp", newfile);
        }

        /// <summary>
        /// Méthode récursive pour calculer la puissance d'un nombre
        /// </summary>
        /// <param name="puisance"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public int Puissance(int puisance, int n)
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

        /// <summary>
        /// Cette méthode n'est pas utilisée.
        /// </summary>
        /// <param name="nom"></param>
        static void Lecture(string nom)
        {
            byte[] myfile = File.ReadAllBytes("C:\\Users\\Arnaud\\Desktop\\images\\" + nom);
            Console.WriteLine("\n Entête\n");
            for (int i = 0; i < 14; i++)
                Console.Write(myfile[i] + " ");
            Console.WriteLine("\n infos entête\n");
            for (int i = 14; i < 54; i++)
                Console.Write(myfile[i] + " ");
            Console.Write("\n Image \n");
            for (int i = 54; i < myfile.Length; i++)
            {
                Console.Write(myfile[i] + " ");
            }
        }

        /// <summary>
        /// Conversion de little endiane en entier
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="debut"></param>
        /// <param name="fin"></param>
        /// <returns></returns>
        public int Convert_Endian_To_Int(byte[] tab, int debut, int fin) ///  Conversion little endian en un entier
        {
            int number = 0; // valeur renvoyer pour la taille, la hauter, etc.
            for (int i = 0; i < fin - debut; i++)
            {
                number = number + tab[debut + i] * Puissance(256, i);
            }

            return number;
        }

        /// <summary>
        /// Conversion d'un entier dans un format little endian
        /// </summary>
        /// <param name="valeur"></param>
        /// <returns></returns>
        public byte[] Convert_Int_To_Endian(int valeur)
        {
            int a = valeur;
            byte[] tab = new byte[4];
            int Opti = 0;

            ///Divisions successives de la valeur de base pour la première opération puis du reste.
            for (int i = 0; i < 4; i++)
            {
                Opti = Convert.ToInt32(Puissance(2, 8 * ((tab.Length - 1) - i)));

                tab[tab.Length - 1 - i] = Convert.ToByte(a / Opti); /// Fin/Début => Little Endian.
                a = valeur % Opti;

            }

            return tab;
        }

        #endregion

        #region Transformation d'image

        /// <summary>
        /// Les méthodes qui permettent de transformer une image
        /// </summary>
        ///Méthode qui calcul la moyenne du pixel et qui va ensuite affecter O où 255 en fonction d'un seuil.
        public void NoirEtBlanc()
        {
            Pixel[,] noirEtBlanc = new Pixel[image.GetLength(0), image.GetLength(1)];
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    int moyenne = (image[i, j].Rouge + image[i, j].Vert + image[i, j].Bleu) / 3;
                    if (moyenne <= 123) //ici on prend la moitié de la valeur maximale
                    {
                        moyenne = 0;
                    }
                    else
                    {
                        moyenne = 255;
                    }
                    byte moyenneNetB = Convert.ToByte(moyenne); //pour initialiser en tant que byte
                    noirEtBlanc[i, j] = new Pixel(moyenneNetB, moyenneNetB, moyenneNetB);
                }

            }
            Enregistrement(noirEtBlanc);
        }

        /// <summary>
        /// Cette méthode calcul la moyenne des sous pixels et l'affecte à tous les sous pixel pour avoir une image avec des nuances de gris.
        /// </summary>
        public void NuancesDeGris()
        {
            Pixel[,] gris = new Pixel[image.GetLength(0), image.GetLength(1)]; // nécessaire pour que l'image soit de la même taille
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    int moyenne = (image[i, j].Rouge + image[i, j].Vert + image[i, j].Bleu) / 3; //Niveau de gris il faut remplacer par la moyenne des trois couleurs
                    byte moyenneGris = Convert.ToByte(moyenne);
                    gris[i, j] = new Pixel(moyenneGris, moyenneGris, moyenneGris);
                }
            }
            Enregistrement(gris);
        }

        /// <summary>
         ///Méthode  que l'on utilise pas
         /// </summary>
        public void Retrecir()
        {
            Pixel[,] deZoom = new Pixel[image.GetLength(0) / 2, image.GetLength(1) / 2]; //Je divise la longueur  par deux
            for (int i = 0; i < image.GetLength(0); i++)
            {

                for (int j = 0; j < image.GetLength(1); j++)
                {

                    int moyenneRouge = (image[i, j].Rouge + image[i, j + 1].Rouge + image[i + 1, j].Rouge + image[i + 1, j + 1].Rouge) / 4;
                    int moyenneVert = (image[i, j].Vert + image[i, j + 1].Vert + image[i + 1, j].Vert + image[i + 1, j + 1].Vert) / 4;
                    int moyenneBleu = (image[i, j].Bleu + image[i, j + 1].Bleu + image[i + 1, j].Bleu + image[i + 1, j + 1].Bleu) / 4;

                    byte rouge = Convert.ToByte(moyenneRouge);
                    byte vert = Convert.ToByte(moyenneVert);
                    byte bleu = Convert.ToByte(moyenneBleu);

                    deZoom[i / 2, j / 2] = new Pixel(rouge, vert, bleu);
                    i++; j++; ;
                }
            }
            Enregistrement(deZoom);
        }

        /// <summary>
        /// Permet de rétrécir une image par deux
        /// </summary>
        /// <param name="sens"></param>
        public void Retrecicement(bool sens) ///true pour un retrecicement horizontale, false pour verticale.
        {
            int newLarg = this.image.GetLength(0) / 2;
            int newHaut = this.image.GetLength(1) / 2;
            ///Je divise la largeur par deux 
            if (sens)
            {
                Pixel[,] deZoom = new Pixel[newLarg, this.image.GetLength(1)];
                for (int j = 0; j < this.image.GetLength(1); j++)
                {
                    for (int i = 0; i < newLarg; i++)
                    {
                        int bleu, rouge, vert;
                        int index = 2 * i;
                        bleu = (this.image[index, j].Bleu + this.image[index + 1, j].Bleu) / 2;
                        vert = (this.image[index, j].Vert + this.image[index + 1, j].Vert) / 2;
                        rouge = (this.image[index, j].Rouge + this.image[index + 1, j].Rouge) / 2;
                        deZoom[i, j] = new Pixel(rouge, vert, bleu);

                    }
                }
                Enregistrement(deZoom);
            }
            else
            {
                Pixel[,] deZoom = new Pixel[this.image.GetLength(0), newHaut];
                for (int j = 0; j < newHaut; j++)
                {
                    for (int i = 0; i < this.image.GetLength(0); i++)
                    {
                        int bleu, rouge, vert;
                        int index = 2 * j;
                        bleu = (this.image[i, index].Bleu + this.image[i, index + 1].Bleu) / 2;
                        vert = (this.image[i, index].Vert + this.image[i, index + 1].Vert) / 2;
                        rouge = (this.image[i, index].Rouge + this.image[i, index + 1].Rouge) / 2;
                        deZoom[i, j] = new Pixel(rouge, vert, bleu);

                    }
                }
                Enregistrement(deZoom);
            }
        }

        /// <summary>
        /// Notre première méthode pour agrandir  PB : Format non pris en compte. A corriger mais cette méthode n'est pas utilisée
        /// </summary>
        public void Agrandir()
        {
            Pixel[,] zoom = new Pixel[(image.GetLength(0) * 2) - 1, (image.GetLength(1) * 2) - 1];
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    zoom[2 * i, 2 * j] = image[i, j];
                }
            }
            for (int i = 0; i < zoom.GetLength(0); i++)
            {
                for (int j = 0; j < zoom.GetLength(1); j++)
                {
                    if (i % 2 == 1 && j % 2 == 0)
                    {
                        int bleu, rouge, vert;
                        bleu = (zoom[i - 1, j].Bleu + zoom[i + 1, j].Bleu) / 2;
                        vert = (zoom[i - 1, j].Vert + zoom[i + 1, j].Vert) / 2;
                        rouge = (zoom[i - 1, j].Rouge + zoom[i + 1, j].Rouge) / 2;
                        zoom[i, j] = new Pixel(rouge, vert, bleu);
                    }
                    if (i % 2 == 0 && j % 2 == 1)
                    {
                        int bleu, rouge, vert;
                        bleu = (zoom[i, j - 1].Bleu + zoom[i, j + 1].Bleu) / 2;
                        vert = (zoom[i, j - 1].Vert + zoom[i, j + 1].Vert) / 2;
                        rouge = (zoom[i, j - 1].Rouge + zoom[i, j + 1].Rouge) / 2;
                        zoom[i, j] = new Pixel(rouge, vert, bleu);
                    }
                }
            }
            for (int i = 1; i < zoom.GetLength(0); i++)
            {
                for (int j = 1; j < zoom.GetLength(1); j++)
                {
                    if (i % 2 == 1 && j % 2 == 1)
                    {
                        int bleu, rouge, vert;
                        bleu = (zoom[i, j - 1].Bleu + zoom[i, j + 1].Bleu + zoom[i - 1, j].Bleu + zoom[i, j + 1].Bleu) / 4;
                        vert = (zoom[i, j - 1].Vert + zoom[i, j + 1].Vert + zoom[i - 1, j].Vert + zoom[i, j + 1].Vert) / 4;
                        rouge = (zoom[i, j - 1].Rouge + zoom[i, j + 1].Rouge + zoom[i - 1, j].Rouge + zoom[i, j + 1].Rouge) / 4;
                        zoom[i, j] = new Pixel(rouge, vert, bleu);
                    }
                }
            }


            Enregistrement(zoom);

        }

        /// <summary>
        /// Deuxième méthode Agrandir qui fonctionne 
        /// </summary>
        public void Agrandir2()
        {
            Pixel[,] blankimage = new Pixel[image.GetLength(0) * 2, image.GetLength(1) * 2];

            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    blankimage[i * 2, j * 2] = image[i, j];
                    blankimage[i * 2, 1 + j * 2] = image[i, j];
                    blankimage[1 + i * 2, j * 2] = image[i, j];
                    blankimage[1 + i * 2, 1 + j * 2] = image[i, j];
                }

            }
            Enregistrement(blankimage);
        }

        /// <summary>
        /// Première version de rotation d'une image. Seulement pour les angles de 90, 180 et 270
        /// </summary>
        /// <param name="angle"></param>
        public void Rotation(int angle)
        {
            Pixel[,] versionFinal = null;
            if (angle == 180)
            {
                versionFinal = new Pixel[image.GetLength(0), image.GetLength(1)];
                for (int i = 0; i < image.GetLength(0); i++)
                {
                    for (int j = 0; j < image.GetLength(1); j++) 
                    {
                        versionFinal[i, j] = image[image.GetLength(0) - i-1, image.GetLength(1) - j-1];//ici pareil
                    }
                }
            }
            else if (angle == 90)
            {
                versionFinal = new Pixel[image.GetLength(1), image.GetLength(0)];
                for (int i = 0; i < image.GetLength(1); i++)
                {
                    for (int j = 0; j < image.GetLength(0); j++) 
                    {
                        versionFinal[i, j] = image[j, i]; //ici pareil
                    }
                }
            }
            else if (angle == 270)
            {
                versionFinal = new Pixel[image.GetLength(1), image.GetLength(0)];
                for (int i = 0; i < image.GetLength(1); i++)
                {
                    for (int j = 0; j < image.GetLength(0); j++)  
                    {
                        versionFinal[i, j] = image[image.GetLength(0)- 1 - j, image.GetLength(1) - 1 - i]; //ici pareil
                    }
                }
            }
            else
            {
                Console.WriteLine("Veuillez choisir entre 90, 180 et 270 degrés.");
            }
            Enregistrement(versionFinal);
        }
        /// <summary>
        /// Première version de n degrés que nous n'utilisons pas.
        /// </summary>
        /// <param name="angle"></param>
        public void Rotation2(int angle)
        {
            /// Nouveau Tableau
            Pixel[,] copie = new Pixel[image.GetLength(0), image.GetLength(1)];

            /// Coordonnées Polaires
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    /// Le centre de l'image est le repère
                    int x = j - (image.GetLength(1) / 2);
                    int y = i - (image.GetLength(0) / 2);

                    /// Nouvelles coordonnées avec la formule
                    int k = Convert.ToInt32(x * Math.Cos((angle * 180) / Math.PI) + (y * Math.Sin((angle * 180) / Math.PI)));
                    int l = Convert.ToInt32((y * Math.Cos((angle * 180) / Math.PI)) - (x * Math.Sin((angle * 180) / Math.PI)));

                    /// Adaptation du repère avec notre matrice de pixel
                    int m = (image.GetLength(0) / 2) - k;
                    int o = l + (image.GetLength(1) / 2);

                    /// Remplir nouveau tableau avec nouvelles coordonnées
                    if (m > 0 && m < image.GetLength(0) && o > 0 && o < image.GetLength(1) && copie[m, o] == null)
                    {
                        copie[m, o] = new Pixel(image[i, j].Rouge, image[i, j].Vert, image[i, j].Bleu);
                    }
                }
            }
            for (int i = 0; i < image.GetLength(0); i++)
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    if (copie[i, j] != null)
                    {
                        image[i, j].Rouge = copie[i, j].Rouge;
                        image[i, j].Vert = copie[i, j].Vert;
                        image[i, j].Bleu = copie[i, j].Bleu;
                    }
                    else
                    {
                        /// On met les autre pixel d'une couleur
                        image[i, j].Rouge = 255;
                        image[i, j].Vert = 255;
                        image[i, j].Bleu = 255;
                    }
                }
            Enregistrement(copie);
        }
        /// <summary>
        /// Rotation n degrés qui fonctionne bien mais il y a une légère baisse de la qualité.
        /// </summary>
        /// <param name="angle"></param>
        public void Rotation3(int angle)
        {
            int diagonale = Convert.ToInt32(Math.Sqrt((hauteur * hauteur) + (largeur * largeur)));
            if (diagonale % 4 != 0)
                diagonale = diagonale - diagonale % 4 + 4;
            Pixel[,] copie = new Pixel[diagonale,diagonale];
            for (int i = 0; i < diagonale; i++)
            {
                for (int j = 0; j < diagonale; j++)
                {
                    copie[i, j] = new Pixel(255, 255, 255);
                }
            }
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    int x = j - (image.GetLength(1) / 2);
                    int y = i - (image.GetLength(0) / 2);

                    int k = Convert.ToInt32((x * Math.Cos((angle * 180) / Math.PI)) + (y * Math.Sin((angle * 180) / Math.PI)));
                    int l = Convert.ToInt32((x * Math.Sin((angle * 180) / Math.PI)) - (y * Math.Cos((angle * 180) / Math.PI)));

                    int m = (copie.GetLength(0) / 2) - k;
                    int o = l + (copie.GetLength(1) / 2);
                    copie[m, o] = new Pixel(image[i, j].Rouge, image[i, j].Vert, image[i, j].Bleu);
                }
            }
            Enregistrement(copie);
        }

        public void Rotation4(int angle)
        {
            int diagonale = Convert.ToInt32(Math.Sqrt((hauteur * hauteur) + (largeur * largeur)));
            if (diagonale % 4 != 0)
                diagonale = diagonale - diagonale % 4 + 4;
            Pixel[,] copie = new Pixel[diagonale, diagonale];
            for (int i = 0; i < diagonale; i++)
            {
                for (int j = 0; j < diagonale; j++)
                {
                    copie[i, j] = new Pixel(255, 255, 255);
                }
            }

            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    int x = j - (image.GetLength(1) / 2);
                    int y = i - (image.GetLength(0) / 2);
                    double hypothenuse =  Convert.ToInt32( Math.Sqrt((x * x) + (y * y)));
                    double newAngle;
                    if (x != 0)
                    { newAngle = Math.Atan(y / x) + angle; }
                    else
                    { newAngle = 90 + angle; }
                    int k = Convert.ToInt32(hypothenuse * Math.Cos((newAngle / 180) * Math.PI));
                    int l = Convert.ToInt32(hypothenuse * Math.Sin((newAngle / 180) * Math.PI));

                    int m = (copie.GetLength(0) / 2) + k;
                    int o = (copie.GetLength(1) / 2) + l;
                    copie[m, o] = new Pixel(image[i, j].Rouge, image[i, j].Vert, image[i, j].Bleu);
                }
            }
            Enregistrement(copie);
        }

        /// <summary>
        /// Cette méthode permet d'inver l'image comme dans un miroir
        /// </summary>
        public void Miroir()
        {
            Pixel[,] miroir = new Pixel[image.GetLength(0), image.GetLength(1)];
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    miroir[i, j] = image[i, image.GetLength(1) - 1 - j];

                }
            }
            Enregistrement(miroir);
        }

        #endregion

        #region Matrice de Convolution et Filtres 

        /// <summary>
        /// Matrice de convolution
        /// </summary>
        /// <param name="matriceConvo"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        static Pixel[,] Convolution(int[,] matriceConvo, Pixel[,] image)
        {
            Pixel[,] imageContour = new Pixel[image.GetLength(0) + 2, image.GetLength(1) + 2]; ///on rajoute un contour blanc a l'image
            for (int i = 0; i < imageContour.GetLength(0); i++)
            {
                for (int j = 0; j < imageContour.GetLength(1); j++)
                {
                    if (j == 0 || i == 0 || j == (imageContour.GetLength(1) - 1) || i == (imageContour.GetLength(0) - 1))
                        imageContour[i, j] = new Pixel(255, 255, 255);
                    else
                        imageContour[i, j] = image[i - 1, j - 1];
                }
            }
            int diviseur = 0;
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    diviseur += matriceConvo[i, j];
                }
            }
            if (diviseur == 0)
                diviseur = 1;
            Pixel[,] newimage = new Pixel[image.GetLength(0), image.GetLength(1)];//créée sans contours
            for (int i = 1; i < imageContour.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < imageContour.GetLength(1) - 1; j++)
                {
                    int rouge = 0;
                    int vert = 0;
                    int bleu = 0;
                    for (int index_1 = 0; index_1 <= 2; index_1++) //récupère les valuers des neuf cases, multipliés par la matrice
                    {
                        for (int index_2 = 0; index_2 <= 2; index_2++)
                        {
                            rouge += matriceConvo[index_1, index_2] * imageContour[i - 1 + index_1, j - 1 + index_2].Rouge;//le '-1 + index' sert a faire une variation de -1 à +1
                            vert += matriceConvo[index_1, index_2] * imageContour[i - 1 + index_1, j - 1 + index_2].Vert;
                            bleu += matriceConvo[index_1, index_2] * imageContour[i - 1 + index_1, j - 1 + index_2].Bleu;
                        }
                    }
                    rouge = rouge / diviseur;
                    vert = vert / diviseur;
                    bleu = bleu / diviseur;
                    if (rouge < 0)
                        rouge = 0;
                    else if (rouge > 255)
                        rouge = 255;
                    if (vert < 0)
                        vert = 0;
                    else if (vert > 255)
                        vert = 255;
                    if (bleu < 0)
                        bleu = 0;
                    else if (bleu > 255)
                        bleu = 255;
                    newimage[i - 1, j - 1] = new Pixel(rouge, vert, bleu);
                }
            }
            return (newimage);
        }

        /// <summary>
        /// Les différentes matrices de convolution qui  vont transformer l'image.
        /// </summary>
        public void DetectionContours()
        {
            Enregistrement(Convolution(new int[3, 3] { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } }, image));
        }
        public void Flou()
        {
            Enregistrement(Convolution(new int[3, 3] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } }, image));
        }
        public void Repoussage()
        {
            Enregistrement(Convolution(new int[3, 3] { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } }, image));
        }
        public void Renforcement()
        {
            Enregistrement(Convolution(new int[3, 3] { { 0, 0, 0 }, { -1, 1, 0 }, { 0, 0, 0 } }, image));
        }

        #endregion

        #region Fractale et Histogramme

        /// <summary>
        /// Première méthode pour l'histogramme qui ne fonctionne pas.
        /// </summary>
        public void Histogramme()
        {
            MyImage histo = new MyImage(750, 256); //Remplacer comme pour fractale avec nouveau constructeur
            Pixel[,] hist_R = new Pixel[250, 256];
            Pixel[,] hist_V = new Pixel[250, 256];
            Pixel[,] hist_B = new Pixel[250, 256];
            //Le 250 c'est la hauteur du cadre et 256 le nombre de bit

            for (int i = 0; i < 250; i++)
            {
                for (int j = 0; j < 250; j++)
                {
                    hist_R[i, j] = new Pixel(255, 255, 255);
                    hist_V[i, j] = new Pixel(255, 255, 255);
                    hist_B[i, j] = new Pixel(255, 255, 255);
                    //Je créer les 3 cadres blancs qui vont loger les histogrammes
                    //On pourra en faire un qui regroupe les 3 ou les niveaux de gris
                }
            }
            int index = 0;

            //On va refaire 3 fois la même chose pour R,G et B

            //Rouge
            for (int x = 0; x < 256; x++) //ici x va parcours les différents valeurs que peut prendre un bit
            {
                for (int i = 0; i < hauteur; i++)
                {
                    for (int j = 0; j < largeur; j++)
                    {
                        if (x == image[j, i].Rouge)
                        {
                            hist_R[index, x] = new Pixel(255, 0, 0);
                            index++;
                        }
                    }
                }
            }

            for (int i = 0; i < 250; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    histo.image[i + 500, j].Rouge = hist_R[i, j].Rouge;
                    histo.image[i + 500, j].Vert = hist_R[i, j].Vert;
                    histo.image[i + 500, j].Bleu = hist_R[i, j].Bleu;
                }
            }

            //Vert
            for (int x = 0; x < 256; x++) //ici x va parcours les différents valeurs que peut prendre un bit
            {
                for (int i = 0; i < hauteur; i++)
                {
                    for (int j = 0; j < largeur; j++)
                    {
                        if (x == image[j, i].Vert)
                        {
                            hist_V[index, x] = new Pixel(0, 255, 0);
                            index++;
                        }
                    }
                }
            }

            for (int i = 0; i < 250; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    histo.image[i + 250, j].Rouge = hist_V[i, j].Rouge;
                    histo.image[i + 250, j].Vert = hist_V[i, j].Vert;
                    histo.image[i + 250, j].Bleu = hist_V[i, j].Bleu;
                }
            }

            //Bleu
            for (int x = 0; x < 256; x++) //ici x va parcours les différents valeurs que peut prendre un bit
            {
                for (int i = 0; i < hauteur; i++)
                {
                    for (int j = 0; j < largeur; j++)
                    {
                        if (x == image[j, i].Vert)
                        {
                            hist_B[index, x] = new Pixel(0, 0, 255);
                            index++;
                        }
                    }
                }
            }

            for (int i = 0; i < 250; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    histo.image[i, j].Rouge = hist_B[i, j].Rouge;
                    histo.image[i, j].Vert = hist_B[i, j].Vert;
                    histo.image[i, j].Bleu = hist_B[i, j].Bleu;
                }
            }
            Enregistrement(histo.image);
        }
        
        /// <summary>
        /// Histogramme d'une image. Cette méthode affich 3 histogrammes. Un pour chaque sous pixel donc RGB.
        /// </summary>
        public void Histogramme2()
        {
            MyImage histo = new MyImage(256*3, 250);
            int nbPixels = this.image.GetLength(0) * this.image.GetLength(1) ;

            for (int x = 0; x < 256; x++)
            {
                int sommeRouge = 0;
                int sommeVert = 0;
                int sommeBleu = 0;
                for (int i = 0; i < this.image.GetLength(0); i++)
                {
                    for (int j = 0; j < this.image.GetLength(1); j++)
                    {
                       // Console.WriteLine(this.image[i, j].Rouge);
                        if (this.image[i, j].Rouge == x)
                            sommeRouge++;
                        if (this.image[i, j].Vert == x)
                            sommeVert++;
                        if (this.image[i, j].Bleu == x)
                            sommeBleu++;
                    }
                }
                sommeRouge = sommeRouge * 20000 / nbPixels ;
                sommeVert = sommeVert * 20000 / nbPixels ;
                sommeBleu = sommeBleu * 20000 / nbPixels ;
                if (sommeRouge >= 250)
                    sommeRouge = 249;
                if (sommeVert >= 250)
                    sommeVert = 249;
                if (sommeBleu >= 250)
                    sommeBleu = 249;
                while (sommeVert > 0)
                {
                    histo.image[x, sommeVert] = new Pixel(0, 150, 0);
                    sommeVert--;
                }
                while (sommeRouge > 0)
                {
                    histo.image[x + 256, sommeRouge] = new Pixel(255, 0, 0);
                    sommeRouge--;
                }
                while (sommeBleu > 0)
                {
                    histo.image[x + 512, sommeBleu] = new Pixel(0, 0, 255);
                    sommeBleu--;
                }
            }
            Enregistrement(histo.image);
        }

        /// <summary>
        /// Cette méthode permet d'afficher l'ensemble de Mandelbrot
        /// </summary>
        public void Fractaleee()
        {
            int largeur_cadre = this.image.GetLength(0);
            int hauteur_cadre = this.image.GetLength(1);
            for (int i = 0; i < largeur_cadre; i++)
            {
                for (int j = 0; j < hauteur_cadre; j++)
                {
                    double a = (double)(i - (largeur_cadre / 2)) / (double)(largeur_cadre / 4);
                    double b = (double)(j - (hauteur_cadre / 2)) / (double)(hauteur_cadre / 4);
                    Complexe c = new Complexe(a, b);
                    Complexe z = new Complexe(0, 0);
                    int it = 0;
                    int it_max = 100;
                    do
                    {
                        it++;
                        z.Carre();
                        z.Addition(c);
                    }
                    while (it < it_max);
                    if ((it == it_max) && (z.Norme() < 6))
                    {
                        this.Image[j, i].Rouge = 0;
                        this.Image[j, i].Vert = 0;
                        this.Image[j, i].Bleu = 0;
                    }
                }
                Enregistrement(this.image);
            }

        }
        
        #endregion

        #region Innovations

        /// <summary>
        /// Cette première innovation est un filtre qui affiche une image à la Andy Warhol
        /// </summary>
        public void Innovation1()
        {
            int larg = this.image.GetLength(0);
            int haut = this.image.GetLength(1);
            MyImage newimage = new MyImage(2 * larg, 2 * haut);

            int[,] moyennes = new int[larg, haut];
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    int moyenne = (image[i, j].Rouge + image[i, j].Vert + image[i, j].Bleu) / 3; ///on réutilise une partie de la fonction nuance de gris
                    moyennes[i, j] = moyenne;
                }
            }


            for (int i = 0; i < larg; i++)
            {
                for (int j = 0; j < haut; j++)
                {
                    newimage.image[i, j] = new Pixel(moyennes[i, j], moyennes[i, j] / 5, moyennes[i, j] / 5); ///on rajoute de petites valeurs pour que l'image soit plus claire
                    newimage.image[i + larg, j] = new Pixel(0, moyennes[i, j], 0);
                    newimage.image[i, j + haut] = new Pixel(moyennes[i, j] / 3, moyennes[i, j] / 3, moyennes[i, j]); ///pareil pour le bleu
                    newimage.image[i + larg, j + haut] = new Pixel(0, moyennes[i, j], moyennes[i, j]);
                }
            }
            Enregistrement(newimage.image);
        }

        /// <summary>
        /// Cette innovation va créer une image avec différentes couleurs et tailles de carrés aléatoirement
        /// </summary>
        public void Innovation2()
        {
            Random r  = new Random();
            int nbRepetition =  r.Next(20, 50);
            while (nbRepetition > 0)
            {
                int [] positions = new int[4]; //respectivement Xa,Ya,Xb,Yb
                for (int i = 0; i<4; i++)
                {
                    positions[i] = r.Next(0, 2*this.image.GetLength(0));
                    positions[i] -= Convert.ToInt32(0.5 * this.image.GetLength(0));
                    if (positions[i] < 0)
                        positions[i] = 0;
                    if (positions[i] >= this.image.GetLength(0))
                        positions[i] = this.image.GetLength(0) - 1;
                }
                int[] couleurs = new int[3];
                for (int i = 0; i < 3; i++)
                {
                   couleurs[i] = r.Next(0,255);
                }
                if (positions[0] > positions[2])
                {
                    int a = positions[0];
                    positions[0] = positions[2];
                    positions[2] = a; }
                if (positions[1] > positions[3])
                {
                    int a = positions[1];
                    positions[1] = positions[3];
                    positions[3] = a;
                }
                if (nbRepetition < 8)
                {
                    if (positions[2] - positions[0] > 0.2 * this.image.GetLength(0))
                    {
                        int diff = r.Next(0, Convert.ToInt32(0.2 * this.image.GetLength(0)));
                        positions[2] = positions[0] + diff;
                    }
                    if (positions[3] - positions[1] > 0.2 * this.image.GetLength(0))
                    {
                        int diff = r.Next(0, Convert.ToInt32(0.2 * this.image.GetLength(0)));
                        positions[3] = positions[1] + diff;
                    }
                }
                if (positions[0] != positions[2]  && positions[1] != positions[3])
                for (int i = 0; i < this.image.GetLength(0); i++)
                {
                    for (int j = 0; j < this.image.GetLength(0); j++)
                    {
                        if (positions[0] <= i && positions[2] >= i && positions[1] <= j && positions[3] >= j)
                            this.image[i, j] = new Pixel(couleurs[0], couleurs[1], couleurs[2]);
                    }
                }
                nbRepetition--;
            }
            Enregistrement(this.image);
        }

        #endregion
    }
}
