using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TankGameV._10版本.Properties;
namespace TankGameV._10版本
{
    class EnemyBullet:Bullet
    {
        private static Image img = Resources.enemymissile;
        public EnemyBullet(Tank tank, int speed, int life, int power)
            : base(tank, speed, life, power, img)
        { 
            
        }
    }
}
