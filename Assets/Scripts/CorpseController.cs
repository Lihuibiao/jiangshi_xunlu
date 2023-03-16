using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CorpseAniType
{
    Null = 0 ,
    Idel = 1 ,
    Awaken = 2,
    Jump = 3,
}

public enum CorpseType
{
    Null = 0 ,
    Idel = 1 ,
    Awaken = 2 ,
    Jump = 3,
}

public class CorpseController : MonoBehaviour
{
    public Animator animator;
    public CorpseType corpseType;
    private void Awake()
    {
        animator = this.GetComponentInChildren<Animator>();
        corpseType = CorpseType.Idel;
    }

    private void Update()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        // 判断动画是否播放完成
        if (info.normalizedTime >= 0.99f) 
        {
            OnAniEnd(info);
            lastStateInfo = info;
        }

        if (this.corpseType == CorpseType.Jump && needMove2Node != null)
        {
            if (Vector3.Distance(transform.position, needMove2Node.StandPos) > 0.2f && Vector3.Dot(transform.forward, needMove2Node.transform.position) > 0)
            {
                var speed = Vector3.Distance(transform.position, needMove2Node.StandPos) / 0.35f;
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            else
            {
                if (needMove2Node != null)
                {
                    move2Node(needMove2Node);
                    needMove2Node = null;
                }
                EnterIdeaState();
            }
        }
    }

    public RoadNode needMove2Node;
    public void Move2Node(RoadNode node , bool byJump = true)
    {
        transform.LookAt(node.transform);
        if (byJump)
        {
            needMove2Node = node;
            EnterJumpState();
        }
        else
        {
            move2Node(node);
        }
    }

    public void EnterJumpState()
    {
        Debug.LogError("EnterJumpState  " + IsInAni(CorpseAniType.Idel));
        if (IsInAni(CorpseAniType.Idel))
        {
            this.corpseType = CorpseType.Jump;
            this.animator.SetBool("Awaken" , false);
            this.animator.SetBool("Idel" , false);
            this.animator.SetBool("Jump" , true);
            StartCoroutine("delaySetJumpAniFalse");
        }
    }

    IEnumerator delaySetJumpAniFalse()
    {
        yield return new WaitForSeconds(0.1f);
        this.animator.SetBool("Jump" , false);
    }
    
    public void EnterIdeaState()
    {
        this.corpseType = CorpseType.Idel;
        lastStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (!IsInAni(CorpseAniType.Idel))
        {
            this.animator.SetBool("Awaken" , false);
            this.animator.SetBool("Jump" , false);
            this.animator.SetBool("Idel" , true);
        }

        this.needMove2Node = null;
    }
    
    private AnimatorStateInfo lastStateInfo = new AnimatorStateInfo();
    public bool IsInAni(CorpseAniType aniType)
    {
        //获取动画层 0 指Base Layer.
        AnimatorStateInfo stateinfo = animator.GetCurrentAnimatorStateInfo(0);
        //如果正在播放walk动画.
        if(stateinfo.IsName("Base Layer." + aniType.ToString()))
        {
            return true;
        }

        return false;
    }

    public void OnAniEnd(AnimatorStateInfo info)
    {
        if (info.shortNameHash == lastStateInfo.shortNameHash)
        {
            return;
        }
        if (info.IsName(CorpseAniType.Idel.ToString()))
        {
            Debug.LogError(CorpseAniType.Idel + " 播放结束");   
        }else if (info.IsName(CorpseAniType.Awaken.ToString()))
        {
            Debug.LogError(CorpseAniType.Awaken + " 播放结束");
        }else if (info.IsName(CorpseAniType.Jump.ToString()))
        {
            Debug.LogError(CorpseAniType.Jump + " 播放结束");
            if (needMove2Node != null)
            {
                move2Node(needMove2Node);
                needMove2Node = null;
            }
            EnterIdeaState();
        }
    }

    private void move2Node(RoadNode node)
    {
        transform.position = new Vector3(node.StandPos.x , node.StandPos.y  , node.StandPos.z);
    }
}
