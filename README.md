# Gotorz2

## Systembeskrivelse 

### Formål:
Systemet er en brugervenlig, fleksibel og sikker webapplikation til håndtering af rejsepakker. Administratorer kan søge fly og overnatning via eksterne API’er og oprette pakkerejser, som lagres i en Azure SQL-database. Kunder kan efterfølgende booke disse rejser via et brugervenligt interface. Applikationen er bygget i Blazor og bruger SQL Server Management Studio til datastyring. Applikationen er deployed på https://gotorz-team8.azurewebsites.net/.  

### Overordnet arkitektur:
Systemet er opbygget som et modulært, serviceorienteret websystem med en klar opdeling mellem frontend (ViewModels), backend (Controllers, Services og Models), interne API’er og eksterne integrationer. 

### Komponenter og lag: 

1. Frontend (Views) 
Frontendlaget består af en række Views, som håndterer præsentationslogik for forskellige brugerroller: _FlightSearch, AccommodationSearch, MainCustomerPage_
Disse Views modtager og strukturerer data fra backend og sørger for at præsentere det korrekt i brugergrænsefladen. 

3. Backend (Controllers & Services@Repository) 
Backend er opdelt i:
 _Controllers_, som håndterer HTTP-requests og validerer input.
_Services_, som indeholder forretningslogik, og som kalder interne og eksterne API’er.
_Repository_, som håndtere adgangen til og styringen af data datakilden (her database). 
Eksempler: 
TravelPackageService kommunikere med backend-API'et angående rejsepakker. 

3. Internal API 
Et internt API-lag danner bro mellem kontrollerne og ekstern  api og database. 

4. Database 
Systemet benytter en database til at gemme information om brugere, rejsepakker, bookinger. 

5. Ekstern API (Amadeus) 
Systemet integrerer med det eksterne Amadeus API til fly- og hoteldata.  

 

## Opsætning: 

1. Clone dette repository (git clone https://github.com/UCL-Team-8-Eksamen/Gotorz2.git). 
2. Clone repositoriet til API'en (git clone https://github.com/Husein100/TravelDataInternalAPI.git) 
3. Kør API-projektet (dotnet run) 
4. Kør Gotorz-projektet (dotnet run) 
