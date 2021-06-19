using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankGameV._10版本.Properties;
namespace TankGameV._10版本
{
    class TankBorn : GameObject
    {
        //导入闪烁的图片
        private Image[] imgs = { 
                              Resources.born1,
                              Resources.born2,
                              Resources.born3,
                              Resources.born4
                               };

        public TankBorn(int x, int y)
            : base(x, y)
        {

        }
        int time = 0;
        public override void Draw(Graphics g)
        {
            time++;
            for (int i = 0; i < imgs.Length; i++)
            {
                switch (time % 10)
                { 
                    case 1:
                        g.DrawImage(imgs[0], this.X, this.Y);break;
                    case 3:
                        g.DrawImage(imgs[1], this.X, this.Y);
                        break;
                    case 5:
                        g.DrawImage(imgs[2], this.X, this.Y);
                        break;
                    case 7:
                        g.DrawImage(imgs[3], this.X, this.Y);
                        break;
                }
                
            }
            //当for循环结束之后 也就是闪烁图片播放完成了
            //这个时候 我们应该将闪烁的图片从屏幕上移除
            if (time % 15 == 0)
            {
                GameController.GetInstance().RemoveGameObject(this);
            }
        }

    }
}
