private static List<User> users = new List<User>();

    static void CreateUsernameAndPassword()
        {
            Console.WriteLine("User- und Passworterstellung");

            string username = "";
            while (!IsValidInput(username))
            {
                Console.Write("Bitte gebe einen Benutzernamen ein (mindestens 4 Zeichen, nur lateinische Buchstaben): ");
                username = Console.ReadLine();
                if (!IsValidInput(username))
                {
                    Console.WriteLine("Ungültiger Benutzername. Versuche es nochmal.");
                }
            }

            string password = "";
            while (!IsValidInput(password))
            {
                Console.Write("Bitte gebe ein Passwort ein (mindestens 4 Zeichen, nur lateinische Buchstaben): ");
                password = Console.ReadLine();
                if (!IsValidInput(password))
                {
                    Console.WriteLine("Ungültiges Passwort. Versuche es nochmal.");
                }
            }

    static bool IsValidInput(string input)
        {
            if ((input.Length < 4) || (input.Length > 16))
            {
                return false;
            }
            return IsAlpha(input);
        }

        static bool IsAlpha(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }
            // Benutzer zur Liste hinzufügen
            users.Add(new User(username, password));

            Console.WriteLine("Benutzername und Passwort erfolgreich erstellt!");
            Console.WriteLine($"Benutzername: {username}");
            Console.WriteLine($"Passwort: {password}");
        }

        // User-Klasse zur Speicherung der Benutzerinformationen
    public class User
        {
            public string Username { get; set; }
            public string Password { get; set; }

            public User(string username, string password)
            {
                Username = username;
                Password = password;
            }
        }
