using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListContainer
{
    int clusterNum;
    Vector3 clusterPosition;

    public ListContainer(int _clusterNum, Vector3 _clusterPosition)
    {
        clusterNum = _clusterNum;
        clusterPosition = _clusterPosition;
    }

    public int getClusterNum()
    {
        return clusterNum;
    }
    public Vector3 getClusterPosition()
    {
        return clusterPosition;
    }
}
