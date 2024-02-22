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
           

            {   //ajuster la taille de la fenêtre
                Console.WindowHeight = 50;
                Console.WindowWidth = 85;
                /*Console.BufferHeight = 40;
                Console.BufferWidth = 85;*/

                //affichage du menu
                Console.WriteLine("*************\n" +
                                  "SPACE INVADER\n" +
                                  "*************\n" +
                                  "\n" +
                                  "[1] Jouer\n" +
                                  "[2] Options\n" +
                                  "[3] Highscore\n" +
                                  "[4] A propos\n" +
                                  "[5] Quitter\n");

                //permet d'attribuer les touches du clavier aux cases
                ConsoleKeyInfo keyMenu = Console.ReadKey();

                //options du menu
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
                        Console.WriteLine("veuillez entrez une option valide (1-5)");
                        break;
                }
                    


                //méthode qui affiche les informations sur le projet
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

                void HandleInput()
                {
                }

                void ShipCreation()
                {
                    // Création du vaisseau du joueur
                    //SpaceShip playerShip = new SpaceShip(10,3,20,40);

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
                            }

                            double elapsedSeconds = 0.1;

                            playerShip.Update(elapsedSeconds);
                            playerShip.Draw();
                        }

                    } while (true);


                    //playerShip.HandleInput();

                    foreach (Missile missile in missiles)
                    {

                        //missile.Update(elapsedSeconds);
                        //missile.Draw();
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
