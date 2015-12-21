using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultikillTimer {
		
	public const float timerLength = 18;
	private float countingDownTo;
	private Bolt.NetworkId networkID;
	private int resets = 0;

	public MultikillTimer (Bolt.NetworkId networkID) {
		this.networkID = networkID;
	}

	public MultikillTimer ResetTimer() {

		if (!TimerRunning || resets == 8) {
			Debug.LogWarning("Reseting with " + TimeRemaining);
			resets = 0;
			countingDownTo = Time.time + timerLength;
			return this;
		}

		if (TimerRunning || resets == 0) {
			Debug.LogWarning("Adding reset with " + TimeRemaining);
			countingDownTo = Time.time + timerLength;
			resets ++;
		}

		return this;
	}

	public float TimeRemaining {
		get { return countingDownTo - Time.time;}
	}

	public int Resets {
		get { return resets; }
	}

	public Bolt.NetworkId NetworkID {
		get { return networkID; }
	}

	public int SecondsRemaining {
		get { return Mathf.RoundToInt(this.TimeRemaining); }
	}

	public bool TimerRunning {
		get { return this.TimeRemaining > 0; }
	}

	public static MultikillTimer ResetKillTimer(Bolt.NetworkId player, List<MultikillTimer> list) {
		try {
			return Find(player, list).ResetTimer();
		} catch {
			list.Add(new MultikillTimer(player));
			return list[list.Count - 1];
		}
	}

	public static MultikillTimer Find (Bolt.NetworkId player, List<MultikillTimer> list) {
		foreach (MultikillTimer m in list) {

			if(m.NetworkID == player) {
				return m;
			}
		}

		return null;
	}
}
