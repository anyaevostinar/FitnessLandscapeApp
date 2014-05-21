using UnityEngine;
using System.Collections;

public class GradientClass : MonoBehaviour {
	public Texture2D tex;
	public Color[] fillcolorarray;
	public Color fillColor;
	public GuiSliders Sliders;
	public Texture2D premade;

	
	void checkShading(Event evt)
	{
		if (evt.isMouse && Input.GetMouseButton (0)) {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit hit;

		if (collider.Raycast (ray, out hit, Mathf.Infinity)) {

			Vector2 uv;

			uv.x = (hit.point.x - hit.collider.bounds.min.x) / hit.collider.bounds.size.x;

			uv.y = (hit.point.y - hit.collider.bounds.min.y) / hit.collider.bounds.size.y;
			tex = hit.transform.gameObject.renderer.sharedMaterial.mainTexture as Texture2D;
			Color col = tex.GetPixel((int)(uv.x * tex.width), (int)(uv.y * tex.height));
			//Debug.Log("x =" + (int)(uv.x * tex.width) + " y = " + (int)(uv.y * tex.height));
			Color newcolor = new Color(col[0]-0.03F, col[1]-0.03F, col[2]-0.03F, 1);
			//Debug.Log(newcolor);
			tex.SetPixel ((int)(uv.x * tex.width), (int)(uv.y * tex.height), newcolor);				
			tex.Apply ();
			}
		}
	}
	
	void checkClear(Event evt){
		if (GUI.Button (new Rect (Screen.width/2.0f,0,100,20), "Clear")) {
			//Debug.Log ("clearing");
			fillcolorarray = tex.GetPixels ();
			fillColor = new Color(255,255f, 255f);
			
				
			for(var i = 0; i < fillcolorarray.Length; i++){
				fillcolorarray[i] = fillColor;
			}
				
			tex.SetPixels(fillcolorarray);
				
			tex.Apply();
		}
	}

	
	void OnGUI (){
		Event evt = Event.current;
		
		checkShading(evt);
		
		checkClear(evt);


		
	}
	
	// Use this for initialization
	void Start () {
		tex = renderer.sharedMaterial.mainTexture as Texture2D;

	}
	
	// Update is called once per frame
	void Update () {
		//check load lesson
		if (Sliders.loadLesson){
			//renderer.material.mainTexture = texture1;
			Color[] savedPixels = premade.GetPixels();
			tex.SetPixels(savedPixels);
			tex.Apply();
			//transform.renderer.material.mainTexture = premade;
			Sliders.loadLesson = false;
		}

	}
}
