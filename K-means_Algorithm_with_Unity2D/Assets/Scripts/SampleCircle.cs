using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SampleCircle : MonoBehaviour
{
    int R, G, B;
    [SerializeField] int x, y;
    int maxWidth, maxHeight;
    GameObject p;
    SpriteRenderer render;
    int clusterNum;
    
    void Start()
    {
        p = transform.parent.gameObject;
        render = GetComponent<SpriteRenderer>();
        maxWidth = p.GetComponent<Algorithm_Manager>().getMaxWidth();
        maxHeight = p.GetComponent<Algorithm_Manager>().getMaxHeight();
        x = Random.Range(0, maxWidth);
        y = Random.Range(0, maxHeight);
        setPosition();
    }

    void Update()
    {
        
    }

    public void setColor(Color _color)
    {
        render.color = _color;
    }

    public void setPosition()
    {
        transform.position = new Vector3(x, y);
    }

    public void setCluster(int num)
    {
        clusterNum = num;
    }

    public int getClusterNum()
    {
        return clusterNum;
    }
}
