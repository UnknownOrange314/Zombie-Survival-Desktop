using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTYD.AI
{
    class SearchState:AI_State
    {
         public SearchState(String m,KTYD.AI.Enemy e,KTYD.Model.Map mm):base(m,e,mm)
        {

        }
         public override void  update()
         {
            
                 myEntity.moveForward(1f);
             
         

         }
    }
}
