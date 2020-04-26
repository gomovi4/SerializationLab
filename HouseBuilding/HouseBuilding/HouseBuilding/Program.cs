using System;

namespace HouseBuilding
{
    class Program
    {
        static void Main(string[] args)
        {
            Factory factory = new Factory();
            Multistorey multistorey = new Multistorey(Material.Brick,4,2,3);
            Individual individual = new Individual(Material.Wood, 2, 2);
            Build(factory);
            Build(multistorey);
            Build(individual);
        }

        public static void Build(IBuilding building)
        {
            building.BuildWalls();
            building.BuildRoof();
        }
    }
}
