using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;
using TrainingFPTCo.Models;
using TrainingFPTCo.Models.Queries;

namespace TrainingFPTCo.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
		public IActionResult Index()
		{
			AccountViewModel account= new AccountViewModel();
			account.AccountDetailsList = new List<AccountDetail>();
			var dataAccounts = new AccountQuery().GetAllDataAccounts();
			foreach (var data in dataAccounts)
			{
				account.AccountDetailsList.Add(new AccountDetail
				{
					Id = data.Id,
					Username = data.Username,
					RoleId = data.RoleId,
					Password = data.Password,
					ExtraCode = data.ExtraCode,
					Email = data.Email,
					Phone = data.Phone,
					Address = data.Address,
					FullName = data.FullName,
					FirstName = data.FirstName,
					LastName = data.LastName,
					Birthday = data.Birthday,
					Gender = data.Gender,
					Education = data.Education,
					ProgrammingLanguage = data.ProgrammingLanguage,
					ToeicScore = data.ToeicScore,
					Skill = data.Skill,
					IpClient = data.IpClient,
					LastLogin = data.LastLogin,
					LastLogout = data.LastLogout, 
                    ViewRoleName = data.ViewRoleName

				});
			}
			return View(account);
		}



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AccountDetail account)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int idAccount = new AccountQuery().InsertAccount(
                        account.Username,
                        account.RoleId,
                        account.Password,
                        account.ExtraCode,
                        account.Email,
                        account.Phone,
                        account.Address,
                        account.FullName,
                        account.FirstName,
                        account.LastName,
                        account.Birthday,
                        account.Gender,
                        account.Education,
                        account.ProgrammingLanguage,
                        account.ToeicScore,
                        account.Skill,
                        account.IpClient,
                        account.LastLogin,
                        account.LastLogout
                    );
                    if (idAccount > 0)
                    {
                        TempData["StatusAdd"] = true;
                    }
                    else
                    {
                        TempData["StatusAdd"] = false;
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return Ok(ex.Message);
                }
            }

            return View(account);
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var account = new AccountQuery().GetDetailAccountById(id);

            var roles = new RoleQuery().GetAllRoles();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");

            return View(account);
        }
        [HttpGet]
        public IActionResult Show(int id)
        {
            var account = new AccountQuery().GetDetailAccountById(id);

            var roles = new RoleQuery().GetAllRoles();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");

            return View(account);
        }
        [HttpPost]
        public IActionResult Update(AccountDetail account)
        {
            try
            {
                bool update = new AccountQuery().UpdateAccountById(
                    account.RoleId,
                    account.Username,
                    account.Password,
                    account.ExtraCode,
                    account.Email,
                    account.Phone,
                    account.Address,
                    account.FullName,
                    account.FirstName,
                    account.LastName,
                    account.Birthday,
                    account.Gender,
                    account.Education,
                    account.ProgrammingLanguage,
                    account.ToeicScore,
                    account.Skill,
                    account.IpClient,
                    account.LastLogin,
                    account.LastLogout,
                    account.Id
                );
                if (update)
                {
                    TempData["StatusUpdate"] = true;
                }
                else
                {
                    TempData["StatusUpdate"] = false;
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            bool delete = new AccountQuery().DeleteAccountById(id);
            if (delete)
            {
                TempData["StatusDelete"] = true;
            }
            else
            {
                TempData["StatusDelete"] = false;
            }
            return RedirectToAction(nameof(Index));
        }
    }

}
