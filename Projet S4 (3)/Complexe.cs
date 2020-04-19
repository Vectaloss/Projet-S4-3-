using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S4__3_
{
    class Complexe
    {
        //Variables
        public double a; //Réel
        public double b; //Imagniaire de a+ib

        //Constructeur
        public Complexe(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        //Méthodes
        public void Carre()
        {
            double valeurtemporaire = (a * a) - (b * b);
            b = 2.0 * a * b;
            a = valeurtemporaire;
        }
        public double Norme()
        {
            return Math.Sqrt((a * a) + (b * b));
        }
        public void Addition(Complexe c)
        {
            a += c.a;
            b += c.b;
        }
    }
}
