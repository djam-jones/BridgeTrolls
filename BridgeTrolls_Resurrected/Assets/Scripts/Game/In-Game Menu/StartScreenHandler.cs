using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartScreenHandler : MonoBehaviour
{
    [SerializeField]
	private Text _countDownText;

	[SerializeField]
	private Text _pointsText;

	[SerializeField]
	private GameObject _beginScreen;

    private string text;

	[SerializeField]
	private float timer = 5;
		
	void Awake()
	{
		_pointsText.text = PlayerPrefs.GetInt("AmountOfGamePoints").ToString();
	}

    void Update()
    {
		Countdown();
    }

	public void Countdown()
	{
		_countDownText.text = text;
		text = timer.ToString("f0");
		if (timer > 0)
		{
			timer -= 1 * Time.deltaTime;
		}
		else
		{
			SendMessage("StartGame", true);
			StartCoroutine(DisableObject(_beginScreen));
		}
	}

	private IEnumerator DisableObject(GameObject panel)
	{
		yield return new WaitForSeconds(0.25f);
		this.enabled = false;
		panel.SetActive(false);
	}
}
