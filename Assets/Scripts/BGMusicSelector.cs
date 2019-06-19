using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicSelector : MonoBehaviour
{
	public AudioSource[] tracks;
	public AudioSource gun;
	public AudioSource bottle;

	public int trackSelector;
	private int tracksCount;

	// Use this for initialization
	void Start()
	{
		trackSelector = 0;
		tracks[0].Play();
		tracksCount = tracks.Length;
		gun.Play();
		bottle.Play();
	}

	// Update is called once per frame
	void Update()
	{
		if (Application.isFocused && !tracks[trackSelector].isPlaying)
		{
			if (trackSelector < tracksCount)
				trackSelector++;
			else
				trackSelector = 0;
			tracks[trackSelector].Play();
		}
	}

	public void PlayGunAndBottle()
	{
		gun.Play();
		bottle.Play();
	}
}
