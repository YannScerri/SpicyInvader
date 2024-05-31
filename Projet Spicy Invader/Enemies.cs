///ETML
///Auteur : Yann Scerri
///Date : 17.05.2024
///Description : Classe qui gère les ennemis du jeu

using System;
using System.Collections.Generic;

namespace Projet_Spicy_Invader
{   

    internal class Enemies 
    {
        private int _positionX; //position x des ennemis
        private int _positionY; //posititon y des ennemis
        private int _speed; //vitesse de déplacement des ennemis
        private int _direction; //direction des ennemis
        private string _enemyDesign; //gérer le design des ennemis
        private int _oldPositionX; //définir la position précédente des ennemis
        private Missile _missile; //missile pour les ennemis
        private int _descentCounter;  // Ajout d'un compteur pour contrôler la descente

        /// <summary>
        /// constructeur de la classe Enemies
        /// </summary>
        /// <param name="positionX">position x des ennemis</param>
        /// <param name="positionY">position y des ennemis</param>
        /// <param name="speed">vitesse des ennemis</param>
        /// <param name="enemyDesign">gérer le design des ennemis</param>
        public Enemies(int positionX, int positionY, int speed, string enemyDesign)
        {
            _positionX = positionX;
            _positionY = positionY;
            _speed = speed;
            _direction = 1; // direction initiale, 1 pour droite, -1 pour gauche
            _enemyDesign = enemyDesign;
            _missile = null; // Initialiser le missile à null
            _oldPositionX = positionX;
            _descentCounter = 0; // Initialiser le compteur à 0
        }

        /// <summary>
        /// getter setter de la positionX
        /// </summary>
        public int EnemiesX
        {
            get { return _positionX; }
            set { _positionX = value; }
        }
        /// <summary>
        /// getter setter de la positionY
        /// </summary>
        public int EnemiesY
        {
            get { return _positionY; }
            set { _positionY = value; }
        }

        /// <summary>
        /// méthode pour mettre à jour la position des ennemis
        /// </summary>
        /// <param name="elapsedSeconds">temps écoulé</param>
        public void Update(double elapsedSeconds)
        {
            _oldPositionX = _positionX; // sauvegarder l'ancienne position
            
            _descentCounter++; // Incrémenter le compteur de descente

            // Descendre les ennemis moins fréquemment en utilisant le compteur
            if (_descentCounter == 10)  // Descendre tous les 10 updates par exemple
            {
                _positionX += _speed * _direction;
                _descentCounter = 0;  // Réinitialiser le compteur après la descente
            }
        }

        /// <summary>
        /// méthode pour faire descendre les ennemis
        /// </summary>
        public void EnemiesDown()
        {
            _positionY++; // descend les ennemis d'un cran vers le bas
            _direction *= -1; // inverser la direction
        }

        /// <summary>
        /// méthode d'affiche des ennemis
        /// </summary>
        public void Draw()
        {
            Console.SetCursorPosition(_oldPositionX, _positionY);
            Console.Write(new string(' ', _enemyDesign.Length)); // Effacer l'ancienne position
            Console.SetCursorPosition(_positionX, _positionY);
            Console.Write(_enemyDesign);
        }

        /// <summary>
        /// méthode pour effacer les ennemis
        /// </summary>
        public void Clear()
        {
            Console.SetCursorPosition(_positionX, _positionY);
            Console.Write(new string(' ', _enemyDesign.Length)); // Effacer à la nouvelle position
        }

        /// <summary>
        /// méthode pour faire tirer les ennemis
        /// </summary>
        public void FireMissile()
        {
            if (_missile == null)
            {
                _missile = new Missile(_positionX + 2, _positionY + 1, 1); // Créer un nouveau missile
            }
        }

        /// <summary>
        /// méthode pour mettre à jour les missiles ennemis
        /// </summary>
        /// <param name="elapsedSeconds"></param>
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

        /// <summary>
        /// getter de _enemyDesign
        /// </summary>
        public string EnemyDesign
        {
            get { return _enemyDesign; }
        }
        
        /// <summary>
        /// méthode qui efface l'ennemi en cas de destruction
        /// </summary>
        public void ClearEnemy()
        {
            Console.SetCursorPosition(this._positionX, this._positionY);
            Console.Write("      ");
        }
        /// <summary>
        /// méthode pour effacer le missile
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
    }
}
