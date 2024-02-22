using Projet_Spicy_Invader;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Spicy_Invader
{
    internal class GameObject
    {
       
    }



    internal class SpaceShip : GameObject
    {
        private double _speedPixelPerSecond;
        private int _lives;

        // Pour la position du vaisseau
        private int _positionX;
        private int _positionY;

        int oldPosition;

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
            get { return oldPosition; }
            set { oldPosition = value; }
        }





        /// <summary>
        /// méthode qui dessine le vaiseau
        /// </summary>
        public void Draw()
        {
            string[] spaceshipDrawing = { "-[O]-" };

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
        /// méthode pour déplacer le vaisseau de gauche à droite via les touches flechées
        /// </summary>
        
        
        /// <summary>
        /// méthode pour donner un nombre de vies au vaisseau
        /// </summary>
        /// <returns></returns>
        private bool IsAlive()
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
                Console.SetCursorPosition(oldPosition + i, PositionY);
                Console.Write(" ");
            }
           //HandleInput();
            
        }


    }

    internal class Missile : GameObject
    {
        private double _positionX;
        private double _positionY;
        private double _speed;
        public Missile(double missilePositionX, double missilePositionY)
        {
            _positionX = missilePositionX;
            _positionY = missilePositionY;
            _speed = 20;
        }

        public void Update(double elapsedSeconds)
        {
            _positionY -= _speed * elapsedSeconds; 
        }

        public void Draw()
        {
            Console.SetCursorPosition((int)_positionX, (int)_positionY);
            Console.Write("|");
        }

        public void Fire()
        {
            Missile missile = new Missile(_positionX, _positionY);
           
        }

        public double PositionY => _positionY;
    }
}



