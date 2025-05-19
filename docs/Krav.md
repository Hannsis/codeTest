# Introduktion 
Kodtest

# Uppgiften
Målbilden är en applikation för att hantera biltullarna i Göteborg med tillhörande avgiftsberäkning för fordonen.

Det finns inga direkta syntaxfel utan det handlar mer om struktur och allmänna programmeringskunskaper som inte är specifika för något språk. 
Det finns utrymme för en mängd förbättringar – vi vill att du tänker igenom vilka förändringar och tillägg du vill göra för att få en kod som du ”kan stå för”.

# Tidsåtgång
Ingen deadline, men lämna ett resultat inom 7-10 dagar.  

# Inlämning
Lägg upp lösningen på din egen github så vi kan följa commit-historiken. 

Varje passage genom en betalstation i Göteborg kostar antingen 8, 13 eller 18 kronor beroende på tidpunkt. 
Det högsta beloppet per dag och fordon är 60 kronor.

```
Tider och avgifter:
06:00–06:29 8 kr
06:30–06:59 13 kr
07:00–07:59 18 kr
08:00–08:29 13 kr
08:30–14:59 8 kr

15:00–15:29 13 kr
15:30–16:59 18 kr
17:00–17:59 13 kr
18:00–18:29 8 kr
18:30–05:59 0 kr
``` 

Trängselskatt tas ut för fordon som passerar en betalstation måndag till fredag mellan 06:00 och 18:29. 
Ingen skatt tas ut på lördagar, helgdagar, dagar före helgdag eller under juli månad. 

Vissa fordon är undantagna från trängselskatt. 

Om en bil passerar flera betalstationer inom 60 minuter tas bara den högsta avgiften ut under den perioden.