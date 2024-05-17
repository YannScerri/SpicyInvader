///ETMl
///Auteur : Yann Scerri
///Date : 18.01.2024
///Description : Programme principal du projet Spicy Invader
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Windows.Input;

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
        [STAThread]
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

            SpaceShip playerShip = new SpaceShip(10, 3, 20, 45);
            bool missileFired = false;
            playerShip.Draw();

            // Création des bunkers
            Bunker bunker1 = new Bunker(10, Console.WindowHeight - 15);
            Bunker bunker2 = new Bunker(30, Console.WindowHeight - 15);
            Bunker bunker3 = new Bunker(50, Console.WindowHeight - 15);

            // Affichage des bunkers
            bunker1.Draw();
            bunker2.Draw();
            bunker3.Draw();

            // Création des ennemis
            List<Enemies> enemiesList = new List<Enemies>();

            // Ajout des ennemis dans la liste
            for (int i = 0; i < 10; i++)
            {
                enemiesList.Add(new Enemies(i * 5, 5, 1, "┌¤■■¤┐"));
                enemiesList.Add(new Enemies(i * 5, 10, 1, "┌¤■■¤┐"));
                enemiesList.Add(new Enemies(i * 5, 15, 1, "┌¤■■¤┐"));
            }

            //variables pour le tir des ennemis
            double enemyMissileInterval = 2.0; // Tirer toutes les 2 secondes
            double timeSinceLastEnemyMissile = 0.0;

            bool isGameRunning = true;
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Boucle principale
            while (isGameRunning)
            {
                double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
                stopwatch.Restart();

                // Effacer les caractères à chaque saut de ligne
                foreach (Enemies enemy in enemiesList)
                {
                    enemy.Clear();
                }

                // Mettre à jour et dessiner les ennemis
                foreach (Enemies enemy in enemiesList)
                {
                    enemy.Update(elapsedSeconds);
                    enemy.Draw();
                }

                // Gérer le tir des missiles par les ennemis
                timeSinceLastEnemyMissile += elapsedSeconds;
                if (timeSinceLastEnemyMissile >= enemyMissileInterval)
                {
                    // Faire tirer un missile par un ennemi aléatoire
                    if (enemiesList.Count > 0)
                    {
                        Enemies randomEnemy = enemiesList[new Random().Next(enemiesList.Count)];
                        randomEnemy.FireMissile();
                    }
                    timeSinceLastEnemyMissile = 0.0;
                }

                // Mettre à jour et dessiner les missiles des ennemis
                foreach (Enemies enemy in enemiesList)
                {
                    enemy.UpdateMissiles(elapsedSeconds);
                }

                // Ajuster la vitesse du jeu
                System.Threading.Thread.Sleep(1);

                if (Keyboard.IsKeyDown(Key.Right))
                {
                    playerShip.OldPosition = playerShip.PositionX++;
                }
                if (Keyboard.IsKeyDown(Key.Left))
                {
                    playerShip.OldPosition = playerShip.PositionX--;
                }
                // Gérer les entrées de l'utilisateur
                //if (Console.KeyAvailable)
                //{
                //    ConsoleKeyInfo keyLeftRight = Console.ReadKey(true);

                //switch (keyLeftRight.Key)
                //{
                //    case ConsoleKey.LeftArrow:
                //        playerShip.OldPosition = playerShip.PositionX--;
                //        break;
                //    case ConsoleKey.RightArrow:
                //        playerShip.OldPosition = playerShip.PositionX++;
                //        break;
                //    case ConsoleKey.Spacebar:
                //        if (!missileFired)
                //        {
                //            playerShip.FireMissile();
                //            missileFired = true; // Mettre à jour l'état du tir du missile
                //        }
                //        break;
                //}

                //}
                foreach(Missile missile in playerShip.missileList)
                {
                    if (Keyboard.IsKeyDown(Key.Space))
                    {
                        if (!missileFired)
                        {
                            playerShip.FireMissile();
                            missileFired = true; // Mettre à jour l'état du tir du missile
                        }
                        else
                        {
                            missileFired = false;
                        }

                        playerShip.DrawMissiles();
                        playerShip.UpdateMissiles(elapsedSeconds);
                    }
                }
                
                playerShip.Update(elapsedSeconds);
                playerShip.Draw();
            }


        }
    }
}
