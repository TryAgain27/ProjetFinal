using UnityEngine;

public class EnnemyFactory : MonoBehaviour
{
    [SerializeField] GameObject[] weakEnnemy;
    [SerializeField] GameObject[] distanceEnnemy;
    [SerializeField] GameObject[] bossEnnemy;
    private int randomEnnemy;
    private int weakIndex = 0;
    private int distanceIndex = 0;
    private int bossIndex = 0;

    private static EnnemyFactory instance;

    public static EnnemyFactory GetInstance() => instance;

    //--------------------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); //Appeler seulement si il existe deja un autre EnnemyFactory
        }
    }

    private void Start()
    {
        for (int i = 0; i < weakEnnemy.Length; i++)
        {
            weakIndex++;
        }
        for (int i = 0; i < distanceEnnemy.Length; i++)
        {

            distanceIndex++;

        }
        for (int i = 0; i < bossEnnemy.Length; i++)
        {

            bossIndex++;

        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public GameObject CreateWeakEnnemy()
    {
        randomEnnemy = Random.Range(0, weakIndex);
        GameObject ennemy = weakEnnemy[randomEnnemy];
        return ennemy;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public GameObject CreateDistanceEnnemy()
    {
        randomEnnemy = Random.Range(0, distanceIndex);
        GameObject ennemy = distanceEnnemy[randomEnnemy];
        return ennemy;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public GameObject CreateBossEnnemy()
    {
        randomEnnemy = Random.Range(0, bossIndex);
        GameObject ennemy = bossEnnemy[randomEnnemy];
        return ennemy;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------


    //--------------------------------------------------------------------------------------------------------------------------------------------
}
