This folder has a simple solution for importing .json files into Qlik Sense

Include or paste the script found in <a href="importJson.qvs">importJson.qvs</a> to your app
script.

If you wwant to load the entire file into a QS table, call
```
CALL importJson('lib://connection/folder/file.json', 'qsTableName');
```

If you want to load the file starting from a certain sub-object position
```

```

