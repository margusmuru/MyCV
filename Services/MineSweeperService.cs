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
using Services.Enums;

namespace Services
{
    public class MineSweeperService
    {
        private readonly List<MinePlate> _minePlates;

        public List<MinePlate> MinePlates => _minePlates;

        private bool _gameHasEnded;
        public bool GameHasEnded
        {
            get { return _gameHasEnded; }
        }

        private GameEndResult _gameEndResult;
        public GameEndResult GameEndResult {get
        {
            return _gameEndResult;
        }}

        private int _minesLeft;
        public int MinesLeft
        {
            get { return _minesLeft; }
        }


        public MineSweeperService()
        {
            _minePlates = new List<MinePlate>();
            _gameHasEnded = false;
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
            _minesLeft = count;
            Random rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                MinePlate plate = _minePlates[index: rnd.Next(maxValue: _minePlates.Count)];
                while (plate.IsMined)
                {
                    plate = _minePlates[index: rnd.Next(maxValue: _minePlates.Count)];
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

        private void SetNeighbourValues()
        {
            foreach (MinePlate plate in _minePlates)
            {
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

        public bool RevealButton(Button btn)
        {
            //find plate
            MinePlate plate = _minePlates.
                FirstOrDefault(predicate: x => Equals(objA: x.BtnButton as Button, objB: btn));
            if (plate == null)
            {
                throw new NullReferenceException();
            }
            if (plate != null && !plate.IsRevealed)
            {
                if (plate.IsMined)
                {
                    SetButtonVisuals(stat: MineStatEnum.IsBombed, btn: btn, plate: plate);
                    RevealAll();
                }
                else if (plate.IntDisplay > 0)
                {
                    SetButtonVisuals(stat: MineStatEnum.IsNumbered, btn: btn, plate: plate);
                }
                else
                {
                    SetButtonVisuals(stat: MineStatEnum.IsPlane, btn: btn, plate: plate);
                    List<MinePlate> neighbours = GetNeighboursList(plate: plate);
                    foreach (MinePlate neighbour in neighbours)
                    {
                        Button button = neighbour.BtnButton as Button;
                        RevealButton(btn: button);
                    }
                }
            }
            return CheckGameEndingConditions();
        }

        private void SetButtonVisuals(MineStatEnum stat, Button btn, MinePlate plate)
        {
            btn.IsEnabled = false;
            btn.Background = Brushes.DimGray;
            plate.IsRevealed = true;
            switch (stat)
            {
                case MineStatEnum.IsBombed:
                    btn.Content = "Boom";
                    btn.Background = Brushes.Crimson;
                    break;
                case MineStatEnum.IsNumbered:
                    btn.Content = plate.IntDisplay;
                    break;
                default:
                    btn.Content = "";
                    break;

            }
        }

        private List<MinePlate> GetNeighboursList(MinePlate plate)
        {
            return _minePlates.Where(predicate: x =>
                        (x.RowPos <= plate.RowPos + 1 && x.RowPos >= plate.RowPos - 1) &&
                        (x.ColumnPos <= plate.ColumnPos + 1 && x.ColumnPos >= plate.ColumnPos - 1) &&
                         x != plate)
                        .ToList();
        }

        private void RevealAll()
        {
            foreach (MinePlate plate in _minePlates)
            {
                Button btn = plate.BtnButton as Button;
                if (!plate.IsRevealed)
                {
                    if (plate.IsMined)
                    {
                        SetButtonVisuals(stat: MineStatEnum.IsBombed, btn: btn, plate: plate);
                    }
                    else if (plate.IntDisplay > 0)
                    {
                        SetButtonVisuals(stat: MineStatEnum.IsNumbered, btn: btn, plate: plate);
                    }
                    else
                    {
                        SetButtonVisuals(stat: MineStatEnum.IsPlane, btn: btn, plate: plate);
                    }
                }
            }
            _gameHasEnded = true;
        }

        public bool MarkButton(Button btn)
        {
            //find plate
            MinePlate plate = _minePlates.
                FirstOrDefault(predicate: x => Equals(objA: x.BtnButton as Button, objB: btn));
            if (plate == null)
            {
                throw new NullReferenceException();
            }
            if (plate.IsFlagged)
            {
                btn.Background = Brushes.DodgerBlue;
                plate.IsFlagged = false;
                _minesLeft++;
            }
            else
            {
                btn.Background = Brushes.BlueViolet;
                plate.IsFlagged = true;
                _minesLeft--;

            }
            return CheckGameEndingConditions();
        }

        private bool CheckGameEndingConditions()
        {
            if (GameHasEnded)
            {
                _gameEndResult = GameEndResult.Loss;
                return true;
            }
            //check if all correct bombs are marked
            bool allMarked = true;
            foreach (MinePlate plate in _minePlates)
            {
                if (plate.IsMined && !plate.IsFlagged)
                {
                    allMarked = false;
                    break;
                }
            }
            if (allMarked)
            {
                RevealAll();
                _gameEndResult = GameEndResult.Win;
            }
            return allMarked;
        }
    }
}
