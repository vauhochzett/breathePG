using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] public int lettersPerSecond;

    public event Action OnCloseDialog;
    public event Action OnShowDialog;

    int currentLine = 0;
    Dialog dialog;
    bool isTyping;

    public static DialogManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isTyping)
        {
            ++currentLine;
            if (currentLine < dialog.Lines.Count)
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else
            {
                currentLine = 0;
                dialogBox.SetActive(false);
                OnCloseDialog?.Invoke();

                if (dialog.SwitchSceneId >= 0)
                {
                    SceneManager.LoadSceneAsync(dialog.SwitchSceneId);
                }
            }
        }
    }

    public IEnumerator ShowDialog(Dialog dialog)
    {
        yield return new WaitForEndOfFrame();

        OnShowDialog?.Invoke();

        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
        this.dialog = dialog;
    }

    public IEnumerator TypeDialog(string dialog)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f/ lettersPerSecond) ;
        }
        isTyping = false;
    }

}
