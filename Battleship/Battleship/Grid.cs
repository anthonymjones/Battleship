using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Grid
    {
        //enumeration to define the PlaceShipDirection value
        public enum PlaceShipDirection { Horizontal, Vertical }

        //define properties
        public Point[,] Ocean { get; set; }
        public List<Ship> ListOfShips { get; set; }
        public bool AllShipsDestroyed { get { return ListOfShips.All(x => x.IsDestroyed); } }
        public int CombatRound { get; set; }


        //constructor
        public Grid()
        {
            //initilize ocean
            this.Ocean = new Point[10, 10];
            //fill each cell with a point
            //for each y
            for (int y = 0; y <= 9; y++)
            {
                //for each x
                for (int x = 0; x <= 9; x++)
                {
                    //fill the point with an Empty Status
                    this.Ocean[x, y] = new Point(x, y, Point.PointStatus.Empty);
                }
            }
            //initialize and fill list of ships with each type of ship
            this.ListOfShips = new List<Ship>() 
            { new Ship(Ship.ShipType.Carrier), new Ship(Ship.ShipType.Battleship), new Ship(Ship.ShipType.Cruiser), new Ship(Ship.ShipType.Submarine), new Ship(Ship.ShipType.Minesweeper) };
            //place each ship on the grid
            PlaceShip(this.ListOfShips[0], PlaceShipDirection.Horizontal, 0,2 );
            PlaceShip(this.ListOfShips[1], PlaceShipDirection.Vertical, 9,3 );
            PlaceShip(this.ListOfShips[2], PlaceShipDirection.Horizontal, 1,9 );
            PlaceShip(this.ListOfShips[3], PlaceShipDirection.Vertical, 7,5 );
            PlaceShip(this.ListOfShips[4], PlaceShipDirection.Horizontal, 2,3 );

        }
        //Methods and Functions
       public void PlaceShip(Ship shipToPlace, PlaceShipDirection direction, int startX, int startY)
        {
           //loop for the length of the ship being currently placed
            for (int i = 0; i < shipToPlace.Length; i++)
            {
                //add the shipToPlace to the occupied points 
                this.Ocean[startX, startY].Status = Point.PointStatus.Ship;
                shipToPlace.OccupiedPoints.Add(this.Ocean[startX, startY]);
                //if the direction is horizontal
                if (direction == PlaceShipDirection.Horizontal)
                {
                    //add ship points to the x axis for the length of the ship
                    startX++;
                }
                //the direction is vertical
                else
                {
                    //add ship points to the y axis for the length of the ship
                    startY++;
                }

            }
        }
        public void DisplayOcean()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"

 .########.....###....########.########.##.......########..######..##.....##.####.########.
 .##.....##...##.##......##.......##....##.......##.......##....##.##.....##..##..##.....##
 .##.....##..##...##.....##.......##....##.......##.......##.......##.....##..##..##.....##
 .########..##.....##....##.......##....##.......######....######..#########..##..########.
 .##.....##.#########....##.......##....##.......##.............##.##.....##..##..##.......
 .##.....##.##.....##....##.......##....##.......##.......##....##.##.....##..##..##.......
 .########..##.....##....##.......##....########.########..######..##.....##.####.##.......");
            //write the x axis to the console
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("      0   1   2   3   4   5   6   7   8   9   X");
            Console.WriteLine("     =======================================");
            
            for (int y = 0; y <= 9; y++)
            {
                //for each row (y), write the Y axis notation "0||"

                Console.Write(" {0}||", y);

                for (int x = 0; x <= 9; x++)
                {
                    //for each column (x)
                    switch (this.Ocean[x, y].Status)
                    {
                        case Point.PointStatus.Empty: Console.Write(" [ ]");
                            break;
                        case Point.PointStatus.Ship: Console.Write(" [ ]");
                            break;
                        case Point.PointStatus.Hit: Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(" [X]"); Console.ForegroundColor = ConsoleColor.Cyan;
                            break;
                        case Point.PointStatus.Miss: Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(" [O]");Console.ForegroundColor = ConsoleColor.Cyan;
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine(" Y");
            Console.WriteLine();
        }
        public void Target(int x, int y)
        {
            //get coordinates from the ocean by using x, y
            
            //PointStatus is Ship
            if (this.Ocean[x, y].Status == Point.PointStatus.Ship)
            {
                //change the Status to Hit
                this.Ocean[x, y].Status = Point.PointStatus.Hit;
            }
            //PointStatus is Empty
            else if(this.Ocean[x, y].Status == Point.PointStatus.Empty)
            {
                //change the Status to Miss
                this.Ocean[x, y].Status = Point.PointStatus.Miss;
            }
            
        }

        public void PlayGame()
        {
            //while not all ships are destroyed
            while(!AllShipsDestroyed)
            {
                //change the console window title
                Console.Title = "BattleShip";
                Console.SetWindowSize(93,42);

                //call the DisplayOcean function
                DisplayOcean();

                //define bools for user input is a true coordinate
                bool xCoordinate = false;
                bool yCoordinate = false;

                //define ints for input
                int xInput = 10;
                int yInput = 10;

                //get x coordinate
                while (!xCoordinate)
                {
                        
                        Console.WriteLine();
                        Console.WriteLine(" Captain, please select an x and y coordinate...");
                        Console.WriteLine();
                        Console.Write(" (");
                        char charInputX = Console.ReadKey().KeyChar;
                        if (char.IsDigit(charInputX))
                        {
                            xInput = int.Parse(charInputX.ToString());
                            xCoordinate = true;
                        }
                        else
                        {
                            DisplayOcean();
                            Console.WriteLine(" Invalid Input");
                        }
                    }
                    DisplayOcean();
                    //get y coordinate
                    while (!yCoordinate)
                    {

                        Console.WriteLine(" Captain, enter coordinates ({0}, Y)", xInput);
                        Console.Write(" ({0}, ", xInput);
                        char charInputY = Console.ReadKey().KeyChar;
                        if (char.IsDigit(charInputY))
                        {
                            yInput = int.Parse(charInputY.ToString());
                            yCoordinate = true;
                        }
                        else
                        {
                            DisplayOcean();
                            Console.WriteLine(" Invalid Input");
                        }
                    }

                    //call the target function
                    Target(xInput, yInput);

                    CombatRound++;
                }
                
                DisplayOcean();
                Console.WriteLine();
                //player found all ships, tell them
                Console.WriteLine(" You Win!! It took {0} rounds.", CombatRound);
            //game over
                Console.ReadKey();
            //System.Threading.Thread.Sleep(1500);
          //  AddHighScore(CombatRound);
          //  DisplayHighScores();
        }
      //  static void AddHighScore(int playerScore)
        //{
        //    //get the player name for high scores
        //    Console.Write(" Your name: ");
        //    string playerName = Console.ReadLine();

        //    //create a gateway to the database
        //    spAnthonyEntities db = new spAnthonyEntities();

        //    //create a new highscore object
        //    HighScore newHighScore = new HighScore();
        //    newHighScore.DateCreated = DateTime.Now;
        //    newHighScore.Game = "Battleship";
        //    newHighScore.Name = playerName;
        //    newHighScore.Score = playerScore;

        //    //add to the database
        //    db.HighScores.Add(newHighScore);

        //    //save our changes
        //    db.SaveChanges();
        //}
      ////  static void DisplayHighScores()
      //  {
      //      Console.SetWindowSize(40, 30);
      //      //clear the console
      //      Console.Clear();
      //      //Write the High Score Text
      //      Console.WriteLine();
      //      Console.WriteLine("       Battleship High Scores");
      //      Console.WriteLine("    *****************************");

      //      //create a new connection to the db
      //      spAnthonyEntities db = new spAnthonyEntities();
      //      //get the high score list
      //      List<HighScore> highScoreList = db.HighScores.Where(x => x.Game == "Battleship").OrderBy(x => x.Score).Take(10).ToList();

      //      foreach (HighScore highScore in highScoreList)
      //      {
      //          Console.WriteLine("    {0}. {1} - {2} on {3}", highScoreList.IndexOf(highScore) + 1, highScore.Name, highScore.Score, highScore.DateCreated.Value.ToShortDateString());
      //      }
      //      Console.ReadKey();
      //  }
    }
}