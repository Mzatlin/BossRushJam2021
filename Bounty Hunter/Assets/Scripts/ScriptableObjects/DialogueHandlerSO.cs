using UnityEngine;
[CreateAssetMenu(menuName = "DialogueHandler")]
public class DialogueHandlerSO : ScriptableObject
{
    public bool IsWriting { get; private set; }

    public void ResetWriter()
    {
        IsWriting = false;
    }

    public void RequestToWrite()
    {
        if (IsWriting)
        {
            return;
        }
        else
        {
            IsWriting = true;
        }
    }

    public void FinishWriting()
    {
        IsWriting = false;
    }

}
