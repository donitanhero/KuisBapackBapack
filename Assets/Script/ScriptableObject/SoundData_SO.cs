using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Sound Data", menuName ="ScriptableObjects/Sound Data", order =1)]
public class SoundData_SO : ScriptableObject
{
     public Sound[] SoundData;


     public void PlaySoundClick(){
        StaticAction.OnSFXSoundPlay(ConstVar.SOUND_BUTTON_CLICK_SFX);
     }
}
