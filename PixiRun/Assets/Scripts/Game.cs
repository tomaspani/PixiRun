using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour, IObserver
{
    Dictionary<string, System.Action> _observerActions;

    IObservable _object;

    [SerializeField] GameObject _panelLose;

    private void Awake()
    {
        FillActionsDictionary();
    }

    void FillActionsDictionary()
    {
        _observerActions = new Dictionary<string, System.Action>();

        _observerActions.Add("OnLose", OnLose);
    }

    private void Start()
    {
        _object = FindObjectOfType<Model>();

        _object?.Subscribe(this);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    void OnLose()
    {
        _panelLose.SetActive(true);
        Time.timeScale = 0;
    }

    public void Notify(string action)
    {
        if (_observerActions.ContainsKey(action))
            _observerActions[action]();
    }
}
