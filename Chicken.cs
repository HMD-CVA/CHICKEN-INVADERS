using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

public class Chicken : Charactor
{
    public int Health { get; set; } // Thêm thuộc tính Health
    public int speedX { get; set; }
    public int speedY { get; set; }
    public Chicken(int x, int y, int height, int width, Image img, int _speedX, int _speedY) : base(x, y, height, width, img)
    {
        Health = width;
        speedX = _speedX;
        speedY = _speedY;
    }
    public void Move(int x, int y)
    {
        base.GS_X += x;
        base.GS_Y += y;
    }
    public void Moves()
    {

        if (base.GS_X <= 0 || base.GS_X >= 700) speedX = -speedX;
        if (base.GS_Y <= 0 || base.GS_Y >= 350) speedY = -speedY;

        base.GS_X += speedX;
        base.GS_Y += speedY;
    }
    public void Movess()    
    {

        if (base.GS_X <= 0 || base.GS_X >= 500) speedX = -speedX;
        if (base.GS_Y <= 0 || base.GS_Y >= 270) speedY = -speedY;

        base.GS_X += speedX;
        base.GS_Y += speedY;
    }
    public override void Draw(Graphics g)
    {
        base.Draw(g);
        DrawHealthBar(g);
    }
    private void DrawHealthBar(Graphics g)
    {
        int barWidth = 80;
        int barHeight = 4;  // Chiều cao của thanh máu
        int barX = GS_X;
        int barY = GS_Y - barHeight - 2; // Đặt thanh máu phía trên con gà

        // Tính toán tỷ lệ máu
        float healthPercentage = (float)Health / 100;

        // Vẽ nền thanh máu (màu đỏ)
        //g.FillRectangle(Brushes.Yellow, barX, barY, 0, 0);

        // Vẽ thanh máu hiện tại (màu xanh lá cây)
        g.FillRectangle(Brushes.Red, barX + 6, barY, barWidth * healthPercentage, barHeight);
        g.DrawRectangle(Pens.Black, barX + 6, barY, barWidth * healthPercentage, barHeight);
    }
}