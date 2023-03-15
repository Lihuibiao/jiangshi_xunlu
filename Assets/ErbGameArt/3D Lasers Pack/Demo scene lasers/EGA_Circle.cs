using System;
using UnityEngine;

public class EGA_Circle : MonoBehaviour
{
    Vector3 centerOfCircle;                   //圆心
    public float Ridus = 0.5f;					//半径
    int positionCount;			//完成一个圆的总点数，
    float angle;				//转角，三个点形成的两段线之间的夹角
    Quaternion quaternion;				//Quaternion四元数
    LineRenderer line;          //LineRenderer组件
    
    private Color orgStartColor;
    private Color orgEndColor;
    private void Awake()
    {
        centerOfCircle = transform.position;//new Vector2(0, 0);
        positionCount = 180;
        angle = 360f / (positionCount - 1);
        line = GetComponent<LineRenderer>();
        line.positionCount = positionCount;
        
        orgStartColor = line.startColor;
        orgEndColor = line.endColor;
    }

    
    void Update()
    {
        DrawCircle();
    }
    void DrawCircle()
    {
        centerOfCircle = transform.position;
        for (int i = 0; i < positionCount; i++)
        {
            if (i != 0)
            {
                //默认围着z轴画圆，所以z值叠加，叠加值为每两个点到圆心的夹角
                // 那个轴转 , 将 angle 替换到哪个轴
                quaternion = Quaternion.Euler(quaternion.eulerAngles.x, quaternion.eulerAngles.y + angle, quaternion.eulerAngles.z);
            }
            // Y轴转
            Vector3 forwardPosition = centerOfCircle + quaternion * Vector3.forward * Ridus;
            //Quaternion与Vector3的右乘操作（*）返回一个将原有向量做旋转操作后的新向量.列如：Quaternion.Euler(0, 90, 0) * Vector3(0.0, 0.0, -10) 相当于把向量Vector3(0.0, 0.0, -10)绕y轴旋转90度，得到的结果为Vector3(-10, 0.0.0.0)
            // Vector3 forwardPosition = v + quaternion * Vector3.down * R;
            line.SetPosition(i, forwardPosition);
        }
    }

    public void SetColor(Color startColor , Color endColor)
    {
        line.startColor = startColor;
        line.endColor = endColor;
    }

    public void SetOrgColor()
    {
        line.startColor = orgStartColor;
        line.endColor = orgEndColor;
    }
}