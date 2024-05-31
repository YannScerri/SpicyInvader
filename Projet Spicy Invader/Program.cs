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
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace Projet_Spicy_Invader
{
    internal class Program
    {   //constantes pour la taille de la console
        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;
        [DllImport("user32.dll")] public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags); //attribut de la méthode dynamique et définition de la méthode DeleteMenu qui supprime le handle du menu, la position de l'élément à supprimer et définit un index de position

        [DllImport("user32.dll")] private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert); //définition de la méthode GetConsoleWindow qui obtient un handle pour la fenêtre

        [DllImport("kernel32.dll", ExactSpelling = true)] private static extern IntPtr GetConsoleWindow();

        [STAThread] //cet attribut indique que le modèle de threading d'application est à un seul thread d'appartement
        static void Main(string[] args)
        {   
            IntPtr handle = GetConsoleWindow(); IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);//redimensionner
            }
            // Ajuster la taille de la fenêtre
            Console.WindowHeight = 50;
            Console.WindowWidth = 85;

            //empêcher le scrolling vertical
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);


            //efface le curseur clignotant
            Console.CursorVisible = false;

            do
            {
                // Affichage du menu
                Console.WriteLine("" +
                                  " ____  ____  _  ____ ___  _   _  _      _     ____  ____  _____ ____ \n" +
                                  "/ ___\\/  __\\/ \\/   _\\\\  \\//  / \\/ \\  /|/ \\ |\\/  _ \\/  _ \\/  __//  __\\\n" +
                                  "|    \\|  \\/|| ||  /   \\  /   | || |\\ ||| | //| / \\|| | \\||  \\  |  \\/|\n" +
                                  "\\___ ||  __/| ||  \\_  / /    | || | \\||| \\// | |-||| |_/||  /_ |    /\n" +
                                  "\\____/\\_/   \\_/\\____//_/     \\_/\\_/  \\|\\__/  \\_/ \\|\\____/\\____\\\\_/\\_\\\n" +
                                  
                                  "\n" +
                                  "[1] Jouer\n" +
                                  "[2] Options\n" +
                                  "[3] Highscore\n" +
                                  "[4] A propos\n" +
                                  "[5] Quitter\n") ;

                // Permet d'attribuer les touches du clavier aux cases
                ConsoleKeyInfo keyMenu = Console.ReadKey();

                // Options du menu
                switch (keyMenu.KeyChar)
                {   //Jeu
                    case '1':
                        GameCreation();
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
                              "\n\n" +
                              "Règles :\n" +
                              "Déplacement : touches flechées\n" +
                              "Tir : Barre espace\n\n" +
                              "Pressez [Enter] afin de revenir au menu");

            Console.ReadLine();
        }

        /// <summary>
        /// Méthode de création des éléments du jeu
        /// </summary>
        static void GameCreation()
        {
            int updateCounter = 0; //compteur pour la descente des ennemis
            int updateThreshold = 10;
            Console.Clear(); // Efface le menu avant le vaisseau

            SpaceShip playerShip = new SpaceShip(10, 3, 20, 45);
            
            playerShip.Draw();

            //création des bunkers via une liste
            List<Bunker> bunkers = new List<Bunker>
            {
            new Bunker(10, Console.WindowHeight - 15),
            new Bunker(30, Console.WindowHeight - 15),
            new Bunker(50, Console.WindowHeight - 15),
            new Bunker(70, Console.WindowHeight - 15)
            };

            //dessiner les bunkers
            foreach(Bunker bunker in bunkers)
            {
                bunker.Draw();
            } 
            
            //SpecialEnemy specialEnemy = null;
            //double timeUntilSpecialEnemy = new Random().Next(4, 8);
            //double elapsedTime = 0;

            // Création de la liste des ennemis
            List<Enemies> enemiesList = new List<Enemies>();

            // Ajout des ennemis dans la liste
            for (int i = 0; i < 10; i++)
            {
                enemiesList.Add(new Enemies(i * 7 + 5, 0, 1, "┌¤■■¤┐"));
                enemiesList.Add(new Enemies(i * 7 + 5, 1, 1, "┌¤■■¤┐"));
                enemiesList.Add(new Enemies(i * 7 + 5, 2, 1, "┌¤■■¤┐"));
            }

            //variables pour le tir des ennemis
            double enemyMissileInterval = 2.0; // Tirer toutes les 2 secondes
            double timeSinceLastEnemyMissile = 0.0; 

            bool isGameRunning = true; //booléen pour démarrer la boucle principale
            bool Victory = false; //booléen pour savoir si le joueur a gagné (démarre à false)
            Stopwatch stopwatch = Stopwatch.StartNew(); //nouveau timer

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

                    if (enemy.EnemiesY == Console.WindowHeight - 5)
                        isGameRunning = false;
                }

                // Utilisez le compteur pour ralentir la descente des ennemis
                updateCounter++;
                if (updateCounter >= updateThreshold)
                {
                    foreach (Enemies enemy in enemiesList)
                    {
                        if (enemy.EnemiesX == 2 || enemy.EnemiesX == Console.WindowWidth - 7)
                        {
                            foreach (Enemies enemies in enemiesList)
                            {
                                enemies.EnemiesDown();
                                Console.SetCursorPosition(0, enemies.EnemiesY - 1);
                                Console.Write("                                                                                     ");
                            }
                            break;
                        }
                    }
                    updateCounter = 0; // Réinitialiser le compteur
                }

                // Vérifiez les collisions entre les missiles du vaisseau et les ennemis
                foreach (Enemies enemy in enemiesList.ToList())
                {
                    // Vérifie si un missile du vaisseau touche l'ennemi
                    if (playerShip.Missile != null &&
                        playerShip.Missile.PositionX >= enemy.EnemiesX &&
                        playerShip.Missile.PositionX < enemy.EnemiesX + enemy.EnemyDesign.Length &&
                        playerShip.Missile.PositionY == enemy.EnemiesY)
                    {
                        // Si une collision est détectée, supprime l'ennemi touché et le missile
                        enemy.ClearEnemy();
                        enemiesList.Remove(enemy);
                        playerShip.ClearMissile();

                        // Sort de la boucle pour ne détecter qu'une collision à la fois
                        break;
                    }
                }

                // Vérifiez les collisions entre les missiles ennemis et le vaisseau
                foreach (Enemies enemy in enemiesList.ToList())
                {
                    if (enemy.Missile != null && playerShip.CheckCollision(enemy.Missile.PositionX, enemy.Missile.PositionY))
                    {
                        playerShip.Lives--;
                        enemy.ClearMissile();
                        break;
                    }
                }

                // Gérer le tir des missiles par les ennemis
                timeSinceLastEnemyMissile += elapsedSeconds;
                if (timeSinceLastEnemyMissile >= enemyMissileInterval)
                {
                    if (enemiesList.Count > 0)
                    {
                        Enemies randomEnemy = enemiesList[new Random().Next(enemiesList.Count)]; //faire tirer par un ennemi aléatoire
                        randomEnemy.FireMissile();
                    }
                    timeSinceLastEnemyMissile = 0.0;
                }

                // Mettre à jour et dessiner les missiles des ennemis
                foreach (Enemies enemy in enemiesList)
                {
                    enemy.UpdateMissiles(elapsedSeconds);
                }

                if (enemiesList.Count == 0)
                {
                    Victory = true;
                    break;
                }

                System.Threading.Thread.Sleep(1); //thread pour gérer la vitesse générale du jeu

                if (Keyboard.IsKeyDown(Key.Right))
                {
                    if(playerShip.PositionX < Console.WindowWidth - 5)
                    playerShip.OldPosition = playerShip.PositionX++;
                }
                if (Keyboard.IsKeyDown(Key.Left))
                {
                    if(playerShip.PositionX >= 2)
                    playerShip.OldPosition = playerShip.PositionX--;
                }
                if (Keyboard.IsKeyDown(Key.Space))
                {
                    playerShip.FireMissile();
                }

                playerShip.DrawMissiles();
                playerShip.Update(elapsedSeconds);
                playerShip.Draw();

                // Vérifiez les collisions entre les missiles et les bunkers
                if (playerShip.Missile != null)
                {
                    if (CheckBunkerCollision(playerShip.Missile.PositionX, playerShip.Missile.PositionY, bunkers))
                    {
                        playerShip.ClearMissile();
                    }
                }
                foreach (Enemies enemy in enemiesList)
                {
                    if (enemy.Missile != null)
                    {
                        if (CheckBunkerCollision(enemy.Missile.PositionX, enemy.Missile.PositionY, bunkers))
                        {
                            enemy.ClearMissile();
                        }
                    }
                }

                // Vérifiez les collisions entre les missiles et les bunkers
                 bool CheckBunkerCollision(int missileX, int missileY, List<Bunker> Bunkers)
                {
                    foreach (Bunker bunker in Bunkers)
                    {
                        if (missileX >= bunker.PositionX && missileX < bunker.PositionX + 10 &&
                            missileY >= bunker.PositionY && missileY < bunker.PositionY + 5)
                        {
                            bunker.TakeDamage(missileX, missileY);
                            return true; // Missile touche un bunker
                        }
                    }
                    return false; // Missile ne touche pas de bunker
                }




                // Vérifier si le vaisseau est détruit et afficher un game over
                if (!playerShip.IsAlive())
                {
                    
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("   _____                         ____                 \n" +
                                      "  / ____|                       / __ \\                \n" +
                                      " | |  __  __ _ _ __ ___   ___  | |  | |_   _____ _ __ \n" +
                                      " | | |_ |/ _` | '_ ` _ \\ / _ \\ | |  | \\ \\ / / _ \\ '__|\n" +
                                      " | |__| | (_| | | | | | |  __/ | |__| |\\ V /  __/ |   \n" +
                                      "  \\_____|\\__,_|_| |_| |_|\\___|  \\____/  \\_/ \\___|_|  ");
                    Console.ResetColor();
                    while (Console.KeyAvailable) { Console.ReadKey(true); } //régler les problèmes d'afficahge après le GameOver
                    Console.ReadLine();
                    Console.Clear();
                    // Sortir de la boucle principale
                    break;
                }
            }

            //vérifier si les ennemis ont tous été détruits pour afficher un écran de victoire
            if (Victory)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  ___                    _ \n" +
                                  " | _ )_ _ __ ___ _____  | |\n" +
                                  " | _ \\ '_/ _` \\ V / _ \\ |_|\n" +
                                  " |___/_| \\__,_|\\_/\\___/ (_)");
                Console.ResetColor();
                while (Console.KeyAvailable) { Console.ReadKey(true); } //régler les problèmes d'afficahge après le Bravo
                Console.ReadLine();
                Console.Clear();
            }




        }
    }
}
