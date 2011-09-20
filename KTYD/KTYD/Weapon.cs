using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTYD
{



    public enum WeaponType
    {
        PISTOL,
        SHOTGUN,
        M16,
        FLAMETHROWER,
        BOMB
    }
    public class Weapon
    {

        private WeaponType myType;
          public static int          PistolAttackValue = 3;
                   public static float PistolSpeedValue = 1.5f;
                   public static int PistolHealth = 50;
                    public static int PistolState = GameConfig.IMG_BULLET;
                    public static int PistolEntityOffset=GameConfig.IMG_OFFSET_PISTOL;
                        public static int PistolCooldown=3000;

        
              public static int M16attackValue = 1;
               public static float M16speedValue = 2.5f;
               public static int M16health = 15;
                public static int M16state = GameConfig.IMG_BULLET;
        public static int M16EntityOffset = GameConfig.IMG_OFFSET_M16;
        public static int M16Cooldown=500;
                   
            public static int FlamethrowerAttackValue = 4;
            public static float FlamethrowerSpeedValue = 1.2f;
             public static int FlamethrowerHealth = 25;
             public static int FlamethrowerState = GameConfig.IMG_BULLET_FLAME;
          public static int FlamethrowerEntityOffset = GameConfig.IMG_OFFSET_FLAMETHROWER;
        public static int FlamethrowerCooldown=1;
       
              public static int ShotgunAttackValue = 5;
              public static float ShotgunSpeedValue = 1f;
             public static int ShotgunHealth=50;
            public static int ShotgunState=GameConfig.IMG_BULLET_SHOTGUN;
          public static int ShotgunEntityOffset=GameConfig.IMG_OFFSET_SHOTGUN;
        public static int ShotgunCooldown=3000;

    



            public int AttackValue;
            public float SpeedValue;
            public int Health;
            public int State;
            public  int EntityOffset;
            public int cooldown;

            public Weapon(int A, float S, int H, int St,int eOff,int c)
            {
                myType = WeaponType.PISTOL;
                AttackValue = A;
                SpeedValue = S;
                Health = H;
                State = St;
                EntityOffset = eOff;
                cooldown = c;
            }

            public void addBullet(Entity e, EntityType t, List<Entity> trashBullets, List<Entity> bulletList)
            {


                int trashSize = trashBullets.Count();

                if (trashSize > 0)
                {


                    KTYD.Model.Bullet b = (KTYD.Model.Bullet)trashBullets[trashSize - 1];
                    b.reuse(e, this, t, bulletList, trashBullets);
        




                }
                else
                {
           
                    bulletList.Add(this.createBullet(e.frontLoc.X, e.frontLoc.Y, e.Rotate, t));
    
                }


                //Needs to be refactored
                if (getType()==WeaponType.SHOTGUN)
                {
                    if (trashBullets.Count > 0)
                    {

                        KTYD.Model.Bullet b = (KTYD.Model.Bullet)trashBullets[trashSize - 1];
                        b.reuse(e, this, t, bulletList, trashBullets);
                    }
                    else
                    {
                        bulletList.Add(this.createBullet(e.Center.X, e.Center.X, e.Rotate, t));
                    }
                    if (trashBullets.Count > 0)
                    {

                        KTYD.Model.Bullet b = (KTYD.Model.Bullet)trashBullets[trashSize - 1];
                        b.reuse(e, this, t, bulletList, trashBullets);
                    }
                    else
                    {
                        bulletList.Add(this.createBullet(e.Center.X, e.Center.X, e.Rotate, t));
                    }
               

                }
             
            }

            public KTYD.Model.Bullet createBullet(float x,float y,float rotate,EntityType owner)
            {




                return new Model.Bullet(x, y, rotate, owner, AttackValue, (float)SpeedValue, Health,State);
            }
            public static Weapon createShotgun()
            {

                return new Weapon(Weapon.ShotgunAttackValue, Weapon.ShotgunSpeedValue, Weapon.ShotgunHealth, Weapon.ShotgunState, Weapon.ShotgunEntityOffset, Weapon.ShotgunCooldown);
            }
            public static Weapon createPistol()
            {
                return new Weapon(Weapon.PistolAttackValue, Weapon.PistolSpeedValue, Weapon.PistolHealth, Weapon.PistolState, Weapon.PistolEntityOffset, Weapon.PistolCooldown);
            }
            public static Weapon createFlamethrower()
            {
                return new Weapon(Weapon.FlamethrowerAttackValue, Weapon.FlamethrowerSpeedValue, Weapon.FlamethrowerHealth, Weapon.FlamethrowerState, Weapon.FlamethrowerEntityOffset,Weapon.FlamethrowerCooldown);
            }
            public static Weapon createM16()
            {
                return createShotgun();
            }

            public WeaponType getType()
            {
                return myType;
            }
            public int getAttackValue()
            {
                return AttackValue;
            }
            public float getSpeedValue()
            {
                return SpeedValue;
            }
            public int getHealth()
            {
                return Health;
            }
            public int getState()
            {

                return State;
            }
        public int getEntityOffset()
        {
            return EntityOffset;
        }
        public int getCooldown()
        {
            return cooldown;

        }
       





   
    }
}
