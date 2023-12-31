﻿namespace backend.Game
{
    public struct Position
    {
        public int x { get; set; }

        public int y { get; set; }
    }

    public class Piece
    {
        public int id { get; set; }
        public bool isKing { get; set; } = false;
        public bool isAlive { get; set; } = true;
        public string color { get; set; } = "white";
        public Position position { get; set; }

        public void Die()
        {
            isAlive = false;
        }

        public void UnDie() 
        { 
            isAlive = true; 
        }

    }
}
