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
    public int CrossNum;
    public int VertNum;
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

    
    // 判断鬼是否可以从 from 横向 发现 to
    public static bool CorpseCanArruve2NodeFromNodeInVert(RoadNode from, RoadNode to  , List<RoadNode> path)
    {
        if (from == null || to == null)
        {
            return false;
        }
        // Debug.LogError("当前节点 " + from.gameObject);
        foreach (var item in from.CorpseCanArriveNodes)
        {
            // Debug.LogError("开始找 " + item.gameObject.name + " , " +
                           // (item.VertNum != to.VertNum || item.CrossNum == to.CrossNum));
            if (item == to)
            {
                // Debug.LogError("已经找到111");
                return true;
            }
            if (item.VertNum != to.VertNum || item.CrossNum == to.CrossNum)
            {
                continue;
            }
            if (path.Contains(item))
            {
                // Debug.LogError(" path 包含 " + item.gameObject.name);
                continue;
            }

            if (item.CorpseCanArriveNodes.Count == 0)
            {
                path.Remove(item);
            }
            path.Add(item);
            // Debug.LogError("寻找节点 " + item.gameObject.name + " , " + to.gameObject.name);
            if (CorpseCanArruve2NodeFromNodeInVert(item, to, path))
            {
                // Debug.LogError("已经找到222");
                return true;
            }
        }

        return false;
    }
}
