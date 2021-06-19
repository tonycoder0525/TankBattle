using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankGameV._10版本.Properties;
namespace TankGameV._10版本
{
    class Prop : GameObject
    {
        private static Image imgStar = Resources.star;
        private static Image imgExplosion = Resources.bomb;
        private static Image imgTimer = Resources.timer;


        /// <summary>
        /// 装备的类型  0  代表 五角星 1代表地雷 2代表计时器
        /// </summary>
        public int Type
        {
            get;
            set;
        }

        public Prop(int x, int y, int Type)
            : base(x, y, imgStar.Width, imgStar.Height)
        {
            this.Type = Type;
        }

        public override void Draw(Graphics g)
        {
            switch (Type)
            { 
                case 0:
                    g.DrawImage(imgStar, this.X, this.Y);
                    break;
                case 1:
                    g.DrawImage(imgExplosion, this.X, this.Y);
                    break;
                case 2:
                    g.DrawImage(imgTimer, this.X, this.Y);
                    break;
            }
        }

    }
}
