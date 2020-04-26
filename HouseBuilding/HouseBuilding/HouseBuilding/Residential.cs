using System;
using System.Collections.Generic;
using System.Text;

namespace HouseBuilding
{
    abstract class Residential : IBuilding
    {

        public int NumberOfFlatsOnFloor { get; set; }
        public int NumberOfFloors { get; set; }
        public bool BuildingIsCompleted { get; set; }
        public Material Material;
        public Residential(Material material, int numberOfFloors, int numberOfFlatsOnFloor)
        {

            NumberOfFloors = numberOfFloors;
            NumberOfFlatsOnFloor = numberOfFlatsOnFloor;
            this.Material = material;
            BuildingIsCompleted = false;
        }

        public abstract void BuildWalls();
        public abstract void BuildRoof();
        

    }
}

