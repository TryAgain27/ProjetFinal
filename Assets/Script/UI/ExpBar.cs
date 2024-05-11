using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{

    protected PlayerController player;
    [SerializeField] Image XpProgessBar;
    private static ExpBar instance;

    public static ExpBar GetInstance() => instance;


    //--------------------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            player = PlayerController.GetInstance();
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    private void Update()
    {
        float expRatio = (float)player.currentExp / player.expToLevel;

        XpProgessBar.transform.localScale = new Vector3(expRatio, 1, 1);
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------
}

