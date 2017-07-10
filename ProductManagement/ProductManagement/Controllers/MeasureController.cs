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
    public class MeasureController : Controller
    {
        [HttpGet]
        public ActionResult Index(Measure measureModel, int page = 1)
        {
            if (measureModel == null)
            {
                measureModel = new Measure();
                measureModel.lstMeasure = new StaticPagedList<Measure>(new List<Measure>(), 1, 20, 0);
            }
            else
            {
                if (measureModel.Code == null && measureModel.Name == null)
                {
                    if ((Session["Code"] != null || Session["Name"] != null) && page > 1)
                    {
                        measureModel.Code = Session["Code"].ToString();
                        measureModel.Name = Session["Name"].ToString();
                    }
                    else
                    {
                        measureModel = new Measure();
                    }
                }
                else
                {
                    Session["Code"] = measureModel.Code;
                    Session["Name"] = measureModel.Name;
                }
            }


            measureModel.page = page;
            measureModel.page_count = new MeasureBL().CountData(measureModel);
            TempData["SearchCount"] = measureModel.page_count + " row(s) has found.";
            DataTable dtResult = new DataTable();
            new MeasureBL().SearchData(measureModel, out dtResult);

            measureModel.lstMeasure = new StaticPagedList<Measure>(CommonMethod.DataTableToDto<Measure>(dtResult), measureModel.page, 20, measureModel.page_count);


            return View(measureModel);
        }


        [HttpGet]
        public ActionResult Add()
        {
            DataTable dtResult = new DataTable();
            new MeasureBL().SearchData(new Measure(), out dtResult);
            ViewBag.ParentID = new SelectList(CommonMethod.DataTableToDto<Measure>(dtResult).ToList(), "id", "name");
            return View();
        }

        [HttpPost]
        public ActionResult Add(Measure measureModel)
        {
            if (ModelState.IsValid)
            {
                //Kiem tra duplicate code
                DataTable dtResult = new DataTable();
                new MeasureBL().SelectMeasureByCode(measureModel, out dtResult);
                if (dtResult.Rows.Count > 0)
                {
                    TempData["Duplicate"] = "Duplicate Code";

                }
                else
                {
                    new MeasureBL().InsertData(measureModel);

                    TempData["Successful"] = "Added Measure";
                    return RedirectToAction("Add");

                }

            }

            DataTable dtParentID = new DataTable();
            new MeasureBL().SearchData(new Measure(), out dtParentID);
            ViewBag.ParentID = new SelectList(CommonMethod.DataTableToDto<Measure>(dtParentID).ToList(), "id", "name");

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Measure measureModel = new Measure();
            
            if (id == null)
            {
                TempData["Danger"] = "Data not found";
                return RedirectToAction("Index");
            }
            else
            {

                measureModel.ID = id;
                DataTable dtResult = new DataTable();
                new MeasureBL().SelectMeasureByID(measureModel, out dtResult);
                if (dtResult.Rows.Count > 0)
                {
                    foreach (DataRow row in dtResult.Rows)
                    {
                        measureModel.Code = row["code"].ToString();
                        measureModel.Name = row["name"].ToString();
                        measureModel.Description = row["description"].ToString();
                    }

                    return View(measureModel);
                }
                else
                {
                    TempData["Danger"] = "Data not found";
                    return RedirectToAction("Index");

                }
            }

            return View(measureModel);
        }

        [HttpPost]
        public ActionResult Edit(Measure measureModel)
        {
            if (ModelState.IsValid)
            {
                new MeasureBL().UpdateData(measureModel);

                TempData["Success"] = "Edited successfuly";
                return RedirectToAction("Index");
            }

            return View(measureModel);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Measure measureModel = new Measure();
            measureModel.ID = id;
            if (new MeasureBL().MeasureIDIsExisted(measureModel) == 1)
            {
                new MeasureBL().DeleteData(measureModel);
                TempData["Success"] = "Deleted successfuly";
                return RedirectToAction("Index");
            }

            TempData["Success"] = "Deleted successfuly";
            return RedirectToAction("Index");
        }

    }
}