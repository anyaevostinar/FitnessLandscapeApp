using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class PopClass : MonoBehaviour {
//	public OrgClass orgref;
	public List<GameObject> cubes;
	public GameObject orgPrefab;
	public float currentmut;
	public int currentpopsize;
	private CliffGuiSliders sliderrefs;
	public float currentspeed;
	public List<GameObject> tournament;
	public float tournUpdate = 0.0f;
	//public string path = @"AppLog.txt";

	void KillPop(){
		for(int i = 0; i < cubes.Count; i++){
			GameObject temp = cubes[i];
			cubes.Remove(temp);
			Destroy(temp);
		}
	}
	
	private static float GaussianRandom(float stdDev, float mean){
		float u1 = Random.Range(0.0f,1.0f); //these are uniform(0,1) random doubles
		float u2 = Random.Range (0.0f,1.0f);
		float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) *
             Mathf.Sin(2.0f * Mathf.PI * u2); //random normal(0,1)
		float randNormal = mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
		return randNormal;
	}
	
	private static int SortbyFitness(GameObject o1, GameObject o2) {
		//attach to main camera
		OrgClass o1fit = (OrgClass) o1.GetComponent("OrgClass");
		OrgClass o2fit = (OrgClass) o2.GetComponent("OrgClass"); 
		return o1fit.fitness.CompareTo(o2fit.fitness);
			
	}
	
	GameObject GenerateOffspring(GameObject winner) {
		float xmut = GaussianRandom(9*currentmut, 0.0f);
		float ymut = GaussianRandom(3*currentmut, 0.0f);
		float offspringx = winner.transform.position.x + xmut;
		float offspringy = winner.transform.position.y + ymut;

		while(-13.0f > offspringx || offspringx > 13.0f){
			xmut = GaussianRandom(currentmut, 0.0f);
			offspringx = winner.transform.position.x + xmut;
		}

		while(-9.0f > offspringy || offspringy > 7.0f){
			ymut = GaussianRandom(currentmut, 0.0f);
			offspringy = winner.transform.position.y + ymut;
		}

		GameObject offspring = (GameObject) Instantiate(winner, new Vector3(offspringx,
		                                                                    offspringy, winner.transform.position.z) , winner.transform.rotation);
		return offspring;
	}
	
	
	void runTourn() {
		//grab 3-5 orgs for tournament
		//rank reproduce orgs
		int tournsize;
		if (cubes.Count > 5) {
			tournsize = 5;
		} else {
			tournsize = cubes.Count;
		}
		tournament = new List<GameObject>();
		while(tournament.Count < tournsize){
			GameObject neworg = cubes[Random.Range(0, cubes.Count)];
			if (neworg == null) {
				cubes.Remove (neworg);
				//Debug.Log("killed a null cube");
				neworg = cubes[Random.Range(0, cubes.Count-1)];
			} else {
				if (!tournament.Contains(neworg)) {
				tournament.Add (neworg);
				}
			}
		}
		//order the tournament by fitness
		//Debug.Log("tourn" + tournament.Count + "cubes" + cubes.Count);
		tournament.Sort(SortbyFitness);
		
		if (tournament.Count > 1) {
			float total_fit = 0.0f;
			for(int i=0; i < tournament.Count; i++){
				OrgClass org = (OrgClass) tournament[i].GetComponent("OrgClass");
				total_fit += org.fitness;	
			}
			float repro_prob = Random.Range(0.0f,1.0f);
			int winner_i = -1;
			float fit_prob = 0.0f;
			while(fit_prob < repro_prob){
				winner_i += 1;
				OrgClass org = (OrgClass) tournament[winner_i].GetComponent("OrgClass");
				fit_prob += org.fitness/total_fit;
			}
			GameObject winner = tournament[4];
			GameObject offspring = GenerateOffspring(winner);
			//winner.renderer.material.color = Color.red;
			cubes.Add (offspring);
			//Debug.Log("new offspring through tournament");
			Destroy (tournament[0].gameObject);
			//Debug.Log("killed org in tournament");
			cubes.Remove(tournament[0]);
			tournament.Clear();
		} else if (tournament.Count == 1){
			GameObject winner = tournament[(tournament.Count-1)];
			GameObject offspring = GenerateOffspring(winner);
			//Debug.Log ("tournament size of 1");
			cubes.Add (offspring);
			cubes.Remove(winner);
		}
	}
	


	// Use this for initialization
	void Start () {
		//initialized at open
		//add in population of organisms
		sliderrefs = gameObject.GetComponent<CliffGuiSliders>();
		currentmut = sliderrefs.MutRate;
		currentpopsize = (int)sliderrefs.PopSize;
		cubes = new List<GameObject>();
		
		for(int i = 0; i < currentpopsize; i++){
			var position = new Vector3(Random.Range(-13, 13), Random.Range(-9, 7),10);
			GameObject orgObject = (GameObject) Instantiate(orgPrefab, position, transform.rotation);
			
			cubes.Add(orgObject);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(System.GC.GetTotalMemory(false));
		//string createText = System.GC.GetTotalMemory(false) + System.Environment.NewLine;
		
		//File.AppendAllText(path, createText);
		currentmut = (float) sliderrefs.MutRate / 100.0f;
		currentpopsize = (int)sliderrefs.PopSize;
		currentspeed = (float)sliderrefs.Speed;

		if (sliderrefs.loadLesson){
			KillPop();
			for(int i = 0; i < currentpopsize; i++){
				var position = new Vector3(Random.Range(2.0f, 4.0f), Random.Range(-1.0f, 1.0f),10);
				GameObject orgObject = (GameObject) Instantiate(orgPrefab, position, transform.rotation);
				
				cubes.Add(orgObject);
			}
		}


		//If user has changed pop size, update that
		if(currentpopsize < cubes.Count) {
			for(int i = 0; i<(cubes.Count-currentpopsize); i++) {
				if (cubes[i]!=null){
					Destroy (cubes[i].gameObject);
				} else {
					cubes.Remove (cubes[i]);
				}
			}
			cubes.RemoveRange (0, (cubes.Count-currentpopsize));
		} else if(currentpopsize > cubes.Count) {
			for(int i = cubes.Count; i < currentpopsize; i++){
				var position = new Vector3(Random.Range(-13.0f, 13.0f), Random.Range(-9.0f, 7.0f),10);
				GameObject orgObject = (GameObject) Instantiate(orgPrefab, position, transform.rotation);
				cubes.Add (orgObject);
				//Debug.Log("made some cubes = "+cubes.Count +", popsize = "+currentpopsize);
			}
		}
		if((!sliderrefs.paused) && (Time.time - tournUpdate > (100.0f - sliderrefs.Speed)/100.0f)){
			runTourn();
			tournUpdate = Time.time;
			Resources.UnloadUnusedAssets();
	
		}
	}
}
