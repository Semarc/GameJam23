using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioScript : MonoBehaviour
{
	public static AudioScript Instance { get; private set; }


	[SerializeField] AudioClip EnergyDistAlarm;
	[SerializeField] AudioClip Throw;
	[SerializeField] AudioClip Select;

	[SerializeField] AudioClip[] Music;

	AudioSource player;
	private void Awake()
	{
		if (Instance == null)
		{
			Debug.Log("Created AudioScript");
			Instance = this;
			player = GetComponent<AudioSource>();
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Debug.LogWarning("Duplicate AudioScript");
			Destroy(gameObject);
		}
	}

	public void PlayThrowSound()
	{
		player.PlayOneShot(Throw);
	}
	public void PlayFlashlightSound()
	{
		player.PlayOneShot(EnergyDistAlarm);
	}
	public void PlaySelectSound()
	{
		player.PlayOneShot(Select);
	}

	public void PlayMusic(int index)
	{
		PlayClip(Music[index]);
	}
	public void PlayRandomMusic()
	{
		PlayClip(Music[Random.Range(1, Music.Length)]);
	}
	public void PlayMainMenuMusic()
	{
		PlayClip(Music[0]);
	}
	public void StopMusic()
	{
		player.Stop();
	}



	private void PlayClip(AudioClip clip)
	{
		player.Stop();
		player.clip = clip;
		player.Play();
	}
	public void SetVolume(float NewVolume)
	{
		player.volume = NewVolume;
	}
}