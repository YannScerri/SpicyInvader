///ETMl
///Auteur : Yann Scerri
///Date :
///Description : Classe program.cs du projet spicy invacder
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;

namespace Projet_Spicy_Invader
{
    internal class Program
    {
        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;//resize
        [DllImport("user32.dll")] public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")] private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)] private static extern IntPtr GetConsoleWindow();

        static void Main(string[] args)
        {
            IntPtr handle = GetConsoleWindow(); IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);//resize
            }
            // Ajuster la taille de la fenêtre
            Console.WindowHeight = 50;
            Console.WindowWidth = 85;

            // Disable vertical scrolling
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);


            //efface le curseur clignotant
            Console.CursorVisible = false;

            do
            {
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
                {   //Jeu
                    case '1':
                        ShipCreation();
                        break;
                    //Options
                    case '2': 
                        DisplayOptions();
                        Console.Clear();
                        break;
                    //Highscore
                    case '3':
                        DisplayHighscore();
                        Console.Clear();
                        break;
                    //Informations sur le projet
                    case '4':
                        DisplayAbout();
                        Console.Clear();
                        break;
                    //Quitter l'application
                    case '5':
                        Environment.Exit(0);
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Veuillez entrez une option valide (1-5)");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }
            } while (true);
        }

        /// <summary>
        /// Méthode qui affiche les options
        /// </summary>
        static void DisplayOptions()
        {
            Console.Clear();
            Console.WriteLine("*******\n" +
                              "OPTIONS\n" +
                              "*******\n\n" +
                              "Pressez [Enter] afin de revenir au menu");
            Console.ReadLine();
        }

        /// <summary>
        /// Méthode qui affiche les Highscores
        /// </summary>
        static void DisplayHighscore()
        {
            Console.Clear();
            Console.WriteLine("*********\n" +
                              "HIGHSCORE\n" +
                              "*********\n\n" +
                              "Pressez [Enter] afin de revenir au menu");
            Console.ReadLine();
        }
        /// <summary>
        /// Méthode qui affiche les informations sur le projet
        /// </summary>
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

        /// <summary>
        /// Méthode de création du vaisseau
        /// </summary>
        static void ShipCreation()
        {
            Console.Clear(); // Efface le menu avant le vaisseau

            SpaceShip playerShip = new SpaceShip(10, 3, 20, 40);
            bool missileFired = false;
            playerShip.Draw();

            //Création des bunkers
            Bunker bunker1 = new Bunker(10, Console.WindowHeight - 15);
            Bunker bunker2 = new Bunker(30, Console.WindowHeight - 15);
            Bunker bunker3 = new Bunker(50, Console.WindowHeight - 15);

            //Affichage des bunkers
            bunker1.Draw();
            bunker2.Draw();
            bunker3.Draw();

            //Création des ennemis
            Enemies[]enemiesRow1 = new Enemies[10];
            Enemies[]enemiesRow2 = new Enemies[10];
            Enemies[]enemiesRow3 = new Enemies[10];

            //Affichage des ennemis ligne par ligne
            for (int i = 0; i < 10; i++)
            {
                enemiesRow1[i] = new Enemies(i * 5, 5, 1,  "┌¤■■¤┐");
                enemiesRow2[i] = new Enemies(i * 5, 10, 1, "┌¤■■¤┐");
                enemiesRow3[i] = new Enemies(i * 5, 15, 1, "┌¤■■¤┐");
            }

            

            bool isGameRunning = true;
            //boucle principale
            while (isGameRunning)
            {   //effacer les caractères à chaque saut de ligne
                foreach (Enemies enemy in enemiesRow1.Concat(enemiesRow2).Concat(enemiesRow3))
                {
                    enemy.Clear();
                }
                foreach (Enemies enemy in enemiesRow1)
                {
                    enemy.Update(0.1);
                    enemy.Draw();
                }

                foreach (Enemies enemy in enemiesRow2)
                {
                    enemy.Update(0.1);
                    enemy.Draw();
                }

                foreach (Enemies enemy in enemiesRow3)
                {
                    enemy.Update(0.1);
                    enemy.Draw();
                }
                //ajuster la vitesse de déplacment des ennemis
                System.Threading.Thread.Sleep(60);
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
                            if (!missileFired)
                            {
                                playerShip.FireMissile();
                                missileFired = true; // Mettre à jour l'état du tir du missile
                            }
                            break;

                            
                    }

                    double elapsedSeconds = 0.1;

                    playerShip.Update(elapsedSeconds);
                    playerShip.Draw();
                }
                else
                {
                    missileFired=false;
                }
                
                
                playerShip.DrawMissiles();
                playerShip.UpdateMissiles(0.1);

                
            } 

           
        }
    }
}
