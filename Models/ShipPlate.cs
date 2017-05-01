using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ShipPlate : MinePlate
    {
        public ShipPlate(int row, int column, object button) : base(row: row, column: column, button: button)
        {
        }
    }
}
