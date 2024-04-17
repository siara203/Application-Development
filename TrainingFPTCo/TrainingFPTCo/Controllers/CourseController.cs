using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainingFPTCo.Helpers;
using TrainingFPTCo.Models;
using TrainingFPTCo.Models.Queries;

namespace TrainingFPTCo.Controllers
{
	public class CoursesController : Controller
	{
		[HttpGet]
		public IActionResult Index(string searchString, string filterStatus)
		{
			CourseViewModel course = new CourseViewModel();
			course.CourseDetailsList = new List<CourseDetail>();

			// Gọi phương thức GetAllDataCourses từ class CourseQuery và truyền tham số tìm kiếm và bộ lọc trạng thái
			var dataCourses = new CourseQuery().GetAllDataCourses(searchString, filterStatus);

			// Sao chép dữ liệu từ danh sách dataCourses vào danh sách của CourseViewModel
			foreach (var data in dataCourses)
			{
				course.CourseDetailsList.Add(new CourseDetail
				{
					Id = data.Id,
					Name = data.Name,
					CategoryId = data.CategoryId,
					Description = data.Description,
					Status = data.Status,
					ViewStartDate = data.ViewStartDate,
					ViewEndDate = data.ViewEndDate,
					StartDate = data.StartDate,
					EndDate = data.EndDate,
					ViewImageCouser = data.ViewImageCouser,
					ViewCategoryName = data.ViewCategoryName
				});
			}

			// Truyền giá trị của tham số tìm kiếm vào ViewData để hiển thị trên giao diện
			ViewData["currentFilter"] = searchString;

			// Truyền giá trị của bộ lọc trạng thái vào ViewBag để sử dụng trong giao diện
			ViewBag.FilterStatus = filterStatus;

			// Trả về view Index với dữ liệu course
			return View(course);
		}

		[HttpGet]
        public IActionResult Add()
        {
            CourseDetail course = new CourseDetail();

            List<SelectListItem> itemCategories = new List<SelectListItem>();
            var dataCategory = new CategoryQuery().GetAllCategories(null, null);
            foreach (var item in dataCategory)
            {
                itemCategories.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            }
            ViewBag.Categories = itemCategories;
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CourseDetail course, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                // xu ly insert course vao database
                try
                {
                    string nameImageCourse = UploadFileHelper.UploadFile(Image, "images");
                    int idCourse = new CourseQuery().InsertCourse(
                        course.Name,
                        course.CategoryId,
                        course.Description,
                        course.StartDate,
                        course.EndDate,
                        course.Status,
                        nameImageCourse
                    );
                    if (idCourse > 0)
                    {
                        TempData["saveStatus"] = true;
                    }
                    else
                    {
                        TempData["saveStatus"] = false;
                    }
                    // quay lai trang danh sach courses
                    return RedirectToAction(nameof(CoursesController.Index), "Courses");
                }
                catch (Exception ex)
                {
                    return Ok(ex.Message);
                }
            }

            List<SelectListItem> itemCategories = new List<SelectListItem>();
            var dataCategory = new CategoryQuery().GetAllCategories(null, null);
            foreach (var item in dataCategory)
            {
                itemCategories.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            }
            ViewBag.Categories = itemCategories;
            return View(course);
        }

        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            // xu ly xoa danh muc khoa hoc
            bool DeleteCourse = new CourseQuery().DeleteItemCourseById(id);
            if (DeleteCourse)
            {
                TempData["DeleteCourse"] = true;
            }
            else
            {
                TempData["DeleteCourse"] = false;
            }
            return RedirectToAction(nameof(CoursesController.Index), "Courses");
        }
        [HttpGet]
		public IActionResult Update(int id)
		{
			CourseDetail detail = new CourseQuery().GetDetailCourseById(id);
			List<SelectListItem> itemCategories = new List<SelectListItem>();
			var dataCategory = new CategoryQuery().GetAllCategories(null, null);
			foreach (var item in dataCategory)
			{
				itemCategories.Add(new SelectListItem
				{
					Value = item.Id.ToString(),
					Text = item.Name
				});
			}
			ViewBag.Categories = itemCategories;
			return View(detail);
		}

		[HttpPost]
		public IActionResult Update(CourseDetail courseDetail, IFormFile Image)
		{
			try
			{
				var infoCourse = new CourseQuery().GetDetailCourseById(courseDetail.Id);
				string imageCourse = infoCourse.ViewImageCouser;
				// check xem nguoi co thay anh hay ko?
				if (courseDetail.Image != null)
				{
					// co muon thay anh
					imageCourse = UploadFileHelper.UploadFile(Image, "images");
				}
				bool update = new CourseQuery().UpdateCourseById(
						courseDetail.CategoryId,
						courseDetail.Name,
						courseDetail.Description,
						imageCourse,
						courseDetail.StartDate,
						courseDetail.EndDate,
						courseDetail.Status,
						courseDetail.Id
					);
				if (update)
				{
					TempData["updateStatus"] = true;
				}
				else
				{
					TempData["updateStatus"] = false;
				}
				return RedirectToAction("Index", "Courses");
			}
			catch (Exception ex)
			{
				return Ok(ex.Message);
			}
			List<SelectListItem> itemCategories = new List<SelectListItem>();
			var dataCategory = new CategoryQuery().GetAllCategories(null, null);
			foreach (var item in dataCategory)
			{
				itemCategories.Add(new SelectListItem
				{
					Value = item.Id.ToString(),
					Text = item.Name
				});
			}
			ViewBag.Categories = itemCategories;
			return View(courseDetail);
		}
	}
}