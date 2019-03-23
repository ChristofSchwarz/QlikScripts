# QlikScripts
Useful Qlik Script Code Snippets like 
 * [AlmostAlternateStates (SyncSomeSelections)](https://github.com/ChristofSchwarz/QlikScripts/blob/master/syncSomeSelections.txt)
 * Substitute for BINARY load: [Store all tables to QVD](https://github.com/ChristofSchwarz/QlikScripts/blob/master/store_all_qvd.txt) and [Load all tables from QVD](https://github.com/ChristofSchwarz/QlikScripts/blob/master/load_all_qvd.txt)
 * [Advanced Section Access Mapping](https://github.com/ChristofSchwarz/QlikScripts/blob/master/CreateSectionAccess.txt)
 
Feel free to either download my snippets or include them directly from GitHub. How?

If you want to refer to the snippet from your Qlik Load script and include it online
 * click on "Raw" button of the snippet page here in Github and copy the link of the raw-text url ...
![alttext](https://github.com/ChristofSchwarz/pics/raw/master/rawsnippet.png "screenshot")

 * Then create a new "Web File" connection type in Qlik Sense and paste the url
 ![alttext](https://github.com/ChristofSchwarz/pics/raw/master/webfileconn.png "screenshot")

 * Check the name of the new data connection (in my case "syncSomeSelections (qse-csw_admincsw) and write the follwing line, which then will include the code live from Github.
```
$(must_include=[lib://syncSomeSelections (qse-csw_admincsw)]);
```


