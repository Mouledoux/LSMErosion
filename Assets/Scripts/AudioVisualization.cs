/* * * * * * * * * * 
 * Creadted by: Eric Mouledoux
 * Contact: EricMouledoux@Gmail.com
 * * * * * * * * * */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class AudioVisualization : MonoBehaviour
{
    /// <summary>
    /// The AudioSource attached to this GameObject
    /// </summary>
    public AudioSource m_AudioSource;

    [SerializeField]
    private float m_StartSec, m_EndSec;

    /// <summary>
    /// Number of frequency bands the audio source will be split into.
    /// Has a floor of 6, and celing of 13
    /// </summary>
    [SerializeField]
    private int m_NumberOfFrequencyBands;

    [SerializeField]
    private UnityEngine.Events.UnityEvent m_OnPlay;
    [SerializeField]
    private UnityEngine.Events.UnityEvent m_OnEnd;

    /// <summary>
    /// The total number of audio sambles from the connect AudioSource (Left).
    /// Will be equal to 2^m_NumberOfFrequenceBands
    /// </summary>
    private float[] m_SamplesLeft;
    /// <summary>
    /// The total number of audio sambles from the connect AudioSource (right).
    /// Will be equal to 2^m_NumberOfFrequenceBands
    /// </summary>
    private float[] m_SamplesRight;

    /// <summary>
    /// The stereo Frequence bands of the AudioSource evenly distrubited amongst m_NumberOfFrequenceBands
    /// </summary>
    public float[] m_CurrentFrequencyStereo;
    /// <summary>
    /// The left Frequence bands of the AudioSource evenly distrubited amongst m_NumberOfFrequenceBands
    /// </summary>
    public float[] m_CurrentFrequencyLeft;
    /// <summary>
    /// The right Frequence bands of the AudioSource evenly distrubited amongst m_NumberOfFrequenceBands
    /// </summary>
    public float[] m_CurrentFrequencyRight;

    /// <summary>
    /// The preavious value of m_CurrentFrequencyStereo
    /// </summary>
    private float[] m_LastFrequencyStereo;
    /// <summary>
    /// The preavious value of m_CurrentFrequencyLeft
    /// </summary>
    private float[] m_LastFrequencyLeft;
    /// <summary>
    /// The preavious value of m_CurrentFrequencyRight
    /// </summary>
    private float[] m_LastFrequencyRight;

    /// <summary>
    /// The change in stereo frequency since the last update
    /// </summary>
    public float[] m_DeltaFrequencyStereo;
    /// <summary>
    /// The change in left channel's frequency since the last update
    /// </summary>
    public float[] m_DeltaFrequencyLeft;
    /// <summary>
    /// The change in right channel's frequency since the last update
    /// </summary>
    public float[] m_DeltaFrequencyRight;

    /// <summary>
    /// The normalized values (0-1) of the stereo audio bands
    /// </summary>
    private float[] m_HighestFrequencyStereo;
    /// <summary>
    /// The normalized values (0-1) of the left audio bands
    /// </summary>
    private float[] m_HighestFrequencyLeft;
    /// <summary>
    /// The normalized values (0-1) of the right audio bands
    /// </summary>
    private float[] m_HighestFrequencyRight;

    public int frequencyBands
    {
        get { return m_NumberOfFrequencyBands; }
    }

    private void Awake()
    {
        // Gets the AudioSource on this GameObject if one is not given in the inspector
        m_AudioSource = m_AudioSource != null ? m_AudioSource : GetComponent<AudioSource>();

        // Makes sure the number of bands is atleast 6, and no larger that 13
        m_NumberOfFrequencyBands = Mathf.Clamp(m_NumberOfFrequencyBands, 6, 13);

        // Ensures there is 1 and only 1 AudioVisualiser in the scene for static refrencing
        // RemoveDuplicateVisualizers();
        // Removed because values are no longer static

        // Creates the empty arrays for the frequency bands
        m_CurrentFrequencyStereo = new float[m_NumberOfFrequencyBands];
        m_CurrentFrequencyLeft = new float[m_NumberOfFrequencyBands];
        m_CurrentFrequencyRight = new float[m_NumberOfFrequencyBands];

        //InitFloatArray(m_CurrentFrequencyStereo, 0.01f);
        //InitFloatArray(m_CurrentFrequencyLeft, 0.01f);
        //InitFloatArray(m_CurrentFrequencyRight, 0.01f);


        // Creates the empty arrays for the last value of the frequencies
        m_LastFrequencyStereo = new float[m_NumberOfFrequencyBands];
        m_LastFrequencyLeft = new float[m_NumberOfFrequencyBands];
        m_LastFrequencyRight = new float[m_NumberOfFrequencyBands];

        //InitFloatArray(m_LastFrequencyStereo, 0);
        //InitFloatArray(m_LastFrequencyLeft, 0);
        //InitFloatArray(m_LastFrequencyRight, 0);


        // Creates the empty arrays for the changes in frequency
        m_DeltaFrequencyStereo = new float[m_NumberOfFrequencyBands];
        m_DeltaFrequencyLeft = new float[m_NumberOfFrequencyBands];
        m_DeltaFrequencyRight = new float[m_NumberOfFrequencyBands];

        //InitFloatArray(m_DeltaFrequencyStereo, 0.001f);
        //InitFloatArray(m_DeltaFrequencyLeft, 0.001f);
        //InitFloatArray(m_DeltaFrequencyRight, 0.001f);


        // Creates the empty arrays for the max recorded samples
        m_HighestFrequencyStereo = new float[m_NumberOfFrequencyBands];
        m_HighestFrequencyLeft = new float[m_NumberOfFrequencyBands];
        m_HighestFrequencyRight = new float[m_NumberOfFrequencyBands];

        //InitFloatArray(m_HighestFrequencyStereo, 1f);
        //InitFloatArray(m_HighestFrequencyLeft, 1f);
        //InitFloatArray(m_HighestFrequencyRight, 1f);


        // Creates the empty array for the total audio samlpes
        m_SamplesLeft = new float[(int)Mathf.Pow(2, m_NumberOfFrequencyBands)];
        m_SamplesRight = new float[(int)Mathf.Pow(2, m_NumberOfFrequencyBands)];

        //InitFloatArray(m_SamplesLeft, 0);
        //InitFloatArray(m_SamplesRight, 0);

        ResetAllValues();

        m_AudioSource.time = m_StartSec;
        m_EndSec = m_StartSec >= m_EndSec ? m_AudioSource.clip.length : m_EndSec;
        m_OnPlay.Invoke();
    }

    private void Update()
    {
        GetAudioSpectrumData();

        SplitStereoAudioArrayToFrequenceBands(m_SamplesLeft, m_SamplesRight, m_CurrentFrequencyStereo);
        SplitSingleAudioChannelArrayToFrequenceBands(m_SamplesLeft, m_CurrentFrequencyLeft);
        SplitSingleAudioChannelArrayToFrequenceBands(m_SamplesRight, m_CurrentFrequencyRight);

        NormalizeFrequencyBands(m_HighestFrequencyStereo, m_CurrentFrequencyStereo);
        NormalizeFrequencyBands(m_HighestFrequencyLeft, m_CurrentFrequencyLeft);
        NormalizeFrequencyBands(m_HighestFrequencyRight, m_CurrentFrequencyRight);

        SetFrequencyDeltas(m_CurrentFrequencyStereo, m_LastFrequencyStereo, m_DeltaFrequencyStereo);
        SetFrequencyDeltas(m_CurrentFrequencyLeft, m_LastFrequencyLeft, m_DeltaFrequencyLeft);
        SetFrequencyDeltas(m_CurrentFrequencyRight, m_LastFrequencyRight, m_DeltaFrequencyRight);

        CheckTimeLimit();
    }


    /// <summary>
    /// Gets the spectrum data (in stereo) of the AudioSource attached to this GameObject
    /// </summary>
    private void GetAudioSpectrumData()
    {
        // Gets the left audio spectrum
        m_AudioSource.GetSpectrumData(m_SamplesLeft, 0, FFTWindow.Blackman);
        // Gets the right audio spectrum
        m_AudioSource.GetSpectrumData(m_SamplesRight, 1, FFTWindow.Blackman);
    }

    /// <summary>
    /// Distributes the stereo audio samples (all 2^number of bands) amongst the frequency bands
    /// </summary>
    /// <param name="audioArrayLeft">Left channel array</param>
    /// <param name="audioArrayright">Right channel array</param>
    /// <param name="splitArray">Array to dump distributed samples in</param>
    private void SplitStereoAudioArrayToFrequenceBands(float[] audioArrayLeft, float[] audioArrayRight, float[] splitArray)
    {
        // The average value for this samples range
        float average = 0;
        // The current Power of Two
        int PoT = 0;

        // For all of our splitArray bands
        for (int i = 0; i < splitArray.Length; ++i)
        {
            // Zero out the average
            average = 0;
            // Set the current Power of Two to 2^i
            PoT = (int)Mathf.Pow(2, i);

            // Then for the range from this PoT to the next PoT
            for (int j = PoT - 1; j < (PoT * 2) - 1; ++j)
            {
                // Increase the average by the left and right (stereo) channels
                average += (audioArrayLeft[j] + audioArrayRight[j]) * (j + 1);
            }

            // Divide the sum by the count to get the average
            average /= PoT;
            // Multiply by 10 to move the decimal
            average *= 10;

            // Ensures that the value can never drop below 0.01
            average = Mathf.Clamp(average, 0.01f, float.MaxValue);

            // Add the average to the array
            splitArray[i] = average;
        }
    }

    /// <summary>
    /// Distributes the single channel audio samples (all 2^number of bands) amongst the frequency bands
    /// </summary>
    /// <param name="audioArrayChannel">single channel array</param>
    /// <param name="splitArray">Array to dump distributed samples in</param>
    private void SplitSingleAudioChannelArrayToFrequenceBands(float[] audioArrayChannel, float[] splitArray)
    {
        // The average value for this samples range
        float average = 0;
        // The current Power of Two
        int PoT = 0;

        // For all of our splitArray bands
        for (int i = 0; i < splitArray.Length; ++i)
        {
            // Zero out the average
            average = 0;
            // Set the current Power of Two to 2^i
            PoT = (int)Mathf.Pow(2, i);

            // Then for the range from this PoT to the next PoT
            for (int j = PoT - 1; j < (PoT * 2) - 1; ++j)
            {
                // Increase the average by single channels
                average += (audioArrayChannel[j]) * (j + 1);
            }

            // Divide the sum by the count to get the average
            average /= PoT;
            // Multiply by 10 to move the decimal
            average *= 10;

            // Add the average to the array
            splitArray[i] = average;
        }
    }

    /// <summary>
    /// Normalizes the array to only contain values between 0 and 1 based on the max recorded past vaslues
    /// </summary>
    /// <param name="highest"></param>
    /// <param name="current"></param>
    private void NormalizeFrequencyBands(float[] highest, float[] current)
    {
        // For each element in the array to normalize
        for (int i = 0; i < current.Length; ++i)
        {
            // Set the new highest value to the larget number between the current highest, and the value to normalize
            highest[i] = highest[i] > current[i] ? highest[i] : current[i];

            // Set the current value to itself divided by the new (or original) highest value
            current[i] = current[i] / highest[i];
        }
    }

    /// <summary>
    /// Stores the current array values as the last, and stores the difference in a delta array
    /// </summary>
    /// <param name="current">Array with the current values</param>
    /// <param name="last">Array with the preavious values</param>
    /// <param name="delta">Array with the difference of the current and last arrays</param>
    private void SetFrequencyDeltas(float[] current, float[] last, float[] delta)
    {
        // For each element in the 'current' array
        for (int i = 0; i < current.Length; ++i)
        {
            // Set the delat to the difference between the current and last array
            delta[i] = (current[i] - last[i]);
            // and store the current in the last array
            last[i] = current[i];
        }
    }

    private void CheckTimeLimit()
    {
        if(m_AudioSource.time >= m_EndSec || !m_AudioSource.isPlaying)
        {
            m_AudioSource.Stop();
            ResetAllValues();
            m_OnEnd.Invoke();
        }
    }

    // HELPER FUNCTIONS // HELPER FUNCTIONS // HELPER FUNCTIONS // HELPER FUNCTIONS // HELPER FUNCTIONS // // HELPER FUNCTIONS // HELPER FUNCTIONS // HELPER FUNCTIONS // HELPER FUNCTIONS // HELPER FUNCTIONS // 

    /// <summary>
    /// Finds all AudioVisualizers in the scene and remves all but this one
    /// </summary>
    private void RemoveDuplicateVisualizers()
    {
        // Gets all the AudioVisualizers int the scene
        AudioVisualization[] visualizers = FindObjectsOfType<AudioVisualization>();

        // For each visualizer in the scene
        foreach (AudioVisualization av in visualizers)
        {
            // If it is NOT this one                                            
            if (av != this)
            {
                // Destroy it
                Destroy(av.gameObject);
            }
        }
    }

    /// <summary>
    /// Sets all elements in a float array to 0;
    /// </summary>
    /// <param name="array"></param>
    private void InitFloatArray(float[] array, float initValue)
    {
        for (int i = 0; i < array.Length; ++i)
        {
            array[i] = initValue;
        }
    }

    public void ResetAllValues()
    {
        InitFloatArray(m_CurrentFrequencyStereo, 0.01f);
        InitFloatArray(m_CurrentFrequencyLeft, 0.01f);
        InitFloatArray(m_CurrentFrequencyRight, 0.01f);

        InitFloatArray(m_LastFrequencyStereo, 0);
        InitFloatArray(m_LastFrequencyLeft, 0);
        InitFloatArray(m_LastFrequencyRight, 0);

        InitFloatArray(m_DeltaFrequencyStereo, 0.001f);
        InitFloatArray(m_DeltaFrequencyLeft, 0.001f);
        InitFloatArray(m_DeltaFrequencyRight, 0.001f);

        InitFloatArray(m_HighestFrequencyStereo, 1f);
        InitFloatArray(m_HighestFrequencyLeft, 1f);
        InitFloatArray(m_HighestFrequencyRight, 1f);

        InitFloatArray(m_SamplesLeft, 0);
        InitFloatArray(m_SamplesRight, 0);
    }

    public void SetStartTime(float time)
    {
        m_StartSec = time;
    }
    public void SetEndTime(float time)
    {
        m_EndSec = time;
    }
}