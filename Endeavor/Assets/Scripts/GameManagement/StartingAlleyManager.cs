using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingAlleyManager : AreaManager
{
    private List<GameObject> nonPlayerTestList;

    public override void LoadNPCs()
    {
        nonPlayerTestList = new List<GameObject>();
        turnBased = false;
        for (int i = 0; i < 10; i++)
        {
            GameObject currentNPC = null;
            if (i % 2 == 0)
            {
                currentNPC = Instantiate(randomMovingNPC);
            }
            else
            {
                currentNPC = Instantiate(presetMovingNPC);
            }

            currentNPC.transform.position = new Vector3(i + 1, i + 1, 0);

            if (currentNPC.GetComponent<MovingObject>().IsOnBlockedLocation())
            {
                Destroy(currentNPC);
            }
            else
            {
                nonPlayerTestList.Add(currentNPC);
            }
        }
    }

    public override void TakeNPCTurns()
    {
        for (int i = 0; i < nonPlayerTestList.Count; i++)
        {
            nonPlayerTestList[i].GetComponent<NonPlayer>().TakeTurn();
        }
    }
}
