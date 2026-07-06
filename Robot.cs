using System;

namespace start {
    public class Robot {

        public static int count;

        private string name = "";
        private int weight;
        private byte[] coords = [];

        public Robot(string name, int weight, byte[] coords) {
            SetValues(name, weight, coords);
        }

        public Robot() { }

        public void SetValues(string name, int weight, byte[] coords) {
            this.name = name;
            this.weight = weight;
            this.coords = coords;
        }

        public void GetValues() {
            System.Console.WriteLine("{0} weight: {1}", name, weight);

            foreach (byte el in coords) {
                System.Console.WriteLine(el);
            }
        }
    }
}