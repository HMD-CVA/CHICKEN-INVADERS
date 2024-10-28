using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
class Femoral : Charactor
{
    private int Speed;
    public Femoral(int x, int y, int height, int width, Image img, int speed)
            : base(x, y, height, width, img)
    {
        Speed = speed;
    }
    public int GS_Speed
    {
        get { return Speed; }
        set { Speed = value; }
    }
    public override void Draw(Graphics g)
    {
        base.Draw(g);
    }
}