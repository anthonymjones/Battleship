using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid grid = new Grid();
            //Console.WriteLine(grid.Ocean[1, 1].Status);
            grid.PlayGame(); 

        }
    }
}
