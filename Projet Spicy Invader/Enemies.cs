///ETML
///Auteur : Yann Scerri
///Date : 17.05.2024
///Description : Classe Enemies comportant les informations pour les ennemis du jeu
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Spicy_Invader
{
    internal class Enemies : GameObject
    {
        private int _positionX;
        private int _positionY;
        private int _speed;
        private int _direction;
        private string _enemyDesign;
        int count = 0;
        private List<Missile> missileList = new List<Missile>();

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
            //if(count == 20)
            //{
            //    _positionX++;
            //    count = 0;
            //}
            // Check boundaries
            if (_positionX <= 0 || _positionX >= Console.WindowWidth - 1)
            {
                _direction *= -1; // Reverse direction if hitting boundaries
                _positionY++; // Move enemies down if hitting boundaries

            }
            // count++;
        }

        public void Draw()
        {
            Console.SetCursorPosition(_positionX, _positionY);
            Console.Write(_enemyDesign);
        }

        public void Move(int direction)
        {
            _positionX += _speed * _direction;

            if (_positionX <= 0 || _positionX >= Console.WindowWidth - 1)
            {
                _direction *= -1;
                _positionY++;
            }
        }

        public void Clear()
        {
            Console.SetCursorPosition(_positionX, _positionY);
            Console.Write(" ");
        }

        public void FireMissile()
        {
            Missile missile = new Missile(_positionX + 2, _positionY + 1, 1);
            missileList.Add(missile);
        }

        public void UpdateMissiles(double elapsedSeconds)
        {
            for (int i = missileList.Count - 1; i >= 0; i--)
            {
                Missile missile = missileList[i];
                missile.Update(elapsedSeconds);

                if (missile.PositionY >= Console.WindowHeight)
                {
                    missileList.RemoveAt(i);
                }
                else
                {
                    missile.Draw();
                }
            }
        }
    }
}
