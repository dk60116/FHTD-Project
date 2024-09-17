using System;
using System.Collections;
using System.Globalization;
using UnityEngine;

namespace Assets.UTime
{
    public class UTime : MonoBehaviour
    {
		// TODO: If you get the error saying "Script invoked too many times per second for this Google user account.", you should create your own instance. Refer to readme.txt.
		private const string TimeServer = "https://script.google.com/macros/s/AKfycbxaN4rDRamGBOiTY8NviW7nQ4GQxBOwQlD9aj42Hw/exec";
        private static UTime _instance;

        private static UTime Instance
        {
            get { return _instance ?? (_instance = new GameObject("UTime").AddComponent<UTime>()); }
        }

        /// <summary>
        /// Get time from server asynchronically with callback (bool success, string error, DateTime time).
        /// </summary>
        public static void GetUtcTimeAsync(Action<bool, string, DateTime> callback)
        {
            Debug.Log("Requesting network time from: " + TimeServer);

            Instance.StartCoroutine(Download(TimeServer, www =>
            {
                if (www.error == null)
                {
                    try
                    {
                        callback(true, null, DateTime.Parse(www.text, CultureInfo.InvariantCulture).ToUniversalTime());
                    }
                    catch (Exception e)
                    {
                        callback(false, e.Message, DateTime.MinValue);
                    }
                }
                else
                {
                    callback(false, www.error, DateTime.MinValue);
                }
            }));
        }

        /// <summary>
        /// Check network connection asynchronically with callback (bool success).
        /// </summary>
        public static void HasConnection(Action<bool> callback)
        {
            Instance.StartCoroutine(Download(TimeServer, www => { callback(www.error == null); }));
        }

        private static IEnumerator Download(string url, Action<WWW> callback)
        {
            var www = new WWW(url);

            yield return www;

            callback(www);
        }
    }
}