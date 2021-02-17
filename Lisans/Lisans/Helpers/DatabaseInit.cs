using Lisans.Data.Context;
using Lisans.Data.Models;
using Lisans.DataAccess.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lisans.Helpers
{
    public class DatabaseInit : IDisposable
    {
        public DatabaseInit()
        {

            using (var uow = new UnitOfWork<MasterContext>())
            {
                if (uow.GetRepository<ProvinceModel>().Count() == 0)
                {
                    List<ProvinceModel> provinces = JsonConvert.DeserializeObject<List<ProvinceModel>>(System.IO.File.ReadAllText($@"{AppDomain.CurrentDomain.BaseDirectory}Province.json"));
                    List<DistrictModel> districts = JsonConvert.DeserializeObject<List<DistrictModel>>(System.IO.File.ReadAllText($@"{AppDomain.CurrentDomain.BaseDirectory}District.json"));

                    foreach (var item in provinces)
                    {
                        uow.GetRepository<ProvinceModel>().Add(item);
                    }

                    foreach (var item in districts)
                    {
                        uow.GetRepository<DistrictModel>().Add(item);
                    }
                    uow.SaveChanges();
                }
            }
        }

        public void Dispose()
        {

        }
    }
}
