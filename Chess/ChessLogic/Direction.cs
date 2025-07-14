using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Direction
    {
        public static readonly Direction Up = new Direction(-1, 0);
        public static readonly Direction Down = new Direction(1, 0);
        public static readonly Direction Left = new Direction(0, -1);
        public static readonly Direction Right = new Direction(0, 1);
        public static readonly Direction UpLeft = new Direction(-1, -1);
        public static readonly Direction UpRight = new Direction(-1, 1);
        public static readonly Direction DownLeft = new Direction(1, -1);
        public static readonly Direction DownRight = new Direction(1, 1);

        public int RowDelta { get; }
        public int ColumnDelta { get; }
        public Direction(int rowDelta, int columnDelta)
        {
            RowDelta = rowDelta;
            ColumnDelta = columnDelta;
        }

        public static Direction operator +(Direction a, Direction b)
        {
            return new Direction(a.RowDelta + b.RowDelta, a.ColumnDelta + b.ColumnDelta);
        }

        public static Direction operator *(int scalar, Direction direction) 
        { 
            return new Direction(scalar * direction.RowDelta, scalar * direction.ColumnDelta);
        }
    } 
}
