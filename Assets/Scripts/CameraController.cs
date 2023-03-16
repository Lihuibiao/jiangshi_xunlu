using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Target;
    public float smooth = 2f;
    Vector3 distance;

    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        distance = transform.position - Target.transform.position;
        camera = Camera.main;
    }
 
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(Target.transform.position+distance, transform.position, Time.deltaTime * smooth);
        transform.LookAt(Target.transform.position);//摄像头Wink物体，不然不丝滑
        
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            camera.transform.localPosition += new Vector3(0, 0, 1) * Input.GetAxis("Mouse ScrollWheel") * 2;
            if (camera.transform.localPosition.z > 3)
            {
                camera.transform.localPosition = new Vector3(0, camera.transform.localPosition.y, 3);
            }else if (camera.transform.localPosition.z < 0)
            {
                camera.transform.localPosition = new Vector3(0, camera.transform.localPosition.y, 0);
            }
        }
    }
}
