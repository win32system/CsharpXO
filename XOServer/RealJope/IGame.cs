using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOServer.RealJope
{
    public interface IGame
    {
        string Result { get; set; }

        void Turn(int index, string unit);
        bool IsGameOver();
        string[] GetMatrix();
        bool IsWrongStep(int index);
    }
}
