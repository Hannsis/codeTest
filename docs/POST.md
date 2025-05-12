Test dotnet run:

$ curl -i -X POST "http://localhost:5251/api/tollevents/getTollFee" \
  -H "Content-Type: application/json" \
  -d '{"vehicleType": "Car", "dates": ["2025-05-09T07:00:00", "2025-05-09T08:00:00"]}'
  % Total    % Received % Xferd  Average Speed   Time    Time     Time  Current
                                 Dload  Upload   Total   Spent    Left  Speed
100   102    0    23  100    79    134    462 --:--:-- --:--:-- --:--:--   600HTTP/1.1 200 OK

Content-Type: text/plain; charset=utf-8
Date: Mon, 12 May 2025 08:09:02 GMT
Server: Kestrel
Transfer-Encoding: chunked

Toll fee calculated: 18

{ "vehicleType": "BananaTruck", "dates": ["2025-05-09T08:00:00"] }


"0000-05-09T08:00:00" is invalid
Not valid DateTime. start from 0001.


"How do I write tests for this in code instead of using curl?"
An array of times when the vehicle passed toll checkpoints in one day, what if more than one day i spassed through? tex omslag midnatt?


This should hit a mix of:
morning peak, overlapping fees (within 60 minutes), late evening (zero fee), max daily cap (should not go above 60 SEK)

curl -i -X POST "http://localhost:5251/api/tollevents/getTollFee" \
  -H "Content-Type: application/json" \
  -d '{
    "vehicleType": "car",
    "dates": [
      "2025-05-09T06:15:00",
      "2025-05-09T06:50:00",
      "2025-05-09T07:45:00",
      "2025-05-09T08:30:00",
      "2025-05-09T15:20:00",
      "2025-05-09T17:40:00",
      "2025-05-09T18:15:00",
      "2025-05-09T19:00:00"
    ]
}'
result 33 (??????)

busses are treated as unsupported vehicle type, epa, moppe, osv


curl -i -X POST "http://localhost:5251/api/tollevents/getTollFee" \
  -H "Content-Type: application/json" \
  -d '{
    "vehicleType": "tractor",
    "dates": [
      "2025-05-09T06:15:00",
      "2025-05-09T06:50:00",
      "2025-05-09T07:45:00",
      "2025-05-09T08:30:00",
      "2025-05-09T15:20:00",
      "2025-05-09T17:40:00",
      "2025-05-09T18:15:00",
      "2025-05-09T19:00:00"
    ]
}'
Result 0