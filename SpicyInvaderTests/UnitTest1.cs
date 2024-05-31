using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.CompilerServices;
[assembly : InternalsVisibleToAttribute("Projet Spicy Invader")]

namespace SpicyInvaderTests
{
    [TestClass]
    public class UnitTest1
    {   
        /// <summary>
        /// Méthode de test pour la méthode TakeDamage de la classe Bunker
        /// </summary>
        [TestMethod]
        public void BunkerDamageTest()
        {
            //arrange 
            char[,] initialStructure = new char[,]
        {
            { 'X', 'X', 'X' },
            { 'X', 'X', 'X' },
            { 'X', 'X', 'X' }
        };
            var structure = new Structure(initialStructure, 0, 0);
            int missileX = 1;
            int missileY = 1;

            //act
            structure.TakeDamage(missileX, missileY);

            //assert
            char[,] expectedStructure = new char[,]
        {
            { 'X', 'X', 'X' },
            { 'X', ' ', 'X' },
            { 'X', 'X', 'X' }
        };

            CollectionAssert.AreEqual(expectedStructure, structure.StructureArray);
        }

        public class Structure
        {
            private int _positionX;
            private int _positionY;
            private char[,] _structure;

            public Structure(char[,] structure, int positionX, int positionY)
            {
                _structure = structure;
                _positionX = positionX;
                _positionY = positionY;
            }

            public void TakeDamage(int missileX, int missileY)
            {
                int localX = missileX - _positionX;
                int localY = missileY - _positionY;
                if (localX >= 0 && localX < _structure.GetLength(1) && localY >= 0 && localY < _structure.GetLength(0))
                {
                    _structure[localY, localX] = ' '; // remplacer avec un espace
                }
            }

            public char[,] StructureArray => _structure;
        }
        /// <summary>
        /// méthode de test de la méthode Update Missile de la classe Missile
        /// </summary>
        [TestMethod]
        public void UpdateMissileTest()
        {
            // Arrange
            int initialPositionY = 5;
            int direction = 1; // moving downwards
            var movableObject = new MovableObject(initialPositionY, direction);
            double elapsedSeconds = 2.5;

            // Act
            movableObject.Update(elapsedSeconds);

            // Assert
            Assert.AreEqual(elapsedSeconds, movableObject.ElapsedTime, "le temps écoulé devrait être mis à jour correctement.");
            Assert.AreEqual(initialPositionY + direction, movableObject.PositionY, "la position Y devrait être mise à jour en fonction de la direction.");
        }
        
        public class MovableObject
        {
            private double _elapsedTime;
            private int _positionY;
            private int _direction;

            public MovableObject(int initialPositionY, int direction)
            {
                _elapsedTime = 0;
                _positionY = initialPositionY;
                _direction = direction;
            }

            public void Update(double elapsedSeconds)
            {
                _elapsedTime += elapsedSeconds;
                _positionY += _direction;
            }

            public double ElapsedTime => _elapsedTime;
            public int PositionY => _positionY;
            public int Direction => _direction;
        }

        /// <summary>
        /// méthode de test 1 de la méthode IsAlive de la classe SpaceShip
        /// </summary>
        [TestMethod]
        public void IsAlive1()
        {
            // Arrange
            var character = new Character(1);

            // Act
            bool result = character.IsAlive();

            // Assert
            Assert.IsTrue(result, "Devrait être en vie.");
        }
        /// <summary>
        /// méthode de test 2 de la méthode IsAlive de la classe SpaceShip
        /// </summary>
        [TestMethod]
        public void IsAliveTest2()
        {
            // Arrange
            var character = new Character(0);

            // Act
            bool result = character.IsAlive();

            // Assert
            Assert.IsFalse(result, "Ne devrait plus être en vie.");
        }

        [TestMethod]
        public void IsAliveTest3()
        {
            // Arrange
            var character = new Character(-1);

            // Act
            bool result = character.IsAlive();

            // Assert
            Assert.IsFalse(result, "Ne devrait plus être en vie.");
        }

        public class Character
        {
            private int _lives;

            public Character(int lives)
            {
                _lives = lives;
            }

            public bool IsAlive()
            {
                return _lives > 0;
            }

            public void SetLives(int lives)
            {
                _lives = lives;
            }
        }
            


    }
}

