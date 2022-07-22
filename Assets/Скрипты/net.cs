using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class net : MonoBehaviour
{
    public bool IsLoggingNow = false;
    public bool Authorized = false;

    [SerializeField] private InputField Email;
    [SerializeField] private InputField Password;
    private const string Code = "d62b004a-d27e-4cbb-bdb6-96291607480d";
    private UnityWebRequest AuthRequest;


    public void ConfirmUserData(string email, string password)
    {
        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password) && email.Contains("@"))
        {

            Email.text = email.Replace("\u200B", "");
            Password.text = password.Replace("\u200B", "");
            AuthUser();
        }

    }

    public void onClic()
    {
        ConfirmUserData(Email.text, Password.text);
        AuthUser();
    }


    public void AuthUser()
    {
        AuthRequest = UnityWebRequest.Get(@"https://mplace.azurewebsites.net/api/v1/login?email=" + Email.text
            + "&password=" + Password.text + "&code=" + Code);
        var response = AuthRequest.SendWebRequest();
        response.completed += OnRequestCompleted;
    }

    private void OnRequestCompleted(AsyncOperation obj)
    {
        IsLoggingNow = false;
        switch (AuthRequest.responseCode)
        {
            case 200:
                Authorized = true;

                SceneManager.LoadScene("END");
                break;
            case 400:
                Debug.Log(Email.text + Password.text);
                print("User not found");
                break;
            case 502:
                print("Bad gateway");
                break;
            default:
                print(AuthRequest.responseCode + ": troubles with connection");
                break;
        }
    }
    public void LoadLoginScreen()
    {
        SceneManager.LoadScene("END");
    }
}
