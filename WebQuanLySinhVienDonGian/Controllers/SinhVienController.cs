using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebQuanLySinhVienDonGian.Models;
using System.Text.Json;
using System.Text;

namespace WebQuanLySinhVienDonGian.Controllers
{
    public class SinhVienController : Controller
    {
        string urlApi = "http://localhost:8080/";

        public async Task<IActionResult> Index()
        {
            List<SinhVien> sinhViens = new List<SinhVien>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(urlApi+"sinhvien"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    sinhViens = JsonSerializer.Deserialize<List<SinhVien>>(apiResponse);
                }
            }
            return View(sinhViens);
        }

        [HttpGet]
        public IActionResult ThemSinhVien()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ThemSinhVien(SinhVien sv)
        {
            string requestBody = JsonSerializer.Serialize(sv);
            HttpContent body = new StringContent(requestBody, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync(urlApi + "sinhvien", body))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["ThongBao"] = "alert('Thêm thành công')";
                        return RedirectToAction("Index");
                    }
                }    
            }
            TempData["ThongBao"] = "alert('Thêm thất bại')";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CapNhatSinhVien(int id)
        {
            SinhVien sinhVien;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(urlApi + "sinhvien/detail?id="+id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    sinhVien = JsonSerializer.Deserialize<SinhVien>(apiResponse);
                }
            }
            return View(sinhVien);
        }

        [HttpPost]
        public async Task<IActionResult> CapNhatSinhVien(SinhVien sv)
        {
            string requestBody = JsonSerializer.Serialize(sv);
            HttpContent body = new StringContent(requestBody, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync(urlApi + "sinhvien", body))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["ThongBao"] = "alert('Cập nhật thành công')";
                        return RedirectToAction("Index");
                    }
                }
            }
            TempData["ThongBao"] = "alert('Cập nhật thất bại')";
            return View();
        }

        public async Task<IActionResult> XoaSinhVien(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(urlApi + "sinhvien?id="+id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["ThongBao"] = "alert('Xoá thành công')";
                    }
                    else
                    {
                        TempData["ThongBao"] = "alert('Xoá thất bại')";
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}
