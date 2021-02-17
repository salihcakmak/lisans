using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lisans.Data.Context;
using Lisans.Data.Models;
using Lisans.Data.RequestModels;
using Lisans.Data.ResponseModels;
using Lisans.DataAccess.UnitOfWork;
using Lisans.Utils.ObjectMapper;
using Microsoft.AspNetCore.Mvc;

namespace Lisans.Controllers
{
    [Route("LisansApi")]
    public class LisansServiceController : Controller
    {
        [HttpPost("LisansControl")]
        public IActionResult LisansControl([FromBody]LisansControlModel lisansControlModel)
        {
            bool result = false;
            using (var uow = new UnitOfWork<MasterContext>())
            {
                LisansModel lisans = uow.GetRepository<LisansModel>().Get(x => x.OnlineProductKey.Equals(lisansControlModel.OnlineProductKey));

                if (lisans != null)
                {
                    if (lisans.HardwareId.Equals(lisansControlModel.HardwareId))
                    {
                        if (LicenseTimeControl(lisans))
                        {
                            if (lisans.Status == true)
                            {
                                lisans.LastOnlineDate = DateTime.Now;
                                uow.GetRepository<LisansModel>().Update(lisans);
                                uow.SaveChanges();
                                result = true;
                            }
                            else
                            {
                                lisans.Status = true;
                                lisans.LastOnlineDate = DateTime.Now;
                                uow.GetRepository<LisansModel>().Update(lisans);
                                uow.SaveChanges();
                                result = true;
                            }
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
                if (result == true)
                {
                    OnlineResponseModel onlineResponseModel = new OnlineResponseModel();
                    ObjectMapper.Map(onlineResponseModel, lisans);

                    return Json(onlineResponseModel);
                }
                else
                {
                    return Json(result);
                }
            }
        }


        public bool LicenseTimeControl(LisansModel lisansModel)
        {
            if (DateTime.Now <= lisansModel.ExpirationDate)
                return true;
            else
                return false;
        }
    }
}