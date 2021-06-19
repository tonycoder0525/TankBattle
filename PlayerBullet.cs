using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankGameV._10版本.Properties;
namespace TankGameV._10版本
{
    class PlayerBullet:Bullet
    {
        //导入玩家子弹图片
        private static Image img = Resources.tankmissile;
        public PlayerBullet(Tank tank, int speed, int life, int power)
            : base(tank, speed, life, power, img)
        {
            this.Power = power;
        }
    }
}
