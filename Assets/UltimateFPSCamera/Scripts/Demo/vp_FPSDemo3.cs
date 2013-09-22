/////////////////////////////////////////////////////////////////////////////////
//
//	vp_FPSDemo3.cs
//	© VisionPunk, Minea Softworks. All Rights Reserved.
//	www.visionpunk.com
//
//	description:	PLEASE NOTE: this class is very specialized	for the demo
//					walkthrough and is not meant to be used as the starting
//					point for a game, nor an example of best workflow practices.
//
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System;

public class vp_FPSDemo3 : MonoBehaviour
{

	private vp_FPCamera m_FPSCamera = null;
	public GameObject PlayerGameObject = null;
	private vp_FPSDemoManager m_Demo = null;

	// images
	public Texture ImageLeftArrow = null;
	public Texture ImageRightArrow = null;
	public Texture ImageCheckmark = null;
	public Texture ImagePresetDialogs = null;
	public Texture ImageShooter = null;
	public Texture ImageAllParams = null;

	// positions
	private Vector3 m_StartPos = new Vector3(113, 106, -87);
	private Vector3 m_OverviewPos = new Vector3(113, 106, -87);
	private Vector3 m_OutroPos = new Vector3(135.0f, 105.8f, -70.7f);

	// angles
	private Vector2 m_StartAngle = new Vector2(13.0f, 153.5f);
	private Vector2 m_OverviewAngle = new Vector2(13.0f, 153.5f);
	private Vector2 m_OutroAngle = new Vector2(-19.3f, 241.7f);

	// misc
	private float m_OutroStartTime = 0.0f;

	/// <summary>
	/// 
	/// </summary>
	void Start()
	{

		m_FPSCamera = (vp_FPCamera)Component.FindObjectOfType(typeof(vp_FPCamera));

		m_Demo = new vp_FPSDemoManager(PlayerGameObject);
		m_Demo.CurrentFullScreenFadeTime = Time.time;
		m_Demo.DrawCrosshair = false;

		m_Demo.Input.MouseCursorZones = new Rect[2];
		m_Demo.Input.MouseCursorZones[0] = new Rect((Screen.width * 0.5f) - 370, 40, 80, 80);
		m_Demo.Input.MouseCursorZones[1] = new Rect((Screen.width * 0.5f) + 290, 40, 80, 80);
		Screen.lockCursor = false;
		
	}
	

	/// <summary>
	/// 
	/// </summary>
	void Update()
	{

		m_Demo.Update();

		// respawn player if straying too far from the platform
		// (you can easily get lost in the hills with megaspeed)
		if (Vector3.Distance(PlayerGameObject.transform.position, m_StartPos) > 100.0f)
			m_Demo.Teleport(m_StartPos, m_StartAngle);

	}


	/// <summary>
	/// demo screen to list some included example scripts
	/// </summary>
	private void DemoIntro()
	{

		if (m_Demo.FirstFrame)
		{
			m_Demo.FirstFrame = false;
			m_Demo.DrawCrosshair = false;
			m_Demo.FreezePlayer(m_OverviewPos, m_OverviewAngle, true);
			m_Demo.Input.ForceCursor = true;
		}

		m_Demo.DrawBoxes("rapid prototyping ?", "This package is a GOLDMINE of optional extras, including realistic shell case physics, bullet holes, muzzleflashes, explosions with area damage & knockback, a damage handler for killing and respawning gameobjects and much more. Version 1.4 introduces PICKUPS and a new awesome SLOW MOTION implementation. Try it out!", ImageLeftArrow, ImageRightArrow, null, delegate() { m_Demo.LoadLevel(1); });
		m_Demo.ForceCameraShake();

	}


	/// <summary>
	/// demo screen for trying out misc gameplay, and to list the
	/// controls of the example player
	/// </summary>
	private void DemoGameplay()
	{

		if (m_Demo.FirstFrame)
		{
			m_Demo.FirstFrame = false;
			m_Demo.DrawCrosshair = true;
			m_Demo.UnFreezePlayer();
			m_Demo.Teleport(m_StartPos, m_StartAngle);
			Screen.lockCursor = true;
			m_Demo.Input.ForceCursor = false;
		}

		m_Demo.DrawBoxes("example player", "This player has been set up with some classic First Person Shooter states.\n• Press SHIFT to RUN, C to CROUCH, and the RIGHT or MIDDLE mouse button to ZOOM.\n• To SWITCH WEAPONS, press Q, E or 1-3.\n• Press R to RELOAD", ImageLeftArrow, ImageRightArrow);

		// draw menu re-enable text
		if (m_Demo.ShowGUI && Screen.lockCursor)
		{
			GUI.color = new Color(1, 1, 1, 1);
			GUI.Label(new Rect((Screen.width / 2) - 200, 140, 400, 20), "(Press ENTER to reenable menu)", m_Demo.CenterStyle);
			GUI.color = new Color(1, 1, 1, 1 * m_Demo.GlobalAlpha);
		}

	}


	/// <summary>
	/// demo screen to show a final summary message and editor screenshot
	/// </summary>
	private void DemoOutro()
	{

		if (m_Demo.FirstFrame)
		{
			m_Demo.FirstFrame = false;
			m_Demo.DrawCrosshair = false;
			m_Demo.FreezePlayer(m_OutroPos, m_OutroAngle, true);
			m_Demo.Input.ForceCursor = true;
			m_OutroStartTime = Time.time;
		}

		// pan camera slowly for the clouds to move a little in the outro screen
		m_FPSCamera.Angle = new Vector2(
			m_OutroAngle.x,
			m_OutroAngle.y + (Mathf.Cos(((Time.time - m_OutroStartTime) + 50) * 0.03f) * 20)
			);

		m_Demo.DrawBoxes("putting it all together", "Included in the package is full, well commented C# source code, an in-depth 63-page MANUAL in PDF format, a game-ready FPS PLAYER prefab along with all the scripts and content used in this demo. A FANTASTIC starting point (or upgrade) for any FPS project.\nBest part? It can be yours in a minute. GET IT NOW on visionpunk.com!", ImageLeftArrow, ImageCheckmark, delegate() { m_Demo.LoadLevel(0);  });
		m_Demo.DrawImage(ImageAllParams);

	}


	/// <summary>
	/// 
	/// </summary>
	void OnGUI()
	{

		m_Demo.OnGUI();

		// perform drawing method specific to the current demo screen
		switch (m_Demo.CurrentScreen)
		{
			case 1:
				DemoIntro();
				break;
			case 2:
				DemoGameplay();
				break;
			case 3:
				DemoOutro();
				break;
		}

	}


}

