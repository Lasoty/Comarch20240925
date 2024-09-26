namespace ComarchTestExplorer.Data.Repositories;

public interface ICompanyRepository
{
    string GetCompany();
}

public class CompanyRepository : ICompanyRepository
{
    string company = "Comarch Szkolenia";

    public CompanyRepository()
    {
    }

    public string GetCompany()
    {
        return company;
    }
}
