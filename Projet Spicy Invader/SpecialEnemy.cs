///ETML
///Auteur : Yann Scerri
///Date : 24.05.2024
///Description : Classe SpecialEnemy qui gère l'ennemi bonus du jeu (vaisseau rouge dans le jeu original)  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Spicy_Invader
{
    internal class SpecialEnemy
    {
        private int _positionX;
        private int _positionY;
        private string _design;
        private double _speedPixelPerSecond;
        private Random _random;

        public SpecialEnemy(double speedPixelPerSecond)
        {
            _random = new Random();
            _positionX = Console.WindowWidth - 1; // Commence à droite
            _positionY = 0; // Tout en haut
            _design = "<->"; // Design spécial
            _speedPixelPerSecond = speedPixelPerSecond;
        }

        public int PositionX
        {
            get { return _positionX; }
            set { _positionX = value; }
        }

        public int PositionY
        {
            get { return _positionY; }
            set { _positionY = value; }
        }

        public string Design
        {
            get { return _design; }
        }

        public void Update(double elapsedSeconds)
        {
            // Déplacer de droite à gauche
            _positionX -= (int)(_speedPixelPerSecond * elapsedSeconds);

            // Supprimer si sort de l'écran
            if (_positionX < -_design.Length)
            {
                _positionX = Console.WindowWidth - 1;
                _positionY = 0; // Réinitialiser en haut
            }
        }

        public void Draw()
        {
            // Effacer l'ancienne position
            Console.SetCursorPosition(Math.Max(_positionX + _design.Length, 0), _positionY);
            Console.Write(new string(' ', _design.Length));

            // Dessiner à la nouvelle position
            if (_positionX >= 0)
            {
                Console.SetCursorPosition(_positionX, _positionY);
                Console.Write(_design);
            }
        }

        public bool CheckCollision(int missileX, int missileY)
        {
            // Vérifie si les coordonnées du missile touchent le SpecialEnemy
            return (missileX >= _positionX && missileX < _positionX + _design.Length &&
                    missileY == _positionY);
        }
    }
}
