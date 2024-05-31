///ETML
///Auteur : Yann Scerri
///Date : 17.05.2024
///Description : Classe Missile comportants les informations permettant de générer les missiles du vaisseau et des ennemis
using System;

namespace Projet_Spicy_Invader
{
    internal class Missile 
    {
        private int _positionX; //position x des missiles
        private int _positionY; //position y des missiles
        private double _speed; //vitesse des missiles
        private double _elapsedTime; //prendre en compte le temps écoulé
        private int _oldposition = 0; //ancienne position des missiles
        private int _direction; //direction des missiles
        

        /// <summary>
        /// Constructeur de la classe missile
        /// </summary>
        /// <param name="missilePositionX">position x du missile</param>
        /// <param name="missilePositionY">position y du missile</param>
        public Missile(int missilePositionX, int missilePositionY, int direction)
        {
            _positionX = missilePositionX;
            _positionY = missilePositionY;
            _speed = 20;
            _elapsedTime = 0;
            _direction = direction; //-1 en haut 1 en bas
        }
            
        /// <summary>
        /// méthode pour mettre à jour les missiles
        /// </summary>
        /// <param name="elapsedSeconds">temps écoulé</param>
        public void Update(double elapsedSeconds)
        {
            _elapsedTime += elapsedSeconds;
            _positionY += _direction;
        }

        /// <summary>
        /// méthode pour afficher les missiles
        /// </summary>
        public void Draw()
        {
            Console.SetCursorPosition((int)_positionX, _oldposition);
            Console.Write(" ");
            Console.SetCursorPosition((int)_positionX, (int)_positionY);
            Console.Write(_direction == -1 ? "^" : "v");
            _oldposition = (int)_positionY;
        }

        /// <summary>
        /// getter setter de la position y
        /// </summary>
        public int PositionY
        {
            get { return _positionY; }
            set { _positionY = value; }
        }

        /// <summary>
        /// getter setter de la position x
        /// </summary>
        public int PositionX
        {
            get { return _positionX; }
            set { _positionX = value; }
        }
    }
}
