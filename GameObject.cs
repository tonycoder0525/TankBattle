using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGameV._10版本
{
    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    abstract class GameObject
    {
        #region 游戏对象的属性
        public int X
        {
            get;
            set;
        }

        public int Y
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        public int Speed
        {
            get;
            set;
        }

        public int Life
        {
            get;
            set;
        }


        public Direction Dir
        {
            get;
            set;
        }
        #endregion

        //初始化对象

        public GameObject(int x, int y, int width, int height, int speed, int life, Direction dir)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Speed = speed;
            this.Life = life;
            this.Dir = dir;
        }


        public abstract void Draw(Graphics g);
        /// <summary>
        /// 游戏对象移动的方法 我们在移动的时候 是根据当前游戏对象的方向
        /// 进行移动
        /// </summary>
        public virtual void Move()
        {
            switch (this.Dir)
            {
                case Direction.Up:
                    this.Y -= this.Speed;
                    break;
                case Direction.Down:
                    this.Y += this.Speed;
                    break;
                case Direction.Left:
                    this.X -= this.Speed;
                    break;
                case Direction.Right:
                    this.X += this.Speed;
                    break;
            }
            //在游戏对象移动完成后 我们应该判断一下 当前游戏对象是否超出当前的窗体 
            if (this.X <= 0)
            {
                this.X = 0;
            }
            if (this.Y <= 0)
            {
                this.Y = 0;
            }
            if (this.X >= 720)
            {
                this.X = 720;
            }
            if (this.Y >= 600)
            {
                this.Y = 600;
            }
        }
        /// <summary>
        /// 用于碰撞检测
        /// </summary>
        /// <returns></returns>
        public Rectangle GetRectangle()
        {
            return new Rectangle(this.X, this.Y, this.Width, this.Height);
        }


        public GameObject(int x, int y, int width, int height)
            : this(x, y, width, height, 0, 0, 0)
        { 
            
        }

        public GameObject(int x, int y)
            : this(x, y, 0, 0, 0, 0, 0)//this 代表当前类的对象 显示的调用自己类中的构造函数
            //base 代表父类空间的引用 显示的调用父类中的构造函数
        { 
            
        }

    }
}
