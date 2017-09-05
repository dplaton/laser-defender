using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

   public static AudioSource PlayClipAt(AudioClip clip, Vector3 pos) {
        GameObject tempGO = new GameObject("TempAudio"); // create the temp object
        tempGO.transform.position = pos; // set its position
        AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
        aSource.clip = clip; // define the clip
        aSource.spatialBlend = 0;
        aSource.volume = 0.7f;
        aSource.Play(); // start the sound
        Destroy(tempGO, clip.length); // destroy object after clip duration
        return aSource; // return the AudioSource reference
    }
}
