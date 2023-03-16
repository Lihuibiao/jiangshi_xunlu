using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadNode : MonoBehaviour
{
    public bool IsSafeSpeak = false;
    public List<RoadNode> PlayerCanArriveNodes = new List<RoadNode>();
    public List<RoadNode> CorpseCanArriveNodes = new List<RoadNode>();
    public Vector3 StandPos;
    private EGA_Circle Circle;

    private void Awake()
    {
        Circle = this.GetComponentInChildren<EGA_Circle>();
    }

    public void SetNextStepCanArriveColor()
    {
        Circle.SetColor(Color.white, Color.white);
    }

    public void SetNextStepCanNtArriveColor()
    {
        Circle.SetOrgColor();
    }
    
}
