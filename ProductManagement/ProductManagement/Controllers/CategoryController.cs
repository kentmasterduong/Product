using Core.Common;
using PagedList;
using ProductManagement.Controllers.BL;
using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Controllers
{
    public class CategoryController : Controller
    {


        [HttpGet]
        public ActionResult Index(Category categoryModel, int page = 1)
        {
            if (categoryModel == null)
            {
                categoryModel = new Category();
                categoryModel.lstCategory = new StaticPagedList<Category>(new List<Category>(), 1, 20, 0);
            }
            else
            {
                if (categoryModel.Code == null && categoryModel.Name == null)
                {
                    if ((Session["Code"] != null || Session["Name"] != null) && page > 1)
                    {
                        categoryModel.Code = Session["Code"].ToString();
                        categoryModel.Name = Session["Name"].ToString();
                    }
                    else
                    {
                        categoryModel = new Category();
                    }
                }
                else
                {
                    Session["Code"] = categoryModel.Code;
                    Session["Name"] = categoryModel.Name;
                }
            }

            
            categoryModel.page = page;
            categoryModel.page_count = new CategoryBL().CountData(categoryModel);
            TempData["SearchCount"] = categoryModel.page_count + " row(s) has found.";
            DataTable dtResult = new DataTable();
            new CategoryBL().SearchData(categoryModel, out dtResult);

            categoryModel.lstCategory = new StaticPagedList<Category>(CommonMethod.DataTableToDto<Category>(dtResult), categoryModel.page, 20, categoryModel.page_count);


            return View(categoryModel);
        }

   
        [HttpGet]
        public ActionResult Add()
        {
            DataTable dtResult = new DataTable();
            new CategoryBL().SearchData(new Category(), out dtResult);
            ViewBag.ParentID = new SelectList(CommonMethod.DataTableToDto<Category>(dtResult).ToList(),"id","name");          
            return View();
        }

        [HttpPost]
        public ActionResult Add(Category categoryModel)
        {
            if(ModelState.IsValid)
            {
                //Kiem tra duplicate code
                DataTable dtResult = new DataTable();
                new CategoryBL().SelectCategoryByCode(categoryModel, out dtResult);
                if (dtResult.Rows.Count > 0)
                {
                    TempData["Duplicate"] = "Duplicate Code";

                }
                else
                {
                    new CategoryBL().InsertData(categoryModel);

                    TempData["Successful"] = "Added category";
                    return RedirectToAction("Add");

                }

            }
                        
            DataTable dtParentID = new DataTable();
            new CategoryBL().SearchData(new Category(), out dtParentID);
            ViewBag.ParentID = new SelectList(CommonMethod.DataTableToDto<Category>(dtParentID).ToList(), "id", "name");

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Category categoryModel = new Category();
            DataTable dtParentID = new DataTable();
            new CategoryBL().SearchData(new Category(), out dtParentID);
            ViewBag.ParentID = new SelectList(CommonMethod.DataTableToDto<Category>(dtParentID).ToList(), "id", "name");

            if (id == null)
            {
                TempData["Danger"] = "Data not found";
                return RedirectToAction("Index");
            }
            else
            {
                
                categoryModel.ID = id;
                DataTable dtResult = new DataTable();
                new CategoryBL().SelectCategoryByID(categoryModel, out dtResult);
                if(dtResult.Rows.Count>0)
                {
                    foreach(DataRow row in dtResult.Rows)
                    {
                        categoryModel.Code = row["code"].ToString();
                        categoryModel.Name = row["name"].ToString();
                        categoryModel.Description = row["description"].ToString();
                    }

                    return View(categoryModel);
                }
                else
                {
                    TempData["Danger"] = "Data not found";
                    return RedirectToAction("Index");

                }
            }

            return View(categoryModel);
        }

        [HttpPost]
        public ActionResult Edit(Category categoryModel)
        {
            if(ModelState.IsValid)
            {
                new CategoryBL().UpdateData(categoryModel);

                TempData["Success"] = "Edited successfuly";
                return RedirectToAction("Index");
            }
            
            return View(categoryModel);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Category categoryModel = new Category();
            categoryModel.ID = id;
            if(new CategoryBL().CategoryIDIsExisted(categoryModel)==1)
            {
                new CategoryBL().DeleteData(categoryModel);
                TempData["Success"] = "Deleted successfuly";
                return RedirectToAction("Index");
            }

            TempData["Success"] = "Deleted successfuly";
            return RedirectToAction("Index");
        }

        

    }
}