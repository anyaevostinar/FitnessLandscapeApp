using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuiSliders : MonoBehaviour {
    public float MutRate;
	public float PopSize;
	public float Speed;
	public GUISkin SliderSkin;
	public Rect windowRect = new Rect (20, 20, 120, 50);
	public bool paused = true;
	public bool loadLesson = false;
	string buttonText = "Play";
	public Texture2D buttontex;
	public Texture2D ybuttontex;
	bool showInst = true;
	bool showLessonInst = false;
	
	public void loadPremade(){
		showLessonInst=true;
		paused = true;
		loadLesson = true;
		MutRate = 5f;
		PopSize = 20f;
		buttonText = "Play";

	}

    void OnGUI() {
		loadLesson=false;
		GUI.skin = SliderSkin;
        MutRate = GUI.HorizontalSlider(new Rect(175,25,Screen.width/6.0f,20),MutRate, 1F, 100F);

        GUI.Label(new Rect(175, 0, Screen.width/6.0f, 20),  "Mutation Rate: ");
		GUI.Label (new Rect(175, 40, 100, 20), "Low");
		GUI.Label (new Rect(Screen.width/3.0f-30, 40, 100, 22), "High");
		
		GUI.DrawTexture(new Rect(0, Screen.height - 22, Screen.width, 22), buttontex);
		GUI.Label (new Rect(Screen.width/2.0f - 100 , Screen.height - 22, 100, 22), "Trait 2: Color");

		GUI.DrawTexture(new Rect(0, 0, 22, Screen.height), ybuttontex);
		Matrix4x4 m2 = Matrix4x4.identity;
		m2.SetTRS(new Vector3(0,Screen.height/2,0), Quaternion.Euler(0,0,-90), Vector3.one);
		GUI.matrix = m2;
		GUI.Label( new Rect(0,0 , 100, 100), "Trait 1: Size");
		GUI.matrix = Matrix4x4.identity;

		PopSize = GUI.HorizontalSlider(new Rect((Screen.width/3.0f) *2 ,25,Screen.width/6.0f,20),PopSize, 0F, 500F);
		GUI.Label (new Rect((Screen.width/3.0f)*2 , 0, 200, 20),"Population Size: "+(int)PopSize);
		//windowRect = GUI.Window (0, windowRect, DoMyWindow, "Menu");

		Speed = GUI.HorizontalSlider(new Rect(Screen.width/2.0f-100.0f ,25,Screen.width/6.0f - 40,20),Speed, 50.0F, 100F);
		GUI.Label (new Rect(Screen.width/2.0f-100.0f ,35,Screen.width/6.0f,20),"Speed");

		if(paused){
			if(GUI.Button(new Rect(Screen.width/2.0f-100.0f, 0, 75, 20), buttonText)){
				paused = false;
				buttonText = "Pause";
			}
		}
		if(!paused){
			if(GUI.Button(new Rect(Screen.width/2.0f-100.0f, 0, 75, 20), buttonText)){
				paused = true;
				buttonText ="Play";
			}
		}

		if (showInst){
			GUI.Box(new Rect(Screen.width/4.0f, Screen.height/2.0f, Screen.width/2.0f,100), "Welcome to the Finger-Painting Fitness Landscape simulation! \nThis program aims to help you understand what fitness landscapes really are.\n" +
				"On the screen you'll see that the plants have two traits: color and size. \nYou can change the fitness landscape by clicking on the gray area to make areas of\n" +
				"higher fitness. You can also change the mutation rate and population size to see how \nthat affects the population's evolution. For an example lesson, click Load Lesson.");
			if (GUI.Button(new Rect(Screen.width/2.0f, Screen.height/2.0f-21, 100, 20), "Close Info")){
				showInst=false;
			}
		}
		if(GUI.Button(new Rect(Screen.width-100, 0, 100, 20), "Show Info")){
			showInst=true;
		}

		if (GUI.Button (new Rect (22,0,100,20), "Load Lesson")) {
			loadPremade();
		}

		if (showLessonInst){
			GUI.Box(new Rect(Screen.width/4.0f, (Screen.height/3.0f)*2, Screen.width/2.0f,160), "You've loaded the sample lesson!\n The mutation rate has been adjusted to a fairly low amount" +
			        " and the \npopulation size has been set to 20. \nYou can see that the population of plants are all fairly similar now, \nwith a similar color and size." +
			        " The two darker\n spots on either side are fitness peaks, but the darker one is a higher fitness peak. \n The population starts closest to the smaller fitness peak and \n evolves to that peak first." +
			        "To start evolution, click Play at the top.\n Are they able to evolve to the higher fitness peak? " +
			        "What do you think\n would happen if you changed the mutation rate or population size? Try it!");
			if (GUI.Button(new Rect(Screen.width/2.0f, (Screen.height/3.0f)*2 -21, 150, 20), "Close Lesson Info")){
				showLessonInst=false;
			}
		}
		if(GUI.Button(new Rect(22, 21, 140, 20), "Show Lesson Info")){
			showLessonInst=true;
		}

    }


//	void DoMyWindow(int windowID) {
	//	if (GUI.Button (new Rect (10,20,100,20), "Load Template")){
		
	//	}
			
	//}
	// Use this for initialization
	void Start () {
		MutRate = 5.0f;
		PopSize = 100.0f;
		Speed = 100.0f;

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
