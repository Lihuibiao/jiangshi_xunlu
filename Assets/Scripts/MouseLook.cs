using UnityEngine;
using System.Collections;

// MouseLook rotates the transform based on the mouse delta.
// To make an FPS style character:
// - Create a capsule.
// - Add the MouseLook script to the capsule.
//   -> Set the mouse look to use MouseX. (You want to only turn character but not tilt it)
// - Add FPSInput script to the capsule
//   -> A CharacterController component will be automatically added.
//
// - Create a camera. Make the camera a child of the capsule. Position in the head and reset the rotation.
// - Add a MouseLook script to the camera.
//   -> Set the mouse look to use MouseY. (You want the camera to tilt up and down like a head. The character already turns.)

[AddComponentMenu("Control Script/Mouse Look")]//设置脚本路径
public class MouseLook : MonoBehaviour
{
    public Camera camera;
    public enum RotationAxes
    {                      //枚举坐标轴
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes axes = RotationAxes.MouseXAndY;      //定义X和Y轴 RotationAxes为旋转轴

    public float sensitivityHor = 9.0f;// 水平灵敏度
    public float sensitivityVert = 9.0f;// 翻转灵敏度

    public float minimumVert = -45.0f;//设定翻转角度的最大值和最小值
    public float maximumVert = 45.0f;

    private float _rotationX = 0;//x轴旋转角度

    void Start()
    {
        // 使刚体不改变旋转
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
            body.freezeRotation = true;//冻结旋转
    }

    void Update()
    {

            //if (Input.GetMouseButton(1))
            {
                if (axes == RotationAxes.MouseX)//判断旋转轴是X or Y轴
                {
                    transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);//围绕X轴旋转 乘以相应的灵敏度
                }
                else if (axes == RotationAxes.MouseY)
                {
                    _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
                    _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);//Mathf.Clamp :限制value的值在min和max之间， 
                    //如果value小于min，返回min。 如果value大于max，返回max，否则返回value

                    transform.localEulerAngles = new Vector3(_rotationX, transform.localEulerAngles.y, 0);//localEulerAngles 自身欧拉角，
                }
                else
                {
                    float rotationY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityHor;

                    _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
                    _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

                    transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
                }
            }
            if (camera!=null&& Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                camera.transform.localPosition += new Vector3(0, 0, 1) * Input.GetAxis("Mouse ScrollWheel") * 10;
                if (camera.transform.localPosition.z > -3)
                {
                    camera.transform.localPosition = new Vector3(0, 0, -3);
                }
            }
    }
}
