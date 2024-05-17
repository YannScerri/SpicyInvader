///ETML
///Auteur : Yann Scerri
///Date : 17.05.2024
///Description : Classe Missile comportants les informations permettant de générer les missiles du vaisseau et des ennemis
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Spicy_Invader
{   
    internal class Missile : GameObject
    {   //variables pour les missiles
        private double _positionX;
        private double _positionY;
        private double _speed;
        private double _timeToLive;
        private double _elapsedTime;
        private int _oldposition = 0;
        private int _direction;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="missilePositionX"></param>
        /// <param name="missilePositionY"></param>
        /// <param name="direction"></param>
        public Missile(double missilePositionX, double missilePositionY, int direction)
        {
            _positionX = missilePositionX;
            _positionY = missilePositionY;
            _speed = 20;
            _timeToLive = 3;
            _elapsedTime = 0;
            _direction = direction; //-1 en haut 1 en bas
        }

        public void Update(double elapsedSeconds)
        {
            _elapsedTime += elapsedSeconds;
            _positionY += _direction;
        }

        public void Draw()
        {
            Console.SetCursorPosition((int)_positionX, _oldposition);
            Console.Write(" ");
            Console.SetCursorPosition((int)_positionX, (int)_positionY);
            //Console.Write("^");
            Console.Write(_direction == -1 ? "^" : "v");
            _oldposition = (int)_positionY;
        }
        //public double PositionY => _positionY;

        public double PositionY
        {
            get { return _positionY; }
            set { _positionY = value; }
        }

        public double PositionX
        {
            get { return _positionX; }
            set { _positionX = value; }
        }
    }
}
