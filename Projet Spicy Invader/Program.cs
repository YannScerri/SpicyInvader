using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Projet_Spicy_Invader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                // Ajuster la taille de la fenêtre
                Console.WindowHeight = 50;
                Console.WindowWidth = 85;
                Console.CursorVisible = false;

                // Affichage du menu
                Console.WriteLine("*************\n" +
                                  "SPACE INVADER\n" +
                                  "*************\n" +
                                  "\n" +
                                  "[1] Jouer\n" +
                                  "[2] Options\n" +
                                  "[3] Highscore\n" +
                                  "[4] A propos\n" +
                                  "[5] Quitter\n");

                // Permet d'attribuer les touches du clavier aux cases
                ConsoleKeyInfo keyMenu = Console.ReadKey();

                // Options du menu
                switch (keyMenu.KeyChar)
                {
                    case '1':
                        ShipCreation();
                        break;
                    case '4':
                        DisplayAbout();
                        Console.Clear();
                        break;
                    case '5':
                        Environment.Exit(0);
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Veuillez entrez une option valide (1-5)");
                        break;
                }
            } while (true);
        }

        // Méthode qui affiche les informations sur le projet
        static void DisplayAbout()
        {
            Console.Clear();
            Console.WriteLine("Projet Spicy Invader réalisé dans le cadre du Projet Dev du module ICT226\n" +
                              "Date de début : 18 janvier 2024\n" +
                              "Date de fin : 31 mai 2024\n" +
                              "ETML FIN1\n" +
                              "Yann Scerri\n" +
                              "\n" +
                              "Pressez [Enter] afin de revenir au menu");

            Console.ReadLine();
        }

        // Méthode de création du vaisseau
        static void ShipCreation()
        {
            Console.Clear(); // Efface le menu avant le vaisseau

            SpaceShip playerShip = new SpaceShip(10, 3, 20, 40);

            playerShip.Draw();

            do
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyLeftRight = Console.ReadKey(true);

                    switch (keyLeftRight.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            playerShip.OldPosition = playerShip.PositionX--;
                            break;
                        case ConsoleKey.RightArrow:
                            playerShip.OldPosition = playerShip.PositionX++;
                            break;
                        case ConsoleKey.Spacebar:
                            playerShip.FireMissile();
                            playerShip.DrawMissiles();
                            playerShip.UpdateMissiles(0.1);
                            break;
                    }

                    double elapsedSeconds = 0.1;

                    playerShip.Update(elapsedSeconds);
                    playerShip.Draw();
                }

            } while (true);

           
        }
    }
}
