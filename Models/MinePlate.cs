using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Models
{
    public class MinePlate
    {
        public int RowPos { get; private set; }
        public int ColumnPos { get; private set; }
        public bool IsFlagged { get; set; }
        public bool IsMined { get; set; }
        public bool IsRevealed { get; set; }
        public int IntDisplay { get; set; }
        public Object BtnButton { get; set; }

        public MinePlate(int row, int column, Object button)
        {
            RowPos = row;
            ColumnPos = column;
            BtnButton = button;
            IntDisplay = 0;
        }

    }
}
