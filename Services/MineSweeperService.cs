using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Models;

namespace Services
{
    public class MineSweeperService
    {
        private readonly List<MinePlate> _minePlates;

        public List<MinePlate> MinePlates => _minePlates;


        public MineSweeperService()
        {
            _minePlates = new List<MinePlate>();
        }

        public void AddPlate(Button btnButton, int x, int y)
        {
            MinePlate plate = new MinePlate(column: y, row: x, button: btnButton);
            _minePlates.Add(item: plate);
            
        }

        public void SetupGameField()
        {
            SetupMines(count: 10);
            SetNeighbourValues();
        }

        private void SetupMines(int count)
        {
            Random rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                MinePlate plate = _minePlates[index: rnd.Next(maxValue: _minePlates.Count)];
                plate.IsMined = true;
#if DEBUG
                Button btn = plate.BtnButton as Button;
                btn.Content = "X";
#endif
            }
        }

        private void SetNeighbourValues()
        {
            foreach (MinePlate plate in _minePlates)
            {
                //setup values for plates in each direction
                if (plate.IsMined)
                {
                    
                    foreach (MinePlate minePlate in GetNeighboursList(plate: plate))
                    {
                        if (!minePlate.IsMined) IncreasePlateValue(plate: minePlate);
                    }
                }

            }
        }

        private void IncreasePlateValue(MinePlate plate)
        {
            plate.IntDisplay++;
#if DEBUG
            Button btn = plate.BtnButton as Button;
            if (btn != null) btn.Content = plate.IntDisplay;
#endif

        }

        public void RevealButton(Button btn)
        {
            //find plate
            MinePlate plate = _minePlates.
                FirstOrDefault(predicate: x => Equals(objA: x.BtnButton as Button, objB: btn));
            if (plate != null && !plate.IsRevealed)
            {
                plate.IsRevealed = true;
                btn.IsEnabled = false;
                btn.Background = Brushes.DimGray;

                if (plate.IsMined)
                {
                    btn.Content = "Boom";
                }
                else if (plate.IntDisplay > 0)
                {
                    btn.Content = plate.IntDisplay;
                }
                else
                {
                    btn.Content = "";
                    List<MinePlate> neighbours = GetNeighboursList(plate: plate);
                    foreach (MinePlate neighbour in neighbours)
                    {
                        Button button = neighbour.BtnButton as Button;
                        RevealButton(btn: button);
                    }
                }
            }
            
        }

        private List<MinePlate> GetNeighboursList(MinePlate plate)
        {
            return _minePlates
                        .Where(predicate: x =>
                        (x.RowPos <= plate.RowPos + 1 && x.RowPos >= plate.RowPos - 1) &&
                        (x.ColumnPos <= plate.ColumnPos + 1 && x.ColumnPos >= plate.ColumnPos - 1) &&
                        x != plate)
                        .ToList();
        }

    }
}
