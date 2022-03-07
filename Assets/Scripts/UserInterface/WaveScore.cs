using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveScore : MonoBehaviour
{
    TMP_Text waveScoreText;

    private void Awake()
    {
        waveScoreText = GetComponent<TMP_Text>();
        waveScoreText.text = "Score: Wave " + WaveIndicator._waveCount.ToString();
    }


}
