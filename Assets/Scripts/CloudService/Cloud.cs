using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudCode;
using UnityEngine.UI;
using UI.Dates;
using System;

public class Cloud : MonoBehaviour
{
    public DatePicker datePicker;
    public Text txtData;

    private async void Start()
    {
        txtData.text = "Loading...";
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        txtData.text = "Ready";
    }

    public async void GetData()
    {
        txtData.text = "Loading...";
        var cloudCodeHadSet = await CloudCodeService.Instance.CallEndpointAsync<Dictionary<string, object>>("Restriction", new());
        var dateUnlock = cloudCodeHadSet["Date unlock"].ToString();
        if (string.IsNullOrEmpty(dateUnlock) || dateUnlock == "null")
        {
            txtData.text = "No date found";
            Debug.Log("No date found");
        }
        else
        {
            txtData.text = dateUnlock;
            Debug.Log(dateUnlock);
            if (DateTime.TryParse(dateUnlock, out var date))
            {
                Debug.Log(date.Date + " vs " + DateTime.Now.Date + ": Get date is the current date: " + (date.Date == DateTime.Now.Date));
                if (date.Date == DateTime.Now.Date)
                    txtData.text = "Date is today";
                else
                    txtData.text = "Date is not today";
            }
            else
            {
                Debug.Log("Date format is not correct");
            }
        }
    }

    public async void PushData()
    {
        if (!datePicker.SelectedDate.HasValue)
        {
            Debug.Log("No date selected");
            txtData.text = "No date selected";
            return;
        }
        var dateToPush = new Dictionary<string, object>
        {
            { "Date_To_Unlock", datePicker.SelectedDate.ToDateString() }
        };
        txtData.text = "Loading...";
        var push = await CloudCodeService.Instance.CallEndpointAsync<Dictionary<string, object>>("Restriction", dateToPush);
        //push return dictionary of key: Set Date and value: the date you set
        txtData.text = "Saved date: " + dateToPush["Date_To_Unlock"].ToString();
        Debug.Log("Date saved. Saved date is: " + push["Set Date"]);
    }

    public void ShowSelectedDate(DateTime date)
    {
        //txtData.text = date.ToDateString();
    }
}
