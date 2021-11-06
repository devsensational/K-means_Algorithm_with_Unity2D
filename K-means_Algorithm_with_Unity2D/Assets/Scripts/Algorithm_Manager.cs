using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithm_Manager : MonoBehaviour
{
    [SerializeField] int SampleCount;
    [SerializeField] int kCount;
    [SerializeField] int maxWidth, maxHeight;
    [SerializeField] GameObject samplePrefab;
    [SerializeField] GameObject Associate;
    
    private ArrayList sampleList = new ArrayList();
    private ArrayList doList;
    Transform thisObject;
    void Start()
    {
        thisObject = gameObject.transform;
        for(int i = 0; i < SampleCount; i++)
        {
            Instantiate(samplePrefab, thisObject);
        }

    }

    void Update()
    {
        
    }
    public void test()
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
}
