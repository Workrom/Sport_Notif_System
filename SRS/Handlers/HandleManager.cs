using Spectre.Console;
using SRS.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void OnRegisteredClientHandler(object? sender, ClientEventArgs e)
        {
            AnsiConsole.MarkupLine($"Client {e.Client.Name}, age: {e.Client.Age} [green]has been registered into session[/] {e.Session.Type}");
        }
        public void OnRegisteredTrainerHandler(object? sender, TrainerEventArgs e)
        {
            AnsiConsole.MarkupLine($"Trainer {e.Trainer.Name}, age: {e.Trainer.Age} [green]has been registered into the system[/]");
        }
        public void OnRegisteredSessionHandler(object? sender, SessionEventArgs e)
        {
            AnsiConsole.MarkupLine($"Session {e.Session.Type}, with capacity: {e.Session.Capacity}, status: {e.Session.Status}. To Trainer: {e.Trainer.Name}, age: {e.Trainer.Age} [green]has been registered into the system[/]");
        }
    }
}
