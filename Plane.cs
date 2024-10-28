using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

public class Plane : Charactor
{
    public int Health {  get; set; }
    private Image ImageBreakPlain;
    private int Speed;
    public Plane(int x, int y, int height_, int width_, Image _image, int _health)
       : base(x, y, height_, width_, _image)
        {
        Health = _health;
    }
    public Image GS_ImageBreakPlain
    {
        get { return ImageBreakPlain; }
        set { ImageBreakPlain = value; }
    }
    public void Move(int dx, int dy, Size clientSize)
    {
        int newX = GS_X + dx;
        int newY = GS_Y + dy;
        if (newX >= 0 && newX + GS_Width <= clientSize.Width)
        {
            GS_X = newX;
        }

        if (newY >= 0 && newY + GS_Height <= clientSize.Height)
        {
            GS_Y = newY;
        }
    }
    public void MouseMove(int dx, int dy, Size clientSize)
    {
        int newX = dx;
        int newY = dy;
        if (newX >= 0 && newX + GS_Width <= clientSize.Width)
        {
            GS_X = newX;
        }

        if (newY >= 0 && newY + GS_Height <= clientSize.Height)
        {
            GS_Y = newY;
        }
    }
    public override void Draw(Graphics g)
    {
        DrawHealthBar(g);
        base.Draw(g);
        
    }
    private void DrawHealthBar(Graphics g)
    {
        int barWidth = 150;
        int barHeight = 20;  // Chiều cao của thanh máu
        int barX = 0;
        int barY = 530; // Đặt thanh máu trong goc

        // Tính toán tỷ lệ máu
        float healthPercentage = (float)Health / 100;

        // Vẽ nền thanh máu (màu đỏ)
        g.FillRectangle(Brushes.Yellow, barX, barY, 0, 0);

        // Vẽ thanh máu hiện tại (màu xanh lá cây)
        g.FillRectangle(Brushes.Red, barX, barY, barWidth * healthPercentage, barHeight);
        g.DrawRectangle(Pens.Black, barX, barY, barWidth * healthPercentage, barHeight);
    }
}
