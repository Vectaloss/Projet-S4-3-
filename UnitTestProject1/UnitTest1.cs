using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projet_S4__3_;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1() ///Ce test regarde si les deux images ont la même hauteur et largeur
        {
            MyImage image1 = new MyImage(200, 600);
            MyImage image2 = new MyImage(200,600);
            int length1 = image1.Largeur;
            int length2 = image2.Largeur;
            int height1 = image1.Hauteur;
            int height2 = image2.Hauteur;
            Assert.AreEqual(length2, length1);
            Assert.AreEqual(height1, height2);
        }
        [TestMethod]
        public void TestMethod2()///Ce test va comparer la taille des fichiers et retourner vrai si elles sont différentes
        {
            MyImage test1 = new MyImage(300,400);
            MyImage test2 = new MyImage(150, 300);
            int tailleFichier1 = test1.TailleFichier;
            int tailleFichier2 = test2.TailleFichier;

            Assert.AreNotEqual(tailleFichier1, tailleFichier2);
        }
    }
}
