using Projet_Spicy_Invader;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Spicy_Invader
{
    internal class GameObject
    {
       
    }



    internal class SpaceShip : GameObject
    {
        List<Missile> missileList = new List<Missile>();
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
                Console.SetCursorPosition(_oldPosition + i, PositionY);
                Console.Write(" ");
            }   
        }

        public void FireMissile()
        {
           
            Missile missile = new Missile(PositionX + 2, PositionY - 1); // Réglez la position de départ du missile pour qu'il parte du vaisseau
            missileList.Add(missile); // Ajoutez ce missile à une liste de missiles

        }

        public void UpdateMissiles(double elapsedSeconds)
        {
            foreach(Missile missile in missileList)
            {
                missile.Update(elapsedSeconds);
                if(missile.PositionY < 0)
                {
                    missileList.Remove(missile);
                }
            }
        }

        public void DrawMissiles()
        {
            foreach(Missile missile in missileList)
            {
                missile.Draw();
            }
        }

    }

    internal class Missile : GameObject
    {
        private double _positionX;
        private double _positionY;
        private double _speed;
        private double _timeToLive;
        private double _elapsedTime;//temps écoulé
        public Missile(double missilePositionX, double missilePositionY)
        {
            _positionX = missilePositionX;
            _positionY = missilePositionY;
            _speed = 20;
            _timeToLive = 3;
            _elapsedTime = 0;   
        }

        public void Update(double elapsedSeconds)
        {   _elapsedTime += elapsedSeconds;
            _positionY -= _speed * elapsedSeconds;

            if(_elapsedTime >= _timeToLive)
            {
                Destroy();
            }
        }

        public void Draw()
        {
            Console.SetCursorPosition((int)_positionX, (int)_positionY);
            Console.Write("|");
        }

        public void Destroy()
        {
            
        }
        public void Fire()
        {
            Missile missile = new Missile(_positionX, _positionY);
           
        }

        public double PositionY => _positionY;
    }
}



