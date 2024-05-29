///ETML
///Auteur : Yann Scerri
///Date : 17.05.2024
///Description : Classe 

using System;
using System.Collections.Generic;

namespace Projet_Spicy_Invader
{
    internal class Enemies : GameObject
    {
        private int _positionX;
        private int _positionY;
        private int _speed;
        private int _direction;
        private string _enemyDesign;
        private int _oldPositionX;
        private Missile _missile;
        private int _descentCounter;  // Ajout d'un compteur pour contrôler la descente

        public Enemies(int positionX, int positionY, int speed, string enemyDesign)
        {
            _positionX = positionX;
            _positionY = positionY;
            _speed = speed;
            _direction = 1; // initial direction, 1 for right, -1 for left
            _enemyDesign = enemyDesign;
            _missile = null; // Initialiser le missile à null
            _oldPositionX = positionX;
            _descentCounter = 0;  // Initialiser le compteur à 0
        }

        public int EnemiesX
        {
            get { return _positionX; }
            set { _positionX = value; }
        }
        public int EnemiesY
        {
            get { return _positionY; }
            set { _positionY = value; }
        }

        public int EnemiesOld
        {
            get { return _oldPositionX; } 
            set { _oldPositionX = value; }
        }

        public void Update(double elapsedSeconds)
        {
            _oldPositionX = _positionX; // Save old position
            //_positionX += _speed * _direction;

            // Incrémenter le compteur de descente
            _descentCounter++;

            // Descendre les ennemis moins fréquemment en utilisant le compteur
            if (_descentCounter == 10)  // Descendre tous les 10 updates par exemple
            {
                _positionX += _speed * _direction;
                //_positionX++;
                _descentCounter = 0;  // Réinitialiser le compteur après la descente
            }
        }

        public void EnemiesDown()
        {
            _positionY++; // Move enemies down
            _direction *= -1; // Reverse direction if hitting boundaries
        }

        public void Draw()
        {
            Console.SetCursorPosition(_oldPositionX, _positionY);
            Console.Write(new string(' ', _enemyDesign.Length)); // Effacer l'ancienne position
            Console.SetCursorPosition(_positionX, _positionY);
            Console.Write(_enemyDesign);
        }

        public void Move(int direction)
        {
            _oldPositionX = _positionX; // Save old position
            _positionX += _speed * _direction;

            if (_positionX <= 0 || _positionX >= Console.WindowWidth - _enemyDesign.Length)
            {
                _direction *= -1;
                _positionY++;
            }
        }

        public void Clear()
        {
            Console.SetCursorPosition(_positionX, _positionY);
            Console.Write(new string(' ', _enemyDesign.Length)); // Effacer à la nouvelle position
        }

        public void FireMissile()
        {
            if (_missile == null)
            {
                _missile = new Missile(_positionX + 2, _positionY + 1, 1); // Créer un nouveau missile
            }
        }

        public void UpdateMissiles(double elapsedSeconds)
        {
            if (_missile != null)
            {
                _missile.Update(elapsedSeconds);

                if (_missile.PositionY >= Console.WindowHeight)
                {
                    _missile = null; // Réinitialiser le missile s'il est sorti des limites
                }
                else
                {
                    _missile.Draw();
                }
            }
        }

        public string EnemyDesign
        {
            get { return _enemyDesign; }
        }
        
        public void ClearEnemy()
        {
            Console.SetCursorPosition(this._positionX, this._positionY);
            Console.Write("      ");
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
    }
}
