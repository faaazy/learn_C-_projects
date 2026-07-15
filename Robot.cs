using System;

namespace start {
    abstract class Robot : IRun, IJump {

        public static int count;

        private string name = "";
        private int weight;
        private byte[] coords = [];

        protected string model = "";

        public string Name {
            get {
                return name;
            }

            private set { }
        }

        public int Weight {
            get {
                System.Console.Write("Res: ");
                return this.weight;
            }

            set {
                if (value < 1)
                    this.weight = 1;
                else
                    this.weight = value;
            }
        }

        public byte[] Coords {
            get {
                return this.coords;
            }
            set { }
        }

        public int Width { get; set; }
        public float Speed { get; set; }
        public float y { get; set; }

        public Robot(string name, int weight, byte[] coords) {
            SetValues(name, weight, coords);
        }

        public Robot() { }

        public void SetValues(string name, int weight, byte[] coords) {
            this.name = name;
            this.weight = weight;
            this.coords = coords;
        }

        public abstract void GetValues();

        public void RobotRun() {
            System.Console.WriteLine("Robot run!");
        }

        public void Jump() {
            System.Console.WriteLine("Robot is jumping!");
        }
    }
}