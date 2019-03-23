# QlikScripts
Useful Qlik Script Code Snippets like 
 * [AlmostAlternateStates (SyncSomeSelections)]https://github.com/ChristofSchwarz/QlikScripts/blob/master/syncSomeSelections.txt

You can download them or run them directly from GitHub.

If you want to refer to the snippet from your Qlik Load script and include it online
 * click on "Raw" button of the snippet page here in Github and copy the link of the raw-text url ...
![alttext](https://github.com/ChristofSchwarz/pics/raw/master/rawsnippet.png "screenshot")

 * Then create a new "Web File" connection type in Qlik Sense and paste the url
 ![alttext](https://github.com/ChristofSchwarz/pics/raw/master/webfileconn.png "screenshot")

 * Check the name of the new data connection (in my case "syncSomeSelections (qse-csw_admincsw) and write the follwing line, which then will include the code live from Github.
```
$(must_include=[lib://syncSomeSelections (qse-csw_admincsw)]);
```


