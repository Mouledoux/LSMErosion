using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public string m_fileName;
    private string m_filePath;

    private void Awake()
    {

    }

    void Update ()
    {

	}

    private void AppendToScoreFile()
    {
        System.IO.StreamWriter file = System.IO.File.AppendText(m_filePath);
        file.WriteLine(Date());
        file.Close();
    }

    private string Date()
    {
        return System.DateTime.Now.Year.ToString() + " " +
            System.DateTime.Now.Month.ToString() + " " +
            System.DateTime.Now.Day.ToString();
    }
}
