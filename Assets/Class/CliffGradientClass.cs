using UnityEngine;
using System.Collections;

public class CliffGradientClass : MonoBehaviour {
	public Texture2D tex;
	public Color[] fillcolorarray;
	public Color fillColor;
	public CliffGuiSliders Sliders;
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
				//Debug.Log("Hitpoint   "+uv.x+" "+uv.y);
			tex = hit.transform.gameObject.renderer.sharedMaterial.mainTexture as Texture2D;
			
//				Color col = tex.GetPixel((int)(uv.x * tex.width), (int)(uv.y * tex.height));
//				Color newcolor = new Color((col[0]*10+(Sliders.brushColor))/11, (col[0]*10+Sliders.brushColor)/11, (col[0]*10+Sliders.brushColor)/11, 1);
//				tex.SetPixel ((int)(uv.x * tex.width), (int)(uv.y * tex.height), newcolor);				
//				tex.Apply ();
				int real_brush = (int)(Sliders.brushSize/10);
			int i = (int)(uv.x*tex.width) - real_brush;
			while (i <= ((int)(uv.x*tex.width) + real_brush)) {
				int j = (int)(uv.y*tex.height) - real_brush;
				while (j <= ((int)(uv.y*tex.height) + real_brush)) {
					Color col = tex.GetPixel(i, j);
						float distance = (Mathf.Sqrt(Mathf.Pow(i-(int)(uv.x*tex.width),2)+Mathf.Pow(j-(int)(uv.y*tex.width),2)));  // how far from center of brush?
						if (distance<=real_brush) {																		// am I in the circle?
							float pixelColor;
							if (((col[0]-Sliders.brushColor)<.01)&((Sliders.brushColor-col[0])<.01)) { pixelColor = col[0]-(col[0]-Sliders.brushColor);} // if color is very close
							else { pixelColor = col[0]-((col[0]-Sliders.brushColor)*(.25f*(1.1f-(col[0]-Sliders.brushColor))));}                         // make it the same!
							pixelColor = col[0] * (1-(((real_brush)-distance)/(real_brush))) + pixelColor * (((real_brush)-distance)/(real_brush));
																																			//make the outer edge of the brush "lighter"

							Color newcolor = new Color(pixelColor,pixelColor,pixelColor,1);													// build the color
							tex.SetPixel (i, j, newcolor);																					// push the color into tex
						}
						j = j + 1;
					}
					i = i + 1;
				}

			tex.Apply ();																													// update the map
			}
		}
	}
	
	void checkClear(Event evt){
		if (GUI.Button (new Rect (Screen.width/2.0f,5,100,35), "Clear")) {
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
		if (Sliders.loadLessonBG){
			//renderer.material.mainTexture = texture1;
			Color[] savedPixels = premade.GetPixels();
			tex.SetPixels(savedPixels);
			tex.Apply();
			//transform.renderer.material.mainTexture = premade;
			Sliders.loadLessonBG=false;
		}

	}
}
