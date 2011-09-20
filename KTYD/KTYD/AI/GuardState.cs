using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTYD.AI
{
    class GuardState:AI_State
    {
        int countSteps;
        int maxSteps;
        public GuardState(String m,KTYD.AI.Enemy e,KTYD.Model.Map mm):base(m,e,mm)
        {

        }
        public override void update()
        {
            if (countSteps > 0)
            {
                --countSteps;
                myEntity.moveForward(1f);
            }
            else if (countSteps == 0)
            {
                if (maxSteps > 0)
                {
                    maxSteps = maxSteps * -1;
                }
                else
                {
                    maxSteps = maxSteps * -1;
                }
                // Go to the opposite directi
                myEntity.invertRotation();
                countSteps = maxSteps;

            }
            else if (countSteps < 0)
            {
                myEntity.moveForward(1f);
                ++countSteps;
            }

        }
    }
}
