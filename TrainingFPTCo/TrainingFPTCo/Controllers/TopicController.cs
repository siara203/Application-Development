using Microsoft.AspNetCore.Mvc;
using TrainingFPTCo.DataDBContext;
using TrainingFPTCo.Models;
using TrainingFPTCo.Models.Queries;
using TrainingFPTCo.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrainingFPTCo.Controllers
{
    public class TopicController : Controller
    {
        [HttpGet]
        public IActionResult Index(string SearchString, string FilterStatus)
        {

                TopicViewModel topicModel = new TopicViewModel();
                topicModel.TopicDetailList = new List<TopicDetail>();
                var dataTopics = new TopicQuery().GetAllTopics(SearchString,FilterStatus);

                foreach (var item in dataTopics)
                {
                    topicModel.TopicDetailList.Add(new TopicDetail
                    {
                        Id = item.Id,
                        CourseId = item.CourseId,
                        Name = item.Name,
                        Description = item.Description,
                        Status = item.Status,
                        NameDocuments = item.NameDocuments,
                        NameAttachFile = item.NameAttachFile,
                        NamePosterTopic = item.NamePosterTopic,
                        TypeDocument = item.TypeDocument,
                        CreatedAt = item.CreatedAt,
                        UpdatedAt = item.UpdatedAt,
                        NameCourse=item.NameCourse,
                    });
                }

                ViewData["currentFilter"] = SearchString;
                ViewBag.FilterStatus = FilterStatus;

                return View(topicModel);
            }
           
        

        [HttpGet]
        public IActionResult Add(string SearchString, string FilterStatus)
        {
            
           
                List<SelectListItem> itemCourses = new List<SelectListItem>();
                var dataCourse = new CourseQuery().GetAllDataCourses(SearchString, FilterStatus);
                foreach (var item in dataCourse)
                {
                    itemCourses.Add(new SelectListItem
                    {
                        Value = item.Id.ToString(),
                        Text = item.Name
                    });
                }
                ViewBag.Courses = itemCourses;

                // Initialize topic detail object
                TopicDetail topic = new TopicDetail();
                return View(topic);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(TopicDetail topic, IFormFile AttachFile, IFormFile Documents, IFormFile PosterTopic)
        {
          
            if (ModelState.IsValid)
            {

                try
                {
                    // Upload file
                    string documents = UploadFileHelper.UploadFile(Documents, "documents");
                    string video = UploadFileHelper.UploadFile(AttachFile, "videos");
                    string image = UploadFileHelper.UploadFile(PosterTopic, "images");

                    int idTopic = new TopicQuery().InsertItemTopic(
                        topic.CourseId,
                        topic.Name,
                        topic.Description,
                        topic.Status,
                        documents,
                        video,
                        image,
                        topic.TypeDocument

                    );
                    if (idTopic > 0)
                    {
                        TempData["saveStatus"] = true;
                        return RedirectToAction(nameof(TopicController.Index), "Topic");
                    }
                    else
                    {
                        TempData["saveStatus"] = false;
                        ModelState.AddModelError("", "Failed to save topic.");
                    }
                }
                catch (Exception ex)
                {
                    TempData["saveStatus"] = false;
                    ModelState.AddModelError("", "An error occurred while processing your request.");
                }
                //return Ok(topic);
                return RedirectToAction(nameof(TopicController.Index), "Topic");
            }
            List<SelectListItem> itemCourses = new List<SelectListItem>();
            var dataCourse = new CourseQuery().GetAllDataCourses("",  "");
            foreach (var item in dataCourse)
            {
                itemCourses.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            }
            ViewBag.Courses = itemCourses;

            return View(topic);
        }




        [HttpGet]
        public IActionResult Update(int id, string SearchString, string FilterStatus)
        {
            
            TopicDetail detail = new TopicQuery().GetDetailTopicById(id);
            List<SelectListItem> itemCourses = new List<SelectListItem>();
            var dataCourse = new CourseQuery().GetAllDataCourses(SearchString, FilterStatus);
            foreach (var item in dataCourse)
            {
                itemCourses.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            }
            ViewBag.Courses = itemCourses;

            // Initialize topic detail object
            return View(detail);
        }

        [HttpPost]
        public IActionResult Update(TopicDetail topicDetail, IFormFile AttachFile, IFormFile Documents, IFormFile PosterTopic, string SearchString, string FilterStatus)
        {
            try
            {
                var infoTopic = new TopicQuery().GetDetailTopicById(topicDetail.Id);
                string documentsTopic = infoTopic.NameDocuments;
                string fileTopic = infoTopic.NameAttachFile;
                string imageTopic = infoTopic.NamePosterTopic;
                // check xem nguoi co thay anh hay ko?
                if (topicDetail.Documents != null)
                {
                    // co muon thay anh
                    documentsTopic = UploadFileHelper.UploadFile(Documents, "documents");
                }
                if (topicDetail.AttachFile != null)
                {
                    fileTopic = UploadFileHelper.UploadFile(AttachFile, "videos");
                }
                if (topicDetail.PosterTopic != null)
                {
                    imageTopic = UploadFileHelper.UploadFile(PosterTopic, "images");
                }
                bool update = new TopicQuery().UpdateTopicById(
                        topicDetail.CourseId,
                        topicDetail.Name,
                        topicDetail.Description,
                        documentsTopic,
                        fileTopic,
                        imageTopic,
                        topicDetail.TypeDocument,
                        topicDetail.Status,
                        topicDetail.Id

                    );
                if (update)
                {
                    TempData["updateStatus"] = true;
                }
                else
                {
                    TempData["updateStatus"] = false;
                }
                return RedirectToAction("Index", "Topic");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            List<SelectListItem> itemCourses = new List<SelectListItem>();
            var dataCourse = new CourseQuery().GetAllDataCourses(SearchString, FilterStatus);
            foreach (var item in dataCourse)
            {
                itemCourses.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            }
            ViewBag.Courses = itemCourses;

            // Initialize topic detail object
            TopicDetail topic = new TopicDetail();
            return View(topic);
        }


        [HttpGet]

        public IActionResult Delete(int id = 0)
        {
            TopicDetail topic = new TopicDetail();
            bool deleteTopic = new TopicQuery().DeleteItemTopicById(id);
            if (deleteTopic)
            {
                TempData["statusDelete"] = true;
            }
            else
            {
                TempData["statusDelete"] = false;

            }

            return RedirectToAction(nameof(TopicController.Index), "Topic");
        }

    }
}