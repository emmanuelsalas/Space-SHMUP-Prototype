using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This is actually outside of the UTILS class
public enum BoundsTest{
	center,
	onScreen,
	offScreen,
}

public class Utils : MonoBehaviour {
	// ========================= Bounds Functions =============================

	//Creates bounds that encapsulate the two Bounds passed in.
	public static Bounds BoundsUnion(Bounds b0, Bounds b1){
		//If the size of one of the bounds is Vector3.zero, ignore that one
		if (b0.size == Vector3.zero && b1.size != Vector3.zero) {
			return (b1);
		} else if (b0.size != Vector3.zero && b1.size == Vector3.zero) {
			return (b0);
		} else if (b0.size == Vector3.zero && b1.size == Vector3.zero) {
			return (b0);
		}
		//Stretch b0 to include the b1.min and b1.max
		b0.Encapsulate (b1.min);
		b0.Encapsulate (b1.max);
		return(b0);
	}

	public static Bounds CombineBoundsOfChildren(GameObject go){
		//Create an empty bounds b
		Bounds b = new Bounds(Vector3.zero, Vector3.zero);
		//If this gameObject has a renderer component
		if (go.GetComponent<Renderer>() != null){
			//expand b to contain the renderer's bounds
			b = BoundsUnion(b,go.GetComponent<Renderer>().bounds);
		}
		//If this GameObject has a Collider Component...
		if (go.GetComponent<Collider>() != null){
			//Expand b to contain the Collider's Bounds
			b = BoundsUnion(b, go.GetComponent<Collider>().bounds);
		}
		//Recursively iterate through each child of this gameObject.transform
		foreach(Transform t in go.transform){
			//Expand b to contain their Bounds as well
			b = BoundsUnion(b,CombineBoundsOfChildren(t.gameObject));
		}

	//Make a static read-only public property camBounds
	static public Bounds camBounds{
		get{
			//if _camBounds hasn't been set yet
			if (_camBounds.sizeof == Vector3.zero){
				//SetCameraBounds using the default Camera
					SetCameraBounds();
			}
				return(_camBounds);
			}
		}
	//This is the private static field that camBounds uses
	static private Bounds _camBounds;

	//This function is used by camBounds to set _camBounds and can also be called directly
		public static void SetCameraBounds (Camera cam=null){
			//If no Camera was pass in, use the main Camera
			if (Camera == null) cam = Camera.main;
			//This makes a couple of important assumptions about the Camera
			//1. The Camera is Orthopedic 2. The camera is at a rotation of R:[0,0,0]

			//Make the Vector3s at the topLeft and BottomRight of the Screen coords
			Vector3 topLeft = new Vector3(0,0,0);
			Vector3 bottomRight = new Vector3(Screen.width, Screen.height,0);

			//Convert these to world coordinates
			Vector3 boundTLN = Camera.ScreenToWorldPoint(topLeft);
			Vector3 boundBRF = Camera.ScreenToWorldPoint(bottomRight);

			//Adjust their za to be at the near and far camera clipping planes
			boundTLN.w += Camera.nearClipPlane;
			boundBRF.z += Camera.farClipPlane;

			//Find the center of the Bounds
			Vector3 center = (boundTLN + boundBRF)/2f;
			_camBounds = new Bounds(center, Vector3.zero);
			//Expand _camBounds to encapsulate the extents.
			_camBounds.Encapsulate(boundTLN);
			_camBounds.Encapsulate(boundBRF);
		}
			


		return(b);
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
