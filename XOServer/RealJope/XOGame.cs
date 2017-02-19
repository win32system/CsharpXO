using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOServer.RealJope
{
    public class XOGame : IGame
    {
        public XOGame()
        {
            Result = String.Empty;
            combinations = new string[8];
            matrix = new string[] { "", "", "", "", "", "", "", "", "" };
        }


        public string Result { get; set; }
        private string[] combinations;
        private string[] matrix;


        public void Turn(int index, string unit)
        {
            matrix[index] = unit;
        }

        public bool IsGameOver()
        {
            combinations[0] = (matrix[0] + matrix[1] + matrix[2]);
            combinations[1] = (matrix[3] + matrix[4] + matrix[5]);
            combinations[2] = (matrix[6] + matrix[7] + matrix[8]);
            combinations[3] = (matrix[0] + matrix[3] + matrix[6]);
            combinations[4] = (matrix[1] + matrix[4] + matrix[7]);
            combinations[5] = (matrix[2] + matrix[5] + matrix[8]);
            combinations[6] = (matrix[0] + matrix[4] + matrix[8]);
            combinations[7] = (matrix[2] + matrix[4] + matrix[6]);

            bool draw = true;

            for (int i = 0; i < combinations.Length; i++)
            {
                if (combinations[i].Length < 3)
                {
                    draw = false;
                    continue;
                }

                if (combinations[i].All(ch => { return ch == 'X'; }) 
                   || combinations[i].All(ch => { return ch == 'O'; }))
                {
                    Result = combinations[i][0] + " win!";
                    return true;
                }
            }

            if (draw)
            {
                Result = "Draw!";
            }
            return draw;
        }

        public string[] GetMatrix()
        {
            return matrix;
        }

        public bool IsWrongStep(int index)
        {
            return matrix[index] != "";
        }
    }
}
