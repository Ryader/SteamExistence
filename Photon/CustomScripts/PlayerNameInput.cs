using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

    public class PlayerNameInput : MonoBehaviour
    {
        public Text Greeting;
        // стандартный префикс
        public string PlayerNamePrefKey = "Marauder ";

        void Start()
        {
            string defaultName = PlayerPrefs.GetString(PlayerNamePrefKey);
            InputField inputField = this.GetComponent<InputField>();

            if (inputField != null)
            {
                if (PlayerPrefs.HasKey(PlayerNamePrefKey))
                {

                    defaultName = PlayerPrefs.GetString(PlayerNamePrefKey);

                    inputField.text = defaultName;
                }
            }
            defaultName = PhotonNetwork.NickName;
        }

        void Update()
        {
            Greeting.text = "Hi, " + PlayerPrefs.GetString(PlayerNamePrefKey);
        Greeting.color = Color.white;
        }

        public void SetPlayerName(string nickname)
        {
            if (string.IsNullOrEmpty(nickname))
            {
                Debug.LogError("Player Name is null or empty");
                return;
            }
            PhotonNetwork.NickName = nickname;
            PlayerPrefs.SetString(PlayerNamePrefKey, nickname);
        }
    }
