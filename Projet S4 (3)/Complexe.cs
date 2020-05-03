using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S4__3_
{
    class Complexe
    {
        /// <summary>
        /// Méthode Complexe qui est utilisée pour la méthode fractale
        /// </summary>
        
        ///Variables
        public double x; ///Réel
        public double y; ///Imagniaire de a+ib

        ///Constructeur
        public Complexe(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        ///Méthodes

        /// <summary>
        /// Calcul le carré d'un nombre  complexe.
        /// </summary>
        public void Carre()
        {
            double valeurtemporaire = (x * x) - (y * y);
            y = 2.0 * x * y;
            x = valeurtemporaire;
        }

        /// <summary>
        /// Calcul la norme d'un nombre  complexe.
        /// </summary>
        /// <returns></returns>
        public double Norme()
        {
            return Math.Sqrt((x * x) + (y * y));
        }

        /// <summary>
        /// Additionne des nombres complexes.
        /// </summary>
        /// <param name="c"></param>
        public void Addition(Complexe c)
        {
            x += c.x;
            y += c.y;
        }
    }
}
