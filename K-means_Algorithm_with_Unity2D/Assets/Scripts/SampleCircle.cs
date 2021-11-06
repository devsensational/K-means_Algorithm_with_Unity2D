using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SampleCircle : MonoBehaviour
{
    int R, G, B;
    [SerializeField] int x, y;
    int maxWidth, maxHeight;
    GameObject p;
    
    void Start()
    {
        p = transform.parent.gameObject;
        maxWidth = p.GetComponent<Algorithm_Manager>().getMaxWidth();
        maxHeight = p.GetComponent<Algorithm_Manager>().getMaxHeight();
        x = Random.Range(0, maxWidth);
        y = Random.Range(0, maxHeight);
        setPosition();
    }

    void Update()
    {
        
    }

    public void setRGB(int _R, int _G, int _B)
    {
        R = _R;
        G = _G;
        B = _B;
    }

    public void setPosition()
    {
        transform.position = new Vector3(x, y);
    }
}
