using StoreManagement.BL.Domain;

namespace StoreManagement.UI.CA.Extensions;

public static class StoreExtensions
{
    public static string GetInfo(this Store store)
    {
        return String.Format("Name Store: {0,-20} address: {1,-20} OpeningHour: {2}:00", store.Name, store.Address, store.OpeningHour);

    }
}