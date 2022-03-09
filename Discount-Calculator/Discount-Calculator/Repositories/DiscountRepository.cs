using Discount.Entities;
using Discount_Calculator.Utility;
using IMDB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        public IConfiguration Configuration { get; }
        private readonly DiscountContext _context;

        public DiscountRepository(IConfiguration configuration, DiscountContext context)
        {
            Configuration = configuration;
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public double Calculatetotalprice(int price,int weight,int? discount)
        {
            double Totalprice = 0;
            try
            {
                double priceweight = (price * weight);
                if (priceweight != 0)
                {
                    if (discount.HasValue)
                    {
                        double disCount = ((double)discount / 100);
                        double discountPrice = disCount * priceweight;
                        Totalprice = (priceweight - discountPrice);
                    }
                    else {
                        Totalprice = priceweight;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Totalprice;
        }
        public bool isAuthorized(string UserName) {
            bool isAuthorized = true;
            DateTime dateTime = DateTime.Now;
            DateTime createdtime = (DateTime)(_context.UserData.FirstOrDefault(a => a.UserName == UserName && a.isActive)?.CreatedTS);

            createdtime = createdtime.AddHours(1);
                if (dateTime > createdtime)
                    isAuthorized = false;

            return isAuthorized;
        }
        public HttpResponseMessage Login(string Username,string Password)
        {
            HttpResponseObject hro = new HttpResponseObject();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    UserData usrDat = new UserData();
                    int Policy = Convert.ToInt32(Configuration["Policy"]);
                    string MemoryName = "UserData";
                    if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
                    {
                        var UserExist = _context.UserData.FirstOrDefault(a => a.UserName == Username && a.isActive);
                        if (UserExist!=null)
                        {
                            UserExist.CreatedTS= DateTime.Now;
                        }
                        else
                        {
                            usrDat.UserName = Username;
                            usrDat.Password = Password;
                            usrDat.CreatedBy = Username;
                            usrDat.isActive = true;
                            usrDat.CreatedTS = DateTime.Now;
                            _context.UserData.Add(usrDat);
                        }
                        _context.SaveChanges();

                        var policy = new CacheItemPolicy().AbsoluteExpiration = DateTime.Now.AddHours(Policy);
                        CacheMemory.SetMemoryData(usrDat, MemoryName, policy);
                        hro.Message = "Logged in SucessFully";
                        hro.IsSuccess = true;
                    }
                    transaction.Commit();
                    return new HttpResponseMessage() { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(hro)) };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    hro.IsSuccess = false;
                    hro.Message = ex.Message + ex.InnerException + ex.StackTrace;
                    return new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest, Content = new StringContent(JsonConvert.SerializeObject(hro)) };
                }
            }
        }
    }
}
