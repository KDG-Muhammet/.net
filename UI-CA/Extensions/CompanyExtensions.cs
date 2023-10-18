using StoreManagement.BL.Domain;

namespace StoreManagement.UI.CA.Extensions;

public static class CompanyExtensions
{
    public static string GetInfo(this Company company)
    {
        return String.Format("Name Company: {0,-20} address: {1,-20} YearFounded: {2}", company.Name, company.Address, company.YearFounded);

    }
}