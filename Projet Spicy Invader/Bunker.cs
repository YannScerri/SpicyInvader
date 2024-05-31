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
    internal class Bunker 
    {
        private int _positionX; //position x des bunkers
        private int _positionY; //position y des bunkers
        private char[,] _structure; //tableau à 2 dimension pour la structure des bunkers

        //getters setters des positions du bunker
        public int PositionX { get { return _positionX; } set { _positionX = value; } } 
        public int PositionY { get { return _positionY; } set { _positionY = value; } }


        /// <summary>
        /// Constructeur de la classe bunker
        /// </summary>
        /// <param name="positionX">position x du bunker</param>
        /// <param name="positionY">position y du bunker</param>
        public Bunker(int positionX, int positionY)
        {
            _positionX = positionX;
            _positionY = positionY;
            InitializeStructure();
        }
        /// <summary>
        /// méthode qui dessine les bunkers
        /// </summary>
        /// <summary>
        /// Méthode qui dessine les bunkers
        /// </summary>
        public void Draw()
        {
            for (int y = 0; y < _structure.GetLength(0); y++)
            {
                Console.SetCursorPosition(_positionX, _positionY + y);
                for (int x = 0; x < _structure.GetLength(1); x++)
                {
                    Console.Write(_structure[y, x]);
                }
            }
        }

        /// <summary>
        /// Initialise la structure du bunker
        /// </summary>
        private void InitializeStructure()
        {
            int width = 10; // Largeur du bunker
            int height = 5; // Hauteur du bunker
            _structure = new char[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    _structure[y, x] = '█';
                }
            }
        }

        /// <summary>
        /// méthode pour réduire les pixels des bunkers
        /// </summary>
        /// <param name="missileX">position x du missile</param>
        /// <param name="missileY">position y du missile</param>
        public void TakeDamage(int missileX, int missileY)
        {
            int localX = missileX - _positionX;
            int localY = missileY - _positionY;
            if (localX >= 0 && localX < _structure.GetLength(1) && localY >= 0 && localY < _structure.GetLength(0))
            {
                _structure[localY, localX] = ' '; // Remplacer par un espace vide
            }
        }
    }
}
