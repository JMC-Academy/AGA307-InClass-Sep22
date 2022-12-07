using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    protected static GameManager _GM { get { return GameManager.INSTANCE; } }
    protected static EnemyManager _EM { get { return EnemyManager.INSTANCE; } }
    protected static UIManager _UI { get { return UIManager.INSTANCE; } }
    protected static PlayerMovement _P { get { return PlayerMovement.INSTANCE; } }
    protected static AudioManager _AM { get { return AudioManager.INSTANCE; } }

    /// <summary>
    /// Shuffles a list using Unity's Random
    /// </summary>
    /// <typeparam name="T">The data type</typeparam>
    /// <param name="_list">The list to shuffle</param>
    /// <returns></returns>
    public static List<T> ShuffleList<T>(List<T> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            T temp = _list[i];
            int randomIndex = UnityEngine.Random.Range(i, _list.Count);
            _list[i] = _list[randomIndex];
            _list[randomIndex] = temp;
        }
        return _list;
    }
}
