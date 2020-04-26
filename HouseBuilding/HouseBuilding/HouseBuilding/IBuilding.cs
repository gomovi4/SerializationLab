using System;
using System.Collections.Generic;
using System.Text;

namespace HouseBuilding
{
    enum Material
    {
        Brick,
        Concrete,
        Wood
    }
    interface IBuilding
    {
        public int NumberOfFloors { get; set; }
        public bool BuildingIsCompleted { get; set; }

        void BuildWalls();
        void BuildRoof();
    }
}
