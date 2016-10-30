﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaJiang.Model.Interfaces
{
    public interface IPlayerAction
    {
        void Draw(Tile tile);

        void Pong(Tile tile);

        void Kong(Tile tile);

        void Chow(Tile tile);

        void Discard(Tile tile);

        void DiscardByOther(Tile tile);

    }
}
