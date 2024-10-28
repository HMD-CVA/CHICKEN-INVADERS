using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace ChikenFIGHT
{
    public partial class Form1 : Form
    {
        private Plane plane;
        private Chicken Boss;

        private List<Chicken> chickenblue = new List<Chicken>();
        private List<Chicken> chickenred = new List<Chicken>();
        private List<Egg> eggs = new List<Egg>();
        private List<Bullet> bullets = new List<Bullet>();
        private List<Femoral> femorals = new List<Femoral>();

        private Timer timerBullet;
        private Timer timerEgg;
        private Timer timerChicken;
        private Timer timers;
        private Timer timerWait;
        private Timer invincibleTimer;     // Timer để kiểm soát thời gian bất tử
        private Timer timer;

        private Random random = new Random();
        private SoundPlayer choinhac;
        SoundPlayer nhacNen = new SoundPlayer(Properties.Resources.nhac);

        private Image planeImage = Properties.Resources.ship;
        private Image bulletImage = Properties.Resources.bullet;
        private Image chickenImageLevel1 = Properties.Resources.chickenLevel1;
        private Image chickenImageLevel2 = Properties.Resources.chickenLevel2;
        private Image chickenImageBoss = Properties.Resources.Boss;
        private Image chickenImageGift = Properties.Resources.Present;
        private Image eggImage = Properties.Resources.egg;
        private Image explosionImage = Properties.Resources.breakplane;
        private Image BackGround = Properties.Resources.BackGrounds;
        private Image Reset = Properties.Resources.Reset;
        private Image Resume = Properties.Resources.Resume;
        private Image close = Properties.Resources.close;
        private Image Pause = Properties.Resources.Play;
        private Image FemoralImage = Properties.Resources.Femoral;
        private Image LogoImage = Properties.Resources.LOGO;
        private Image GameRules = Properties.Resources.LuatChoi;

        private bool gameOver = false;
        private bool checkScore = false;
        private bool checkBossMove = false;
        private bool checkWin = false;
        private bool isPlaying = false;
        private bool clicks = false;
        private bool isInvincible = false; // Trạng thái bất tử
        private bool gameStarted = false; //2 nay la de bat dau game
        private bool ClickRules = false;
        private bool isPaused = false;

        private int chickenNumber = 27;
        private int planHeal = 150;
        private int chickenHeal = 100;
        private int bossHeal = 500;
        private int Level = 0;
        private int Score = 0;
        private int tmpX = -5;
        private int bonusSpeed = 0;

        private Button closeButton;
        private Button startButton;
        private Button pauseButton;
        private Button resumeButton; //tao nut pause reset resume đồ nè
        private Button resetButton;
        private Button GameRulesButton;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Chicken Invaders";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            timer = new Timer();
            timer.Interval = 1;
            timer.Tick += CreateTimer;
            timer.Start();

            plane = new Plane(350, 460, 100, 100, planeImage, planHeal);
            this.KeyDown += Form1_KeyDown;
            this.KeyPreview = true;

            timerBullet = new Timer();
            timerBullet.Interval = 100;
            timerBullet.Tick += TimerTick;
            timerBullet.Stop();

            CreateChicken(chickenNumber, chickenImageLevel1, chickenImageLevel2);

            timerChicken = new Timer();
            timerChicken.Interval = 16;
            timerChicken.Tick += ChickenTick;
            timerChicken.Stop();

            timerEgg = new Timer();
            timerEgg.Interval = 1000;
            timerEgg.Tick += TimerEggDrop_Tick;
            timerEgg.Stop();

            timers = new Timer();
            timers.Interval = 500;
            timers.Tick += SpamChickenLV3; // Timer Spam Ga o LV3

            //ẩn label
            Winpanel.Hide();

            // Tạo nút luật chơi
            createRuleButton();

            // Nút Start
            createStartButton();

            // Nút Close
            createCloseButton();

            // Nút Pause
            createPauseButton();

            // Nút Resume
            createResumeButton();

            // Nút Reset
            createResetButton();

            // Khởi động nhạc nền
            nhacNen.Stop();
        }
        private void createRuleButton()
        {
            GameRulesButton = new Button();
            GameRulesButton.Size = new Size(50, 50);
            GameRulesButton.BackgroundImage = GameRules; // Sử dụng hình ảnh cho nút
            GameRulesButton.BackgroundImageLayout = ImageLayout.Stretch; // Thiết lập cách hiển thị hình ảnh
            GameRulesButton.BackColor = Color.Transparent;
            GameRulesButton.FlatStyle = FlatStyle.Flat;
            GameRulesButton.FlatAppearance.BorderSize = 0; // Không hiển thị viền
            GameRulesButton.Location = new Point(20, 20); // Vị trí nút luật chơi
            GameRulesButton.Click += GameRulesButton_Click; // Gọi hàm khi nhấn nút
            GameRulesButton.Cursor = Cursors.Hand;
            this.Controls.Add(GameRulesButton); // Thêm vào form
        }
        private void createStartButton()
        {
            startButton = new Button(); // Khởi tạo nút Start
            startButton.Text = "START";
            startButton.Size = new Size(150, 60);
            startButton.Font = new Font("Arial", 16, FontStyle.Bold);
            startButton.BackColor = Color.DarkBlue;
            startButton.ForeColor = Color.White;
            startButton.FlatStyle = FlatStyle.Flat;
            startButton.FlatAppearance.BorderColor = Color.Gold;
            startButton.FlatAppearance.BorderSize = 2;
            startButton.Location = new Point(320, 380);
            startButton.Cursor = Cursors.Hand;
            startButton.MouseEnter += (s, ev) =>
            {
                startButton.BackColor = Color.White;
                startButton.ForeColor = Color.DarkBlue;
            };
            startButton.MouseLeave += (s, ev) =>
            {
                startButton.BackColor = Color.DarkBlue;
                startButton.ForeColor = Color.White;
            };
            startButton.Click += new EventHandler(StartButton_Click);
            this.Controls.Add(startButton);
        }
        private void createCloseButton()
        {
            closeButton = new Button(); // Khởi tạo nút Close
            closeButton.Text = "CLOSE";
            closeButton.Size = new Size(150, 60);
            closeButton.Font = new Font("Arial", 16, FontStyle.Bold);
            closeButton.BackColor = Color.DarkBlue;
            closeButton.ForeColor = Color.White;
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.FlatAppearance.BorderColor = Color.Gold;
            closeButton.FlatAppearance.BorderSize = 1;
            closeButton.Location = new Point(320, 450); // Đặt vị trí dưới nút
            closeButton.Cursor = Cursors.Hand;
            closeButton.MouseEnter += (s, ev) =>
            {
                closeButton.BackColor = Color.White;
                closeButton.ForeColor = Color.DarkBlue;
            };
            closeButton.MouseLeave += (s, ev) =>
            {
                closeButton.BackColor = Color.DarkBlue;
                closeButton.ForeColor = Color.White;
            };
            closeButton.Click += new EventHandler(CloseButton_Click);
            this.Controls.Add(closeButton);
        }
        private void createPauseButton()
        {
            pauseButton = new Button();
            pauseButton.Size = new Size(50, 50);
            // Đăng ký sự kiện PreviewKeyDown cho pauseButton
            pauseButton.PreviewKeyDown += new PreviewKeyDownEventHandler(PauseButton_PreviewKeyDown);
            pauseButton.BackgroundImage = Pause;
            pauseButton.BackgroundImageLayout = ImageLayout.Stretch;
            pauseButton.BackColor = Color.Transparent;
            pauseButton.FlatStyle = FlatStyle.Flat;
            pauseButton.FlatAppearance.BorderSize = 0;
            pauseButton.Visible = false; // Giữ ẩn ban đầu
            pauseButton.Location = new Point(720, 500);
            pauseButton.Click += new EventHandler(PauseButton_Click);
            this.Controls.Add(pauseButton);
        }
        private void createResumeButton()
        {
            resumeButton = new Button();
            resumeButton.Size = new Size(50, 50);
            resumeButton.BackgroundImage = Resume;
            resumeButton.BackgroundImageLayout = ImageLayout.Stretch;
            resumeButton.BackColor = Color.Transparent;
            resumeButton.FlatStyle = FlatStyle.Flat;
            resumeButton.FlatAppearance.BorderSize = 0;
            resumeButton.Location = new Point(340, 250); // Vị trí trong form
            resumeButton.Visible = false;
            resumeButton.Click += new EventHandler(ResumeButton_Click);
            this.Controls.Add(resumeButton);
        }
        private void createResetButton()
        {
            resetButton = new Button();
            resetButton.Size = new Size(50, 50);
            resetButton.BackgroundImage = Reset;
            resetButton.BackgroundImageLayout = ImageLayout.Stretch;
            resetButton.BackColor = Color.Transparent;
            resetButton.FlatStyle = FlatStyle.Flat;
            resetButton.FlatAppearance.BorderSize = 0;
            resetButton.Location = new Point(410, 250); // Vị trí trong form
            resetButton.Visible = false;
            resetButton.Click += new EventHandler(ResetButton_Click);
            this.Controls.Add(resetButton);
        }
        private void GameRulesButton_Click(object sender, EventArgs e)
        {
            if (!ClickRules) // Nếu chưa nhấn
            {
                ClickRules = true; // Đặt biến ClickRules thành true
            }
        }
        private void StartButton_Click(object sender, EventArgs e) //de bat dau game
        {
            gameStarted = true;
            startButton.Visible = false;
            pauseButton.Visible = true;
            closeButton.Visible = false;

            // Bắt đầu game
            timerBullet.Start();
            timerEgg.Start();
            timerChicken.Start();

            nhacNen.PlayLooping();
            GameRulesButton.Hide();
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng Form
        }
        //Sự kiện khi nhấn nút Pause
        private void PauseButton_Click(object sender, EventArgs e)
        {
            isPaused = true;
            pauseButton.Visible = false;
            resumeButton.Visible = true;
            resetButton.Visible = true;
            timerChicken.Stop();
            timerBullet.Stop();
            timerEgg.Stop();
            checkBossMove = true;
        }
        // Sự kiện khi nhấn nút Resume
        private void ResumeButton_Click(object sender, EventArgs e)
        {
            isPaused = false;
            pauseButton.Visible = true;
            resumeButton.Visible = false;
            resetButton.Visible = false;
            checkBossMove = false;
            timerChicken.Start();
            timerBullet.Start();
            timerEgg.Start();
        }
        // Sự kiện khi nhấn nút Reset
        private void ResetButton_Click(object sender, EventArgs e)
        {
            timers.Stop();
            isPaused = false;
            gameOver = false;
            checkWin = false;
            closeButton.Hide();
            // Xóa danh sách hiện tại
            chickenblue.Clear();
            chickenred.Clear();
            bullets.Clear();
            eggs.Clear();

            //
            bonusSpeed = 1;
            Winpanel.Hide();
            // Đặt lại cấp độ về 1
            Level = 0;
            Score = 0;
            // Tạo lại gà với số lượng và hình ảnh phù hợp với cấp độ
            CreateChicken(chickenNumber, chickenImageLevel1, chickenImageLevel2); // Gà xanh cấp độ 1

            // Khởi tạo lại máy bay
            plane = new Plane(350, 460, 100, 100, planeImage, 150);

            // Cập nhật trạng thái của các nút
            pauseButton.Visible = true;
            resumeButton.Visible = false;
            resetButton.Visible = false;

            timerChicken.Start();

            // Bắt đầu lại các timer
            timerBullet.Start();
            timerEgg.Start();
            checkBossMove = false;

            Invalidate(); // Vẽ lại giao diện
        }
        private void CreateTimer(object sender, EventArgs e)
        {
            Invalidate();
        }
        private void CreateFemoral(Chicken chicken, Image image)
        {
            int FeW = 20, FeH = 20;
            int FeX = chicken.GS_X + chicken.GS_Width / 2 - FeW / 2;
            int FeY = chicken.GS_Y + FeH * 2;

            femorals.Add(new Femoral(FeX, FeY, FeW, FeH, image, 5));
        }
        private void StartInvincibility()
        {
            isInvincible = true;
            // Tạo timer để tắt chế độ bất tử sau 0.5 giây
            invincibleTimer = new Timer();
            invincibleTimer.Interval = 500; // 0.5 giây
            invincibleTimer.Tick += new EventHandler(EndInvincibility);
            invincibleTimer.Start();
        }
        private void EndInvincibility(object sender, EventArgs e)
        {
            isInvincible = false;
            invincibleTimer.Stop();
            invincibleTimer = null; // Giải phóng timer
        }
        private bool CrashChickenBullet(Chicken chicken, Bullet bullet)
        {
            Rectangle bulletXY = new Rectangle(bullet.GS_X, bullet.GS_Y, bullet.GS_Width, bullet.GS_Height);
            Rectangle chickenXY = new Rectangle(chicken.GS_X, chicken.GS_Y, chicken.GS_Width, chicken.GS_Height);
            return bulletXY.IntersectsWith(chickenXY);
        }
        private bool CrashChickenPlane(Chicken chicken, Plane plane)
        {
            Rectangle planeXY = new Rectangle(plane.GS_X, plane.GS_Y, plane.GS_Width, plane.GS_Height);
            Rectangle chickenXY = new Rectangle(chicken.GS_X, chicken.GS_Y, chicken.GS_Width, chicken.GS_Height);
            return planeXY.IntersectsWith(chickenXY);
        }
        private bool CrashEggPlane(Egg egg, Plane plane)
        {
            Rectangle EggXY = new Rectangle(egg.GS_X, egg.GS_Y, egg.GS_Width - 20, egg.GS_Height - 5);
            Rectangle PlaneXY = new Rectangle(plane.GS_X, plane.GS_Y, plane.GS_Width - 20, plane.GS_Height - 10);
            return PlaneXY.IntersectsWith(EggXY);
        }
        private bool CrashFemoralPlane(Femoral femoral, Plane plane)
        {
            Rectangle femoralXY = new Rectangle(femoral.GS_X, femoral.GS_Y, femoral.GS_Width, femoral.GS_Height);
            Rectangle planeXY = new Rectangle(plane.GS_X, plane.GS_Y, plane.GS_Width, plane.GS_Height);
            return planeXY.IntersectsWith(femoralXY);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (!gameStarted)
            {
                int logoX = 220; // Vị trí x cho logo
                int logoY = 100; // Vị trí y cho logo
                e.Graphics.DrawImage(LogoImage, new Rectangle(logoX, logoY, 350, 200));
            }
            if (ClickRules)
            {
                Huong_Dan_Choi Rule = new Huong_Dan_Choi();
                Rule.Show();
                ClickRules = false;
            }
            // Kiểm tra nếu trò chơi đã bắt đầu
            if (gameStarted)
            {
                if (!isInvincible || DateTime.Now.Millisecond % 500 < 250) plane.Draw(e.Graphics); // Máy bay nhấp nháy nếu đang bất tử

                foreach (var bullet in bullets) bullet.Draw(e.Graphics);
                foreach (var egg in eggs) egg.Draw(e.Graphics);
                foreach (var femoral in femorals) femoral.Draw(e.Graphics);

                if (Level == 3)
                {
                    Boss.Draw(e.Graphics);
                    if (!checkBossMove) Boss.Movess();
                    foreach (var chicken in chickenblue) chicken.Draw(e.Graphics);
                    foreach (var chicken in chickenred) chicken.Draw(e.Graphics);
                }
                if (Level == 1)
                    foreach (var chicken in chickenblue) chicken.Draw(e.Graphics);

                if (Level == 2)
                    foreach (var chicken in chickenred) chicken.Draw(e.Graphics);

                if (gameOver)
                {
                    label4.Text = "Defeat";
                    timerChicken.Stop();
                    checkBossMove = true;
                    timerBullet.Stop();
                    Winpanel.Show();
                    clicks = false;
                }

                string s = Score.ToString();
                Font scoreFont = new Font("Arial", 10, FontStyle.Bold); // Đổi tên biến
                e.Graphics.DrawString(s, scoreFont, Brushes.Yellow, 10, this.ClientSize.Height - 50);

                if (checkWin && Level >= 3 && chickenblue.Count == 0 && chickenred.Count == 0)
                {
                    label4.Text = "Victory";
                    timerBullet.Stop();
                    Winpanel.Show();
                    clicks = false;
                }
                else if (chickenred.Count == 0 && chickenblue.Count == 0 && eggs.Count == 0) // Crash
                {
                    Font levelCompleteFont = new Font("Arial", 25, FontStyle.Italic); // Đổi tên biến
                    e.Graphics.DrawString("Qua Màn", levelCompleteFont, Brushes.Red, this.ClientSize.Width / 2 - 65, 10);

                    if (timerWait == null)
                    {
                        timerWait = new Timer();
                        timerWait.Interval = 2000;
                        timerWait.Tick += new EventHandler(Level_Tick);
                        timerWait.Start();
                    }
                }
            }
        }
        private void CreateChicken(int sizeChicken, Image imgblue, Image imgred)
        {
            int width = 60;
            int height = 60;
            int X = 45;
            int Y = 25;
            Level++;
            for (int i = 1; i <= sizeChicken; i++)
            {
                if (Level == 1)
                {
                    if (X >= 650)
                    {
                        X = 45;
                        Y += 70;
                    }
                    chickenblue.Add(new Chicken(X, Y, height, width, imgblue, 5, 5));
                }
                else if (Level == 2)
                {
                    chickenred.Add(new Chicken(random.Next(60, 651), random.Next(60, 300), height, width, imgred, random.Next(1, 2), random.Next(-2, 2)));
                }
                X += 70;
            }
            Boss = new Chicken(250, 60, 300, 300, chickenImageBoss, 2, 2);
            Boss.Health = bossHeal;
        }
        private void ChickenTick(object sender, System.EventArgs e)
        {
            foreach (var chicken in chickenred) chicken.Moves();
            if (Level == 3)
            {
                foreach (var chicken in chickenblue) chicken.Moves();
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!gameOver && !isPaused)
            {
                int step = 5;
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        plane.Move(0, -step, this.ClientSize);
                        break;
                    case Keys.Down:
                        plane.Move(0, step, this.ClientSize);
                        break;
                    case Keys.Left:
                        plane.Move(-step, 0, this.ClientSize);
                        break;
                    case Keys.Right:
                        plane.Move(step, 0, this.ClientSize);
                        break;
                    case Keys.P:
                        pauseButton.PerformClick();
                        break;
                }
            }
        }
        // Hủy đăng kí nút mũi tên
        private void PauseButton_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.IsInputKey = true; // Ngăn chặn button nhận phím mũi tên
            }
        }
        private void ShootBullet()
        {
            int bulletW = 9, bulletH = 54;
            int bulletX = plane.GS_X + plane.GS_Width / 2 - bulletW / 2;
            int bulletY = plane.GS_Y - bulletH;

            if (bullets.Count == 0 || (Math.Abs(bulletX - bullets.Last().GS_X) + Math.Abs(bulletY - bullets.Last().GS_Y) >= 60))
            {
                bullets.Add(new Bullet(bulletX, bulletY, bulletH, bulletW, bulletImage, bonusSpeed + 5));
            }
        }
        private void ProcessChicken(List<Chicken> chicken)
        {
            for (int i = 0; i < chicken.Count; i++)
            {
                if (CrashChickenPlane(chicken[i], plane))
                {
                    chicken.RemoveAt(i);
                    if (!isInvincible) LoseLife();
                    break;
                }
                else
                {
                    for (int j = bullets.Count - 1; j >= 0; j--)
                    {
                        if (CrashChickenBullet(chicken[i], bullets[j]))
                        {
                            bullets.RemoveAt(j);
                            chicken[i].Health -= 40; // Ví dụ mỗi viên đạn gây 40 sát thương
                            if (chicken[i].Health <= 0)
                            {
                                Score += 20;
                                Chicken tmp = chicken[i];
                                chicken.RemoveAt(i);

                                int pos = random.Next(0, 12);
                                if (pos == 8 || pos == 9 || pos == 10)
                                {
                                    CreateFemoral(tmp, chickenImageGift);
                                }
                                else if (pos < 6)
                                {
                                    CreateFemoral(tmp, FemoralImage);
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }
        private void TimerTick(object sender, System.EventArgs e)
        {
            if (!gameOver)
            {
                if ((Level == 3 && Boss.Health <= 500) || chickenred.Count != 0 || chickenblue.Count != 0)
                {
                    ShootBullet();
                }

                for (int i = bullets.Count - 1; i >= 0; i--)
                {
                    bullets[i].GS_Y -= bullets[i].GS_Speed;
                }

                for (int i = femorals.Count - 1; i >= 0; i--)
                {
                    femorals[i].GS_Y += femorals[i].GS_Speed;
                    if (CrashFemoralPlane(femorals[i], plane) && femorals[i].GS_Image == FemoralImage)
                    {
                        femorals.RemoveAt(i);
                        Score += 50;
                    }
                    else if (CrashFemoralPlane(femorals[i], plane) && femorals[i].GS_Image == chickenImageGift)
                    {
                        femorals.RemoveAt(i);
                        plane.Health += 5;
                        bonusSpeed += 1;
                    }
                }

                for (int i = eggs.Count - 1; i >= 0; i--)
                {
                    eggs[i].GS_Y += eggs[i].GS_Speed;
                    if (eggs[i].GS_Y > 570)
                    {
                        eggs.RemoveAt(i);
                    }
                }

                if (Level == 1)
                {
                    bool check = true;
                    foreach (var chicken in chickenblue)
                    {
                        chicken.Move(tmpX, 0);
                        if (chicken.GS_X <= 0 || chicken.GS_X + chicken.GS_Width >= this.ClientSize.Width)
                        {
                            check = false;
                        }
                    }
                    if (!check)
                    {
                        tmpX = -tmpX;
                    }
                    ProcessChicken(chickenblue);
                }

                if (Level == 2)
                {
                    ProcessChicken(chickenred);
                }

                if (Level == 3)
                {
                    timers.Start();
                    ProcessChicken(chickenblue);
                    ProcessChicken(chickenred);
                    if (CrashChickenPlane(Boss, plane))
                    {
                        Boss.Health -= 5;
                        if (!isInvincible) LoseLife();
                    }
                    else
                    {
                        for (int j = bullets.Count - 1; j >= 0; j--)
                        {
                            if (CrashChickenBullet(Boss, bullets[j]))
                            {
                                bullets.RemoveAt(j);
                                Boss.Health -= 5; // Ví dụ mỗi viên đạn gây 20 sát thương
                                if (Boss.Health <= 0)
                                {
                                    timers.Stop();
                                    Boss.Move(-1000, -1000); // Xóa gà nếu máu <= 0
                                    Score += 2000;
                                    checkWin = true;
                                }
                            }
                        }
                    }
                }
                for (int i = eggs.Count - 1; i >= 0; i--)
                {
                    if (CrashEggPlane(eggs[i], plane))
                    {
                        eggs.RemoveAt(i);
                        if (!isInvincible) LoseLife();
                        break;
                    }
                }
            }
        }
        private void SpamChickenLV3(object sender, System.EventArgs e)
        {
            if (chickenred.Count + chickenblue.Count < 6 && !checkWin)
            {
                chickenred.Add(new Chicken(Boss.GS_X, Boss.GS_Y, 60, 60, chickenImageLevel2, random.Next(1, 2), random.Next(-2, 2)));
                chickenblue.Add(new Chicken(Boss.GS_X, Boss.GS_Y, 60, 60, chickenImageLevel1, random.Next(1, 2), random.Next(-2, 2)));
            }
        }
        private void LoseLife()
        {
            plane.Health -= 15;
            if (plane.Health > 0)
            {
                StartInvincibility(); // Kích hoạt chế độ bất tử khi mất mạng
            }
            else
            {
                plane = new Plane(plane.GS_X, plane.GS_Y, 50, 50, explosionImage, 0);
                gameOver = true;
                clicks = false;
            }
        }
        private void TimerEggDrop_Tick(object sender, EventArgs e)
        {
            if (Level == 1)
            {
                if (chickenblue.Count != 0 && !gameOver)
                {
                    int pos = random.Next(0, chickenblue.Count);
                    int eggW = 20, eggH = 20;
                    int eggX = chickenblue[pos].GS_X + chickenblue[pos].GS_Width / 2 - eggW / 2;
                    int eggY = chickenblue[pos].GS_Y + eggH * 2;

                    eggs.Add(new Egg(eggX, eggY, eggW, eggH, eggImage, 5));
                }
            }
            if (Level == 2)
            {
                if (chickenred.Count != 0 && !gameOver)
                {
                    int pos = random.Next(0, chickenred.Count);
                    int eggW = 20, eggH = 20;
                    int eggX = chickenred[pos].GS_X + chickenred[pos].GS_Width / 2 - eggW / 2;
                    int eggY = chickenred[pos].GS_Y + eggH * 2;

                    eggs.Add(new Egg(eggX, eggY, eggW, eggH, eggImage, 5));
                }
            }
            if (Level == 3)
            {
                if (Boss.Health != 0 && chickenred.Count != 0 && chickenblue.Count != 0 && !gameOver)
                {
                    int eggW = 20, eggH = 20;
                    int eggX = Boss.GS_X + Boss.GS_Width / 2 - eggW / 2;
                    int eggY = Boss.GS_Y + eggH * 2;

                    eggs.Add(new Egg(eggX, eggY, eggW, eggH, eggImage, 5));
                }
            }
        }
        private void Level_Tick(object sender, EventArgs e)
        {
            timerWait.Stop();
            timerWait = null;
            if (Level == 1 || Level == 2)
            {
                CreateChicken(chickenNumber, chickenImageLevel1, chickenImageLevel2);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = BackGround;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (clicks && !isPaused)
            {
                Point pos = e.Location;
                plane.MouseMove(pos.X, pos.Y, this.ClientSize);
            }
        }
        private void Form1_Click(object sender, EventArgs e)
        {
            clicks = !clicks;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //trong suốt panel
            Winpanel.BackColor = Color.Transparent;
            Winpanel.ForeColor = Color.Transparent;

            //diều chỉnh lại nút close
            closeButton.FlatAppearance.BorderColor = Color.Empty;
            closeButton.BackColor = Color.Transparent;
            closeButton.MouseEnter += (s, ev) =>
            {
                closeButton.ForeColor = Color.Transparent;
            };
            closeButton.MouseLeave += (s, ev) =>
            {
                closeButton.ForeColor = Color.White;
            };
            closeButton.Font = new Font("Arial", 10, FontStyle.Bold);
            closeButton.Size = new Size(70, 70);
            closeButton.ForeColor = Color.White;
            closeButton.TextAlign = ContentAlignment.BottomCenter;
            closeButton.BackgroundImage = close;
            closeButton.Location = new Point(Winpanel.Location.X + 30, Winpanel.Location.Y + 160);
            closeButton.BringToFront();
            closeButton.Visible = true;
            closeButton.BackgroundImageLayout = ImageLayout.Stretch;

            //điều chỉnh lại nút reset
            resetButton.FlatStyle = FlatStyle.Flat;
            resetButton.FlatAppearance.BorderSize = 1;
            resetButton.FlatAppearance.BorderColor = Color.White; // 
            resetButton.Font = new Font("Arial", 10, FontStyle.Bold);
            resetButton.Size = new Size(70, 70);
            resetButton.Text = "Restart";
            resetButton.BackColor = Color.Transparent;
            resetButton.MouseEnter += (s, ev) =>
            {
                resetButton.ForeColor = Color.Transparent;
            };
            resetButton.MouseLeave += (s, ev) =>
            {
                resetButton.ForeColor = Color.White;
            };
            resetButton.TextAlign = ContentAlignment.BottomCenter;
            resetButton.BackgroundImage = Reset;
            resetButton.Location = new Point(Winpanel.Location.X + 270, Winpanel.Location.Y + 160);
            resetButton.BringToFront();
            resetButton.Visible = true;

            //gán score cho label
            label2.Text = Score.ToString();
        }
    }
}
