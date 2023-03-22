using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

public class Registration : MonoBehaviour
{
    public InputField emailInput;
    public InputField passwordInput;
    public InputField walletIdInput;

    private FirebaseAuth auth;
    private DatabaseReference databaseReference;

    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            auth = FirebaseAuth.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }

    public void RegisterUser()
    {
        string email = emailInput.text;
        string password = passwordInput.text;
        string walletId = walletIdInput.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to register user: " + task.Exception);
                return;
            }

            FirebaseUser user = task.Result;
            Debug.Log("User registered: " + user.Email);

            UserData userData = new UserData(email, password, walletId);
            string json = JsonUtility.ToJson(userData);

            databaseReference.Child("users").Child(user.UserId).SetRawJsonValueAsync(json);
        });
    }
}
