using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace RTSGame
{
    class Map 
    {
        #region Variables
        private const int MAX_RANDOM_UNITS = 50;
        public const string FIELD_SYMBOL = ".";
        private string[,] grid = new string[20, 20];
        private List<Unit> unitsOnMap = new List<Unit>();
        private int unitsOnMapNum = 0;
        private int buildingsOnMapNum = 0;
        #endregion

        #region Accessors

        public string[,] Grid
        {
            get { return grid; }
        }

        public List<Unit> UnitsonMap
        {
            get { return unitsOnMap; }
        }

        public int UnitsOnMapNum
        {
            get { return unitsOnMapNum; }
        }

        public int BuildingsOnMapNum
        {
            get { return buildingsOnMapNum; }
        }

        #endregion

        #region Methods

        public void populate()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = FIELD_SYMBOL;
                }
            }
        }

        //Places all the units on the Map.
        public void SetUnits()
        {
            Random rnd = new Random();
            int tempx;
            int tempy;
            for (int i = 0; i < MAX_RANDOM_UNITS; i++)
            {
                do
                {
                    tempx = rnd.Next(0, 19);
                    tempy = rnd.Next(0, 19);
                }
                while (grid[tempx, tempy] != FIELD_SYMBOL);
                if (i < rnd.Next(0, 51))
                {
                    bool attackOption;
                    int randomAttackRange;
                    string team;
                    string symbol;
                    attackOption = rnd.Next(0, 2) == 1 ? true : false;
                    randomAttackRange = rnd.Next(1, 3);
                    team = rnd.Next(0, 2) == 1 ? "Aisha's Warriors" : "Kruben's Knights";
                    symbol = "M";
                    Unit tmp = new MeleeUnit(tempx, tempy, 100, 1, attackOption, 1, team, symbol);
                    unitsOnMap.Add(tmp);
                    grid[unitsOnMap[unitsOnMapNum].X, unitsOnMap[unitsOnMapNum].Y] = unitsOnMap[unitsOnMapNum].Symbol;
                    unitsOnMapNum++;
                }
                else
                {
                    bool attackOption;
                    int randomAttackRange;
                    string team;
                    string symbol;
                    attackOption = rnd.Next(0, 2) == 1 ? true : false;
                    randomAttackRange = rnd.Next(1, 5);
                    team = rnd.Next(0, 2) == 1 ? "Aisha's Warriors" : "Kruben's Knights";
                    symbol = "R";

                    unitsOnMap.Add(new RangedUnit(tempx, tempy, 100, 1, attackOption, 5, team,symbol));
                    grid[unitsOnMap[unitsOnMapNum].X, unitsOnMap[unitsOnMapNum].Y] = unitsOnMap[unitsOnMapNum].Symbol;
                    unitsOnMapNum++;
                }
            }
        }

        private void moveOnMap(Unit u,int newX, int newY)
        {
            grid[u.X, u.Y] = FIELD_SYMBOL;
            grid[newX, newY] = u.Symbol;

        }

        public void update(Unit u, int newX, int newY)
        {
            if ((newX >= 0 && newX < 20) && (newY >= 0 && newY < 20))
            {
                moveOnMap(u, newX, newY);
                u.Move(newX, newY);
            }
        }

        //Checks the health of all the units and remove them from list if needed.
        public void checkHealth()
        {
            for (int i = 0; i < unitsOnMapNum; i++)
                if (unitsOnMap[i].IsDead())
                {
                    grid[unitsOnMap[i].X, unitsOnMap[i].Y] = FIELD_SYMBOL;
                    unitsOnMap.RemoveAt(i);
                    unitsOnMapNum--;
                }
        }
        #endregion
    }
}