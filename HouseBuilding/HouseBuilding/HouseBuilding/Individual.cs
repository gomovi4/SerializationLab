using System;
using System.Collections.Generic;
using System.Text;

namespace HouseBuilding
{
    class Individual: Residential
    {
        
        private int numberOfRoomsOnFloor { get; set; }
       
        public Individual(Material material, int numberOfFloors, int _numberOfRoomsOnFloor) : base(material, numberOfFloors, 0)
        {
            numberOfRoomsOnFloor = _numberOfRoomsOnFloor;
            BuildingIsCompleted = false;
        }

        public override void BuildWalls()
        {
            int _numberOfRooms = numberOfRoomsOnFloor * NumberOfFloors;
            BuildingIsCompleted = true;
            Console.WriteLine($"You've built the walls of {Material}. Number of floors: {NumberOfFloors}. Number of rooms {_numberOfRooms}");
        }
        public override void BuildRoof()
        {
            Console.WriteLine($"Individual's roof is completed");
        }
    }
}
