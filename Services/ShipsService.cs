using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Models;
using Services.Enums;

namespace Services
{
    public class ShipsService
    {
        public List<ShipPlate> ShipPlates { get; private set; }

        public int ShipsLeft { get; private set; }

        public bool GameHasEnded { get; private set; }

        private readonly Random _rnd;

        public ShipsService()
        {
            ShipPlates = new List<ShipPlate>();
            _rnd = new Random();
        }

        public void AddPlate(Button btnButton, int x, int y)
        {
            ShipPlate plate = new ShipPlate(row: y, column: x, button: btnButton);
#if DEBUG
            Trace.WriteLine(message: "Adding plate btn " +  plate.BtnButton.ToString());
#endif
            ShipPlates.Add(item: plate);
        }

        public void GenerateShips(int count, Random rnd)
        {
            if (ShipPlates.Count == 0)
            {
                throw new Exception(message: "There must be plates to add ships");
            }
            ShipsLeft = count;
            
            for (int i = 0; i < count; i++)
            {
                MinePlate plate = null;
                while (plate == null || plate.IsMined)
                {
                    plate = ShipPlates[index: rnd.Next(maxValue: ShipPlates.Count)];
                }
                plate.IsMined = true;
#if DEBUG
                Button btn = plate.BtnButton as Button;
                if (btn == null)
                {
                    throw new NullReferenceException();
                }
                btn.Content = "X";
#endif
            }
        }

        public bool RevealButton(Button btn)
        {
            ShipPlate plate = ShipPlates.
                FirstOrDefault(predicate: x => Equals(objA: x.BtnButton as Button, objB: btn));
            if (plate == null)
            {
                throw new NullReferenceException(message: "Plate not found from " + ShipPlates.Count);
            }
            if (!plate.IsRevealed)
            {
                SetButtonVisuals(stat: plate.IsMined ? ShipStat.Ship : ShipStat.Water, btn: btn, plate: plate);
            }
            return CheckGameEndingConditions();
        }

        public bool RevealRandomButton()
        {
            ShipPlate plate = null;
            while (plate == null || plate.IsRevealed)
            {
                plate = ShipPlates[index: _rnd.Next(maxValue: ShipPlates.Count)];
                if (plate == null)
                {
                    plate = ShipPlates[index: 0];
                    break;
                }
            }
            return RevealButton(btn: plate.BtnButton as Button);
        }

        private void SetButtonVisuals(ShipStat stat, Button btn, ShipPlate plate)
        {
            btn.IsEnabled = false;
            btn.Background = Brushes.DimGray;
            plate.IsRevealed = true;
            switch (stat)
            {
                case ShipStat.Ship:
                    btn.Content = "X";
                    ShipsLeft--;
                    btn.Background = Brushes.Crimson;
                    break;
                case ShipStat.Water:
                    btn.Content = "";
                    break;
                default:
                    btn.Content = "";
                    break;

            }
        }

        public void RevealAll()
        {
            foreach (ShipPlate plate in ShipPlates)
            {
                Button btn = plate.BtnButton as Button;
                if (!plate.IsRevealed)
                {
                    SetButtonVisuals(stat: plate.IsMined ? ShipStat.Ship : ShipStat.Water, btn: btn, plate: plate);
                }
            }
            GameHasEnded = true;
        }

        private bool CheckGameEndingConditions()
        {
            bool allMarked = true;
            foreach (MinePlate plate in ShipPlates)
            {
                if (plate.IsMined && !plate.IsRevealed)
                {
                    allMarked = false;
                    break;
                }
            }
            if (allMarked)
            {
                RevealAll();
                GameHasEnded = true;
            }
            return allMarked;
        }

    }
}
