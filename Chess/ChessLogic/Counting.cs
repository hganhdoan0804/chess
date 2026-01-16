using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Counting
    {
        private readonly Dictionary<PieaceType, int> whiteCount = new();
        private readonly Dictionary<PieaceType, int> blackCount = new();
        public int TotalCount { get; private set; }

        public Counting()
        {
            foreach(PieaceType type in Enum.GetValues(typeof(PieaceType)))
            {
                whiteCount[type] = 0;
                blackCount[type] = 0;
            }
        }

        public void Increment(Player color, PieaceType type)
        {
            if(color == Player.White)
            {
                whiteCount[type]++;
            }
            else if(color == Player.Black)
            {
                blackCount[type]++;
            }
            TotalCount++;
        }

        public int White(PieaceType type)
        {
            return whiteCount[type];
        }

        public int Black(PieaceType type)
        {
            return blackCount[type];
        }
    }
}
