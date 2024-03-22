using Projet_Spicy_Invader;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Spicy_Invader
{   //le reste des classes héritent de celle-ci
    internal class GameObject
    {
       
    }



    internal class SpaceShip : GameObject
    {
        public List<Missile> missileList = new List<Missile>();
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
                Console.SetCursorPosition(_oldPosition + i, PositionY);
                Console.Write(" ");
            }   
        }

        public void FireMissile()
        {
           if (missileList.Count == 0)
            {
                Missile missile = new Missile(PositionX + 2, PositionY - 1); // Réglez la position de départ du missile pour qu'il parte du vaisseau
                missileList.Add(missile); // Ajoutez ce missile à une liste de missiles
            }
            

        }

        public void UpdateMissiles(double elapsedSeconds)
        {
            foreach(Missile missile in missileList)
            {
                missile.Update(elapsedSeconds);
                if(missile.PositionY < 0)
                {
                    //missileList.Remove(missile);
                }
            }
        }

        public void DrawMissiles()
        {
            foreach(Missile missile in missileList)
            {
                if(missile.PositionY < 0)
                {
                    missile.Draw();
                }
            }
        }

    }

    internal class Missile : GameObject
    {   //variables pour les missiles
        private double _positionX;
        private double _positionY;
        private double _speed;
        private double _timeToLive;
        private double _elapsedTime;
        private int _oldposition = 0;

        /// <summary>
        /// Constructeur de la classe missile
        /// </summary>
        /// <param name="missilePositionX"></param>
        /// <param name="missilePositionY"></param>
        public Missile(double missilePositionX, double missilePositionY)
        {
            _positionX = missilePositionX;
            _positionY = missilePositionY;
            _speed = 20;
            _timeToLive = 3;
            _elapsedTime = 0;   
        }

        public void Update(double elapsedSeconds)
        {   
            _elapsedTime += elapsedSeconds;
            _positionY--;
        }

        public void Draw()
        {
            Console.SetCursorPosition((int)_positionX, _oldposition);
            Console.Write(" ");
            Console.SetCursorPosition((int)_positionX, (int)_positionY);
            Console.Write("^");
            _oldposition = (int)_positionY;
        }
        public double PositionY => _positionY;
    }


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
   
   
    
    internal class Enemies : GameObject
       {
            private int _positionX;
            private int _positionY;
            private int _speed;
            private int _direction;
            private string _enemyDesign;

            public Enemies(int positionX, int positionY, int speed, string enemyDesign)
            {
                _positionX = positionX;
                _positionY = positionY;
                _speed = speed;
                _direction = 1; // initial direction, 1 for right, -1 for left
                _enemyDesign = enemyDesign;
            }

            public void Update(double elapsedSeconds)
            {
                _positionX += _speed * _direction;

                // Check boundaries
                if (_positionX <= 0 || _positionX >= Console.WindowWidth - 1)
                {
                    _direction *= -1; // Reverse direction if hitting boundaries
                    _positionY++; // Move enemies down if hitting boundaries
                }
            }

            public void Draw()
            {
                Console.SetCursorPosition(_positionX, _positionY);
                Console.Write(_enemyDesign);
            }
       }
}
 



