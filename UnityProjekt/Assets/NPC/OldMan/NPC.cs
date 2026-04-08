using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour{
    public NPCDialogue dialogueData;
    public GameObject  dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portaitImage;

    private int dialogueIndex;
    private bool isTyping, idDialogueActive;
}