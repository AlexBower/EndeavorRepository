using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetMovingNPC : NonPlayer
{
    private List<Location.Direction> directionList;
    public int directionIndex;

    protected override void Start()
    {
        base.Start();
        if (directionList == null || directionList.Count == 0)
        {
            directionList = CreateDirectionList();
        }
        directionIndex = 0;
    }

    private List<Location.Direction> CreateDirectionList()
    {
        List<Location.Direction> list = new List<Location.Direction>();
        list.Add(Location.Direction.EAST);
        list.Add(Location.Direction.NORTH);
        list.Add(Location.Direction.STAY);
        list.Add(Location.Direction.STAY);
        list.Add(Location.Direction.STAY);
        list.Add(Location.Direction.WEST);
        list.Add(Location.Direction.SOUTH);
        list.Add(Location.Direction.STAY);
        list.Add(Location.Direction.STAY);
        list.Add(Location.Direction.STAY);
        return list;
    }


    private void MoveNextInDirectionsList()
    {
        Move(directionList[directionIndex], out RaycastHit2D hit);
    }

    override protected void MoveSuccessfull()
    {
        base.MoveSuccessfull();
        directionIndex = (directionIndex + 1) % directionList.Count;
    }

    public override void TakeTurn()
    {
        MoveNextInDirectionsList();
    }
}
