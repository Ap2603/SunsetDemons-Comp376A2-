using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepscore : MonoBehaviour
{
    float score;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance != null)
        {
            score = GameController.instance.playerScore;
        }
        else
        {
            score = 0;
        }
    }

    public float getscore()
    {
        return score;
    }
}
