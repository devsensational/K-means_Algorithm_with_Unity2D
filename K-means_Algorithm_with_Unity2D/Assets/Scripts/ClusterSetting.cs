using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterSetting : MonoBehaviour
{
    SpriteRenderer render;
    Color color;
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        color = render.color;
    }

    public Color getColor()
    {
        return color;
    }
}
