using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public string m_fileName;

    private void AppendToScoreFile()
    {
        string filePath = Application.dataPath + "/" + m_fileName;

        string score = Date() + ", ";
        score += System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute.ToString("00") + ", ";
        string testAnswers = "";
        float landScore = 0;

        foreach (string s in FindObjectOfType<TestManager>().GetAnswers())
        {
            testAnswers += s + ", ";
        }
        testAnswers += FindObjectOfType<TestManager>().GetScore() + ", ";

        foreach (_PLACEHOLDER_LAND_DEFORM p in FindObjectsOfType<_PLACEHOLDER_LAND_DEFORM>())
        {
            landScore += p.CalculateLandRemaining();
        }
        landScore /= FindObjectsOfType<_PLACEHOLDER_LAND_DEFORM>().Length;

        score += testAnswers;
        score += ((landScore) * 100f).ToString("00") + "%";

        System.IO.StreamWriter file = System.IO.File.AppendText(filePath);
        file.WriteLine(score);
        file.Close();
    }

    private string Date()
    {
        return System.DateTime.Now.Year.ToString() + " " +
            System.DateTime.Now.Month.ToString() + " " +
            System.DateTime.Now.Day.ToString();
    }
    
    public void PrintScoreToText(UnityEngine.UI.Text text)
    {
        float landScore = 0;
        foreach (_PLACEHOLDER_LAND_DEFORM p in FindObjectsOfType<_PLACEHOLDER_LAND_DEFORM>())
        {
            landScore += p.CalculateLandRemaining();
        }
        landScore /= FindObjectsOfType<_PLACEHOLDER_LAND_DEFORM>().Length;

        string title = "";

        if(landScore <= 0.10f)
        {
            title = "Steward of the Swamp";
        }
        else if (landScore <= 0.35f)
        {
            title = "Warden of the Water";

        }
        else if (landScore <= 0.50f)
        {
            title = "Defender of the Delta";

        }
        else if (landScore <= 0.75f)
        {
            title = "Guardian of the Ground";

        }
        else if (landScore <= 0.90f)
        {
            title = "Hero of the Habitat";

        }
        else // Perfect score or better
        {
            title = "Champion of the Coast";

        }

        text.text = "Congratulations!" + "\n" +
            "With a final score of " + "\n" +
            (landScore * 10000).ToString("0") + "\n" +
            "You have earned the title " + "\n" +
            title + "!";

    }

    private void OnDisable()
    {
        print("dead");
        AppendToScoreFile();   
    }
}
