https://learn.microsoft.com/en-us/training/modules/build-web-api-aspnet-core/

CRUD, in memory db

HTTP - alles get post osv i know this

Don't create a web API controller by deriving from the "Controller" class. "Controller" derives from "ControllerBase" and adds support for views, so it's for handling webpages, not web API requests.

Do not put toll calculation logic directly in controller - logic works with any type of vehicle