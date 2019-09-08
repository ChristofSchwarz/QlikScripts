# Fill Calendar Events
For each day between the Min and Max Event Date a filled row should be created, repeating the row of the last day before. Basically, all the rows in below table tagged "orig" were given, the ones with "filled" were automatically created. The script is below

 | ID | FilledEvent | More |   | 
 | --- | --- | --- | --- |
 | 1 | 2019-08-25 | C | orig | 
 | 1 | 2019-08-26 | C | filled | 
 | 1 | 2019-08-27 | D | orig | 
 | 1 | 2019-08-28 | D | filled | 
 | 1 | 2019-08-29 | E | orig | 
 | 1 | 2019-08-30 | E | filled | 
 | 1 | 2019-08-31 | E | filled | 
 | 1 | 2019-09-01 | F | orig | 
 | 1 | 2019-09-02 | F | filled | 
 | 2 | 2019-08-26 | G | orig | 
 | 2 | 2019-08-27 | G | filled | 
 | 2 | 2019-08-28 | G | filled | 
 | 2 | 2019-08-29 | G | filled | 
 | 2 | 2019-08-30 | G | filled | 
 | 2 | 2019-08-31 | H | orig | 
 | 2 | 2019-09-01 | H | filled | 
 | 2 | 2019-09-02 | I | orig | 


```
PoT:
LOAD * INLINE [
ID, Event, More, MuchMore
1, 2019-08-15, C, y
1, 2019-08-17, D, x
1, 2019-08-29, E, x
1, 2019-09-02, F, y
2, 2019-08-16, G, y
2, 2019-08-31, H, x
2, 2019-09-03, I, x
];
// Add a "SpanEventTo" column which is 1 second before the subsequent known Event
// Find out the total Max and Min Event while loading each line
PoT2:
LOAD 
	*, 
	TimeStamp(
  		IF(Peek('ID',-1,'PoT2') = ID, Peek('Event',-1,'PoT2')-1/24/3600, MakeDate(2100,12,31)) 
    ) AS SpanEventTo,
	RangeMax(Peek('Event.Max',-1,'PoT2'), Event) AS Event.Max,
	RangeMin(Peek('Event.Min',-1,'PoT2'), Event) AS Event.Min
RESIDENT 
	PoT
ORDER BY 
	ID ASC, Event DESC;

// get the last rows value for Min and Max (which is logically the total min/total max)
LET vMaxDate = Peek('Event.Max',-1,'PoT2');
LET vMinDate = Peek('Event.Min',-1,'PoT2');
DROP FIELDS Event.Min, Event.Max;
DROP TABLE PoT;

// Create a calender with dates spanning from Min to Max
AllTimes:
LOAD
Date(RecNo() -1 + $(vMinDate), 'YYYY-MM-DD') AS FilledEvent
AUTOGENERATE ($(vMaxDate) - $(vMinDate) + 1);
// Create a cartesean product with all existing IDs 
// (JOIN with no matching field)
JOIN LOAD DISTINCT ID RESIDENT PoT2;
// Merge the PoT2 table into the calendar. IntervalMatch
// does the magic to find out into which From-To timespan a 
// given day falls
INNER JOIN
IntervalMatch(FilledEvent, ID)
LOAD Event, SpanEventTo, ID RESIDENT PoT2;

// Move all the other columns from original table into new calendar and drop old table
INNER JOIN (AllTimes)
LOAD * RESIDENT PoT2;
DROP TABLE PoT2;
//DROP FIELD SpanEventTo;
```

