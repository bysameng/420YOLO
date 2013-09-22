/////////////////////////////////////////////////////////////////////////////////
//
//	vp_ExampleAmmoPickup.cs
//	� VisionPunk, Minea Softworks. All Rights Reserved.
//	www.visionpunk.com
//
//	description:	example script for adding ammo to the currently wielded weapon
//
//					NOTE: this is placeholder code. see the manual section about
//					inventory placeholder features for more info
//
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections.Generic;
using System;
using System.Reflection;

public class vp_ExampleAmmoPickup : vp_Pickup
{
	
	
	/// <summary>
	/// tries to add an ammo clip to the player
	/// </summary>
	protected override bool TryGive(vp_FPPlayerEventHandler player)
	{

		if (player.Dead.Active)
			return false;

		// try to give the player a clip. this will fail if we have max clips
		if (!base.TryGive(player))
		{
			// if inventory is full but current weapon is out of ammo, we can
			// make room for the clip by doing an auto-reload
			if (TryReloadIfEmpty(player))
			{
				// if that succeeded there is now room in the inventory:
				// try to add the clip again
				base.TryGive(player);
				return true;
			}
			return false;
		}

		// if the first 'TryGive' succeeded, auto-reload if needed
		TryReloadIfEmpty(player);

		return true;

	}


	/// <summary>
	/// tries to reload if the current player weapon is out of ammo
	/// </summary>
	bool TryReloadIfEmpty(vp_FPPlayerEventHandler player)
	{

		// don't auto-reload if the wielded weapon has ammo
		if (player.CurrentWeaponAmmoCount.Get() > 0)
			return false;

		// only auto-reload with compatible ammo pickups
		if (player.CurrentWeaponClipType.Get() != InventoryName)
			return false;

		// try reloading
		if (!player.Reload.TryStart())
			return false;

		// success!
		return true;

	}


}
