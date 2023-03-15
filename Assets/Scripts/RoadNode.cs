using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadNode : MonoBehaviour
{
    public bool IsSafeSpeak = false;
    public List<RoadNode> PlayerCanArriveNodes = new List<RoadNode>();
    public List<RoadNode> CorpseCanArriveNodes = new List<RoadNode>();

    private EGA_Circle Circle;

    private void Awake()
    {
        Circle = this.GetComponentInChildren<EGA_Circle>();
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
