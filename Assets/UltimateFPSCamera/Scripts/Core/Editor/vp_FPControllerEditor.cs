/////////////////////////////////////////////////////////////////////////////////
//
//	vp_FPControllerEditor.cs
//	© VisionPunk, Minea Softworks. All Rights Reserved.
//	www.visionpunk.com
//
//	description:	custom inspector for the vp_FPController class
//
/////////////////////////////////////////////////////////////////////////////////

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(vp_FPController))]

public class vp_FPControllerEditor : Editor
{

	// target component
	public vp_FPController m_Component;

	// foldouts
	public static bool m_MotorMoveFoldout;
	public static bool m_MotorJumpFoldout;
	public static bool m_PhysicsFoldout;
	public static bool m_StateFoldout;
	public static bool m_PresetFoldout = true;
	
	private static vp_ComponentPersister m_Persister = null;


	/// <summary>
	/// hooks up the component object as the inspector target
	/// </summary>
	public virtual void OnEnable()
	{

		m_Component = (vp_FPController)target;

		if (m_Persister == null)
			m_Persister = new vp_ComponentPersister();
		m_Persister.Component = m_Component;
		m_Persister.IsActive = true;

		if (m_Component.DefaultState == null)
			m_Component.RefreshDefaultState();

	}


	/// <summary>
	/// disables the persister and removes its reference
	/// </summary>
	public virtual void OnDestroy()
	{

		m_Persister.IsActive = false;

	}


	/// <summary>
	/// 
	/// </summary>
	public override void OnInspectorGUI()
	{

		GUI.color = Color.white;

		if (Application.isPlaying || m_Component.DefaultState.TextAsset == null)
		{

			DoMotorMoveFoldout();
			DoMotorJumpFoldout();
			DoPhysicsFoldout();

		}
		else
			vp_PresetEditorGUIUtility.DefaultStateOverrideMessage();

		// state
		m_StateFoldout = vp_PresetEditorGUIUtility.StateFoldout(m_StateFoldout, m_Component, m_Component.States, m_Persister);

		// preset
		m_PresetFoldout = vp_PresetEditorGUIUtility.PresetFoldout(m_PresetFoldout, m_Component);

		// update
		if (GUI.changed)
		{

			EditorUtility.SetDirty(target);

			// update the default state in order not to loose inspector tweaks
			// due to state switches during runtime
			if(Application.isPlaying)
				m_Component.RefreshDefaultState();

			if (m_Component.Persist)
				m_Persister.Persist();

		}

	}


	/// <summary>
	/// 
	/// </summary>
	public virtual void DoMotorMoveFoldout()
	{

		m_MotorMoveFoldout = EditorGUILayout.Foldout(m_MotorMoveFoldout, "Motor");

		if (m_MotorMoveFoldout)
		{

			m_Component.MotorAcceleration = EditorGUILayout.Slider("Acceleration", m_Component.MotorAcceleration, 0, 1);
			m_Component.MotorDamping = EditorGUILayout.Slider("Damping", m_Component.MotorDamping, 0, 1);
			m_Component.MotorAirSpeed = EditorGUILayout.Slider("Air Speed", m_Component.MotorAirSpeed, 0, 1);
			m_Component.MotorSlopeSpeedUp = EditorGUILayout.Slider("Slope Speed Up", m_Component.MotorSlopeSpeedUp, 0, 2);
			m_Component.MotorSlopeSpeedDown = EditorGUILayout.Slider("Slope Sp. Down", m_Component.MotorSlopeSpeedDown, 0, 2);

			vp_EditorGUIUtility.Separator();
		}
	
	}


	/// <summary>
	/// 
	/// </summary>
	public virtual void DoMotorJumpFoldout()
	{

		m_MotorJumpFoldout = EditorGUILayout.Foldout(m_MotorJumpFoldout, "Jump");

		if (m_MotorJumpFoldout)
		{

			m_Component.MotorJumpForce = EditorGUILayout.Slider("Force", m_Component.MotorJumpForce, 0, 10);
			m_Component.MotorJumpForceDamping = EditorGUILayout.Slider("Force Damping", m_Component.MotorJumpForceDamping, 0, 1);
			m_Component.MotorJumpForceHold = EditorGUILayout.Slider("Force (Hold)", m_Component.MotorJumpForceHold, 0, 0.01f);
			m_Component.MotorJumpForceHoldDamping = EditorGUILayout.Slider("Force Damping (Hold)", m_Component.MotorJumpForceHoldDamping, 0, 1);

			m_Component.MotorMinJumpDelay = EditorGUILayout.Slider("Min Jump Delay", m_Component.MotorMinJumpDelay, 0.0f, 10.0f);

			vp_EditorGUIUtility.Separator();
		}

	}


	/// <summary>
	/// 
	/// </summary>
	public virtual void DoPhysicsFoldout()
	{

		m_PhysicsFoldout = EditorGUILayout.Foldout(m_PhysicsFoldout, "Physics");
		if (m_PhysicsFoldout)
		{

			m_Component.PhysicsForceDamping = EditorGUILayout.Slider("Force Damping", m_Component.PhysicsForceDamping, 0, 1);
			m_Component.PhysicsPushForce = EditorGUILayout.Slider("Push Force", m_Component.PhysicsPushForce, 0, 100);
			m_Component.PhysicsGravityModifier = EditorGUILayout.Slider("Gravity Modifier", m_Component.PhysicsGravityModifier, 0, 1);
			m_Component.PhysicsSlopeSlideLimit = EditorGUILayout.Slider("Slope Slide Limit", m_Component.PhysicsSlopeSlideLimit, 0, 90);
			m_Component.PhysicsSlopeSlidiness = EditorGUILayout.Slider("Slope Slidiness", m_Component.PhysicsSlopeSlidiness, 0.0f, 1.0f); 
			m_Component.PhysicsWallBounce = EditorGUILayout.Slider("Wall Bounce", m_Component.PhysicsWallBounce, 0, 0.9f);
			m_Component.PhysicsWallFriction = EditorGUILayout.Slider("Wall Friction", m_Component.PhysicsWallFriction, 0.0f, 1);

			vp_EditorGUIUtility.Separator();

		}

	}


}

