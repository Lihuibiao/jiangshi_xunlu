using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CorpseAniType
{
    Idel = 0 ,
    Awaken = 1,
}

public class CorpseController : MonoBehaviour
{
    public Animator animator;

    private void Awake()
    {
        animator = this.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        IsInAni(CorpseAniType.Idel);
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        // 判断动画是否播放完成
        if (info.normalizedTime >= 0.99f) 
        {
            OnAniEnd(info);
            lastStateInfo = info;
        }
    }

    public void Move2Node(RoadNode node , bool byJump = true)
    {
        if (byJump)
        {
            
        }
        else
        {
            transform.position = new Vector3(node.transform.position.x , node.transform.position.y  , node.transform.position.z + 0.5f);
        }
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
        }
    }
}
