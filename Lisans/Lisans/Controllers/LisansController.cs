using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Lisans.Data.Context;
using Lisans.Data.Dto;
using Lisans.Data.Models;
using Lisans.DataAccess.UnitOfWork;
using Lisans.Utils.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lisans.Controllers
{
    public class LisansController : Controller
    {
        /// <summary>
        /// Tüm lisansların listesi
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            using (var uow = new UnitOfWork<MasterContext>())
            {
                List<LisansIndexDto> lisansIndexDtos = new List<LisansIndexDto>();
                var lisansList = uow.GetRepository<LisansModel>().GetAll().ToList();
                List<LisansModel> okulListesi = new List<LisansModel>();

                foreach (var item in lisansList)
                {
                    if (!okulListesi.Exists(x => x.SchoolCode == item.SchoolCode))
                        okulListesi.Add(item);
                }

                foreach (var okul in okulListesi)
                {
                    LisansIndexDto lisansIndexDto = new LisansIndexDto();

                    lisansIndexDto.LisansList = lisansList.FindAll(x => x.SchoolCode == okul.SchoolCode).ToList();
                    lisansIndexDto.SchoolName = okul.SchoolName;
                    lisansIndexDtos.Add(lisansIndexDto);
                }

                return View(lisansIndexDtos);
            }

        }

        /// <summary>
        /// Tüm lisansların listesi
        /// </summary>
        /// <returns></returns>
        public IActionResult Index1()
        {
            using (var uow = new UnitOfWork<MasterContext>())
            {
                List<LisansIndexDto> lisansIndexDtos = new List<LisansIndexDto>();
                var lisansList = uow.GetRepository<LisansModel>().GetAll().ToList();
                List<LisansModel> okulListesi = new List<LisansModel>();

                foreach (var item in lisansList)
                {
                    if (!okulListesi.Exists(x => x.SchoolCode == item.SchoolCode))
                        okulListesi.Add(item);
                }

                foreach (var okul in okulListesi)
                {
                    LisansIndexDto lisansIndexDto = new LisansIndexDto();

                    lisansIndexDto.LisansList = lisansList.FindAll(x => x.SchoolCode == okul.SchoolCode).ToList();
                    lisansIndexDto.SchoolName = okul.SchoolName;
                    lisansIndexDtos.Add(lisansIndexDto);
                }

                return View(lisansIndexDtos);
            }

        }


        /// <summary>
        /// Lisans oluşturma ekranı
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddLisans()
        {
            using (UnitOfWork<MasterContext> uow = new UnitOfWork<MasterContext>())
            {
                ViewBag.Province = uow.GetRepository<ProvinceModel>().GetAll().ToList();
            }
            LisansModel lisansModel = new LisansModel();
            lisansModel.OnlineProductKey = Guid.NewGuid().ToString().ToUpper();
            lisansModel.CreationDate = DateTime.Now;
            lisansModel.ExpirationDate = DateTime.Now.AddYears(1);
            return View(lisansModel);
        }

        /// <summary>
        /// Lisans kayıt servisi
        /// </summary>
        /// <param name="lisansModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddLisans(LisansModel lisansModel)
        {
            using (var uow = new UnitOfWork<MasterContext>())
            {
                lisansModel.HardwareId = lisansModel.HardwareId.ToUpper();
                lisansModel.OfflineProductKey = LisansHelper.Instance.GetOfflineProductKey(lisansModel.HardwareId);

                uow.GetRepository<LisansModel>().Add(lisansModel);
                uow.SaveChanges();

                MailHelper.Instance.SendMailLicenseInfoAsync(lisansModel);




                return RedirectToAction("Index", "Lisans");

            }

        }

        /// <summary>
        /// Lisans silme servisi
        /// </summary>
        /// <param name="lisansId"></param>
        /// <returns></returns>
        public IActionResult DeleteLisans(int id)
        {
            using (var uow = new UnitOfWork<MasterContext>())
            {
                var model = uow.GetRepository<LisansModel>().Get(x => x.LisansId == id);
                uow.GetRepository<LisansModel>().Delete(model);
                uow.SaveChanges();
                return RedirectToAction("Index", "Lisans");
            }

        }


        /// <summary>
        /// Lisans güncelleme ekranı
        /// </summary>
        /// <param name="lisansId">Güncellenecek lisans nesnesi id'si</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult UpdateLisans(int id)
        {
            using (var uow = new UnitOfWork<MasterContext>())
            {
                var model = uow.GetRepository<LisansModel>().Get(x => x.LisansId == id);

                ViewBag.Province = uow.GetRepository<ProvinceModel>().GetAll().ToList();
                ViewBag.District = uow.GetRepository<DistrictModel>().GetAll(x => x.ProviceId == model.Province).ToList();


                return View(model);
            }

        }


        /// <summary>
        /// Lisans güncelleme servisi
        /// </summary>
        /// <param name="lisansModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdateLisans(LisansModel lisansModel)
        {
            using (var uow = new UnitOfWork<MasterContext>())
            {
                uow.GetRepository<LisansModel>().Update(lisansModel);
                uow.SaveChanges();
                return RedirectToAction("Index", "Lisans");
            }


        }


        public JsonResult GetDistrictList(int pid)
        {
            using (var uow = new UnitOfWork<MasterContext>())
            {
                var districts = uow.GetRepository<DistrictModel>().GetAll(x => x.ProviceId == pid).ToList();
                return Json(new SelectList(districts, "Id", "Name"));
            }
        }

    }
}