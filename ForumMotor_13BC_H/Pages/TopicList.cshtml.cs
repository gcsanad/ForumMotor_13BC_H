using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ForumMotor_13BC_H.Data;
using ForumMotor_13BC_H.Models;

namespace ForumMotor_13BC_H.Pages
{
    public class TopicListModel : PageModel
    {
        private readonly ForumMotor_13BC_H.Data.ApplicationDbContext _context;

        public TopicListModel(ForumMotor_13BC_H.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int CategoryId { get; set; }




        public IList<Topic> Topic { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Topic = await _context.Topics
                .Where(x=>x.CategoryId == CategoryId)
                .Include(t => t.Category)
                .Include(t => t.User).ToListAsync();
        }
    }
}
