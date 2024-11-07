using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Score
    {
        private int score;

        public Score()
        {
            score = 0;
        }

        public void PlusScore()
        {
            score++;
        }
        public void MinusScore()
        {
            score--;
        }

        public int GetScore()
        {
            return score;
        }
    }
}