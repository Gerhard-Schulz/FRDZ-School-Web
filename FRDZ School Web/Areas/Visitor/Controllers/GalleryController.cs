﻿using FRDZSchool.DataAccess.Data;
using FRDZSchool.Models.DatabaseModels;
using FRDZSchool.Models.ViewModels.EditModels;
using FRDZSchool.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SchoolWeb.Controllers
{
    public class GalleryController : Controller
    {
        private ApplicationContext _db;
        private IWebHostEnvironment _environment;

        public GalleryController(ApplicationContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        public IActionResult Index(int id = 0) // page (Id для красоты)
        {
            int allCount = _db.Photo.Count();
            if (allCount == 0)
            {
                return View(null);
            }
            if (id * 30 >= allCount)
            {
                id = allCount / 30;
                return RedirectToAction("Index", new { id = id });
            }
            ViewData["total"] = allCount / 30;
            return View(_db.Photo.Skip(id * 30).Take(30));
        }

        public IActionResult Photo(int id = 0)
        {
            Photo? foundPhoto = _db.Photo.FirstOrDefault(x => x.Id == id);
            if (foundPhoto == null)
            {
                return NotFound();
            }
            return View(foundPhoto);
        }

        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Photo model)
        {
            if (_db.Photo.Select(x => x.Title).Any(x => x.Equals(model.Title)))
            {
                ModelState.AddModelError("Title", "Это название уже использовано");
                return View(model);
            }
            if (!model.ImageFile.FileName.EndsWith(".png") && !model.ImageFile.FileName.EndsWith(".jpg") && !model.ImageFile.FileName.EndsWith(".jpeg") && !model.ImageFile.FileName.EndsWith(".jfif"))
            {
                ModelState.AddModelError("ImageFile", "Неверный формат. Загрузите изображение в одом из этих форматов: *.png, *.jpg, *.jpeg, *.jfif");
                return View(model);
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string wwwRootImagePath = $"{_environment.WebRootPath}\\gallery\\";
            string fileExtention = Path.GetExtension(model.ImageFile.FileName);
            model.ImageName = $"{model.Title}{fileExtention}";
            try
            {
                using (var imageCreateStream = new FileStream(Path.Combine(wwwRootImagePath, model.ImageName), FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(imageCreateStream);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Title", $"Некорректное название: {ex}");
                return View(model);
            }
            await _db.Photo.AddAsync(model);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Delete(int id = 0)
        {
            Photo? foundModel = _db.Photo.FirstOrDefault(x => x.Id == id);
            if (foundModel == null)
            {
                return NotFound();
            }
            return View(id);
        }

        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id = 0)
        {
            Photo? foundPhoto = _db.Photo.FirstOrDefault(x => x.Id == id);
            if (foundPhoto == null)
            {
                return NotFound();
            }
            string wwwRootImagePath = $"{_environment.WebRootPath}\\gallery\\";
            string oldPath = Path.Combine(wwwRootImagePath, foundPhoto.ImageName);
            try
            {
                System.IO.File.Delete(oldPath);
            }
            catch { }
            _db.Photo.Remove(foundPhoto);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Edit(int id = 0)
        {
            Photo? foundModel = _db.Photo.FirstOrDefault(x => x.Id == id);
            if (foundModel == null)
            {
                return NotFound();
            }
            return View(EditPhotoModel.FromPhotoModel(foundModel));
        }

        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public async Task<IActionResult> Edit(EditPhotoModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Photo? foundModel = _db.Photo.FirstOrDefault(x => x.Id == model.Id);
            if (foundModel == null)
            {
                return NotFound();
            }
            #region Если неверный формат фото
            if (model.ImageFile != null && !model.ImageFile.FileName.EndsWith(".png") && !model.ImageFile.FileName.EndsWith(".jpg") && !model.ImageFile.FileName.EndsWith(".jpeg") && !model.ImageFile.FileName.EndsWith(".jfif"))
            {
                ModelState.AddModelError("ImageFile", "Неверный формат. Загрузите изображение в одом из этих форматов: *.png, *.jpg, *.jpeg, *.jfif");
                return View(model);
            }
            #endregion
            #region Если такое название уже существует
            if (!foundModel.Title.Equals(model.Title) && _db.Photo.Select(x => x.Title).Any(x => x.Equals(model.Title)))
            {
                ModelState.AddModelError("Title", "Это название уже использовано");
                return View(model);
            }
            #endregion
            string wwwRootImagePath = $"{_environment.WebRootPath}\\gallery\\";
            string oldPath = Path.Combine(wwwRootImagePath, foundModel.ImageName);
            #region Если название не меняли
            if (foundModel.Title.Equals(model.Title))
            {
                if (model.ImageFile != null)
                {
                    try
                    {
                        System.IO.File.Delete(oldPath);
                        using (var imageCreateStream = new FileStream(oldPath, FileMode.Create))
                        {
                            await model.ImageFile.CopyToAsync(imageCreateStream);
                        }
                    }
                    catch
                    { }
                }
                foundModel.Description = model.Description;
                _db.Photo.Update(foundModel);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            #endregion
            //Title not equals
            string newFileName = $"{model.Title}{Path.GetExtension(foundModel.ImageName)}";
            foundModel.ImageName = newFileName;
            string newPath = Path.Combine(wwwRootImagePath, newFileName);
            #region Если название поменяли и загрузили фото
            if (model.ImageFile != null)
            {
                try
                {
                    System.IO.File.Delete(oldPath);
                    using (var imageCreateStream = new FileStream(newPath, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(imageCreateStream);
                    }
                }
                catch
                {
                    ModelState.AddModelError("Title", "Некорректное название");
                    return View(model);
                }
                foundModel.Title = model.Title;
                foundModel.Description = model.Description;
                _db.Photo.Update(foundModel);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            #endregion
            #region Если название поменяли, но фото не загрузили
            try
            {
                System.IO.File.Copy(oldPath, newPath);
                System.IO.File.Delete(oldPath);
            }
            catch
            {
                ModelState.AddModelError("Title", "Некорректное название");
                return View(model);
            }

            foundModel.Title = model.Title;
            foundModel.Description = model.Description;
            _db.Photo.Update(foundModel);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
            #endregion
        }
    }
}
