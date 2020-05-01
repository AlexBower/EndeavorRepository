using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovingNPC : NonPlayer
{
    public override void TakeTurn()
    {
        MoveRandom();
    }

    private void MoveRandom()
    {
        int vertical = Random.Range(-1, 2);
        int horizontal = Random.Range(-1, 2);

        if (horizontal != 0 || vertical != 0)
        {
            RaycastHit2D hit;

            if (horizontal != 0)
            {
                if (currentlyMovingHorizontal && vertical != 0)
                {
                    MoveVertical(horizontal, vertical, out hit);
                }
                else
                {
                    MoveHorizontal(horizontal, vertical, out hit);
                }
            }
            else if (vertical != 0)
            {
                if (!currentlyMovingHorizontal && horizontal != 0)
                {
                    MoveHorizontal(horizontal, vertical, out hit);
                }
                else
                {
                    MoveVertical(horizontal, vertical, out hit);
                }
            }
        }
    }
}
