///ETML
///Auteur : Yann Scerri
///Date : 17.05.2024
///Description : Classe SpaceShip comportant les informations pour le vaisseau du joueur
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Spicy_Invader
{
    internal class SpaceShip : GameObject
    {
        private Missile _missile;
        private double _speedPixelPerSecond;
        private int _lives;

        // Pour la position du vaisseau
        private int _positionX;
        private int _positionY;

        private int _oldPosition;

        /// <summary>
        /// constructeur du SpaceShip 
        /// </summary>
        /// <param name="speedPixelPerSecond"></param>
        /// <param name="lives"></param>
        /// <param name="positionX"></param>
        /// <param name="positionY"></param>
        public SpaceShip(double speedPixelPerSecond, int lives, int positionX, int positionY)
        {
            _speedPixelPerSecond = speedPixelPerSecond;
            _lives = lives;
            _positionX = positionX;
            _positionY = positionY;
            _missile = null; // Initialiser le missile à null
        }

        public int Lives
        {
            get { return _lives; }
            set { _lives = value; }
        }

        public double SpeedPixelPerSecond
        {
            get { return _speedPixelPerSecond; }
            set { _speedPixelPerSecond = value; }
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

        public int OldPosition
        {
            get { return _oldPosition; }
            set { _oldPosition = value; }
        }

        /// <summary>
        /// méthode qui dessine le vaiseau
        /// </summary>
        public void Draw()
        {
            string[] spaceshipDrawing = { "|-O-|" };

            // limites
            int x = (int)Math.Max(0, Math.Min(Console.WindowWidth - 1, PositionX));
            int y = (int)Math.Max(0, Math.Min(Console.WindowHeight - 1, PositionY));

            //position initiale du curseur
            Console.SetCursorPosition(x, y);

            foreach (string line in spaceshipDrawing)
            {
                Console.Write(line);
            }
        }

        /// <summary>
        /// méthode pour donner un nombre de vies au vaisseau
        /// </summary>
        /// <returns></returns>
        public bool IsAlive()
        {
            return _lives > 0;
        }

        /// <summary>
        /// méthode métant à jour le vaisseau (appelée à chaque itération du jeu)
        /// </summary>
        /// <param name="elapsedSeconds"></param>
        public void Update(double elapsedSeconds)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.SetCursorPosition(_oldPosition + i, PositionY);
                Console.Write(" ");
            }

            // Mettre à jour le missile s'il existe
            if (_missile != null)
            {
                _missile.Update(elapsedSeconds);

                // Supprimer le missile s'il sort des limites de la console
                if (_missile.PositionY < 0)
                {
                    _missile = null;
                }
                else
                {
                    _missile.Draw();
                }
            }
        }

        public void FireMissile()
        {
            // Créer un nouveau missile seulement s'il n'y a pas de missile actif
            if (_missile == null)
            {
                _missile = new Missile(PositionX + 2, PositionY - 1, -1);
            }
        }

        public void DrawMissiles()
        {
            // Dessiner le missile s'il existe
            if (_missile != null)
            {
                _missile.Draw();
            }
        }

        public void ClearMissile()
        {
            _missile = null;
        }

        public Missile Missile
        {
            get { return _missile; }
            set { _missile = value; }
        }

        public bool CheckCollision(int missileX, int missileY)
        {
            // Vérifie si les coordonnées du missile ennemi touchent le vaisseau
            return (missileX >= _positionX && missileX < _positionX + 5 &&
                    missileY == _positionY);
        }

    }
}
