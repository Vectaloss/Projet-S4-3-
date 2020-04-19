using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Projet_S4__3_
{
    class Pixel
    {
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
    }
}

