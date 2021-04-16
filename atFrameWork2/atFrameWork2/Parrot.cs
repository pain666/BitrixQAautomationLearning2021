using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace atFrameWork2
{
    class Parrot : ICanFly
    {
        const string agneorgnorgne = "";
        Color color;
        public Color Color
        {
            get
            {
                return color;
            }
            set 
            { 
                color = value; 
            }
        }

        public void Fly()
        {
            Debug.Print($"Вжух вжух {Color} крыльями");
        }

        public Parrot(Color parrotColor)
        {
            Color = parrotColor;
        }

        public static Parrot DefaultParrot1111 = new Parrot(Color.Brown);
    }
}
