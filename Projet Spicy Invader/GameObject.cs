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

        // Position du vaisseau spatial
        private double _positionX;
        private double _positionY;

        public SpaceShip(double speedPixelPerSecond, int lives)
        {
            _speedPixelPerSecond = speedPixelPerSecond;
            _lives = lives;
            _positionX = 20; // Position initiale X
            _positionY = 40; // Position initiale Y
            
        }

        /*public int Lives
        {
            get { return _lives; }
            set { _lives = value; }
        }

        public double SpeedPixelPerSecond
        {
            get { return _speedPixelPerSecond; }
            set { _speedPixelPerSecond = value; }
        }

        public double PositionX
        {
            get { return _positionX; }
            set { _positionX = value; }
        }*/
       
        

        // Dessine le vaisseau 
        private void Draw()
        {
            string[] spaceshipDrawing = { "-[O]-" };

            // limites
            int x = (int)Math.Max(0, Math.Min(Console.WindowWidth - 1, _positionX));
            int y = (int)Math.Max(0, Math.Min(Console.WindowHeight - 1, _positionY));

            //position initiale du curseur
            Console.SetCursorPosition(x, y);

            foreach (string line in spaceshipDrawing)
            {
                Console.Write(line);
            }
        }

        

        public void HandleInput()
        {
            //Draw();
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        _positionX -= 1;
                        break;
                    case ConsoleKey.RightArrow:
                        _positionX += 1;
                        break;
                }
            }
        }
        
        // Vérifie si le vaisseau est encore en vie
        private bool IsAlive()
        {
            return _lives > 0;
        }

        // Met à jour le vaisseau (appelée à chaque itération du jeu)
        public void Update(double elapsedSeconds)
        {
           
            HandleInput();
            Draw();
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



