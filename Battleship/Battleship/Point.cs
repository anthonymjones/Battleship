﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Point
    {   //enumerations defining the value of each PointStatus
        public enum PointStatus
        {
            Empty, Ship, Hit, Miss
        }
        //declare properties
        public int X { get; set; }
        public int Y { get; set; }
        public PointStatus Status { get; set; }

        //constructor
        public Point(int x, int y, PointStatus status)
    {
        this.X = x;
        this.Y = y;
        this.Status = status;
    }
    }
}