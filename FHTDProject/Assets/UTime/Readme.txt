UTime is a crossplatform plugin which will help you to get network time and check network connection.


Features:

- Get network time

- Check network connection

- Uses secure connection over HTTPS (SSL)

- Works on iOS, Android, WebGL and Desktop


Usage:

- UTime.GetUtcTimeAsync((success, error, time) => { YOUR_HANDLER });

- UTime.HasConnection(connection => { YOUR_HANDLER });


Notes:

- Google has execution quotas (https://developers.google.com/apps-script/guides/services/quotas), so it's prefered to create your own instance of time server.
 If you get the error saying "Script invoked too many times per second for this Google user account.", you should create your own instance.
- Don't forget to check your script execution statistics and make sure there are no errors.

- If you see "Unsupported encoding" message in console, don't worry as it appears only in Unity Editor.


How to add your own time server:

- go to https://www.google.com/script/start/

- create a new script and paste the code: function doGet() { return ContentService.createTextOutput(new Date().toISOString()); }

- publish it as web app under your account and set anonymous access (IMPORTANT)

- replace UTime.TimeServer URL constant

- test it!


Feel free to rate it and leave your feedback! Thanks!