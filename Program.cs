using System;

namespace start {
    public class Program {
        static void Main() {
            Robot bot = new Robot("bot", 200, [1, 2, 3]);


            Robot killer = new Robot();
            killer.SetValues("killer", 600, [3, 2, 1]);

            bot.GetValues();
            killer.GetValues();

            Robot.count = 5;
        }


    }
}