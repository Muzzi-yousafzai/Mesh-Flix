using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LogIn : MonoBehaviour
{

    public GameObject Email;
    public GameObject Password;
    private string email;
    private string password;
    public Text PopText;
    public GameObject PopUP;
    public GameObject Loading;
    public Text JsonText;
    public GameObject LogInPanel;
    public GameObject JsonReaderPanel;

    
    // Update is called once per frame
    void Update()
    {
        email = Email.GetComponent<InputField>().text;
        password = Password.GetComponent<InputField>().text;
        
      
    }
  
    public void ValidateSignIn()
    {
        StartCoroutine(PopupDisappear());
        if (!Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase) && password == "")
        {
            print("Enter Valid Email...");
            PopText.text = "Enter Valid Email";
            PopUP.SetActive(true);
        }
      else  if (Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase)&& password !="")
        
            
            {
                print("Succes");
            Loading.SetActive(true);
                StartCoroutine(CallSignIn(email, password));
            
            }
       
      else  if (!Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase) && password != "")
        {
            print("Enter Valid Email...");
            PopText.text = "Enter Valid Email";
              PopUP.SetActive(true);
           // Instantiate(PopUP, transform.position, transform.rotation);
           // Object.Destroy(PopUP, 2.0f);
          //  PopUP.SetActive(false);

        }
      

         else if (email == "" && password !="")
        {
            PopText.text = "Enter Email";
            PopUP.SetActive(false);
            print("Enter Email");
              PopUP.SetActive(true);
          //  Instantiate(PopUP, transform.position, transform.rotation);
         //   Object.Destroy(PopUP, 2.0f);
            //PopUP.SetActive(false);



        }
    else   if (email!="" &&  password == "")
        {
            print("Enter Password");
            PopText.text = "Enter Password";
             PopUP.SetActive(true);
           // Instantiate(PopUP, transform.position, transform.rotation);
          //  Object.Destroy(PopUP, 2.0f);
            //PopUP.SetActive(false);

        }
      if (email== "" && password=="")
        {
            PopText.text = "Enter Email & Password";
             PopUP.SetActive(true);
           // Instantiate(PopUP, transform.position, transform.rotation);
           // Object.Destroy(PopUP, 2.0f);
           // PopUP.SetActive(false);
        }
    }
    public IEnumerator CallSignIn(string email, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post("https://staging.interappusa.com/api/v1/auth/sign_in", form);
        yield return www.Send();

        if(www.error != null)
        {
            Loading.SetActive(false);

            Debug.Log("Error" + www.error);
            print("InValid Email or Password...");
            PopText.text = "InValid Email or Password";
            PopUP.SetActive(true);

        }
        else
        {
            Loading.SetActive(false);
            LogInPanel.SetActive(false);
            JsonReaderPanel.SetActive(true);
            Debug.Log("Response" + www.downloadHandler.text);
            JsonText.text = "" + www.downloadHandler.text;
        }
    }
    IEnumerator PopupDisappear()
    {
        yield return new WaitForSeconds(2);
        PopUP.SetActive(false);

    }
   
}

