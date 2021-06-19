using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TankGameV._10版本.Properties;
namespace TankGameV._10版本
{
    class PlayerTank : Tank
    {
        const double FIREINTERVAL = 0.5;

        private static Image[] imgs = { 
                              Resources.p1tankU,
                              Resources.p1tankD,
                              Resources.p1tankL,
                              Resources.p1tankR
                               };

        public PlayerTank(int x, int y, int speed, int life, Direction dir)
            : base(x, y, imgs, speed, life, dir)
        {
            Born();
        }

        public int BulletLevel
        {
            get;
            set;
        }//火力等级


        public override void Born()
        {
            GameController.GetInstance().AddGameObject(new TankBorn(this.X, this.Y));
        }

        double lastFireTime  = 0;

        public void KeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    this.Dir = Direction.Up;
                    base.Move();
                    break;
                case Keys.S:
                    this.Dir = Direction.Down;
                    base.Move();
                    break;
                case Keys.A:
                    this.Dir = Direction.Left;
                    base.Move();
                    break;
                case Keys.D:
                    this.Dir = Direction.Right;
                    base.Move();
                    break;
                case Keys.K:
                    double now = DateTime.Now.Second + (double)DateTime.Now.Millisecond/1000;
                    if (now >= lastFireTime + FIREINTERVAL)
                    {
                        Fire();
                        lastFireTime = now;
                    }
                    break;
            }
        }


        public override void Fire()
        {
            switch (BulletLevel)
            {
                case 0: GameController.GetInstance().AddGameObject(new PlayerBullet(this, 10, 10, 1));
                    break;
                case 1: GameController.GetInstance().AddGameObject(new PlayerBullet(this, 10, 10, 2));
                    break;
                case 2: GameController.GetInstance().AddGameObject(new PlayerBullet(this, 10, 10, 3));
                    break;
            }

        }


        public override void IsOver()
        {
            GameController.GetInstance().AddGameObject(new Explosion(this.X - 25, this.Y - 25));
            
            
            //被击中了 删除掉坦克
            GameController.GetInstance().RemoveGameObject(this);
            SoundPlayer sp = new SoundPlayer(Resources.hit);
            sp.Play();
            //跳转到任务失败页面

            
        }

    }
}
