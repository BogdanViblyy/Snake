using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using NAudio.Wave;
using System.Media;
using static System.Formats.Asn1.AsnWriter;




namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            Sounds sound = new Sounds();
            Thread backgroundMusicThread = new Thread(sound.PlayBackgroundMusic);

            Difficulty easyDifficulty = new Difficulty{FieldColor = ConsoleColor.Green, FieldSize = 80, DeadlyFood=false };

            Difficulty mediumDifficulty = new Difficulty { FieldColor = ConsoleColor.Yellow, FieldSize =60, DeadlyFood = false };

            Difficulty hardDifficulty = new Difficulty { FieldColor = ConsoleColor.Red, FieldSize = 40, DeadlyFood = true };

            Difficulty gameDifficulty = null;

            Console.WriteLine("Choose difficulty: ");
            Console.WriteLine("Easy - 1");
            Console.WriteLine("Medium - 2");
            Console.WriteLine("Hard - 3");
            




            int difficultyChoice = Convert.ToInt32(Console.ReadLine());
            if (difficultyChoice<1 || difficultyChoice > 3)
            {
                Console.WriteLine("Invalid difficulty. default difficulty - Medium\nPress enter to start");
                gameDifficulty = mediumDifficulty;
                Console.ReadKey();
            }
            else if (difficultyChoice==1)
            {
                gameDifficulty = easyDifficulty;
            }
            else if (difficultyChoice==2) 
            {
                gameDifficulty = mediumDifficulty;
            }
            else
            {
                gameDifficulty= hardDifficulty;
            }
            



            Console.Clear();
            Console.ForegroundColor = gameDifficulty.FieldColor;

            Walls walls = new Walls(gameDifficulty.FieldSize, 24);
            walls.Draw();

            Point p = new Point(4, 5, '*');
            Score score = new Score();
            Snake snake = new Snake(p, 4, Direction.RIGHT, score);
            snake.Draw();

            FoodCreator foodCreator = new FoodCreator(gameDifficulty.FieldSize - 1, 24, '$');
            Point food = foodCreator.CreateFood();
            food.Draw();

            FoodCreator badfoodCreator = new FoodCreator(gameDifficulty.FieldSize - 1, 24, '&');
            Point badfood = badfoodCreator.CreateFood();
            badfood.Draw();

            FoodCreator poisonfoodCreator = new FoodCreator(gameDifficulty.FieldSize - 1, 24, '-');
            Point poisonfood = poisonfoodCreator.CreateFood();
            poisonfood.Draw();





            while (true)
            {
                if (walls.IsHit(snake) || snake.IsHitTail())
                {
                    break;
                }
                if (snake.Eat(food))
                {
                    food = foodCreator.CreateFood();
                    food.Draw();
                }
                if (snake.BadEat(badfood))
                {
                    badfood = badfoodCreator.CreateFood();
                    badfood.Draw();
                }
                if (snake.Eat(poisonfood))
                {

                    score.MinusScore();
                    break;
                }
                else
                {
                    snake.Move();
                }

                Thread.Sleep(100);
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.HandleKey(key.Key);
                }
            }

            WriteGameOver(score);
            Console.ReadLine();
        }

        static void WriteGameOver(Score score)
        {
            int xOffset = 25;
            int yOffset = 8;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(xOffset, yOffset++);
            WriteText("============================", xOffset, yOffset++);
            WriteText("     G A M E   O V E R", xOffset + 1, yOffset++);
            yOffset++;
            WriteText("Author: Bogdan Viblyy", xOffset + 2, yOffset++);

            WriteText("============================", xOffset, yOffset++);
            WriteText("Enter your name:", xOffset + 2, yOffset++);
            string name = InputText(xOffset + 3, yOffset++);
            int finalScore = score.GetScore();

            using (StreamWriter writer = File.AppendText("Users.txt"))
            {
                writer.WriteLine(name + ' ' + finalScore);
            }

            WriteText("============================", xOffset, yOffset++);
            WriteText($"Your score: {finalScore}", xOffset + 2, yOffset++);
            WriteText($"Press enter to view top 15 of player scores", xOffset + 1, yOffset++);
            Console.ReadKey();
            Console.Clear();
            User user = new User("some", 10);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Users:\n");
            user.UsersOutput();

        }



        static void WriteText(String text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }

        static string InputText(int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            string name = Console.ReadLine();
            return name;
        }
    }
}