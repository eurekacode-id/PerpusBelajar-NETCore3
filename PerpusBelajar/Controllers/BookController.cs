using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PerpusBelajar.Interfaces;
using PerpusBelajar.Models;
using PerpusBelajar.ViewModels;
using System;
using System.IO;

namespace PerpusBelajar.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookRepository _bookRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public BookController(ILogger<BookController> logger, 
            IBookRepository bookRepository,
            IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _bookRepository = bookRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = _bookRepository.GetAllBooks();
            _logger.LogInformation($"Getting All Books List");
            return View(model);
        }

        public IActionResult Details(int? id)
        {
            Book book = _bookRepository.GetBook(id.Value);

            if(book == null)
            {
                _logger.LogError($"Can not found book with id: {id.Value}");
                Response.StatusCode = 404;
                return View("BookNotFound", id.Value);
            }

            BookDetailsViewModel bookDetailsVM = new BookDetailsViewModel()
            {
                Book = book,
                PageTitle = "Book Detail Page"
            };
            _logger.LogInformation($"Show book(id: {book.Id})");
            return View(bookDetailsVM);
        }
        
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Book book = _bookRepository.GetBook(id);
            BookEditViewModel bookEditVM = new BookEditViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                Category = book.Category,
                Quantity = book.Quantity,
                ExistingImagePath = book.ImageFileName,
                PageTitle = "Book Edit Page"
            };
            return View(bookEditVM);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(BookEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Book book = _bookRepository.GetBook(model.Id);
                book.Title = model.Title;
                book.Author = model.Author;
                book.ISBN = model.ISBN;
                book.Category = model.Category;
                book.Quantity = model.Quantity;

                if(model.Image != null)
                {
                    if (!string.IsNullOrWhiteSpace(model.ExistingImagePath))
                    {
                        string existingPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/books", model.ExistingImagePath);
                        System.IO.File.Delete(existingPath);
                    }
                    book.ImageFileName = ProcessUploadFile(model);
                }
                

                _bookRepository.Update(book);
                _logger.LogInformation($"Updated book(id: {book.Id})");

                return RedirectToAction("Index");
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(BookCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadFile(model);

                Book book = new Book
                {
                    Title = model.Title,
                    ISBN = model.ISBN,
                    Author = model.Author,
                    Quantity = model.Quantity,
                    Category = model.Category,
                    ImageFileName = uniqueFileName
                };
                    
                Book newBook = _bookRepository.Create(book);
                _logger.LogInformation($"Created new Book(id: {newBook.Id})");

                return RedirectToAction("Details", new { id = newBook.Id });
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            BookDetailsViewModel bookDetailsVM = new BookDetailsViewModel()
            {
                Book = _bookRepository.GetBook(id ?? 1),
                PageTitle = "Are you sure want to delete this book?"
            };
            return View(bookDetailsVM);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Book book = _bookRepository.GetBook(id);

            if (!string.IsNullOrWhiteSpace(book.ImageFileName))
            {
                string existingPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/books", book.ImageFileName);
                System.IO.File.Delete(existingPath);
            }
            _bookRepository.Delete(id);
            _logger.LogInformation($"Deleted Book(id: {book.Id})");

            return RedirectToAction("Index");
        }

        private string ProcessUploadFile(BookCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Image != null)
            {
                string imageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/books");
                uniqueFileName = Guid.NewGuid().ToString() + '_' + model.Image.FileName;
                string filePath = Path.Combine(imageFolder, uniqueFileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
