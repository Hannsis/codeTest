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