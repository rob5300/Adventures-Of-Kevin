using UnityEngine;
using System.Collections;

public class TopdownMouselook : MonoBehaviour {

    void Update() {
        Rotate();
    }

    void Rotate() {
        //http://answers.unity3d.com/questions/585035/lookat-2d-equivalent-.html
        //This solution is from Unity answers, as was unable to workout a 2D equivilant of Lookat. 
        //I knew that Transform.Lookat existed but it is for 3D use and points positive z to the point.

        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }
}
