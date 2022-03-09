using IMDB.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public interface IDiscountRepository
    {
        double Calculatetotalprice(int price, int weight, int? discount);
        bool isAuthorized(string UserName);
        HttpResponseMessage Login(string Username, string Password);
    }
}
