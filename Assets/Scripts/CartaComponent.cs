using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CartaComponent : MonoBehaviour
{
    [SerializeField] private string textContent;
    [SerializeField] private string textAuthor;
    [SerializeField] private TMP_Text tmp_text_top;
    [SerializeField] private TMP_Text tmp_text_author;
    [SerializeField] private GameObject envelopeGameObject;
    [SerializeField] private GameObject letterGameObject;
    public void changeToLetter()
    {
        letterGameObject.SetActive(true);
        envelopeGameObject.SetActive(false);
    }

    public void changeToEnvelope()
    {
        letterGameObject.SetActive(false);
        envelopeGameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        letterGameObject.SetActive(false);
        envelopeGameObject.SetActive(true);

        tmp_text_top.text = textContent;
        tmp_text_author.text = textAuthor;
    }
}
