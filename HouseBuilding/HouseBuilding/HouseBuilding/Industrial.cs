using System;
using System.Collections.Generic;
using System.Text;

namespace HouseBuilding
{
    abstract class Industrial: IBuilding
    {
        public int NumberOfRooms { get; set; }
        public int NumberOfRoomsOnFloor { get; set; }
        public int NumberOfFloors { get; set; }
        public bool BuildingIsCompleted { get; set; }
        public Material Material;
        public Industrial(Material material, int numberOfRooms, int numberOfRoomsOnFloor)
        {

            NumberOfRooms = numberOfRooms;
            NumberOfRoomsOnFloor = numberOfRoomsOnFloor;
            this.Material = material;
            BuildingIsCompleted = false;
        }

        public abstract void BuildWalls();
        public abstract void BuildRoof();        

    }
}

