using System;

namespace start {
    class Killer : Robot {
        public int Health { get; set; }

        public Killer() { }
        public Killer(string name, int weight, byte[] coords, int health) : base(name, weight, coords) {
            this.Health = health;
        }

        public override void GetValues() {
            System.Console.WriteLine("{0} weight: {1}", this.Name, this.Weight);
            System.Console.WriteLine("Health:" + this.Health);

            foreach (byte el in Coords) {
                System.Console.WriteLine(el);
            }
        }

        public void Laser() {
            System.Console.WriteLine("SHOOT!");

            base.model = "SE-1234";
        }
    }
}