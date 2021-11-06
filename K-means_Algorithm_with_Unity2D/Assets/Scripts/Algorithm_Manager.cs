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

    private ArrayList sampleList = new ArrayList();
    private ArrayList doList;
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

    void Update()
    {
        
    }

    public int getMaxWidth()
    {
        return maxWidth;
    }

    public int getMaxHeight()
    {
        return maxHeight;
    }

    public void startAlgorithm()
    {
        while (!isOver)
        {
            clusterAssociate();
            changeClusterPosition();
        }
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
            kPtr.position = new Vector3(cPositonX.Average(), cPositonY.Average());
        }
        for(int i = 0; i < kCount; i++)
        {
            if (isSame[i] == false)
                return;
        }
        isOver = true;
    }
}
