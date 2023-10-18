using StoreManagement.BL.Domain;

namespace StoreManagement.UI.CA.Extensions;

public static class GameExtensions
{
    
    public static string GetInfo(this Game game)
    {
        return String.Format("Name: {0,-25} | Price: {1,-5:n2} $ | Genre: {2}", game.Name, game.Price, game.Genre);

    }
    
    
}