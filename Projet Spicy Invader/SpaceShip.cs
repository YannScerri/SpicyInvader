///ETML
///Auteur : Yann Scerri
///Date : 17.05.2024
///Description : Classe SpaceShip comportant les informations pour le vaisseau du joueur
using System;

namespace Projet_Spicy_Invader
{
    internal class SpaceShip 
    {
        private Missile _missile; //missile pour le vaisseau
        private double _speedPixelPerSecond; //vitesse du vaisseau
        private int _lives; //vies du vaisseau
        private int _positionX;//position x du vaisseau 
        private int _positionY; //position y du vaisseau
        private int _oldPosition; //ancienne position du vaisseau


        /// <summary>
        /// constructeur du SpaceShip 
        /// </summary>
        /// <param name="speedPixelPerSecond">vitesse du vaisseau</param>
        /// <param name="lives">vies du vaisseau</param>
        /// <param name="positionX">position x du vaisseau</param>
        /// <param name="positionY">position y du vaisseau</param>
        public SpaceShip(double speedPixelPerSecond, int lives, int positionX, int positionY)
        {
            _speedPixelPerSecond = speedPixelPerSecond;
            _lives = lives;
            _positionX = positionX;
            _positionY = positionY;
            _missile = null; // Initialiser le missile à null
        }

        /// <summary>
        /// getter settter des vies 
        /// </summary>
        public int Lives
        {
            get { return _lives; }
            set { _lives = value; }
        }

        /// <summary>
        /// getter setter de la position x
        /// </summary>
        public int PositionX
        {
            get { return _positionX; }
            set { _positionX = value; }
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
        /// getter setter de la position précédente
        /// </summary>
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
            string[] spaceshipDrawing = { "|-O-|" }; //design du vaisseau

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
        /// méthode qui retourne les vies du vaisseau
        /// </summary>
        /// <returns></returns>
        public bool IsAlive()
        {
            return _lives > 0;
        }

        /// <summary>
        /// méthode métant à jour le vaisseau (appelée à chaque itération du jeu)
        /// </summary>
        /// <param name="elapsedSeconds">temps écoulé</param>
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

        /// <summary>
        /// méthode pour que le vaisseau tire un missile
        /// </summary>
        public void FireMissile()
        {
            // Créer un nouveau missile seulement s'il n'y a pas de missile actif
            if (_missile == null)
            {
                _missile = new Missile(PositionX + 2, PositionY - 1, -1);
            }
        }

        /// <summary>
        /// méthode pour afficher les missiles
        /// </summary>
        public void DrawMissiles()
        {
            // Dessiner le missile s'il existe
            if (_missile != null)
            {
                _missile.Draw();
            }
        }

        /// <summary>
        /// méthode pour effacer les missiles
        /// </summary>
        public void ClearMissile()
        {
            _missile = null;
        }

        /// <summary>
        /// getter setter du missile
        /// </summary>
        public Missile Missile
        {
            get { return _missile; }
            set { _missile = value; }
        }

        /// <summary>
        /// méthode pour gérer les collisions
        /// </summary>
        /// <param name="missileX">coordonée x du missile</param>
        /// <param name="missileY">coordonée y du missile</param>
        /// <returns></returns>
        public bool CheckCollision(int missileX, int missileY)
        {
            // Vérifie si les coordonnées du missile ennemi touchent le vaisseau
            return (missileX >= _positionX && missileX < _positionX + 5 &&
                    missileY == _positionY);
        }

    }
}
