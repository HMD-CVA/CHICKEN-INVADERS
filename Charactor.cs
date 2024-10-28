using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

public class Charactor
{
    private int X;
    private int Y;
    private int height;
    private int width;
    private int speed;
    private Image image;

    public Charactor(int x, int y, int height_, int width_, Image _image)
    {
        X = x;
        Y = y;
        height = height_;
        width = width_;
        image = _image;
    }
    public Image GS_Image
    {
        get { return image; }
        set { image = value; }
    }
    public int GS_X
    {
        get { return X; }
        set { X = value; }
    }
    public int GS_Y
    {
        get { return Y; }
        set { Y = value; }
    } 
    public int GS_Height
    {
        get { return height; }
        set { height = value; }
    }
    public int GS_Width
    {
        get { return width; }
        set { width = value; }
    }
    public virtual void Draw(Graphics g)
    {
        g.DrawImage(image, X, Y, width, height);
    }
}

