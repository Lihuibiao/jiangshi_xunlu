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

    private MonkController Monk;
    private void Awake()
    {
        mask = LayerMask.GetMask("RoadNode");
        Corpse = GameObject.FindObjectOfType<CorpseController>();
        Monk = GameObject.FindObjectOfType<MonkController>();
    }

    private void Start()
    {
        Corpse.Move2Node(CorpseStartNode , false);
        Corpse.EnterIdeaState();
        
        Monk.Move2Node(PlayerStartNode , false);
        Monk.EnterIdeaState();
    }

    public void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
        
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity , mask))
            {
                var node = hitInfo.transform.GetComponent<RoadNode>();
                if (node != null)
                {
                    Monk.Move2Node(node , true);   
                }
            }
        }
        
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButtonUp(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
        
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity , mask))
            {
                var node = hitInfo.transform.GetComponent<RoadNode>();
                if (node != null)
                {
                    Corpse.Move2Node(node , true);   
                }
            }   
        }
    }
}
