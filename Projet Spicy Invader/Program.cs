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
        static List<Missile> missiles = new List<Missile>(); 
        static void Main(string[] args)
        {
            do
           

            {
                Console.WindowHeight = 40;
                Console.WindowWidth = 85;
                Console.BufferHeight = 40;
                Console.BufferWidth = 85;
                Console.WriteLine("*************\n" +
                                  "SPACE INVADER\n" +
                                  "*************\n" +
                                  "\n" +
                                  "[1] Jouer\n" +
                                  "[2] Options\n" +
                                  "[3] Highscore\n" +
                                  "[4] A propos\n" +
                                  "[5] Quitter\n");

                ConsoleKeyInfo keyMenu = Console.ReadKey();

                switch (keyMenu.KeyChar)
                {
                    case '1': ShipCreation();
                        
                        break;
                    case '4':
                        DisplayAbout();
                        Console.Clear();
                        break;
                    case '5':
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("veuillez entrez une option valide (1-5)");
                        break;
                }



                void DisplayAbout()
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

                void ShipCreation()
                {
                    // Création du vaisseau du joueur
                    SpaceShip playerShip = new SpaceShip(speedPixelPerSecond: 10, lives: 3);

                    Console.Clear(); // Efface l'écran avant la boucle



                    double elapsedSeconds = 0.1;


                    playerShip.Update(elapsedSeconds);
                    playerShip.HandleInput();

                    foreach (Missile missile in missiles)
                    {

                        missile.Update(elapsedSeconds);
                        missile.Draw();
                    }
                    missiles.RemoveAll(missile => missile.PositionY < 0);

                    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Spacebar)
                    {
                        
                    }



                    Console.ReadLine();
                }

                







            } while (true);
        }
    }
}
