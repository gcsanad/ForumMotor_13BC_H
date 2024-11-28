using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ForumMotor_13BC_H.Data;
using ForumMotor_13BC_H.Models;
using Microsoft.AspNetCore.Identity;

namespace ForumMotor_13BC_H.Pages
{
    public class CreatePostModel : PageModel
    {
        private readonly ForumMotor_13BC_H.Data.ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public CreatePostModel(ForumMotor_13BC_H.Data.ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
        ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "Id");
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public int TopicId { get; set; }

        [BindProperty]
        public Post Post { get; set; } = default!;



        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Post.UserId = _userManager.GetUserId(User);
            Post.CreateDate = DateTime.Now;
            Post.TopicId = TopicId;
            _context.Posts.Add(Post);
            await _context.SaveChangesAsync();

            return RedirectToPage($"./PostList", new { CategoryId = _context.Topics.Find(TopicId)});
        }  
    }
}
