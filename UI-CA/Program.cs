// See https://aka.ms/new-console-template for more information

using BL;
using DAL;
using UI.CA;

IRepository repository = new InMemoryRepository();
IManager manager = new Manager(repository);
InMemoryRepository.Seed();

ConsoleUi consoleUi = new ConsoleUi(manager);
consoleUi.Run();