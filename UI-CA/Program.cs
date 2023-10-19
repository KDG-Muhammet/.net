// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using StoreManagement.BL;
using StoreManagement.DAL;
using StoreManagement.DAL.EF;
using StoreManagement.UI.CA;


DbContextOptionsBuilder dbOptionsBuilder = new DbContextOptionsBuilder<GameDbContext>();
dbOptionsBuilder.UseSqlite("Data Source=AppDatabase.db.sqlite");
GameDbContext ctx = new GameDbContext(dbOptionsBuilder.Options);

IRepository repository = new InMemoryRepository();
IManager manager = new Manager(repository);
InMemoryRepository.Seed();

ConsoleUi consoleUi = new ConsoleUi(manager);
consoleUi.Run();