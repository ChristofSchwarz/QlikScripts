Download <a href="csshacks.qvf">the app</a>.

Video resource: https://www.youtube.com/watch?v=9lhL3Nrel5Q

The app loads this text file, you can also copy/paste the css right from here it you want.


// lib://csshacks.txt points to a Webfile connection at 
// https://raw.githubusercontent.com/ChristofSchwarz/QlikScripts/master/css/csshacks.txt

LOAD
    $Effect,
    $CSS
FROM [lib://csshacks.txt]
(txt, utf8, embedded labels, delimiter is '|', no quotes);

