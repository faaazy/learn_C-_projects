namespace start {
    public class Program {
        static void Main() {

            Book newBook = new Book();

            newBook.setValues("Новая книга", "Империал хал");
            newBook.printValues();

            Bot bot = new Bot("bot", 200, [1, 2, 3]);
            Bot bot1 = new Bot();

            Killer killer = new Killer("killer", 600, [3, 2, 1], 100);
            // killer.Laser();

            // bot.GetValues();
            // killer.GetValues();

            // Robot.count = 5;

            // bot.Weight = -100;

            // List<Killer> robots =
            // [
            //     new Killer("Alex", 400, [1, 2, 3], 100),
            //     new Killer("Bob", 600, [3, 2, 3], 100),
            //     new Killer("Jonn", 200, [4, 16, 3], 100),
            //     new Killer("Sam", 700, [11, 22, 33], 100),
            // ];

            // Robot? newRobot = null;

            // foreach (Killer obj in robots) {
            //     if (obj.Name == "John") {
            //         newRobot = obj as Robot;
            //     }

            //     Console.WriteLine(obj is Robot);
            // }
        }


    }
}