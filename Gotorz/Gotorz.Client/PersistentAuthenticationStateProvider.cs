using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
//Denne klasse PersistentAuthenticationStateProvider har til opgave
//at levere information om brugerens autentificeringstilstand (Er brugeren logget ind eller ej – og hvem er de, hvis de er logget ind) i en Blazor-applikation.
//Den forsøger at hente brugeroplysninger (UserInfo) fra PersistentComponentState, og hvis de findes,
//opretter den en autentificeret bruger med relevante claims. Den bruges af Blazor til at bestemme,
//om brugeren er logget ind, og hvad deres identitet er.
//To hovedtilstande findes. Ikke-autentificeret (anonym):Brugeren er ikke logget ind, så systemet ved ikke, hvem de er
//Autentificeret (logget ind):Brugeren er logget ind, og systemet kender deres identitet og kan give adgang til beskyttede ressourcer og funktioner.
//Brugeren er logget ind, og vi ved:Hvem de er (f.eks. deres ID og email).Hvilke roller eller tilladelser de har (via claims)

//Det er vigtigt, fordi Blazor bruger AuthenticationStateProvider til at forstå, hvem brugeren er,
//og hvad de må. Uden den fungerer login, rollebaseret adgang og beskyttede sider ikke korrekt.
//Gør det muligt at identificere brugeren via claims (f.eks. bruger-ID og email).
//Uden dette ved appen ikke, om brugeren er logget ind – og hvem de er.
//AuthenticationStateProvider giver data til [Authorize]-attributter, <AuthorizeView>, m.m.
//Hvis brugeren ikke er registreret korrekt her, får de ikke adgang til beskyttede sider eller funktioner.
//Blazor Server og Blazor WebAssembly glemmer normalt logininfo ved reload eller navigation.
//Ved at bruge PersistentComponentState, gemmes brugerens loginstatus, selv efter en opdatering eller genindlæsning.
//Uden denne klasse ved Blazor ikke, at brugeren er logget ind, og alt sikkerheds- og adgangsstyring vil fejle –
//brugeren vil blive behandlet som anonym hver gang.
//Det er altså et fundament i login-håndteringen i en Blazor-app, især når man ikke bruger indbygget Identity direkte.
//Det bruges i Blazor til at vise/skjule indhold og beskytte sider eller funktioner.
namespace Gotorz.Client
{
    // This is a client-side AuthenticationStateProvider that determines the user's authentication state by
    // looking for data persisted in the page when it was rendered on the server. This authentication state will
    // be fixed for the lifetime of the WebAssembly application. So, if the user needs to log in or out, a full
    // page reload is required.
    //
    // This only provides a user name and email for display purposes. It does not actually include any tokens
    // that authenticate to the server when making subsequent requests. That works separately using a
    // cookie that will be included on HttpClient requests to the server.



    // Definerer en intern klasse, der arver fra Blazors AuthenticationStateProvider.
    // Den bruges til at fortælle Blazor, hvem brugeren er (autentificeringstilstand).
    internal class PersistentAuthenticationStateProvider : AuthenticationStateProvider
    {
        // En standard "ikke-logget-ind"-tilstand (en anonym ClaimsPrincipal uden identitet).
        private static readonly Task<AuthenticationState> defaultUnauthenticatedTask =
            Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

        // Felt der gemmer den aktuelle autentificeringstilstand.
        // Starter som "ikke-autentificeret".
        private readonly Task<AuthenticationState> authenticationStateTask = defaultUnauthenticatedTask;

        // Konstruktør der modtager komponentens tilstand (PersistentComponentState),
        // som indeholder data gemt under sidenavigation (f.eks. login-information).
        //PersistentComponentState bruges i Blazor Server til at gemme data imellem navigeringer uden at bruge cookies eller local storage.
        public PersistentAuthenticationStateProvider(PersistentComponentState state)
        {
            // Prøver at hente UserInfo-objektet fra tilstanden som JSON.
            // Hvis det ikke findes eller er null, forlades konstruktøren, og brugeren forbliver anonym.
            //Ved at bruge TryTakeFromJson, henter vi gemte brugeroplysninger, som fx er sat efter login.
            if (!state.TryTakeFromJson<UserInfo>(nameof(UserInfo), out var userInfo) || userInfo is null)
            {
                return;
            }

            // Opretter en liste af claims (oplysninger om brugeren), f.eks. ID og e-mail.
            Claim[] claims = [
                new Claim(ClaimTypes.NameIdentifier, userInfo.UserId), // Unikt bruger-ID
            new Claim(ClaimTypes.Name, userInfo.Email),            // Brugernavn (her sat som e-mail)
            new Claim(ClaimTypes.Email, userInfo.Email)            // E-mail
            ];

            // Skaber en ny autentificeret AuthenticationState baseret på claims.
            // Brugeren får en identitet, og den bruges til at overskrive standardtilstanden.
            //ClaimsPrincipal og ClaimsIdentity er .NET’s måde at repræsentere en bruger og deres oplysninger (claims).
            authenticationStateTask = Task.FromResult(
                new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims,
                    authenticationType: nameof(PersistentAuthenticationStateProvider)))));
        }

        // Overstyrer metoden som Blazor kalder for at få autentificeringstilstanden.
        // Returnerer den aktuelle tilstand (enten anonym eller autentificeret).
        public override Task<AuthenticationState> GetAuthenticationStateAsync() => authenticationStateTask;
    }


}
