using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankGameV._10版本.Properties;
using System.Drawing;
using System.Media;
namespace TankGameV._10版本
{
    class EnemyTank : Tank
    {
        private static Image[] imgs1 = {
                                       Resources.enemy2U,
                                       Resources.enemy2D,
                                       Resources.enemy2L,
                                       Resources.enemy2R
                                       };
        private static Image[] imgs2 = {
                                       Resources.enemy1U,
                                       Resources.enemy1D,
                                       Resources.enemy1L,
                                       Resources.enemy1R
                                       };
        private static Image[] imgs3 = {
                                       Resources.enemy3U,
                                       Resources.enemy3D,
                                       Resources.enemy3L,
                                       Resources.enemy3R
                                       };

        //存储敌人坦克的速度
        private static int _speed;
        //存储敌人坦克的生命
        private static int _life;

        public int EnemyTankType
        {
            get;
            set;
        }


        /// <summary>
        /// 通过一个静态方法设置敌人坦克的速度
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static int SetSpeed(int Type)
        {
            switch (Type)
            {
                case 0:
                    _speed = 3;
                    break;
                case 1:
                    _speed = 2;
                    break;
                case 2:
                    _speed = 2;
                    break;
            }
            return _speed;
        }


        /// <summary>
        /// 通过一个静态方法设置敌人坦克的生命
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static int SetLife(int Type)
        {
            switch (Type)
            {
                case 0:
                    _life = 1;
                    break;
                case 1:
                    _life = 3;
                    break;
                case 2:
                    _life = 5;
                    break;
            }
            return _life;
        }



        public EnemyTank(int x, int y, int Type, Direction dir)
            : base(x, y, imgs1, SetSpeed(Type), SetLife(Type), dir)
        {
            this.EnemyTankType = Type;
            Born();
        }

        public bool isStop = true;
        public int stopTime = 0;
        //向窗体当中绘制我们的敌人坦克
        public override void Draw(Graphics g)
        {
            bornTime++;
            if (bornTime % 20 == 0)
            {
                //标记敌人坦克就被绘制出来啦
                isMove = true;
            }
            //一绘制我们的敌人坦克 就让坦克移动
            if (isMove)
            {
                if (isStop)
                {
                    Move();
                }
                else
                {
                    stopTime++;
                    if (stopTime % 100 == 0)
                    {
                        isStop = true;
                    }
                }
                switch (EnemyTankType)
                {
                    case 0:
                        switch (this.Dir)
                        {
                            case Direction.Up:
                                g.DrawImage(imgs1[0], this.X, this.Y);
                                break;
                            case Direction.Down:
                                g.DrawImage(imgs1[1], this.X, this.Y);
                                break;
                            case Direction.Left:
                                g.DrawImage(imgs1[2], this.X, this.Y);
                                break;
                            case Direction.Right:
                                g.DrawImage(imgs1[3], this.X, this.Y);
                                break;
                        }
                        break;
                    case 1:
                        switch (this.Dir)
                        {
                            case Direction.Up:
                                g.DrawImage(imgs2[0], this.X, this.Y);
                                break;
                            case Direction.Down:
                                g.DrawImage(imgs2[1], this.X, this.Y);
                                break;
                            case Direction.Left:
                                g.DrawImage(imgs2[2], this.X, this.Y);
                                break;
                            case Direction.Right:
                                g.DrawImage(imgs2[3], this.X, this.Y);
                                break;
                        }
                        break;
                    case 2:
                        switch (this.Dir)
                        {
                            case Direction.Up:
                                g.DrawImage(imgs3[0], this.X, this.Y);
                                break;
                            case Direction.Down:
                                g.DrawImage(imgs3[1], this.X, this.Y);
                                break;
                            case Direction.Left:
                                g.DrawImage(imgs3[2], this.X, this.Y);
                                break;
                            case Direction.Right:
                                g.DrawImage(imgs3[3], this.X, this.Y);
                                break;
                        }
                        break;
                }

                //敌人坦克一边移动一遍发射子弹
                if (r.Next(0, 100) < 2)
                {
                    Fire();
                }
            }
        }


        /// <summary>
        /// 敌人发射子弹
        /// </summary>
        public override void Fire()
        {
            GameController.GetInstance().AddGameObject(new EnemyBullet(this, 10, 10, 1));
        }

        public override void IsOver()
        {
            if (this.Life <= 0)//敌人被击中了并且死亡
            {
                //出现爆炸的图片
                GameController.GetInstance().AddGameObject(new Explosion(this.X - 25, this.Y - 25));
                //被击中了 删除掉被击中的坦克
                GameController.GetInstance().RemoveGameObject(this);
                //播放坦克爆炸的声音
                SoundPlayer sp = new SoundPlayer(Resources.fire);
                sp.Play();
                //当敌人坦克死亡的时候 会有一定的几率 在出生新的坦克
                if (r.Next(0, 100) >= 70)
                {
                    GameController.GetInstance().AddGameObject(new EnemyTank(r.Next(0, 700), r.Next(0, 600), r.Next(0, 3), Direction.Down));
                }
                //给一定的几率能够产生装备

                //只要敌人挂了 马上出装备
                if (r.Next(0, 100) <= 30)
                {
                    GameController.GetInstance().AddGameObject(new Prop(this.X, this.Y, r.Next(0, 3)));
                }

            }
            else//敌人被击中 但是没有死亡
            {
                SoundPlayer sp = new SoundPlayer(Resources.hit);
                sp.Play();
            }
        }

        /// <summary>
        /// 敌人坦克出生的方法
        /// </summary>
        public override void Born()
        {
            GameController.GetInstance().AddGameObject(new TankBorn(this.X, this.Y));
        }



        //静态的 在内存中是唯一的
        static Random r = new Random();
        /// <summary>
        /// 当敌人
        /// </summary>
        public override void Move()
        {
            base.Move();
            //给一个很小的概率 产生随机数
            if (r.Next(0, 100) < 5)
            {
                switch (r.Next(0, 4))
                {
                    case 0:
                        this.Dir = Direction.Up;
                        break;
                    case 1:
                        this.Dir = Direction.Down;
                        break;
                    case 2:
                        this.Dir = Direction.Left;
                        break;
                    case 3:
                        this.Dir = Direction.Right;
                        break;
                }
            }
        }

    }
}
