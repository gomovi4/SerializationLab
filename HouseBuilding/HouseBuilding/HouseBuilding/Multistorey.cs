using System;
using System.Collections.Generic;
using System.Text;

namespace HouseBuilding
{
    class Multistorey : Residential
    {
        private int section { get; set; }
        public Multistorey(Material material, int numberOfFloors, int numberOfFlatsOnFloor, int _section) : base(material,numberOfFloors,numberOfFlatsOnFloor) 
        {
            section = _section;
            BuildingIsCompleted = false;
        }
        
            public override void BuildWalls()
        {
            int numberOfFlats = NumberOfFlatsOnFloor * NumberOfFloors*section;
            BuildingIsCompleted = true;
            Console.WriteLine($"You've built the walls of {Material}. Number of floors: {NumberOfFloors}. Number of sections: {section} Number of flats: {numberOfFlats}");
        }
        public override void BuildRoof()
        {
            Console.WriteLine($"Multifloor's roof is completed");
        }
    }
}
