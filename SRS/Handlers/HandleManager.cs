using Spectre.Console;
using SRS.CustomEventArgs;

namespace SRS.Handlers
{
    public class HandleManager
    {
        //i aint doing this in seperate files >:|
        public HandleManager() { }
        public void OnSessionFullHandler(object? sender, SessionFullEventArgs e)
        {
            AnsiConsole.MarkupLine($"Session {e.Session.Type}, with capacity: {e.Session.Capacity} [red]is full.[/]");
        }

        public void OnClientRegisteredHandler(object? sender, ClientEventArgs e)
        {
            AnsiConsole.MarkupLine($"Client {e.Client.Name}, age: {e.Client.Age} VIP: {e.Client.isVIP}[green]has been registered into session[/] {e.Session.Type}");
        }
        public void OnTrainerRegisteredHandler(object? sender, TrainerEventArgs e)
        {
            AnsiConsole.MarkupLine($"Trainer {e.Trainer.Name}, age: {e.Trainer.Age} [green]has been registered into the system[/]");
        }
        public void OnSessionRegisteredHandler(object? sender, SessionEventArgs e)
        {
            AnsiConsole.MarkupLine($"Session {e.Session.Type}, with capacity: {e.Session.Capacity}, status: {e.Session.Status} [green]has been registered into the system[/]");
        }

        public void OnClientRemovedHandler(object? sender, ClientEventArgs e)
        {
            AnsiConsole.MarkupLine($"Client {e.Client.Name}, age {e.Client.Age} [red]was removed from the system[/]");
        }
        public void OnTrainerRemovedHandler(object? sender, TrainerEventArgs e)
        {
            AnsiConsole.MarkupLine($"Trainer {e.Trainer.Name}, age: {e.Trainer.Age} [red]was removed from the system[/]");
        }
        public void OnSessionRemovedHandler(object? sender, SessionEventArgs e)
        {
            AnsiConsole.MarkupLine($"Session {e.Session.Type}, with capacity: {e.Session.Capacity}, status: {e.Session.Status} [red]was removed from the system[/]");
        }


        public void OnClientUpdated(object? sender, ClientEventArgs e)
        {
            AnsiConsole.MarkupLine($"Previous Client {e.previousClient.Name}, age {e.previousClient.Age} [yellow]was updated to[/] Client {e.Client.Name}, age {e.Client.Age} ");
        }
        public void OnTrainerUpdated(object? sender, TrainerEventArgs e)
        {
            AnsiConsole.MarkupLine($"Previous Trainer {e.previousTrainer.Name}, age {e.previousTrainer.Age} [yellow]was updated to[/] Trainer {e.Trainer.Name}, age {e.Trainer.Age} ");
        }
        public void OnSessionUpdated (object? sender, SessionEventArgs e)
        {
            AnsiConsole.MarkupLine($"Previous Session {e.previousSession.Type}, with capacity {e.previousSession.Capacity} [yellow]was updated to[/] Session {e.Session.Type} with capacity {e.Session.Capacity}");
        }
    }
}
