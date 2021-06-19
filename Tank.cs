﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGameV._10版本
{
    abstract class Tank : GameObject
    {
        //在父类当中需要提供玩家坦克子类和敌人坦克子类所共有的成员
        private Image[] imgs = new Image[] { };

        public Tank(int x, int y, Image[] imgs, int speed, int life, Direction dir)
            : base(x, y, imgs[0].Width, imgs[0].Height, speed, life, dir)
        {
            this.imgs = imgs;
        }


        public abstract void Fire();

        public abstract void IsOver();

        public abstract void Born();

        public int bornTime = 0;
        public bool isMove = false;

        public override void Draw(Graphics g)
        {
            bornTime++;
            if (bornTime % 20 == 0)
            {
                isMove = true;
            }
            if (isMove)
            {
                switch (this.Dir)
                {
                    case Direction.Up:
                        g.DrawImage(imgs[0], this.X, this.Y);
                        break;
                    case Direction.Down:
                        g.DrawImage(imgs[1], this.X, this.Y);
                        break;
                    case Direction.Left:
                        g.DrawImage(imgs[2], this.X, this.Y);
                        break;
                    case Direction.Right:
                        g.DrawImage(imgs[3], this.X, this.Y);
                        break;
                }
            }
        }

    }
}
