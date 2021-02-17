using Lisans.Data.Context;
using Lisans.Data.Models;
using Lisans.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lisans.Controllers
{
    public class UserController :Controller
    {
        //Kullanıcı Kayıt Ekranını Açan Methot
        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }


        //Create Sayfasında Kullanıcı Bilgilerini DataBase'e kaydeden Methot
        [HttpPost]
        public IActionResult RegisterUser(RegisterViewModel model)
        {
            model.Password = Md5Helper.Instance.GetMd5Hash(model.Password);
            using (MasterContext context = new MasterContext())
            {
                context.UserModels.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("Login");
        }




        //Login Sayfasını Açan Methot
        public IActionResult Login()
        {
            return View();
        }


        //Login butonuna bastıktan sonra girilen Kullanıcı Bilgilerini Karşılaştıran methot
        public IActionResult LoginControl(string UserName, string Password)
        {
            var md5password = Md5Helper.Instance.GetMd5Hash(Password);
            using (MasterContext context = new MasterContext())
            {
                var degisken = context.UserModels.Where(x => x.UserName == UserName && x.Password == md5password).FirstOrDefault();
                if (degisken == null)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    if (degisken.isSuperAdmin == true)
                    {
                        return RedirectToAction("UserDetails");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Lisans");
                    }
                }

            }
        }

        //Kullanıcı Girişi Doğrulandıktan sonra Kullanıcı listesini getiren metohot
        public IActionResult UserDetails()
        {
            using (MasterContext context = new MasterContext())
            {
                var userlistesi = context.UserModels.ToList();
                return View(userlistesi);
            }

        }



        //Kullanıcı Tabolusunda Kullanıcıyı Silen Methot
        public IActionResult Delete(int id)
        {
            using (MasterContext context = new MasterContext())
            {
                context.UserModels.Remove(context.UserModels.Find(id));
                context.SaveChanges();

                return RedirectToAction("UserDetails");
            }
        }

        public IActionResult Update(UserModel model)
        {
            using (MasterContext context = new MasterContext())
            {
                context.UserModels.Update(model);
                context.SaveChanges();
            }
            return RedirectToAction("UserDetails");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            using (MasterContext context = new MasterContext())
            {
                var kullanici = context.UserModels.Where(x => x.Id == id).FirstOrDefault();
                return View(kullanici);
            }
        }

    }
}

   



