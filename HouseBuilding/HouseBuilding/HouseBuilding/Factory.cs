using System;
using System.Collections.Generic;
using System.Text;

namespace HouseBuilding
{
    class Factory : Industrial
    {
        public Factory() : base(Material.Concrete, 0, 0)
        { 
        
        }
        public override void BuildRoof()
        {
            Console.WriteLine ("Factory walls are built");
        }

        public override void BuildWalls()
        {
            Console.WriteLine("Factory roof is built");
        }
    }
}
