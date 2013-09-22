/////////////////////////////////////////////////////////////////////////////////
//
//	vp_ExampleHealthPickup.cs
//	© VisionPunk, Minea Softworks. All Rights Reserved.
//	www.visionpunk.com
//
//	description:	placeholder example script for adding health to the player.
//					see the 'Damage' and 'OnGUI' methods in 'vp_ExamplePlayer.cs'
//					for an example of displaying and reducing health of the player
//
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class vp_ExampleHealthPickup : vp_Pickup
{

	public float Health = 0.1f;

	/// <summary>
	/// tries to add 'Health' to the player (range is 0-1)
	/// </summary>
	protected override bool TryGive(vp_FPPlayerEventHandler player)
	{

		if (player.Health.Get() < 0.0f)
			return false;

		if (player.Health.Get() >= 1.0f)
			return false;

	    player.Health.Set(Mathf.Min(1, (player.Health.Get() + Health)));

		return true;

	}

}
