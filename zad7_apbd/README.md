**EntityFramework - Scaffolding z istniejącej bazy danych**

Po zainstalowaniu możemy zrobić niniejszą komendą wygenerowanie automatyczne klas, jak w wykładzie nr 7:

`dotnet ef dbcontext scaffold "Data Source=db-mssql16.pjwstk.edu.pl;Initial Catalog=s22086;Integrated Security=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -o Repo -c DatabaseContext`

Parametr `-c` określa nazwę klasy, jest to opcjonalne 

Parametr `-o` w wykładzie był jako -Output, ale w tej wersji inaczej dokumentacja podaje. Parametr `-o` oznacza do jakiego folderu wrzucone zostaną wygenerowane pliki, czyli np. Repo, Repositories, itd.

**BARDZO WAŻNE**: aby wybrać konkretne tabele, np. X, Y, Z, to na końcu tej komendy powyżej trzeba dopisać taki fragment: 

`-t X -t Y -t Z`

Spowoduje to zmapowanie tylko tych tabel.

ConnectionString kopiujemy taki jak używaliśmy wcześniej z jednym zastrzeżeniem -> Należy dopisać do niego na końcu `TrustServerCertificate=true` aby nie było problemu z odpaleniem tej komendy, klikamy enter i powinno wygenerować nam klasy odpowiednie tak jak w przykładzie z wykładu