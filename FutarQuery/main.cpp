#include <QCoreApplication>
#include <iostream>
#include "FutarStopRequest.h"
#include "DemoQuit.h"
using namespace std;

/*
Megálló azonosító kiderítése:
    Ha ezen az oldalon
    http://kovibusz.mefi.be/
    rákattintotok egy megállóra, azon belül pedig egy járatra, akkor a továbblépés
    URL-jében benne van a megálló ID-ja:
    http://kovibusz.mefi.be/stop/F02222/route/BKK_2120
    Vagyis a megálló az F02222.
*/
int main(int argc, char *argv[])
{
    QCoreApplication a(argc, argv);
    FutarStopRequest request(true);    // true: verbose mode

    // Ha a demó véget ér, a DemoQuit::Quit slotot meghívva kérjük a program leállását.
    DemoQuit quit;
    QObject::connect(&request, SIGNAL(QueryFinished()), &quit, SLOT(Quit()));
    // Kérés aszinkron elküldése és belépés a végtelen eseménykezelő ciklusba.
    request.SendRequest("F02222");

    return a.exec();
}

/* Példa rövid válasz:
  Server: nginx
  Date: Tue, 07 Mar 2017 13:02:39 GMT
  Content-Type: application/json
  Content-Length: 3564
  Connection: keep-alive
  X-BKK-Status: OK
  X-Server: prod121.kozlek.local_8092
  Expires: Tue, 07 Mar 2017 13:02:38 GMT
  Cache-Control: no-cache
  Pragma: no-cache
  Access-Control-Allow-Origin: *
----- RAW ANSWER -----
----- ROOT OBJECT -----
----- ROOT->DATA OBJECT -----
----- ROOT->DATA->ENTRY OBJECT -----
----- ROOT->DATA->ENTRY->STOPTIMES ARRAY -----
-- -- Arrival time: 2017-03-07 14:02
-- -- Arrival time: 2017-03-07 14:11
-- -- Arrival time: 2017-03-07 14:12
-- -- Arrival time: 2017-03-07 14:22
*/

/* Verbose válasz:
  Server: nginx
  Date: Tue, 07 Mar 2017 13:03:13 GMT
  Content-Type: application/json
  Content-Length: 4001
  Connection: keep-alive
  X-BKK-Status: OK
  X-Server: prod121.kozlek.local_8092
  Expires: Tue, 07 Mar 2017 13:03:12 GMT
  Cache-Control: no-cache
  Pragma: no-cache
  Access-Control-Allow-Origin: *
----- RAW ANSWER -----
{"version":2,"status":"OK","code":200,"text":"OK","currentTime":1488891793964,"data":{"limitExceeded":false,"entry":{"stopId":"BKK_F02222","alertIds":[],"nearbyStopIds":[],"stopTimes":[{"arrivalTime":1488891720,"departureTime":1488891720,"predictedArrivalTime":1488891883,"predictedDepartureTime":1488891883,"tripId":"BKK_B624151330","serviceDate":"20170307"},{"arrivalTime":1488892260,"departureTime":1488892260,"predictedArrivalTime":1488892138,"predictedDepartureTime":1488892138,"tripId":"BKK_B608454921","serviceDate":"20170307"},{"arrivalTime":1488892320,"departureTime":1488892320,"predictedArrivalTime":1488892233,"predictedDepartureTime":1488892233,"tripId":"BKK_B624151334","serviceDate":"20170307"},{"arrivalTime":1488892920,"departureTime":1488892920,"predictedArrivalTime":1488893129,"predictedDepartureTime":1488893129,"tripId":"BKK_B624151338","serviceDate":"20170307"},{"arrivalTime":1488893580,"departureTime":1488893580,"predictedArrivalTime":1488893591,"predictedDepartureTime":1488893591,"tripId":"BKK_B624151504","serviceDate":"20170307"}]},"references":{"agencies":{"BKK":{"id":"BKK","name":"BKK","url":"http://www.bkk.hu","timezone":"Europe/Budapest","lang":"hu","phone":"+36 1 3 255 255"}},"routes":{"BKK_1530":{"id":"BKK_1530","shortName":"153","longName":null,"description":"GazdagrÃ©ti tÃ©r | Neumann JÃ¡nos utca","type":"BUS","url":null,"color":"009FE3","textColor":"FFFFFF","agencyId":"BKK","bikesAllowed":false},"BKK_1542":{"id":"BKK_1542","shortName":"154B","longName":null,"description":"Neumann JÃ¡nos utca","type":"BUS","url":null,"color":"009FE3","textColor":"FFFFFF","agencyId":"BKK","bikesAllowed":false},"BKK_VP06":{"id":"BKK_VP06","shortName":"6","longName":null,"description":"SzÃ©ll KÃ¡lmÃ¡n tÃ©r M | MÃ³ricz Zsigmond kÃ¶rtÃ©r M","type":"BUS","url":null,"color":"009FE3","textColor":"FFFFFF","agencyId":"BKK","bikesAllowed":false},"BKK_2120":{"id":"BKK_2120","shortName":"212","longName":null,"description":"SvÃ¡bhegy | BorÃ¡ros tÃ©r H","type":"BUS","url":null,"color":"009FE3","textColor":"FFFFFF","agencyId":"BKK","bikesAllowed":false},"BKK_9180":{"id":"BKK_9180","shortName":"918","longName":null,"description":"Ã“buda, BogdÃ¡ni Ãºt | KelenfÃ¶ld vasÃºtÃ¡llomÃ¡s M","type":"BUS","url":null,"color":"1E1E1E","textColor":"FFFFFF","agencyId":"BKK","bikesAllowed":false}},"stops":{"BKK_F02222":{"id":"BKK_F02222","lat":47.47659,"lon":19.059228,"name":"PetÅ‘fi hÃ­d, budai hÃ­dfÅ‘","code":"F02222","direction":"50","locationType":0,"parentStationId":"BKK_CSF02225","type":"BUS","wheelchairBoarding":true,"routeIds":["BKK_VP06","BKK_1530","BKK_1542","BKK_2120","BKK_9180"],"stopColorType":"BUS"}},"trips":{"BKK_B624151330":{"id":"BKK_B624151330","routeId":"BKK_2120","shapeId":"BKK_B624151330","blockId":null,"tripHeadsign":"BorÃ¡ros tÃ©r H","tripShortName":null,"serviceId":"BKK_B62415RAAHCSN-0021","directionId":"1","bikesAllowed":false,"wheelchairAccessible":true},"BKK_B608454921":{"id":"BKK_B608454921","routeId":"BKK_1530","shapeId":"BKK_B608454921","blockId":null,"tripHeadsign":"Neumann JÃ¡nos utca","tripShortName":null,"serviceId":"BKK_B60845AHCRM-0041","directionId":"1","bikesAllowed":false,"wheelchairAccessible":true},"BKK_B624151504":{"id":"BKK_B624151504","routeId":"BKK_2120","shapeId":"BKK_B624151504","blockId":null,"tripHeadsign":"BorÃ¡ros tÃ©r H","tripShortName":null,"serviceId":"BKK_B62415RAAHCSN-0021","directionId":"1","bikesAllowed":false,"wheelchairAccessible":true},"BKK_B624151338":{"id":"BKK_B624151338","routeId":"BKK_2120","shapeId":"BKK_B624151338","blockId":null,"tripHeadsign":"BorÃ¡ros tÃ©r H","tripShortName":null,"serviceId":"BKK_B62415RAAHCSN-0021","directionId":"1","bikesAllowed":false,"wheelchairAccessible":false},"BKK_B624151334":{"id":"BKK_B624151334","routeId":"BKK_2120","shapeId":"BKK_B624151334","blockId":null,"tripHeadsign":"BorÃ¡ros tÃ©r H","tripShortName":null,"serviceId":"BKK_B62415RAAHCSN-0021","directionId":"1","bikesAllowed":false,"wheelchairAccessible":true}},"alerts":{}},"class":"entryWithReferences"}}
----- ROOT OBJECT -----
-- key: code = DOUBLE (200)
-- key: currentTime = DOUBLE (1.48889e+012)
-- key: data = OBJECT
-- key: status = STRING (OK)
-- key: text = STRING (OK)
-- key: version = DOUBLE (2)
----- ROOT->DATA OBJECT -----
-- key: class = STRING (entryWithReferences)
-- key: entry = OBJECT
-- key: limitExceeded = BOOL (0)
-- key: references = OBJECT
----- ROOT->DATA->ENTRY OBJECT -----
-- key: alertIds = ARRAY
-- key: nearbyStopIds = ARRAY
-- key: stopId = STRING (BKK_F02222)
-- key: stopTimes = ARRAY
----- ROOT->DATA->ENTRY->STOPTIMES ARRAY -----
-- key: arrivalTime = DOUBLE (1.48889e+009)
-- key: departureTime = DOUBLE (1.48889e+009)
-- key: predictedArrivalTime = DOUBLE (1.48889e+009)
-- key: predictedDepartureTime = DOUBLE (1.48889e+009)
-- key: serviceDate = STRING (20170307)
-- key: tripId = STRING (BKK_B624151330)
-- -- Arrival time: 2017-03-07 14:02
-- key: arrivalTime = DOUBLE (1.48889e+009)
-- key: departureTime = DOUBLE (1.48889e+009)
-- key: predictedArrivalTime = DOUBLE (1.48889e+009)
-- key: predictedDepartureTime = DOUBLE (1.48889e+009)
-- key: serviceDate = STRING (20170307)
-- key: tripId = STRING (BKK_B608454921)
-- -- Arrival time: 2017-03-07 14:11
-- key: arrivalTime = DOUBLE (1.48889e+009)
-- key: departureTime = DOUBLE (1.48889e+009)
-- key: predictedArrivalTime = DOUBLE (1.48889e+009)
-- key: predictedDepartureTime = DOUBLE (1.48889e+009)
-- key: serviceDate = STRING (20170307)
-- key: tripId = STRING (BKK_B624151334)
-- -- Arrival time: 2017-03-07 14:12
-- key: arrivalTime = DOUBLE (1.48889e+009)
-- key: departureTime = DOUBLE (1.48889e+009)
-- key: predictedArrivalTime = DOUBLE (1.48889e+009)
-- key: predictedDepartureTime = DOUBLE (1.48889e+009)
-- key: serviceDate = STRING (20170307)
-- key: tripId = STRING (BKK_B624151338)
-- -- Arrival time: 2017-03-07 14:22
-- key: arrivalTime = DOUBLE (1.48889e+009)
-- key: departureTime = DOUBLE (1.48889e+009)
-- key: predictedArrivalTime = DOUBLE (1.48889e+009)
-- key: predictedDepartureTime = DOUBLE (1.48889e+009)
-- key: serviceDate = STRING (20170307)
-- key: tripId = STRING (BKK_B624151504)
-- -- Arrival time: 2017-03-07 14:33
*/
