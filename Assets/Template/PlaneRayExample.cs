//This script detects mouse clicks on a plane using Plane.Raycast.
//In this example, the plane is set to the Camera's x and y position, but you can set the z position so the plane is in front of your Camera.
//The normal of the plane is set to facing forward so it is facing the Camera, but you can change this to suit your own needs.

//In your GameObject's Inspector, set your clickable distance and attach a cube GameObject in the appropriate fields

using UnityEngine;

public class PlaneRayExample : MonoBehaviour
{
    //Attach a cube GameObject in the Inspector before entering Play Mode
    //public GameObject m_Cube;

    //This is the distance the clickable plane is from the camera. Set it in the Inspector before running.
    //public float m_DistanceZ;
    //public GameObject target;
    Plane m_Plane;
    Vector3 m_DistanceFromCamera;
    public GameObject prefab;

    [SerializeField]
    private Transform handT = null;
    void Start()
    {
        m_DistanceFromCamera = new Vector3(Camera.main.transform.position.x, 0.0f /*Camera.main.transform.position.y*/, Camera.main.transform.position.z/* + m_DistanceZ*/);
        m_Plane = new Plane(Vector3.up, m_DistanceFromCamera);
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Detect when there is a mouse click
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float enter = 0.0f;

        if (m_Plane.Raycast(ray, out enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            Vector3 oldRotate = Vector3.zero;
            handT.transform.LookAt(hitPoint);
            oldRotate.x = handT.transform.localEulerAngles.x;
            handT.transform.localEulerAngles = oldRotate;
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(prefab, handT.position, handT.rotation);
            }
        }
    }
}