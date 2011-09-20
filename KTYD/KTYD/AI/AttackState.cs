using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTYD.AI
{


    class AttackState:AI_State
    {
         public AttackState(String m,KTYD.AI.Enemy e,KTYD.Model.Map mm):base(m,e,mm)
        {
             
        }

        public override void update()
        {

            if (myEntity.alreadyShot == false)
            {
                //KTYD.Model.Bullet temp = reuseBullet();



                //if (temp!=null)
                //bulletList.Add(reuseBullet());
                //temp = reuseBullet();
                //if (temp!=null)
                //bulletList.Add(temp);
                myMap.addBullet(myEntity, myEntity.getWeapon(), EntityType.ENEMY);



                myEntity.AlreadyShot = true;
            }


        }

    }

}
