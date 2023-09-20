This folder has a simple solution for importing .json files into Qlik Sense. 

Include or paste the script found in <a href="(https://raw.githubusercontent.com/ChristofSchwarz/QlikScripts/master/json/_importJson.qvs)" target="_blank">_importJson.qvs</a> 7
to your app script.

If you wwant to load the entire file into a QS table, call
```
CALL importJson('lib://connection/folder/file.json', 'myTable');
```

If you want to load the file starting from a certain sub-object position and below, call
```
CALL importJson('lib://connection/folder/file.json', 'myTable', 'data');
```
where data is the position.

If you want to load json from a variable that holds it, provide the variable as the 1st parameter (the 
2nd and optional 3rd parameter is as before)
```
SET vJson = `{"key1": 123, "key2": "abc", "key3": true, "arr": [5,6,7] }`;
CALL importJson(vJson, 'myTable');
```

Written by Christof Schwarz, databridge (csw@databridge.ch). Provided as is.
