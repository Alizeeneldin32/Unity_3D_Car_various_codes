using UnityEngine;
using System.Collections;

// ADD THIS SCRIPT TO EACH OF THE WHEEL MESHES / WHEEL MESH CONTAINER OBJECTS
public class Wheel : MonoBehaviour {
  public Transform wheelMesh;
  public WheelCollider wheelCollider;

  private RaycastHit hit;

  // Initialization
  void Start () {

  }

  // Display
  void Update () {
    //wheelCCenter = wheelC.transform.TransformPoint(wheelC.center);
        if (Physics.Raycast(wheelCollider.transform.position, -wheelCollider.transform.up, out hit, wheelCollider.suspensionDistance + wheelCollider.radius))
        {
            wheelMesh.position = hit.point + wheelCollider.transform.up * wheelCollider.radius;
        }
        //if ( Physics.Raycast(wheelCCenter, -wheelC.transform.up, out hit, wheelC.suspensionDistance + wheelC.radius) ) {
        //  transform.position = hit.point + (wheelC.transform.up * wheelC.radius);
        else {
		wheelMesh.position = wheelCollider.transform.position - (wheelCollider.transform.up* wheelCollider.suspensionDistance);
	}

    wheelMesh.transform.Rotate(wheelCollider.rpm/60*360* Time.deltaTime*-1,0,0);
        wheelMesh.localEulerAngles = new Vector3(wheelMesh.localEulerAngles.x, wheelCollider.steerAngle - wheelMesh.localEulerAngles.z, wheelMesh.localEulerAngles.z);
    }
  }


