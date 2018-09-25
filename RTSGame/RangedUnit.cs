using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace RTSGame
{
    class RangedUnit : Unit
    {
        #region Variables

        protected const int DAMAGE = 100;

        #endregion

        #region Methods and Constructor

        public RangedUnit(int x, int y, int health, int speed, bool attack, int attackRange, string faction, string Symbol)
            : base(x, y, health, speed, attack, attackRange, faction, Symbol)
        {
        }

        ~RangedUnit()
        {

        }

        public override void Move(int x, int y)
        {
            if (x >= 0 && x < 20)
            {
                X = x;
            }
            if (y >= 0 && y < 20)
            {
                Y = y;
            }
        }

        public override void combat(Unit enemy)
        {
            if (this.InRange(enemy))
            {
                enemy.Health -= DAMAGE;
            }
        }

        public override bool InRange(Unit enemy)
        {
            if (!this.Faction.Equals(enemy.Faction))
            {
                if ((Math.Abs(this.X - enemy.X) <= this.AttackRange) && (Math.Abs(this.Y - enemy.Y) <= this.AttackRange))
                    return true;
            }
            return false;
        }

        public override Unit nearestUnit(List<Unit> list)
        {
            Unit closest = null;
            int attackRangeX, attackRangeY;
            double range;
            double shortestRange = 1000;

            foreach (Unit u in list)
            {
                if (!this.Faction.Equals(u.Faction))
                {
                    attackRangeX = Math.Abs(this.X - u.X);
                    attackRangeY = Math.Abs(this.Y - u.Y);

                    range = Math.Sqrt(Math.Pow(attackRangeX, 2) + Math.Pow(attackRangeY, 2));

                    if (range < shortestRange)
                    {
                        shortestRange = range;
                        closest = u;
                    }

                    if (attackRangeY < shortestRange)
                    {
                        shortestRange = range;
                        closest = u;
                    }
                }
            }
            return closest;
        }

        public override bool IsDead()
        {
            return (this.Health <= 0);
        }

        public override string toString()
        {
            string output = "x : " + X + Environment.NewLine
                + "y : " + Y + Environment.NewLine
                + "Speed : " + Speed + Environment.NewLine
                + "Attack : " + (Attack ? "Yes" : "No") + Environment.NewLine
                + "Attack Range : " + AttackRange + Environment.NewLine
                + "Faction/Team : " + Faction + Environment.NewLine
                + "Symbol : " + Symbol + Environment.NewLine;

            return output;
        }
        #endregion
    }
}
