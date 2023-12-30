// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using StoreManagement.BL;
using StoreManagement.DAL;
using StoreManagement.DAL.EF;
using StoreManagement.UI.CA;


DbContextOptionsBuilder dbOptionsBuilder = new DbContextOptionsBuilder<GameDbContext>();
dbOptionsBuilder.UseSqlite("Data Source=../../../../AppDatabase.db.sqlite");
GameDbContext ctx = new GameDbContext(dbOptionsBuilder.Options);
IRepository repo = new Repository(ctx);
IManager manager = new Manager(repo);

if (ctx.CreateDatabase(dataBase: true))
{
    DataSeeder.Seed(ctx);
}

ConsoleUi consoleUi = new ConsoleUi(manager);
consoleUi.Run();