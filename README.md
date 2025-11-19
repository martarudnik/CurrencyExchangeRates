# **Currency Exchange Rates – Recruitment Task**


Napisać aplikację, która: 
1. Pobiera cyklicznie kursy walut z tabeli B udostępnionej przez NBP i zapisuje je w bazie danych. 
2. Wyświetla kursy walut. 
3. Aplikacja pobierająca i zwracająca kursy walut powinna być napisana w .NET 8 lub wyższym oraz korzystać z Entity Framework do komunikacji z bazą danych.
4. Aplikacja wyświetlająca kursy walut powinna być odrębną aplikacją napisaną JavaScript/TypeScript (preferowany React).
---

# **Co zostało zrealizowane**

## **1. Pobieranie kursów z tabeli B (NBP)**  
Z dokumentacji NBP wynika, że tabela B jest publikowana raz w tygodniu — w środę, zwykle między 11:45 a 12:15.  
Jeśli środa wypada w święto, publikacja pojawia się dzień roboczy wcześniej.

W projekcie zaimplementowano rozwiązanie, w którym:

- aplikacja uruchamia synchronizację **raz dziennie o 12:30**,  
- jeśli pojawi się **nowa tabela**, jest zapisywana w bazie,  
- baza przechowuje **pełną historię** poprzednich publikacji,  
- jeśli dane nie zmieniły się od ostatniej publikacji — **nowy wpis nie jest dodawany**.

Dzięki temu:

- baza zawiera kompletny zestaw historycznych danych,  
- unikane są duplikaty,  
- harmonogram działa poprawnie nawet przy przesunięciach publikacji po stronie NBP.

---

## **2. Wyświetlanie kursów (aplikacja React)**  
Aplikacja frontendowa:

- prezentuje **aktualne kursy** z najnowszej zapisanej tabeli,  
- każda waluta jest wyświetlana jako **oddzielny kafelek**,  
- po kliknięciu w wybraną walutę wyświetlana jest **historia jej kursu**,  
- dane historyczne pobierane są z wcześniejszych zapisanych tabel,  
- użytkownik może podejrzeć, jak dana waluta zmieniała się w czasie.
  
  ## **Screeny:**  
<img width="1457" height="1061" alt="image" src="https://github.com/user-attachments/assets/e30f0c4a-b00a-49f4-9469-1f1d14328731" />

<img width="1057" height="363" alt="image" src="https://github.com/user-attachments/assets/cdcdf477-d95f-486c-aa7a-ac1460c5d9de" />


# **Technologie**
- .NET 8  
- ASP.NET HostedService (scheduler)  
- Entity Framework Core  
- React  
- Lekka  architektura
- xUnit + Moq 
---

# **Future Improvements**
1) zastąpienie prostego retry → Polly
2) dodatkowe testy
3) uszczelnienie aplikacji (walidacja + odporność produkcyjna)
4) pełny pipeline CI/CD 
5) biznesowe możliwości rozbudowy:
   
   a) dodanie wykresów zmian kursów dla wybranej waluty,
   
   b) pobieranie i wyświetlanie archiwalnych tabel z dowolnego zakresu dat,
   
