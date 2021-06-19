using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
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
        public void InitialMap()
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



        /// <summary>
        /// 当键盘按下的时候会触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GameController.GetInstance().PlayerTank.KeyDown(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
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
