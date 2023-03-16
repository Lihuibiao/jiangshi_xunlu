using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum MonkAniType
{
    Null = 0 ,
    Idel = 1 ,
    Run = 2,
}

public enum MonkType
{
    Null = 0 ,
    Idel = 1 ,
    Run = 2,
}

public class MonkController : MonoBehaviour
{
    public Animation animation;
    public MonkType monkType;
    public RoadNode currentNode;
    public RoadNode needMove2Node;
    public bool canMove = true;
    private AnimatorStateInfo lastStateInfo = new AnimatorStateInfo();
    public static MonkController Inst;
    private void Awake()
    {
        Inst = this;
        animation = this.GetComponentInChildren<Animation>();
        monkType = MonkType.Idel;
    }

    public float speed;
    private void Update()
    {
        if (this.monkType == MonkType.Run && needMove2Node != null)
        {
            float step = speed * Time.deltaTime;
            gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition,needMove2Node.StandPos,step);
            if (Vector3.Distance(transform.position, needMove2Node.StandPos) < 0.2f)
            {
                if (needMove2Node != null)
                {
                    move2Node(needMove2Node);
                    needMove2Node = null;
                }
                EnterIdeaState();
                OnArrivedNode();
            }
        }
    }
    
    public void Move2Node(RoadNode node , bool byRun = true)
    {
        if (this.monkType == MonkType.Run)
        {
            return;
        }
        if (currentNode != null && !currentNode.PlayerCanArriveNodes.Contains(node))
        {
            return;
        }

        if (!canMove)
        {
            return;
        }
        
        if (currentNode != null)
        {
            foreach (var item in currentNode.PlayerCanArriveNodes)
            {
                item.SetNextStepCanNtArriveColor();
            }
        }
        
        currentNode = node;
        speed = Vector3.Distance(transform.position, node.StandPos) / 1f;
        transform.LookAt(node.transform);
        if (byRun)
        {
            needMove2Node = node;
            EnterRunState();
        }
        else
        {
            move2Node(node);
            OnArrivedNode();
        }
    }

    public void EnterIdeaState()
    {
        this.monkType = MonkType.Idel;
        this.animation.CrossFade("Idel");;
        this.needMove2Node = null;
    }
    
    public void EnterRunState()
    {
        this.monkType = MonkType.Run;
        if (!IsInAni(MonkAniType.Run))
        {
            this.animation.CrossFade("Run");
        }
    }

    private void move2Node(RoadNode node)
    {
        transform.position = new Vector3(node.StandPos.x , node.StandPos.y  , node.StandPos.z);
    }
    
    public bool IsInAni(MonkAniType aniType)
    {

        if (this.animation.clip.name == aniType.ToString())
        {
            return true;
        }
        return false;
    }

    private void OnArrivedNode()
    {
        foreach (var item in currentNode.PlayerCanArriveNodes)
        {
            item.SetNextStepCanArriveColor();
        }

        CorpseController.Inst.OnPlayerMove(this , this.currentNode);
    }
}
