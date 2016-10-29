using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    class BasicHitStrategy : IHitStrategy
    {
        private const int g_hitLimit = 17;
        public bool DoHit(Player theDealer) { return theDealer.CalcScore() < g_hitLimit; }
    }
}