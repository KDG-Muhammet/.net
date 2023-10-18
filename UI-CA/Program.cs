// See https://aka.ms/new-console-template for more information

using StoreManagement.BL;
using StoreManagement.DAL;
using StoreManagement.UI.CA;

IRepository repository = new InMemoryRepository();
IManager manager = new Manager(repository);
InMemoryRepository.Seed();

ConsoleUi consoleUi = new ConsoleUi(manager);
consoleUi.Run();