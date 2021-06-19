using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TankGameV._10版本.Properties;
namespace TankGameV._10版本
{

    /// <summary>
    /// 这个单例类用来创建我们全局唯一的游戏对象
    /// </summary>
    class GameController
    {
        private GameController()
        { }

        public static GameController instance = null;

        public static GameController GetInstance()
        {
            if (instance == null)
            {
                instance = new GameController();
            }
            return instance;
        }

        public PlayerTank PlayerTank
        {
            get;
            set;
        }

        KeyEventArgs moveBuffer, fireBuffer;
        bool moveBufferEnabled = false, fireBufferEnabled = false;

        public void AddBuffer(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.K)
            {
                fireBufferEnabled = true;
                fireBuffer = e;
            }
            else
            {
                moveBufferEnabled = true;
                moveBuffer = e;
            }
        }

        public void ClearBuffer()
        {
            if (moveBufferEnabled)
                PlayerTank.KeyDown(moveBuffer);
            if (fireBufferEnabled)
                PlayerTank.KeyDown(fireBuffer);
            moveBufferEnabled = fireBufferEnabled = false;
        }


        //将我们的敌人存储在泛型集合中
        List<EnemyTank> enemyTanks = new List<EnemyTank>();
        List<PlayerBullet> playerBullets = new List<PlayerBullet>();
        List<EnemyBullet> enemyBullets = new List<EnemyBullet>();
        List<Explosion> bombs = new List<Explosion>();
        List<Prop> props = new List<Prop>();

        List<TankBorn> listTankBorn = new List<TankBorn>();
        List<Wall> walls = new List<Wall>();

        Random r = new Random();

        //hjh：根据已有坦克的位置生成新的敌方坦克
        public void NewEnemyTank(int width, int height)
        {
            //Console.Write(PlayerTank.X);
            //Console.Write(PlayerTank.Y);
            while (true)
            {
                EnemyTank enemyTank = new EnemyTank(r.Next(0, width), r.Next(0, height), r.Next(0, 3), Direction.Down);
                bool flg = false;
                for (int i = 0; i < enemyTanks.Count(); i++)
                    if (enemyTanks[i].GetRectangle().IntersectsWith(enemyTank.GetRectangle()))
                        flg = true;
                int dis = Math.Abs(PlayerTank.X - enemyTank.X) + Math.Abs(PlayerTank.Y - enemyTank.Y);
                if (dis <= 200) flg = true;
                if (!flg)
                {
                    AddGameObject(enemyTank);
                    //Console.Write(enemyTank.X);
                    //Console.Write(enemyTank.Y);
                    break;
                }
            }

        }

        /// <summary>
        /// 添加游戏对象
        /// </summary>
        /// <param name="go"></param>
        public void AddGameObject(GameObject go)
        {
            if (go is PlayerTank)
            {
                PlayerTank = go as PlayerTank;//as  如果转换成功 则返回对应的对象 否则返回一个null
            }
            else if (go is EnemyTank)
            {
                enemyTanks.Add(go as EnemyTank);
            }
            else if (go is PlayerBullet)
            {
                playerBullets.Add(go as PlayerBullet);
            }
            else if (go is EnemyBullet)
            {
                enemyBullets.Add(go as EnemyBullet);
            }
            else if (go is Explosion)
            {
                bombs.Add(go as Explosion);
            }
            else if (go is TankBorn)
            {
                listTankBorn.Add(go as TankBorn);
            }
            else if (go is Prop)
            {
                props.Add(go as Prop);
            }
            else if (go is Wall)
            {
                walls.Add(go as Wall);
            }
        }


        public void RemoveGameObject(GameObject go)
        {

            if (go is Explosion)
            {
                bombs.Remove(go as Explosion);
            }
            if (go is PlayerBullet)
            {
                playerBullets.Remove(go as PlayerBullet);
            }
            if (go is EnemyBullet)
            {
                enemyBullets.Remove(go as EnemyBullet);
            }
            if (go is EnemyTank)
            {
                enemyTanks.Remove(go as EnemyTank);
            }
            if (go is TankBorn)
            {
                listTankBorn.Remove(go as TankBorn);
            }
            if (go is Prop)
            {
                props.Remove(go as Prop);
            }
            if (go is Wall)
            {
                walls.Remove(go as Wall);
            }
        }



        /// <summary>
        /// 绘制游戏对象
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            if (PlayerTank.Life > 0)
            {
                PlayerTank.Draw(g);
            }
            for (int i = 0; i < enemyTanks.Count; i++)
            {
                enemyTanks[i].Draw(g);
            }
            for (int i = 0; i < playerBullets.Count; i++)
            {
                playerBullets[i].Draw(g);
            }
            for (int i = 0; i < enemyBullets.Count; i++)
            {
                enemyBullets[i].Draw(g);
            }
            for (int i = 0; i < bombs.Count; i++)
            {
                bombs[i].Draw(g);
            }
            for (int i = 0; i < listTankBorn.Count; i++)
            {
                listTankBorn[i].Draw(g);
            }
            for (int i = 0; i < props.Count; i++)
            {
                props[i].Draw(g);
            }
            for (int i = 0; i < walls.Count; i++)
            {
                walls[i].Draw(g);
            }
        }



        /// <summary>
        /// 碰撞检测的方法
        /// </summary>
        public void DetectCollision()
        {
            #region 首先来判断玩家的子弹是否打在了敌人的身上
            for (int i = 0; i < playerBullets.Count; i++)
            {
                for (int j = 0; j < enemyTanks.Count; j++)
                {

                    if (playerBullets[i].GetRectangle().IntersectsWith(enemyTanks[j].GetRectangle()))
                    {
                        //表示玩家的子弹打到了敌人的身上
                        //敌人应该减少生命值
                        enemyTanks[j].Life -= playerBullets[i].Power;
                        enemyTanks[j].IsOver();
                        //当玩家坦克的子弹击中敌人坦克的时候  应该将玩家坦克子弹移除
                        playerBullets.Remove(playerBullets[i]);
                        break;
                    }
                }
            }
            #endregion
            #region 判断敌人的子弹是否打在了玩家的身上
            for (int i = 0; i < enemyBullets.Count; i++)
            {
                //敌人子弹的矩形区域跟玩家的矩形区域相交
                if (enemyBullets[i].GetRectangle().IntersectsWith(PlayerTank.GetRectangle()))
                {

                    PlayerTank.Life -= PlayerTank.Life;
                    PlayerTank.IsOver();
                    //将敌人子弹删除
                    enemyBullets.Remove(enemyBullets[i]);

                    break;
                }
            }
            #endregion
            #region 玩家是否和产生的装备发生了碰撞
            for (int i = 0; i < props.Count; i++)
            {
                //玩家吃到了装备
                if (props[i].GetRectangle().IntersectsWith(PlayerTank.GetRectangle()))
                {
                    //调用CollectProps
                    CollectProps(props[i].Type);
                    //玩家吃到了装备后 应该将装备移除
                    props.Remove(props[i]);
                    //添加吃了装备的声音
                    SoundPlayer sp = new SoundPlayer(Resources.add);
                    sp.Play();
                }
            }
            #endregion

            #region 判断敌人是否和墙体发生了碰撞
            for (int i = 0; i < walls.Count; i++)
            {
                for (int j = 0; j < enemyTanks.Count; j++)
                {
                    if (walls[i].GetRectangle().IntersectsWith(enemyTanks[j].GetRectangle()))
                    {
                        //当敌人和墙体发生碰撞的时候 我们应该让敌人的坐标固定到
                        //碰撞的位置，不允许穿过墙体
                        //需要判断 敌人是从哪个方向过来发生碰撞的
                        switch (enemyTanks[j].Dir)
                        {
                            case Direction.Up:
                                enemyTanks[j].Y = walls[i].Y + walls[i].Height;
                                break;

                            case Direction.Down:
                                enemyTanks[j].Y = walls[i].Y - walls[i].Height;
                                break;
                            case Direction.Left:
                                enemyTanks[j].X = walls[i].X + walls[i].Width;
                                break;
                            case Direction.Right:
                                enemyTanks[j].X = walls[i].X - walls[i].Width;
                                break;
                        }
                    }
                }
            }
            #endregion
            #region 判断玩家是否和墙体发生了碰撞
            for (int i = 0; i < walls.Count; i++)
            {

                if (walls[i].GetRectangle().IntersectsWith(PlayerTank.GetRectangle()))
                {
                    //当玩家和墙体发生碰撞的时候 我们应该让玩家的坐标固定到
                    //碰撞的位置，不允许穿过墙体
                    //需要判断 玩家是从哪个方向过来发生碰撞的
                    switch (PlayerTank.Dir)
                    {
                        case Direction.Up:
                            PlayerTank.Y = walls[i].Y + walls[i].Height;
                            break;

                        case Direction.Down:
                            PlayerTank.Y = walls[i].Y - walls[i].Height;
                            break;
                        case Direction.Left:
                            PlayerTank.X = walls[i].X + walls[i].Width;
                            break;
                        case Direction.Right:
                            PlayerTank.X = walls[i].X - walls[i].Width;
                            break;
                    }
                }

            }
            #endregion


            #region 判断玩家的子弹是否打到了墙体
            for (int i = 0; i < playerBullets.Count; i++)
            {
                for (int j = 0; j < walls.Count; j++)
                {
                    if (playerBullets[i].GetRectangle().IntersectsWith(walls[j].GetRectangle()))
                    {
                        //移除玩家子弹
                        playerBullets.Remove(playerBullets[i]);
                        //移除被击中的墙体
                        walls.Remove(walls[j]);
                        break;
                    }
                }
            }
            #endregion


            #region 判断玩家的子弹是否和敌人的子弹相撞
            for (int i = 0; i < playerBullets.Count; i++)
            {
                for (int j = 0; j < enemyBullets.Count; j++)
                {
                    if (playerBullets[i].GetRectangle().IntersectsWith(enemyBullets[j].GetRectangle()))
                    {
                        playerBullets.Remove(playerBullets[i]);
                        enemyBullets.Remove(enemyBullets[j]);
                        break;
                    }
                }
            }
            #endregion

            #region 判断玩家的坦克是否与敌人碰撞
            for (int j = 0; j < enemyTanks.Count; j++)
            {
                if (PlayerTank.GetRectangle().IntersectsWith(enemyTanks[j].GetRectangle()))
                {
                    //与装甲车相撞
                    if (enemyTanks[j].EnemyTankType == 0)
                    {
                        enemyTanks[j].Life = 0;
                        enemyTanks[j].IsOver();
                        break;
                    }
                    else if (enemyTanks[j].EnemyTankType == 1 && enemyTanks[j].Life == 1)
                    {//与血量为一的轻型坦克相撞
                        enemyTanks[j].Life = 0;
                        enemyTanks[j].IsOver();
                        break;
                    }
                    else if (enemyTanks[j].EnemyTankType == 1 && enemyTanks[j].Life > 1)
                    {//与血量不为一的轻型坦克相撞
                        PlayerTank.Life -= PlayerTank.Life;
                        PlayerTank.IsOver();

                    }
                    else if (enemyTanks[j].EnemyTankType == 2)
                    {//与重型坦克相撞
                        PlayerTank.Life -= PlayerTank.Life;
                        PlayerTank.IsOver();


                        break;
                    }

                }
            }
            #endregion
            #region 判断敌人的坦克是否与敌人碰撞
            for (int j = 0; j < enemyTanks.Count; j++)
            {
                for (int i = 0; i < enemyTanks.Count; i++)
                {
                    if (i == j)
                    {
                        break;
                    }
                    else if (enemyTanks[j].GetRectangle().IntersectsWith(enemyTanks[i].GetRectangle()))
                    {
                        switch (enemyTanks[j].Dir)
                        {
                            case Direction.Up:
                                enemyTanks[j].Y = enemyTanks[j].Y + enemyTanks[i].Height / 2;
                                break;

                            case Direction.Down:
                                enemyTanks[j].Y = enemyTanks[j].Y - enemyTanks[i].Height / 2;
                                break;
                            case Direction.Left:
                                enemyTanks[j].X = enemyTanks[j].X + enemyTanks[i].Width / 2;
                                break;
                            case Direction.Right:
                                enemyTanks[j].X = enemyTanks[j].X - enemyTanks[i].Width / 2;
                                break;
                        }
                    }
                }
            }
            #endregion
        }



        public void CollectProps(int Type)
        {
            switch (Type)
            {
                case 0://吃到了五角星 让玩家的子弹威力变大
                    //怎么让玩家子弹威力变大 BulletLevel++
                    if (PlayerTank.BulletLevel < 3)
                    {
                        PlayerTank.BulletLevel++;
                    }
                    break;
                case 1:
                    //吃地雷后 应该炸掉一片敌人 12  3    9
                    for (int i = 0; i < enemyTanks.Count; i++)
                    {
                        //hjh：判断敌方坦克与玩家的曼哈顿距离
                        //若曼哈顿距离<=100则炸毁该敌方坦克
                        EnemyTank enemyTank = enemyTanks[i];
                        int distance = Math.Abs(enemyTank.X - PlayerTank.X) + Math.Abs(enemyTank.Y - PlayerTank.Y);
                        if (distance > 500) continue;


                        //把敌人坦克的生命值赋值为0
                        enemyTanks[i].Life = 0;
                        //调用敌人死亡的方法
                        enemyTanks[i].IsOver();
                    }
                    break;
                case 2://想办法让所有的敌人暂停
                    for (int i = 0; i < enemyTanks.Count; i++)
                    {
                        //让标记变成false
                        enemyTanks[i].isStop = false;
                    }

                    break;
            }
        }


    }
}
