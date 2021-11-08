using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Algorithm_Manager : MonoBehaviour
{
    [SerializeField] int SampleCount;
    [SerializeField] int kCount;
    [SerializeField] int maxWidth, maxHeight;
    [SerializeField] GameObject samplePrefab;
    [SerializeField] GameObject cluster;
    [SerializeField] GameObject clusterGroup;
    [SerializeField] GameObject sampleGroup;

    private List<Transform> sampleList = new List<Transform>();
    private List<ListContainer> doList = new List<ListContainer>();
    private int doNum = 0;
    private double fValue = 0;
    Transform thisObject;
    bool isOver = false;
    bool[] isSame;
    void Start()
    {
        thisObject = gameObject.transform;
        for(int i = 0; i < SampleCount; i++)
        {
            Instantiate(samplePrefab, thisObject);
        }
        for(int i = 0; i < kCount; i++)
        {
            Instantiate(cluster, clusterGroup.transform);
        }
        isSame = new bool[kCount];

    }

    public void startAlgorithm()
    {
        for (int j = 0; j < kCount; j++)
        {
            Transform kPtr = clusterGroup.gameObject.transform.GetChild(j).gameObject.transform;
            doList.Add(new ListContainer(j, kPtr.position));
            doNum++;
        }
        while (!isOver)
        {
            clusterAssociate();
            changeClusterPosition();
        }
        for (int j = 0; j < kCount; j++)
        {
            Transform kPtr = clusterGroup.gameObject.transform.GetChild(j).gameObject.transform;
            for (int i = 0; i < SampleCount; i++)
            {
                Transform sPtr = gameObject.transform.GetChild(i).gameObject.transform;
                if (j == sPtr.GetComponent<SampleCircle>().getClusterNum())
                {
                    fValue += Vector3.Distance(sPtr.position, kPtr.position);
                }
            }
        }
        Debug.Log("목적함수 값 = " + fValue);
    }
    
    private void clusterAssociate()
    {
        for(int i = 0; i < SampleCount; i++)
        {
            double minDist = maxHeight + maxWidth;
            Transform sPtr = gameObject.transform.GetChild(i).gameObject.transform;
            for (int j = 0; j < kCount; j++)
            {
                Transform kPtr = clusterGroup.gameObject.transform.GetChild(j).gameObject.transform;
                double dist = Vector3.Distance(sPtr.position, kPtr.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    Color tmp = kPtr.GetComponent<ClusterSetting>().getColor();
                    sPtr.GetComponent<SampleCircle>().setColor(tmp);
                    sPtr.GetComponent<SampleCircle>().setCluster(j);
                }
            }
        }
    }

    private void changeClusterPosition()
    {
        for(int j = 0; j < kCount; j++)
        {
            Transform kPtr = clusterGroup.gameObject.transform.GetChild(j).gameObject.transform;
            var cPositonX = new List<float>();
            var cPositonY = new List<float>();
            for (int i = 0; i < SampleCount; i++)
            {
                Transform sPtr = gameObject.transform.GetChild(i).gameObject.transform;
                if(j == sPtr.GetComponent<SampleCircle>().getClusterNum())
                {
                    cPositonX.Add(sPtr.position.x);
                    cPositonY.Add(sPtr.position.y);
                }
            }
            if (kPtr.position == new Vector3(cPositonX.Average(), cPositonY.Average()))
            {
                isSame[j] = true;
            }
            else
            {
                kPtr.position = new Vector3(cPositonX.Average(), cPositonY.Average());
                doList.Add(new ListContainer(j, kPtr.position));
                doNum++;
            }
        }
        for(int i = 0; i < kCount; i++)
        {
            if (isSame[i] == false)
                return;
        }
        isOver = true;
    }


    public void ClickOnUndo()
    {
        if (doNum > 0)
        {
            doNum--;
            controlCluster();
        }
    }
    public void ClickOnNext()
    {
        if (doNum < doList.Count - 1)
        {
            doNum++;
            controlCluster();
        }

    }

    public void ClickOnMedoids()
    {
        for (int j = 0; j < kCount; j++)
        {
            Transform kPtr = clusterGroup.gameObject.transform.GetChild(j).gameObject.transform;
            doList.Add(new ListContainer(j, kPtr.position));
            doNum++;
        }
        while (!isOver)
        {
            clusterAssociate();
            medoidsAlgorithm();
        }
        for (int j = 0; j < kCount; j++)
        {
            Transform kPtr = clusterGroup.gameObject.transform.GetChild(j).gameObject.transform;
            for (int i = 0; i < SampleCount; i++)
            {
                Transform sPtr = gameObject.transform.GetChild(i).gameObject.transform;
                if (j == sPtr.GetComponent<SampleCircle>().getClusterNum())
                {
                    fValue += Vector3.Distance(sPtr.position, kPtr.position);
                }
            }
        }
        Debug.Log("목적함수 값 = " + fValue);
    }

    private void controlCluster()
    {
        int childNum = doList[doNum].getClusterNum();
        Transform kPtr = clusterGroup.gameObject.transform.GetChild(childNum).gameObject.transform;
        kPtr.position = doList[doNum].getClusterPosition();
        clusterAssociate();
        Debug.Log(doNum);
    }

    private void medoidsAlgorithm()
    {
        for (int j = 0; j < kCount; j++)
        {
            double minDist = maxHeight + maxWidth;
            Transform kPtr = clusterGroup.gameObject.transform.GetChild(j).gameObject.transform;
            for (int i = 0; i < SampleCount; i++)
            {
                Transform sPtr = gameObject.transform.GetChild(i).gameObject.transform;
                double dist = 0;
                int cnt = 0;
                for (int k = 0; k < SampleCount; k++)
                {
                    Transform cPtr = gameObject.transform.GetChild(k).gameObject.transform;
                    if(j == sPtr.GetComponent<SampleCircle>().getClusterNum())
                    {
                        if (cPtr.GetComponent<SampleCircle>().getClusterNum() == sPtr.GetComponent<SampleCircle>().getClusterNum())
                        {
                            dist += Vector3.Distance(cPtr.position, sPtr.position);
                            cnt++;
                        }
                    }
                }
                dist = dist / cnt;
                if (dist < minDist)
                {
                    kPtr.position = sPtr.position;
                    minDist = dist;
                }
                if (dist == minDist)
                {
                    if (kPtr.position == sPtr.position)
                    {
                        isSame[j] = true;
                    }
                }
            }

        }
        for (int i = 0; i < kCount; i++)
        {
            if (isSame[i] == false)
                return;
        }
        isOver = true;
    }
    public int getMaxWidth()
    {
        return maxWidth;
    }

    public int getMaxHeight()
    {
        return maxHeight;
    }

}
