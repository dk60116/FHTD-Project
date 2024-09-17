using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UTime
{
    public class Example : MonoBehaviour
    {
        public Text Time;

        /// <summary>
        /// Get time from server on start.
        /// </summary>
        public void Start()
        {
            UTime.HasConnection(connection =>
                Debug.Log("Connection: " + connection));

            InvokeRepeating(nameof(updateTime), 0, 0.1f);
        }

        private void updateTime()
        {
            UTime.GetUtcTimeAsync(OnTimeReceived);
        }

        /// <summary>
        /// Proceed callback.
        /// </summary>
        private void OnTimeReceived(bool success, string error, DateTime time)
        {
            if (success)
            {
                Time.text = string.Format("Network time (UTC+0) = {0}\nLocal time = {1}", time, time.ToLocalTime());
            }
            else
            {
                Time.text = error;
            }
        }
    }
}