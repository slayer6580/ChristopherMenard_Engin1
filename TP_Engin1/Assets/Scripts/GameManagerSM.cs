using UnityEngine;

public class GameManagerSM : MonoBehaviour
{
    private static GameManagerSM _Instance;

    public static GameManagerSM GetInstance()
    {
        if (_Instance == null)
        {
            _Instance = new GameManagerSM();
        }
        return _Instance;
    }

    public GameManagerSM()
    {
        if (_Instance)
        {
            Destroy(this);
            return;
        }
    }

}
