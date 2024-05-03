// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using CMS.Data;
using CMS.Models;
using CMS.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CMS.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
            _roleManager = roleManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            // Properties related to Student fields
            [Required(ErrorMessage = "The student roll number field is required.")]
            public string StudentRollNo { get; set; }

            [Required(ErrorMessage = "The student first name field is required.")]
            public string StudentFirstName { get; set; }

            [Required(ErrorMessage = "The student last name field is required.")]
            public string StudentLastName { get; set; }

            [Required(ErrorMessage = "The student age field is required.")]
            [Range(0, 150, ErrorMessage = "The student age must be between 0 and 150.")]
            public int StudentAge { get; set; }

            [Required(ErrorMessage = "The student gender field is required.")]
            public string StudentGender { get; set; }

            [Required(ErrorMessage = "The student city field is required.")]
            public string StudentCity { get; set; }

            [Required(ErrorMessage = "The student country field is required.")]
            public string StudentCountry { get; set; }

            [Required(ErrorMessage = "The student phone number field is required.")]
            [Phone(ErrorMessage = "The student phone number is not valid.")]
            public string StudentPhoneNo { get; set; }

            [Required(ErrorMessage = "The student address field is required.")]
            public string StudentAddress { get; set; }

            // Properties related to Teacher fields
            [Required(ErrorMessage = "The teacher code field is required.")]
            public string TeacherCode { get; set; }

            [Required(ErrorMessage = "The teacher first name field is required.")]
            public string TeacherFirstName { get; set; }

            [Required(ErrorMessage = "The teacher last name field is required.")]
            public string TeacherLastName { get; set; }

            [Required(ErrorMessage = "The teacher age field is required.")]
            [Range(0, 150, ErrorMessage = "The teacher age must be between 0 and 150.")]
            public int TeacherAge { get; set; }

            [Required(ErrorMessage = "The teacher gender field is required.")]
            public string TeacherGender { get; set; }

            [Required(ErrorMessage = "The teacher phone number field is required.")]
            [Phone(ErrorMessage = "The teacher phone number is not valid.")]
            public string TeacherPhoneNo { get; set; }


            [Required(ErrorMessage = "The role field is required.")]
            public string Role { get; set; }

            [Required(ErrorMessage = "Please select a department.")]
            public int DepartmentId { get; set; }

            [Required(ErrorMessage = "Please select a batch.")]
            public int BatchId { get; set; }

            [Required(ErrorMessage = "Please select a campus.")]
            public int CampusId { get; set; }

            [Required(ErrorMessage = "Please select a section.")]
            public int SectionId { get; set; }
        }


        public async Task OnGetAsync(string role, string returnUrl = null)
        {
            if (!_roleManager.RoleExistsAsync(SD.Role_Student).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Student)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Teacher)).GetAwaiter().GetResult();
            }
            ViewData["Departments"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentName");
            ViewData["Batches"] = new SelectList(_context.Batches, "BatchID", "BatchName");
            ViewData["Campuses"] = new SelectList(_context.Campuses, "CampusID", "CampusName");
            ViewData["Sections"] = new SelectList(_context.Sections, "SectionID", "SectionName");
            ViewData["role"] = role;
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            string role = Input.Role;
            Console.WriteLine(role);
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (role == "student")
            {
                ModelState.Remove("Input.TeacherCode");
                ModelState.Remove("Input.TeacherFirstName");
                ModelState.Remove("Input.TeacherLastName");
                ModelState.Remove("Input.TeacherGender");
                ModelState.Remove("Input.TeacherPhoneNo");
            }
            else if (role == "teacher")
            {
                ModelState.Remove("Input.StudentRollNo");
                ModelState.Remove("Input.StudentFirstName");
                ModelState.Remove("Input.StudentLastName");
                ModelState.Remove("Input.StudentGender");
                ModelState.Remove("Input.StudentPhoneNo");
                ModelState.Remove("Input.StudentCity");
                ModelState.Remove("Input.StudentCountry");
                ModelState.Remove("Input.StudentPhoneNo");
                ModelState.Remove("Input.StudentAddress");
                ModelState.Remove("Input.BatchId");
                ModelState.Remove("Input.SectionId");
                ModelState.Remove("Input.CampusId");
            }
            else
            {
                ModelState.Remove("Input.TeacherCode");
                ModelState.Remove("Input.TeacherFirstName");
                ModelState.Remove("Input.TeacherLastName");
                ModelState.Remove("Input.TeacherGender");
                ModelState.Remove("Input.TeacherPhoneNo");
                ModelState.Remove("Input.StudentRollNo");
                ModelState.Remove("Input.StudentFirstName");
                ModelState.Remove("Input.StudentLastName");
                ModelState.Remove("Input.StudentGender");
                ModelState.Remove("Input.StudentPhoneNo");
                ModelState.Remove("Input.StudentCity");
                ModelState.Remove("Input.StudentCountry");
                ModelState.Remove("Input.StudentPhoneNo");
                ModelState.Remove("Input.StudentAddress");
                ModelState.Remove("Input.BatchId");
                ModelState.Remove("Input.SectionId");
                ModelState.Remove("Input.CampusId");
                ModelState.Remove("Input.Role");

            }
            if (ModelState.IsValid)
            {
                
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);
                var userId = await _userManager.GetUserIdAsync(user);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    // Populate Teacher or Student object based on selected role
                    if (role == "student")
                    {
                        await _userManager.AddToRoleAsync(user, SD.Role_Student);
                        var student = new Student
                        {
                            RollNo = Input.StudentRollNo,
                            FirstName = Input.StudentFirstName,
                            LastName = Input.StudentLastName,
                            Age = Input.StudentAge,
                            Gender = Input.StudentGender,
                            City = Input.StudentCity,
                            Country = Input.StudentCountry,
                            PhoneNo = Input.StudentPhoneNo,
                            Address = Input.StudentAddress,
                            DepartmentID = Input.DepartmentId,
                            BatchID = Input.BatchId,
                            CampusID = Input.CampusId,
                            SectionID = Input.SectionId,
                            UserId = userId
                        };

                        _context.Students.Add(student);

                    }
                    else if (role == "teacher")
                    {
                        await _userManager.AddToRoleAsync(user, SD.Role_Teacher);
                        var teacher = new Teacher
                        {
                            TeacherCode = Input.TeacherCode,
                            FirstName = Input.TeacherFirstName,
                            LastName = Input.TeacherLastName,
                            Age = Input.TeacherAge,
                            Gender = Input.TeacherGender,
                            PhoneNo = Input.TeacherPhoneNo,
                            Email = Input.Email,
                            DepartmentID = Input.DepartmentId,
                            UserId = userId
                        };

                        _context.Teachers.Add(teacher);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, SD.Role_Admin);
                    }
                    await _context.SaveChangesAsync();

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    //{
                    //    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    //}
                    //else
                    //{
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    //return LocalRedirect(returnUrl);
                    //}
                    string indexPage;
                    if (role == "student")
                    {
                        indexPage = "/Students/Index";
                    }
                    else if (role == "teacher")
                    {
                        indexPage = "/Teachers/Index";
                    }
                    else
                    {
                        // Default to home page if role is unknown
                        indexPage = "/";
                    }

                    // Redirect the user to the appropriate index page
                    return Redirect(indexPage);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            //here i need to pass the role to the rendered page 
            //return Page();
            var returnUrlWithRole = Url.Page("/Account/Register", pageHandler: null, values: new { role = role });
            return Redirect(returnUrlWithRole);
        }

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
