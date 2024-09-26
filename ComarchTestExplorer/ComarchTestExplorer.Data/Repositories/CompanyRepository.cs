namespace ComarchTestExplorer.Data.Repositories;

public class CompanyRepository
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
