using Spectre.Console;
using SRS.Models;
namespace SRS
{
    internal class Program
    {
        private static Style DGreyStyle = new Style(foreground: Color.DarkSlateGray3, background: Color.Grey19);
        private static Controller Controller;
        static void Main(string[] args)
        {
            Controller = new Controller();
            while (true)
            {
                Console.Clear();
                var mainChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Main Menu - Choose an [green]option[/]:")
                        .PageSize(5)
                        .HighlightStyle(DGreyStyle)
                        .AddChoices(new[] { "Manage", "Settings", "Exit" }));
                switch (mainChoice)
                {
                    case "Manage":
                        Manage();
                        break;
                    case "Settings":
                        Settings();
                        break;
                    case "Exit":
                        AnsiConsole.MarkupLine("[red]Exiting...[/]");
                        return;
                }
            }
        }
        static void Settings()
        {
            while (true)
            {
                Console.Clear();
                var mainChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Settings Menu - Choose an [green]option[/]:")
                        .PageSize(5)
                        .HighlightStyle(DGreyStyle)
                        .AddChoices(new[] { "View all", "Go back" }));
                switch (mainChoice)
                {
                    case "View all":
                        AnsiConsole.AlternateScreen(() =>
                        {
                            Table table = new Table();
                            table.AddColumn(new TableColumn("Name/Type"));
                            table.AddColumn(new TableColumn("Age/Capacity"));
                            table.AddColumn(new TableColumn("Status"));
                            table.AddColumn(new TableColumn(""));
                            foreach (Trainer trainer in Controller.GetListOf<Trainer>())
                            {
                                table.AddRow(trainer.Name, trainer.Age.ToString());
                                foreach (TrainingSession session in trainer.Sessions)
                                {
                                    string color = session.Status.Equals(TrainingSession.StatusType.FREE) ? "[green]"
                                    : session.Status.Equals(TrainingSession.StatusType.FULL) ? "[red]"
                                    : session.Status.Equals(TrainingSession.StatusType.BUSY) ? "[indianred]"
                                    : "[darkolivegreen3_2]";
                                    table.AddRow(session.Type, session.Capacity.ToString(), $"{color}{session.Status.ToString()}[/]");
                                    foreach (Client client in session.Clients)
                                    {
                                        table.AddRow(client.Name, client.Age.ToString(), "[lightgoldenrod1]VIP: [/]" + client.isVIP.ToString());
                                    }
                                    if (session.VIPClient != null)
                                    {
                                        table.AddRow(session.VIPClient.Name, session.VIPClient.Age.ToString(), "[lightgoldenrod1]VIP: [/]" + session.VIPClient.isVIP.ToString());
                                    }
                                }
                                table.AddEmptyRow();
                            }
                            while (true)
                            {
                                Console.Clear();
                                AnsiConsole.Write(table);
                                var mainChoice = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                .Title("...")
                                .PageSize(5)
                                .HighlightStyle(DGreyStyle)
                                .AddChoices(new[] { "Go back" }));
                                switch (mainChoice)
                                {
                                    case "Go back":
                                        return;
                                }
                            }
                        });
                        break;
                    case "Go back":
                        return;
                }
            }
        }
        static void Manage()
        {
            while (true)
            {
                Console.Clear();
                var mainChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Manage Menu - Choose an [green]option[/]:")
                        .PageSize(5)
                        .HighlightStyle(DGreyStyle)
                        .AddChoices(new[] { "Trainers", "Training Sessions", "Clients", "Go back" }));
                switch(mainChoice)
                {
                    case "Trainers":
                        Trainers();
                        break;
                    case "Training Sessions":
                        TrainingSessions();
                        break;
                    case "Clients":
                        Clients();
                        break;
                    case "Go back":
                        return;
                }
            }
        }
        static void Trainers()
        {
            while (true)
            {
                Console.Clear();
                var mainChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Trainers Menu - Choose an [green]option[/]:")
                        .PageSize(5)
                        .HighlightStyle(DGreyStyle)
                        .AddChoices(new[] { "Add", "Edit", "Remove", "Go back" }));

                switch (mainChoice)
                {
                    case "Add":
                        AnsiConsole.AlternateScreen(() =>
                        {
                            var name = AnsiConsole.Ask<string>("Enter trainer's name [grey]default[/]:", "Unknown");
                            var age = AnsiConsole.Ask<int>("Enter trainer's age [grey]default[/]:", 0);

                            Controller.AddTrainer(name, age, null);

                            AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                            Console.ReadKey(true);
                        });
                        break;

                    case "Edit":
                        AnsiConsole.AlternateScreen(() =>
                        {
                            var trainers = Controller.GetListOf<Trainer>().ToList();
                            if (!trainers.Any())
                            {
                                AnsiConsole.MarkupLine("[red]No trainers available to edit![/]");
                                AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                                Console.ReadKey(true);
                                return;
                            }

                            var selectedTrainer = AnsiConsole.Prompt(
                                new SelectionPrompt<Trainer>()
                                    .Title("Select a trainer to edit:")
                                    .PageSize(5)
                                    .HighlightStyle(DGreyStyle)
                                    .UseConverter(t => $"{t.Name} (Age: {t.Age})")
                                    .AddChoices(trainers));

                            var newName = AnsiConsole.Ask<string>($"Enter new name for {selectedTrainer.Name} [grey]default[/]:", selectedTrainer.Name);
                            var newAge = AnsiConsole.Ask<int>($"Enter new age for {selectedTrainer.Name} [grey]default[/]:", selectedTrainer.Age);

                            Controller.UpdateTrainer(selectedTrainer, newName, newAge);

                            AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                            Console.ReadKey(true);
                        });
                        break;

                    case "Remove":
                        AnsiConsole.AlternateScreen(() =>
                        {
                            var trainers = Controller.GetListOf<Trainer>().ToList();
                            if (!trainers.Any())
                            {
                                AnsiConsole.MarkupLine("[red]No trainers available to remove![/]");
                                AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                                Console.ReadKey(true);
                                return;
                            }

                            var selectedTrainer = AnsiConsole.Prompt(
                                new SelectionPrompt<Trainer>()
                                    .Title("Select a trainer to remove:")
                                    .PageSize(5)
                                    .HighlightStyle(DGreyStyle)
                                    .UseConverter(t => $"{t.Name} (Age: {t.Age})")
                                    .AddChoices(trainers));

                            Controller.RemoveTrainer(selectedTrainer);

                            AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                            Console.ReadKey(true);
                        });
                        break;

                    case "Go back":
                        return;
                }
            }
        }
        static void TrainingSessions()
        {
            while (true)
            {
                Console.Clear();
                var mainChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Training Sessions Menu - Choose an [green]option[/]:")
                        .PageSize(5)
                        .HighlightStyle(DGreyStyle)
                        .AddChoices(new[] { "Add", "Edit", "Update", "Remove", "Go back" }));
                switch (mainChoice)
                {
                    case "Add":
                        AnsiConsole.AlternateScreen(() =>
                        {
                            var trainers = Controller.GetListOf<Trainer>().ToList();
                            if (!trainers.Any())
                            {
                                AnsiConsole.MarkupLine("[red]No trainers available to choose from![/]");
                                AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                                Console.ReadKey(true);
                                return;
                            }

                            var selectedTrainer = AnsiConsole.Prompt(
                                new SelectionPrompt<Trainer>()
                                    .Title("Select a trainer to add a session:")
                                    .PageSize(5)
                                    .HighlightStyle(DGreyStyle)
                                    .UseConverter(t => $"{t.Name} (Age: {t.Age})")
                                    .AddChoices(trainers));

                            var type = AnsiConsole.Ask<string>("Enter session's name [grey]default[/]:", "Unknown");
                            var capacity = AnsiConsole.Ask<int>("Enter sessions's capacity [grey]default[/]:", 0);

                            Controller.AddSession(type, capacity, selectedTrainer, null);
                            AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                            Console.ReadKey(true);
                        });
                        break;
                    case "Edit":
                        AnsiConsole.AlternateScreen(() =>
                        {
                            var trainers = Controller.GetListOf<Trainer>().ToList();
                            if (!trainers.Any())
                            {
                                AnsiConsole.MarkupLine("[red]No trainers available to choose from![/]");
                                AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                                Console.ReadKey(true);
                                return;
                            }

                            var selectedTrainer = AnsiConsole.Prompt(
                                new SelectionPrompt<Trainer>()
                                    .Title("Select a trainer to select a session from:")
                                    .PageSize(5)
                                    .HighlightStyle(DGreyStyle)
                                    .UseConverter(t => $"{t.Name} (Age: {t.Age})")
                                    .AddChoices(trainers));

                            var sessions = selectedTrainer.Sessions;
                            if (!sessions.Any())
                            {
                                AnsiConsole.MarkupLine("[red]No sessions available to choose from![/]");
                                AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                                Console.ReadKey(true);
                                return;
                            }

                            var selectedSession = AnsiConsole.Prompt(
                                new SelectionPrompt<TrainingSession>()
                                    .Title("Select a session to edit:")
                                    .PageSize(5)
                                    .HighlightStyle(DGreyStyle)
                                    .UseConverter(s => $"{s.Type} (Capacity: {s.Capacity}), Status: {s.Status}")
                                    .AddChoices(sessions));


                            var newType = AnsiConsole.Ask<string>($"Enter new type for {selectedSession.Type} [grey]default[/]:", selectedSession.Type);
                            var newCapacity = AnsiConsole.Ask<int>($"Enter new capacity for {selectedSession.Capacity} [grey]default[/]:", selectedSession.Capacity);

                            Controller.UpdateSession(selectedSession, newType, newCapacity);
                            AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                            Console.ReadKey(true);
                        });
                        break;
                    case "Update":
                        AnsiConsole.AlternateScreen(() =>
                        {
                            var trainers = Controller.GetListOf<Trainer>().ToList();
                            if (!trainers.Any())
                            {
                                AnsiConsole.MarkupLine("[red]No trainers available to choose from![/]");
                                AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                                Console.ReadKey(true);
                                return;
                            }

                            var selectedTrainer = AnsiConsole.Prompt(
                                new SelectionPrompt<Trainer>()
                                    .Title("Select a trainer to select a session from:")
                                    .PageSize(5)
                                    .HighlightStyle(DGreyStyle)
                                    .UseConverter(t => $"{t.Name} (Age: {t.Age})")
                                    .AddChoices(trainers));

                            var sessions = selectedTrainer.Sessions;
                            if (!sessions.Any())
                            {
                                AnsiConsole.MarkupLine("[red]No sessions available to choose from![/]");
                                AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                                Console.ReadKey(true);
                                return;
                            }

                            var selectedSession = AnsiConsole.Prompt(
                                new SelectionPrompt<TrainingSession>()
                                    .Title("Select a session to update status of:")
                                    .PageSize(5)
                                    .HighlightStyle(DGreyStyle)
                                    .UseConverter(s => $"{s.Type} (Capacity: {s.Capacity}), Status: {s.Status}")
                                    .AddChoices(sessions));

                            string previousColor =selectedSession.Status.Equals(TrainingSession.StatusType.FREE) ? "[green]"
                                    : selectedSession.Status.Equals(TrainingSession.StatusType.FULL) ? "[red]"
                                    : selectedSession.Status.Equals(TrainingSession.StatusType.BUSY) ? "[indianred]"
                                    : "[darkolivegreen3_2]";
                            TrainingSession.StatusType previousStatus = selectedSession.Status;
                            if (selectedSession.Status == TrainingSession.StatusType.FREE)
                            {
                                selectedSession.UpdateStatus(TrainingSession.StatusType.BUSY);
                            }
                            else if (selectedSession.Status == TrainingSession.StatusType.BUSY)
                            {
                                selectedSession.UpdateStatus(TrainingSession.StatusType.DONE);
                            }
                            else if(selectedSession.Status == TrainingSession.StatusType.DONE)
                            {
                                selectedSession.UpdateStatus(TrainingSession.StatusType.FREE);
                            }
                            string NextColor = selectedSession.Status.Equals(TrainingSession.StatusType.FREE) ? "[green]"
                                    : selectedSession.Status.Equals(TrainingSession.StatusType.FULL) ? "[red]"
                                    : selectedSession.Status.Equals(TrainingSession.StatusType.BUSY) ? "[indianred]"
                                    : "[darkolivegreen3_2]";

                            AnsiConsole.MarkupLine($"Updated status from {previousColor}{previousStatus}[/] to {NextColor}{selectedSession.Status}[/]");

                            if (selectedSession.Status == TrainingSession.StatusType.FREE)
                            {
                                AnsiConsole.MarkupLine($"Session {selectedSession.Type} [gold]has ended![/]");
                                foreach(Client client in selectedSession.Clients)
                                {
                                    Controller.RemoveClient(client, selectedSession);
                                }
                            }
                            AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                            Console.ReadKey(true);
                        });
                        break;
                    case "Remove":
                        AnsiConsole.AlternateScreen(() =>
                        {
                            var trainers = Controller.GetListOf<Trainer>().ToList();
                            if (!trainers.Any())
                            {
                                AnsiConsole.MarkupLine("[red]No trainers available to choose from![/]");
                                AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                                Console.ReadKey(true);
                                return;
                            }

                            var selectedTrainer = AnsiConsole.Prompt(
                                new SelectionPrompt<Trainer>()
                                    .Title("Select a trainer to select sessions from:")
                                    .PageSize(5)
                                    .HighlightStyle(DGreyStyle)
                                    .UseConverter(t => $"{t.Name} (Age: {t.Age})")
                                    .AddChoices(trainers));

                            var sessions = selectedTrainer.Sessions;
                            if (!sessions.Any())
                            {
                                AnsiConsole.MarkupLine("[red]No sessions available to choose from![/]");
                                AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                                Console.ReadKey(true);
                                return;
                            }

                            var selectedSession = AnsiConsole.Prompt(
                                new SelectionPrompt<TrainingSession>()
                                    .Title("Select a session to remove:")
                                    .PageSize(5)
                                    .HighlightStyle(DGreyStyle)
                                    .UseConverter(s => $"{s.Type} (Capacity: {s.Capacity}), Status: {s.Status}")
                                    .AddChoices(sessions));


                            Controller.RemoveSession(selectedSession, selectedTrainer);
                            AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                            Console.ReadKey(true);
                        });
                        break;
                    case "Go back":
                        return;
                }
            }
        }
        static void Clients()
        {
            while (true)
            {
                Console.Clear();
                var mainChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Clients Menu - Choose an [green]option[/]:")
                        .PageSize(5)
                        .HighlightStyle(DGreyStyle)
                        .HighlightStyle(DGreyStyle)
                        .AddChoices(new[] { "Add", "Edit", "Remove", "Go back" }));
                switch (mainChoice)
                {
                    case "Add":
                        AnsiConsole.AlternateScreen(() =>
                        {
                            var sessions = Controller.GetListOf<TrainingSession>().ToList();
                            if (!sessions.Any())
                            {
                                AnsiConsole.MarkupLine("[red]No sessions available to choose from![/]");
                                AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                                Console.ReadKey(true);
                                return;
                            }

                            var selectedSession = AnsiConsole.Prompt(
                                new SelectionPrompt<TrainingSession>()
                                    .Title("Select a session to add a client to:")
                                    .PageSize(5)
                                    .HighlightStyle(DGreyStyle)
                                    .UseConverter(s => $"{s.Type} (Capacity: {s.Capacity}), Status: {s.Status}")
                                    .AddChoices(sessions));

                            var name = AnsiConsole.Ask<string>("Enter client's name [grey]default[/]:", "Unknown");
                            var age = AnsiConsole.Ask<int>("Enter client's age [grey]default[/]:", 0);
                            var isvip = AnsiConsole.Ask<bool>("Enter if a client is a VIP [grey]default[/]:", false);

                            Controller.AddClient(name, age, isvip, selectedSession);

                            AnsiConsole.MarkupLine($"{selectedSession.Clients.Count}, {selectedSession.Capacity}");
                            AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                            Console.ReadKey(true);
                        });
                        break;
                    case "Edit":
                        AnsiConsole.AlternateScreen(() =>
                        {
                            var sessions = Controller.GetListOf<TrainingSession>().ToList();
                            if (!sessions.Any())
                            {
                                AnsiConsole.MarkupLine("[red]No sessions available to choose from![/]");
                                AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                                Console.ReadKey(true);
                                return;
                            }

                            var selectedSession = AnsiConsole.Prompt(
                                new SelectionPrompt<TrainingSession>()
                                    .Title("Select a session to add a client to:")
                                    .PageSize(5)
                                    .HighlightStyle(DGreyStyle)
                                    .UseConverter(s => $"{s.Type} (Capacity: {s.Capacity}), Status: {s.Status}")
                                    .AddChoices(sessions));

                            var clients = selectedSession.Clients;
                            if (!clients.Any())
                            {
                                AnsiConsole.MarkupLine("[red]No clients available to edit![/]");
                                AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                                Console.ReadKey(true);
                                return;
                            }

                            var selectedClient = AnsiConsole.Prompt(
                                new SelectionPrompt<Client>()
                                    .Title("Select a client to edit:")
                                    .PageSize(5)
                                    .HighlightStyle(DGreyStyle)
                                    .UseConverter(c => $"{c.Name} (Age: {c.Age}), IsVIP: {c.isVIP}")
                                    .AddChoices(clients));

                            var newName = AnsiConsole.Ask<string>($"Enter new name for {selectedClient.Name} [grey]default[/]:", selectedClient.Name);
                            var newAge = AnsiConsole.Ask<int>($"Enter new age for {selectedClient.Name} [grey]default[/]:", selectedClient.Age);
                            var newIsvip = AnsiConsole.Ask<bool>($"Enter new VIP status for {newName}[grey]default[/]:", false);

                            Controller.UpdateClient(selectedClient, newName, newAge, newIsvip, selectedSession);

                            AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                            Console.ReadKey(true);
                        });
                        break;
                    case "Remove":
                        AnsiConsole.AlternateScreen(() =>
                        {
                            var sessions = Controller.GetListOf<TrainingSession>().ToList();
                            if (!sessions.Any())
                            {
                                AnsiConsole.MarkupLine("[red]No sessions available to choose from![/]");
                                AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                                Console.ReadKey(true);
                                return;
                            }

                            var selectedSession = AnsiConsole.Prompt(
                                new SelectionPrompt<TrainingSession>()
                                    .Title("Select a session to select a client from:")
                                    .PageSize(5)
                                    .HighlightStyle(DGreyStyle)
                                    .UseConverter(s => $"{s.Type} (Capacity: {s.Capacity}), Status: {s.Status}")
                                    .AddChoices(sessions));

                            var clients = selectedSession.Clients;
                            if (!clients.Any())
                            {
                                AnsiConsole.MarkupLine("[red]No clients available to edit![/]");
                                AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                                Console.ReadKey(true);
                                return;
                            }

                            var selectedClient = AnsiConsole.Prompt(
                                new SelectionPrompt<Client>()
                                    .Title("Select a client to remove:")
                                    .PageSize(5)
                                    .HighlightStyle(DGreyStyle)
                                    .UseConverter(c => $"{c.Name} (Age: {c.Age}), IsVIP: {c.isVIP}")
                                    .AddChoices(clients));

                            Controller.RemoveClient(selectedClient, selectedSession);

                            AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                            Console.ReadKey(true);
                        });
                        break;
                    case "Go back":
                        return;
                }
            }
        }
    }
}