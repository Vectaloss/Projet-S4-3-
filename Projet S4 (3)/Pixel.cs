using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Projet_S4__3_
{
    public class Pixel
    {
        /// <summary>
        /// Classe Pixel pour commander les différentes couleurs
        /// </summary>
        private int rouge, vert, bleu;
        public int Rouge
        {
            get { return this.rouge; }
            set { this.rouge = value; }
        }
        public int Vert
        {
            get { return this.vert; }
            set { this.vert = value; }

        }
        public int Bleu
        {
            get { return this.bleu; }
            set { this.bleu = value; }
        }
        public Pixel(int rouge, int vert, int bleu)
        {
            this.rouge = rouge;
            this.vert = vert;
            this.bleu = bleu;
        }


        public void CacherPixel(Pixel pixel_image2)
        {
            byte octet1 = (byte)(this.rouge & 240);
            byte octet2 = (byte)(((pixel_image2.Rouge & 240)) >> 4);
            this.rouge = (byte)(octet1 | octet2);

            octet1 = (byte)(this.vert & 240);
            octet2 = (byte)(((pixel_image2.Vert & 240)) >> 4);
            this.vert = (byte)(octet1 | octet2);

            octet1 = (byte)(this.bleu & 240);
            octet2 = (byte)(((pixel_image2.Bleu & 240)) >> 4);
            this.bleu = (byte)(octet1 | octet2);
        }

        public byte[] Retrouver_pixels_image1()
        {
            byte pixelrouge = (byte)(this.rouge & 240); // on isole les bits de poids fort du pixel pour avoir seulement ceux de la première image
            byte pixelvert = (byte)(this.vert & 240);
            byte pixelbleu = (byte)(this.bleu & 240);
            // On créer un tableau de byte dans lequel on stock nos 3 valeurs ( une par couleur), tableau qu'on retourne ensuite
            byte[] tableaupixel = new byte[3] { pixelrouge, pixelvert, pixelbleu };
            return tableaupixel;
        }

        public byte[] Retrouver_pixels_image2()
        {
            byte pixelrouge = (byte)(((byte)(this.rouge & 15)) << 4); // on isole les bits de poids fort du pixel pour avoir seulement ceux de la seconde image
            byte pixelvert = (byte)(((byte)(this.vert & 15)) << 4);
            byte pixelbleu = (byte)(((byte)(this.bleu & 15)) << 4);
            // On créer un tableau de byte dans lequel on stock nos 3 valeurs ( une par couleur), tableau qu'on retourne ensuite
            byte[] tableaupixel = new byte[3] { pixelrouge, pixelvert, pixelbleu };
            return tableaupixel;
        }
    }
}

