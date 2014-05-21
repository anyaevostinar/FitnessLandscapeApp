using UnityEngine;
using System.Collections;

public class OrgClass : MonoBehaviour {
	//list of public GameObject OrgPrefab
	public float fitness;
	public GameObject gradientref;
	public CliffGradientClass gradient;
	public Texture2D tex;
	public Color col = Color.blue;


	void calcFitness(){
		gradient = (CliffGradientClass) gradientref.GetComponent("CliffGradientClass");
		RaycastHit hit;
		if(Physics.Raycast(transform.position, Vector3.forward, out hit)) {
			
			//Debug.Log (hit.collider);

			Vector2 uv;

			uv.x = (hit.point.x - hit.collider.bounds.min.x) / hit.collider.bounds.size.x;

			uv.y = (hit.point.y - hit.collider.bounds.min.y) / hit.collider.bounds.size.y;
			tex = hit.transform.gameObject.renderer.sharedMaterial.mainTexture as Texture2D;
			col = tex.GetPixel((int)(uv.x * tex.width), (int)(uv.y * tex.height));
			//Debug.Log(col);
		}

		//set fitness
		fitness = 1.0f-col[0];
		//Debug.Log("fitness " + fitness);
	}
	
	
	void setTraits(){
		float org_x = this.transform.position.x;
		float org_y = this.transform.position.y;
		HSBColor orgcol = new HSBColor(this.renderer.material.color);
		//orgcol.s = ((org_y + 14.0f)/28.0f);
		orgcol.s = 1.0f;
		orgcol.b = 1.0f;
		orgcol.h = ((org_x + 15.0f)/40.0f);
		this.renderer.material.color = HSBColor.ToColor(orgcol);

		this.transform.localScale = new Vector3(((org_y + 12.0f)/40.0f), ((org_y + 12.0f)/40.0f), this.transform.localScale.z);
	}
	
	// Use this for initialization
	void Start () {
		calcFitness();
		setTraits();
	}
	
	// Update is called once per frame
	void Update () {
		//check if fitness has changed
		calcFitness();

	}


}
