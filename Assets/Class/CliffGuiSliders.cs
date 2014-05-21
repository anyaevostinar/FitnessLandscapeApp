using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CliffGuiSliders : MonoBehaviour {
    public float MutRate;
	public float PopSize;
	public float Speed;
	public float brushSize = 0f;
	public float brushColor = 0f;
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
		// Mutation Rate Slider //
		MutRate = GUI.HorizontalSlider(new Rect(175,50,Screen.width/6.0f,100),MutRate, 1F, 100F);
        GUI.Label(new Rect(175, 25, Screen.width/6.0f, 20),  "Mutation Rate: ");
		GUI.Label (new Rect(175, 60, 100, 20), "Low");
		GUI.Label (new Rect(145+Screen.width/6.0f, 60, 100, 22), "High");


		// Population Size Slider //
		PopSize = GUI.HorizontalSlider(new Rect((Screen.width/3.0f) *2 ,50,Screen.width/6.0f,100),PopSize, 0F, 500F);
		GUI.Label (new Rect((Screen.width/3.0f)*2 , 25, 200, 20),"Population Size: "+(int)PopSize);

		// Speed Slider //
		Speed = GUI.HorizontalSlider(new Rect(Screen.width/2.0f-75.0f ,50,Screen.width/6.0f,100),Speed, 50.0F, 100F);
		GUI.Label (new Rect(Screen.width/2.0f-75.0f ,60,Screen.width/6.0f,20),"Speed");

		// Brush Size Slider //
		brushSize = GUI.HorizontalSlider(new Rect(50,Screen.height-50,Screen.width/3.0f,50),brushSize, 10F, 200F);
		GUI.Label(new Rect(50, Screen.height-70,100,50),  "Brush Size: ");
		GUI.Label(new Rect(50, Screen.height-40, 100, 20), "Small");
		GUI.Label(new Rect(50+Screen.width/3.0f-30, Screen.height-40, 100, 22), "Large");
		// Brush Color Slider //
		brushColor = GUI.HorizontalSlider(new Rect(2*Screen.width/3.0f-50,Screen.height-50,Screen.width/3.0f,50),brushColor, 1F, 0F);
		GUI.Label(new Rect(2*Screen.width/3.0f-50, Screen.height-70,100,50),  "Brush Color: ");
		GUI.Label(new Rect(2*Screen.width/3.0f-50, Screen.height-40, 100, 20), "White");
		GUI.Label(new Rect(Screen.width-75, Screen.height-40, 100, 22), "Black");

		//GUI.Box(new Rect(45 - brushSize/2,Screen.height - 80 - brushSize/2,brushSize,brushSize),"size "+(int)brushSize+"\n color "+ brushColor);

		// Color Grad //
		GUI.DrawTexture(new Rect(0, Screen.height - 22, Screen.width, 22), buttontex);
		// Color Grad Label //
		GUI.Label (new Rect(Screen.width/2.0f - 100 , Screen.height - 22, 100, 22), "Trait 2: Color");

		// Size Grad //
		GUI.DrawTexture(new Rect(0, 0, 22, Screen.height), ybuttontex);
		// Rotate Interface //
		Matrix4x4 m2 = Matrix4x4.identity;
		m2.SetTRS(new Vector3(0,Screen.height/2,0), Quaternion.Euler(0,0,-90), Vector3.one);
		GUI.matrix = m2;
		// Size Label //
		GUI.Label( new Rect(0,0 , 100, 100), "Trait 1: Size");
		// Rotate Interface Back //
		GUI.matrix = Matrix4x4.identity;

		// Play Pause Button //
		if(paused){
			if(GUI.Button(new Rect(Screen.width/2.0f-100.0f, 5, 75, 35), buttonText)){
				paused = false;
				buttonText = "Pause";
			}
		}
		if(!paused){
			if(GUI.Button(new Rect(Screen.width/2.0f-100.0f, 5, 75, 35), buttonText)){
				paused = true;
				buttonText ="Play";
			}
		}

		// Display Instructions //
		if (showInst){
			GUI.Box(new Rect(Screen.width/4.0f, Screen.height/2.0f-50, 501,100), "Welcome to the Finger-Painting Fitness Landscape simulation! \nThis program aims to help you understand what fitness landscapes really are.\n" +
				"On the screen you'll see that the plants have two traits: color and size. \nYou can change the fitness landscape by clicking on the gray area to make areas of\n" +
				"higher fitness. You can also change the mutation rate and population size to see how \nthat affects the population's evolution. For an example lesson, click Load Lesson.");
			if (GUI.Button(new Rect(Screen.width/2.0f-100, Screen.height/2.0f-81, 200, 30), "Close Instructions")){
				showInst=false;
			}
		}

		// Toggle On Intructions //
		if(GUI.Button(new Rect(Screen.width-150, 10, 150, 30), "Show Instructions")){
			showInst=true;
		}

		// Load Lesson //
		if (GUI.Button (new Rect (22,5,100,30), "Load Lesson")) {
			loadPremade();
		}

		// Display Lesson //
		if (showLessonInst){
			GUI.Box(new Rect(Screen.width/4.0f, (Screen.height/3.0f)*2-20, Screen.width/2.0f+55,160), "You've loaded the sample lesson!\n The mutation rate has been adjusted to a fairly low amount" +
			        " and the \npopulation size has been set to 20. \nYou can see that the population of plants are all fairly similar now, \nwith a similar color and size." +
			        " The two darker\n spots on either side are fitness peaks, but the darker one is a higher fitness peak. \n The population starts closest to the smaller fitness peak and \n evolves to that peak first." +
			        "To start evolution, click Play at the top.\n Are they able to evolve to the higher fitness peak? " +
			        "What do you think\n would happen if you changed the mutation rate or population size? Try it!");
			if (GUI.Button(new Rect(Screen.width/2.0f-75, (Screen.height/3.0f)*2 -51, 150, 30), "Close Lesson Info")){
				showLessonInst=false;
			}
		}

		// Toggle on Lesson //
		if(GUI.Button(new Rect(22, 40, 140, 30), "Show Lesson Info")){
			showLessonInst=true;
		}

    }
	
	void Start () {
		MutRate = 5.0f;
		PopSize = 100.0f;
		Speed = 100.0f;
		brushSize = 20f;
		brushColor = .5f;

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
