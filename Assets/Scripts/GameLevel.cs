using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [Header("玩家开局位置")]
    public RoadNode PlayerStartNode;
    [Header("僵尸开局位置")]
    public RoadNode CorpseStartNode;
    
    private LayerMask mask;

    private CorpseController Corpse;

    private void Awake()
    {
        mask = LayerMask.GetMask("RoadNode");
        Corpse = GameObject.FindObjectOfType<CorpseController>();
    }

    private void Start()
    {
        Corpse.Move2Node(CorpseStartNode , false);
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity , mask))
            {
                Debug.LogError("鼠标点击射线检测到 " + hitInfo.transform.name);
            }   
        }
    }
}
