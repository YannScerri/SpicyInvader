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
        private void Update()
        {

        }

        private void Draw()
        {

        }

        private void IsAlive()
        {

        }
    }

    

internal class SpaceShip : GameObject
    {
        private double _speedPixelPerSecond;
        private int _lives;

        // Position du vaisseau spatial
        private double _positionX;
        private double _positionY;

        public SpaceShip()
        {
            // Initialisation des valeurs par défaut si nécessaire
            _speedPixelPerSecond = 0;
            _lives = 0;
            _positionX = 0;
            _positionY = 0;
        }

        public SpaceShip(double speedPixelPerSecond, int lives)
        {
            _speedPixelPerSecond = speedPixelPerSecond;
            _lives = lives;
            _positionX = 0; // Position initiale X
            _positionY = 0; // Position initiale Y
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

        // Met à jour la position du vaisseau en fonction du temps écoulé
        private void UpdatePosition(double elapsedSeconds)
        {
            // Utilisez la vitesse pour mettre à jour la position (une mise à jour simple)
            _positionX += _speedPixelPerSecond * elapsedSeconds;
        }

        // Méthode appelée pour déplacer le vaisseau
        public void Move(double elapsedSeconds)
        {
            UpdatePosition(elapsedSeconds);
            Draw(); // Dessinez à chaque déplacement pour simuler le mouvement
        }

        // Dessine le vaisseau (vous pouvez personnaliser cette méthode pour votre application)
        private void Draw()
        {
            Console.WriteLine($"Drawing spaceship at position: {_positionX}, {_positionY}");
        }

        // Vérifie si le vaisseau est encore en vie
        private bool IsAlive()
        {
            return _lives > 0;
        }

        // Met à jour le vaisseau (appelée à chaque trame/itération du jeu)
        public void Update(double elapsedSeconds)
        {
            Move(elapsedSeconds);

            if (!IsAlive())
            {
                Console.WriteLine("Game Over - Spaceship destroyed!");
                // Vous pouvez ajouter ici des actions à effectuer lorsque le vaisseau est détruit.
            }
        }


        }
    }

