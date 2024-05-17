///ETML
///Auteur : Yann Scerri
///Date : 17.05.2024
///Description : Classe Bunker permettant de générer les bunker de protection au bas de l'écran
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Spicy_Invader
{
    internal class Bunker : GameObject
    {
        private int _positionX;
        private int _positionY;

        /// <summary>
        /// Constructeur de la classe bunker
        /// </summary>
        /// <param name="positionX"></param>
        /// <param name="positionY"></param>
        public Bunker(int positionX, int positionY)
        {
            _positionX = positionX;
            _positionY = positionY;
        }
        /// <summary>
        /// méthode qui dessine les bunkers
        /// </summary>
        public void Draw()
        {
            int width = 10; // Largeur du bunker
            int height = 5; // Hauteur du bunker

            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(_positionX, _positionY + i);
                Console.WriteLine(new string('█', width));
            }
        }
    }
}
