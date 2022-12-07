using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioClip[] enemyHitSounds;
    public AudioClip[] enemyDieSounds;
    public AudioClip[] footsteps;

    public AudioClip GetEnemyHitSound()
    {
        return enemyHitSounds[Random.Range(0, enemyHitSounds.Length)];
    }

    public AudioClip GetEnemyDieSound()
    {
        return enemyDieSounds[Random.Range(0, enemyDieSounds.Length)];
    }

    public AudioClip GetFootsteps()
    {
        return footsteps[Random.Range(0, footsteps.Length)];
    }
}
