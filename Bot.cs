using System;

namespace start {
    class Bot : Robot {
        public Bot() { }
        public Bot(string name, int weight, byte[] coords) : base(name, weight, coords) {

        }

        public override void GetValues() {
            System.Console.WriteLine("{0} weight: {1}", this.Name, this.Weight);

            foreach (byte el in Coords) {
                System.Console.WriteLine(el);
            }
        }
    }
}