using System;
using System.Media;
using System.Windows.Forms;
using TankGameV._10版本.Properties;
namespace TankGameV._10版本
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //对我们的游戏进行初始化
            InitialGame();
        }

        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.MaximumSize = new System.Drawing.Size(800, 700);
            this.MinimumSize = new System.Drawing.Size(800, 700);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;


        /// <summary>
        /// 初始化游戏
        /// </summary>
        private void InitialGame()
        {
            GameController.GetInstance().AddGameObject(new PlayerTank(200, 200, 10, 10, Direction.Up));
            SetEnemyTanks();
            InitialMap();
        }
        //Random r = new Random();

        /// <summary>
        ///初始化敌人坦克对象
        /// </summary>
        public void SetEnemyTanks()
        {
            for (int i = 0; i < 8; i++)
            {
                //hjh：避免敌人坦克在初始化时就出现重叠
                //GameController.GetInstance().AddGameObject(new EnemyTank(r.Next(0, this.Width), r.Next(0, this.Height), r.Next(0, 3), Direction.Down));
                GameController.GetInstance().NewEnemyTank(this.Width, this.Height);
            }

        }


        /// <summary>
        /// 初始化游戏地图
        /// </summary>
        int temp = 0;
        public void InitialMap()
        {
            maps map1 = new maps(Map1);
            maps map2 = new maps(Map2);
            maps map3 = new maps(Map3);
            switch (temp % 3)
            {
                case 0:
                    clearMap();
                    map3();
                    temp++;
                    Console.WriteLine(0);
                    break;
                case 1:
                    clearMap();
                    map1();
                    temp++;
                    Console.WriteLine(0);
                    break;
                default:
                    clearMap();
                    map2();
                    temp++;
                    Console.WriteLine(0);
                    break;
            }

        }
        public delegate void maps();
        public void Map2()
        {
            for (int i = 0; i < 10; i++)
            {

                GameController.GetInstance().AddGameObject(new Wall(100, 100 + 12 * i));
                GameController.GetInstance().AddGameObject(new Wall(100 + i * 6, 100));
                GameController.GetInstance().AddGameObject(new Wall(160, 100 + 5 * i));
                GameController.GetInstance().AddGameObject(new Wall(100 + i * 6, 145));
                GameController.GetInstance().AddGameObject(new Wall(100 + i * 6, 145 + 7 * i));

                GameController.GetInstance().AddGameObject(new Wall(200, 100 + 12 * i));
                GameController.GetInstance().AddGameObject(new Wall(200 + 5 * i, 210));
                GameController.GetInstance().AddGameObject(new Wall(250, 100 + 12 * i));

                GameController.GetInstance().AddGameObject(new Wall(300, 100 + 12 * i));
                GameController.GetInstance().AddGameObject(new Wall(300 + 5 * i, 100 + 12 * i));
                GameController.GetInstance().AddGameObject(new Wall(350, 100 + 12 * i));

                GameController.GetInstance().AddGameObject(new Wall(400, 100 + 12 * i));
                GameController.GetInstance().AddGameObject(new Wall(400 + 5 * i, 100));
                GameController.GetInstance().AddGameObject(new Wall(450, 100 + 12 * i));
                GameController.GetInstance().AddGameObject(new Wall(400 + 5 * i, 210));

                GameController.GetInstance().AddGameObject(new Wall(500, 100 + 12 * i));
                GameController.GetInstance().AddGameObject(new Wall(500 + 5 * i, 100));
                GameController.GetInstance().AddGameObject(new Wall(550, 100 + 12 * i));
                GameController.GetInstance().AddGameObject(new Wall(500 + 5 * i, 210));

                GameController.GetInstance().AddGameObject(new Wall(600, 100 + 12 * i));
                GameController.GetInstance().AddGameObject(new Wall(600 + 5 * i, 100));
                GameController.GetInstance().AddGameObject(new Wall(600 + 5 * i, 155));
                GameController.GetInstance().AddGameObject(new Wall(650, 100 + 12 * i));
                GameController.GetInstance().AddGameObject(new Wall(600 + 5 * i, 210));


            }
        }
        public void Map3()
        {
            for (int i = 0; i < 10; i++)
            {
                GameController.GetInstance().AddGameObject(new Wall(100, 100 + 12 * i));
                GameController.GetInstance().AddGameObject(new Wall(100 + i * 3, 100+6*i));
                GameController.GetInstance().AddGameObject(new Wall(100+i*3, 220 - 6 * i));


                GameController.GetInstance().AddGameObject(new Wall(130+9*i, 160 + 12 * i));
                GameController.GetInstance().AddGameObject(new Wall(130 + i * 9, 160 - 12 * i));

                GameController.GetInstance().AddGameObject(new Wall(220 + 15 * i, 280));
                GameController.GetInstance().AddGameObject(new Wall(220 + i * 15, 40));

                GameController.GetInstance().AddGameObject(new Wall(300 + 15 * i, 280));
                GameController.GetInstance().AddGameObject(new Wall(300 + i * 15, 40));

                GameController.GetInstance().AddGameObject(new Wall(450 + 9 * i, 40 + 12 * i));
                GameController.GetInstance().AddGameObject(new Wall(450 + i * 9, 280 - 12 * i));

                GameController.GetInstance().AddGameObject(new Wall(540, 160));

                GameController.GetInstance().AddGameObject(new Wall(400 + 8 * i, 160 + 7* i));
                GameController.GetInstance().AddGameObject(new Wall(400 + i * 8, 160 - 7* i));

                GameController.GetInstance().AddGameObject(new Wall(480, 160));
                GameController.GetInstance().AddGameObject(new Wall(490, 170));
                GameController.GetInstance().AddGameObject(new Wall(490, 150));
                GameController.GetInstance().AddGameObject(new Wall(470, 170));
                GameController.GetInstance().AddGameObject(new Wall(470, 150));
            }
        }
        public void Map1()
        {
            for (int i = 0; i < 10; i++)
            {
                GameController.GetInstance().AddGameObject(new Wall(i * 15 + 30, 100));
                GameController.GetInstance().AddGameObject(new Wall(95, 100 + 15 * i));

                GameController.GetInstance().AddGameObject(new Wall(245 - i * 7, 100 + 15 * i));
                GameController.GetInstance().AddGameObject(new Wall(245 + i * 7, 100 + 15 * i));
                GameController.GetInstance().AddGameObject(new Wall(215 + i * 15 / 2, 185));

                GameController.GetInstance().AddGameObject(new Wall(390 - i * 5, 100 + 15 * i));
                GameController.GetInstance().AddGameObject(new Wall(390 + i * 5, 100 + 15 * i));
                GameController.GetInstance().AddGameObject(new Wall(480 - i * 5, 100 + 15 * i));

                GameController.GetInstance().AddGameObject(new Wall(515, 100 + 15 * i));
                GameController.GetInstance().AddGameObject(new Wall(595 - i * 8, 100 + 15 * i / 2));
                GameController.GetInstance().AddGameObject(new Wall(530 + i * 8, 165 + 15 * i / 2));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">触发当前这个事件的对象</param>
        /// <param name="e">执行这个方法所需要的资源</param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            GameController.GetInstance().Draw(e.Graphics);
        }
        public void clearMap()
        {
            GameController.GetInstance().ClearMap();
        }
        public void changeMap()
        {
            clearMap();
            InitialMap();
        }



        /// <summary>
        /// 当键盘按下的时候会触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //GameController.GetInstance().PlayerTank.KeyDown(e);
            GameController.GetInstance().AddBuffer(e);
            switch (e.KeyCode)
            {
                case Keys.M:
                    changeMap();
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GameController.GetInstance().ClearBuffer();
            //对窗体进行更新
            this.Invalidate();
            //调用碰撞检测方法
            GameController.GetInstance().DetectCollision();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //让控件不闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);

            //在程序加载的时候 播放开始的音乐
            SoundPlayer sp = new SoundPlayer(Resources.start);
            sp.Play();

        }

    }
}
