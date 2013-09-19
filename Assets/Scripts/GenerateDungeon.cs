using UnityEngine;
using System.Collections;

public class GenerateDungeon : MonoBehaviour {
	
	public GameObject cube;
	private int[,] gameWorld;
	public int gameSizeX = 256;
	public int gameSizeY = 256;
	
	

	// Use this for initialization
	void Start () {
		gameWorld = new int[gameSizeX, gameSizeY];
		for (int i = 0; i < gameSizeX; i++)
			for (int j = 0; j < gameSizeY; j++)
				gameWorld[i, j] = 0;
	
	}
	
	void DrawGameWorld(){
		for (int i = 0; i < gameSizeX; i++)
			for (int j = 0; i < gameSizeY; j++)
				if (gameWorld[i, j] == 1)
					DrawCube ((float)i, (float)j);
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void DrawRectangle(int x, int y, int length, int width){
		for (int i = x; i < x+width+2; i++){
			gameWorld[i, y] = 1;
			gameWorld[i, y+length+1] = 1;
		}
		for (int i = y; i < y+length; i++){
			gameWorld[x, i+1] = 1;
			gameWorld[x+width, i] = 1;
		}
	}
	
	void DrawCube(float x, float y){
		GameObject myCube = (GameObject)Instantiate(cube, new Vector3(x, 1, y), Quaternion.identity);
	}
		
		
	void DrawEmptyRoom (int x, int y, int length, int width) {
		float xsize = cube.renderer.bounds.size.x; 
		Vector3 roomPosition = new Vector3 (x, 1, y);
		for (float i = 0; i < (float)length+xsize*2; i+=xsize){
			DrawCube (x, y+i);
			DrawCube (x+width+xsize, y+i);
		}
		for (float i = xsize; i < (float)width+xsize; i+=xsize){
			DrawCube (x+i, y);
			DrawCube (x+i, y+length+xsize);
		}
		
		
	}
}
