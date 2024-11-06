using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Snake
{
    class User
    {
        public string Name { get; }
        public int Score { get; }

        public User(string name, int score)
        {
            Name = name;
            Score = score;
        }

        public void UsersOutput()
        {
            string[] lines = File.ReadAllLines("Users.txt");  //C:\Users\Administrator\source\repos\Snake\Snake\bin\Debug\net8.0\


            List<User> users = new List<User>();

            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                if (parts.Length == 2)
                {
                    string name = parts[0];
                    int score = int.Parse(parts[1]);
                    User user = new User(name, score);
                    users.Add(user);
                }
            }


            users = users.OrderByDescending(u => u.Score).ToList();


            int count = Math.Min(users.Count, 15);
            for (int i = 0; i < count; i++)
            {
                User user = users[i];
                Console.WriteLine($"{user.Name} {user.Score}");
            }

            Console.ReadLine();
        }

    }
}