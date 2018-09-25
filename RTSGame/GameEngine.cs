using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RTSGame
{
    class GameEngine
    {
        #region Variables
        Map map = new Map();
        #endregion

        #region Methods
        public void Initialize()
        {
            map.populate();
        }

        public void setGame()
        {
            map.SetUnits();
        }

        public void Combat()
        {
            Random rnd = new Random();

            for (int i = 0; i < map.UnitsOnMapNum - 1; i++)
            {
                map.checkHealth();
                Unit closest = map.UnitsonMap[i].nearestUnit(map.UnitsonMap);
                if (map.UnitsonMap[i].Health < 25)
                {
                    map.update(map.UnitsonMap[i], map.UnitsonMap[i].X, map.UnitsonMap[i].Y);
                }

                if ((map.UnitsonMap[i].InRange(closest)))
                {
                    map.UnitsonMap[i].combat(closest);
                }

                if ((map.UnitsonMap[i].X < closest.X))
                {
                    if (map.Grid[map.UnitsonMap[i].X + 1, map.UnitsonMap[i].Y] == ".")
                    {
                        map.update(map.UnitsonMap[i], map.UnitsonMap[i].X + 1, map.UnitsonMap[i].Y);
                    }
                }

                if ((map.UnitsonMap[i].X > closest.X))
                {
                    if (map.Grid[map.UnitsonMap[i].X - 1, map.UnitsonMap[i].Y] == ".")
                    {
                        map.update(map.UnitsonMap[i], map.UnitsonMap[i].X - 1, map.UnitsonMap[i].Y);
                    }
                }

                if ((map.UnitsonMap[i].Y < closest.Y))
                {
                    if (map.Grid[map.UnitsonMap[i].X, map.UnitsonMap[i].Y + 1] == ".")
                    {
                        map.update(map.UnitsonMap[i], map.UnitsonMap[i].X, map.UnitsonMap[i].Y + 1);
                    }
                }

                if ((map.UnitsonMap[i].Y > closest.Y))
                {
                    if (map.Grid[map.UnitsonMap[i].X, map.UnitsonMap[i].Y - 1] == ".")
                    {
                        map.update(map.UnitsonMap[i], map.UnitsonMap[i].X, map.UnitsonMap[i].Y - 1);
                    }
                }
            }
        }
        #endregion

        #region Accessors
        public Map Map
        {
            get { return map; }
        }
        #endregion
    }
}
